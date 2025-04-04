using Supabase.Gotrue;
        using Client = Supabase.Client;
        using SupabaseAppIntro.Models;
        using static Supabase.Postgrest.Constants;

        namespace ConsoleSupabase.Data.Sourсe.Remote.SupabaseDB;
        
        public class SupabaseService
        {
            private const string SupabaseUrl = "https://eslbdblrkfgwinyvtzfm.supabase.co";
            private const string SupabaseKey = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpc3MiOiJzdXBhYmFzZSIsInJlZiI6ImVzbGJkYmxya2Znd2lueXZ0emZtIiwicm9sZSI6ImFub24iLCJpYXQiOjE3NDI5MjQ2ODMsImV4cCI6MjA1ODUwMDY4M30.I2LMOYui77dyQk9HPklVEVVIaO-veIU_oBJeetQ-YYM";
        
            private readonly Supabase.Client _client;
        
            public User? SupabaseUser { get; set; } = null;
        
            public bool IsLoggedIn { get; set; } = false;
        
            public SupabaseService()
            {
                try
                {
                    var options = new Supabase.SupabaseOptions
                    {
                        AutoConnectRealtime = true,
                    };
                    _client = new Client(SupabaseUrl, SupabaseKey, options);
                }
                catch (Exception ex)
                {
                    throw new Exception($"SupabaseService() raise Exception: {ex.Message}");
                }
            }
        
            public async Task InitServiceAsync()
            {
                try
                {
                    await _client.InitializeAsync()!;
                }
                catch (Exception ex)
                {
                    throw new Exception($"InitServiceAsync() raise Exception: {ex.Message}");
                }
            }
        
            public async Task<Session?> RegisterAsync(string email, string password)
            {
                try
                {
                    var session = await _client.Auth.SignUp(email, password);
                    return session;
                }
                catch (Exception ex)
                {
                    throw new Exception($"RegisterAsync(string email, string password) raise Exception: {ex.Message}");
                }
            }
        
            public async Task<Session?> LoginAsync(string email, string password)
            {
                try
                {
                    var session = await _client.Auth.SignIn(email, password);
                    return session;
                }
                catch (Exception ex)
                {
                    throw new Exception($"LoginAsync(string email, string password) raise Exception: {ex.Message}");
                }
            }
        
            public void SetAuthUser()
            {
                if (_client.Auth.CurrentUser != null)
                {
                    SupabaseUser = _client.Auth.CurrentUser;
                    IsLoggedIn = true;
                }
            }
            
            public async Task Logout()
            {
                try
                {
                    await _client.Auth.SignOut();
                    IsLoggedIn = false;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Logout() raise Exception: {ex.Message}");
                }
            }

            
            
            public async Task<List<Student>> GetStudentsByUserId()
            {
                try
                {
                    var students = await _client.From<Student>()
                        .Select("*").Filter("UserId", Operator.Equals, SupabaseUser?.Id).Get();
                    return students.Models;
                }
                catch (Exception ex)
                {
                    throw new Exception($"GetStudentsByUserId() raised an exception: {ex.Message}");
                }
            }
            
            public async Task<List<Mark>> GetMarksByStudentId(int studentId)
            {
                try
                {
                    var marks = await _client.From<Mark>()
                        .Select("*").Filter("StudentId", Operator.Equals, studentId).Get();
                    return marks.Models;
                }
                catch (Exception ex)
                {
                    throw new Exception($"GetMarksByStudentId(int studentId) raised an exception: {ex.Message}");
                }
            }
            
            public async Task<bool> AddStudent(string name)
            {
                try
                {
                    var userId = int.TryParse(SupabaseUser?.Id, out var id) ? id : 0;
            
                    // Check if the student already exists
                    var existingStudents = await _client.From<Student>()
                        .Select("*")
                        .Filter("Name", Operator.Equals, name)
                        .Filter("UserId", Operator.Equals, userId)
                        .Get();
            
                    if (existingStudents.Models.Count > 0)
                    {
                        // Student already exists
                        return false;
                    }
            
                    // Add the new student
                    var student = new Student
                    {
                        Name = name,
                        UserId = userId
                    };
                    var result = await _client.From<Student>().Insert(student);
                    return result.ResponseMessage.StatusCode == System.Net.HttpStatusCode.Created;
                }
                catch (Exception ex)
                {
                    throw new Exception($"AddStudent(string name) raise Exception: {ex.Message}");
                }
            }
            
            public async Task<bool> AddMark(int mark, int studentId)
            {
                try
                {
                    var newMark = new Mark
                    {
                        mark = mark,
                        StudentId = studentId
                    };
            
                    var result = await _client.From<Mark>().Insert(newMark);
            
                    if (result.ResponseMessage.StatusCode == System.Net.HttpStatusCode.Created)
                    {
                        return true;
                    }
                    else
                    {
                        throw new Exception($"Failed to add mark. Status code: {result.ResponseMessage.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"AddMark(int mark, int studentId) raised an exception: {ex.Message}");
                }
            }
        }