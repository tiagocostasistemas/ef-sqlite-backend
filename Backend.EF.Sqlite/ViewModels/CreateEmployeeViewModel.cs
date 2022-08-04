using Backend.EF.Sqlite.Enums;
using System.ComponentModel.DataAnnotations;

namespace Backend.EF.Sqlite.ViewModels
{
    public class CreateEmployeeViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public EPosition Position { get; set; }
        [Required]
        public double Salary { get; set; }        
        [Required]
        public int CompanyId { get; set; }

        public string? CompanyName { get; set; }
    }
}
