using System.ComponentModel.DataAnnotations;

namespace Backend.EF.Sqlite.ViewModels
{
    public class CreateCompanyViewModel
    {
        [Required]
        public string Name { get; set; }        
        [Required]
        public string Street { get; set; }        
        [Required]
        public int Number { get; set; }        
        public string? Complement { get; set; }
        [Required]
        public string District { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int ZipCode { get; set; }        
        public string? Phone { get; set; }
    }
}
