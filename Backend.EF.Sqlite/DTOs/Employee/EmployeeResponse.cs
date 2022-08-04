namespace Backend.EF.Sqlite.DTOs.Employee
{
    public class EmployeeResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Position { get; set; }
        public double Salary { get; set; }
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
    }
}
