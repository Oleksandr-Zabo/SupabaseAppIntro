using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace SupabaseAppIntro.Models;

[Table("Mark")]
public class Mark : BaseModel
{
    [PrimaryKey("id")]
    public int Id { get; set; }
    [Column("Mark")]
    public int mark { get; set; }
    [Column("StudentId")]
    public int StudentId { get; set; }
    
    public Mark()
    {
        Id = 0;
        mark = 0;
        StudentId = 0;
    }
    
    public Mark(int id, int mark, int studentId)
    {
        Id = id;
        this.mark = mark;
        StudentId = studentId;
    }
    
    public override string ToString()
    {
        return $"Id: {Id}, Mark: {mark}, StudentId: {StudentId}";
    }
}