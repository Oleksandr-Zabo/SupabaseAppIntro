using Supabase.Gotrue;
        using Client = Supabase.Client;
        
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
        }