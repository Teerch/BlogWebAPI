namespace Web.Models;
public abstract class Time
{
    public DateTime CreatedOn { get; set; }
    public DateTime LastModified { get; set; }

    public Time()
    {
        CreatedOn = DateTime.UtcNow;
        LastModified = DateTime.UtcNow;
    }
}