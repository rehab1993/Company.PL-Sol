using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.DAL.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Name Is Required")]
        [MaxLength(50,ErrorMessage ="Max Length is 50")]
        [MinLength(3, ErrorMessage = "Min Length is 3")]
        public string Name { get; set; }

        [Range(25,45,ErrorMessage ="Age Must Be Between 25,45")]
        public int? Age { get; set; }
        
        [RegularExpression(@"[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-z]{4,10}-[a-zA-z]{5,10}$",
            ErrorMessage ="Address Must Be Like 123-street-city-country") ]
        public string Address { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        [Phone]

        public string Phone { get; set; }
        [DataType(DataType.Currency)]
        public Decimal Salary { get; set; }
        public bool IsActive { get; set; }
        [DisplayName("Hiring Date")]
        public DateTime HiringDate { get; set; }
        [DisplayName("Date Of Creation")]
        public DateTime DateOfCreation { get; set; } = DateTime.Now;



    }
}
