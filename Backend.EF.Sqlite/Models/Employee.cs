using Backend.EF.Sqlite.Enums;

namespace Backend.EF.Sqlite.Models
{
    public class Employee : Entity
    {
        public string? Name { get; set; }
        public EPosition Position { get; set; }
        public double Salary { get; set; }
        public int CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
