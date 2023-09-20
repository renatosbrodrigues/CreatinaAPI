using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CreatinaAPI.Models;

public class HourMinute
{
    public int Hour { get; set; }
    public int Minute { get; set; }

    public HourMinute(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
    }

    public override string ToString()
    {
        return $"{Hour:D2}:{Minute:D2}";
    }
}
public class Creatine
{
    [Key]
    public int CreatineId { get; set; }
    [Required]
    [StringLength(64)]
    public string Label { get; set; }
    [Required]
    public double Amount { get; set; }
    [Required]
    public double WarningAmount { get; set; }
    [Required]
    public HourMinute WarningTime { get; set; }
    [Required]
    public double DailyDose { get; set; }
    [ForeignKey("User")]
    public int UserId { get; set; }
    public User User { get; set; }
}

