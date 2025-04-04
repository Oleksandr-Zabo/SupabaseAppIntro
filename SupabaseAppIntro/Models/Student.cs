using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SupabaseAppIntro.Models;

[Table("Student")]
public class Student : BaseModel
{
    [PrimaryKey("id")]
    public int id { get; set; }
    [Column("Name")]
    public string Name { get; set; } = string.Empty;
    [Column("UserId")]
    public int UserId { get; set; }
    
    public List<Mark> Marks { get; set; }
    
    public Student()
    {
        id = 0;
        Name = string.Empty;
    }
    
    public Student(int id, string name)
    {
        this.id = id;
        Name = name;
    }
    
    public override string ToString()
    {
        return $"Id: {id}, Name: {Name}";
    }
}