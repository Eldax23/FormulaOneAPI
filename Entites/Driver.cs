namespace Entites;

public class Driver : BaseEntity
{


    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } =  string.Empty;
    public int DriverNumber { get; set; }
    public DateTime DateOfBirth { get; set; }

    public ICollection<Achievement> Achievements { get; set; } = new HashSet<Achievement>();
}