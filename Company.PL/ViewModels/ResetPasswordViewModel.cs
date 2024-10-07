using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Company.PL.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="New Password Is Required")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "Confirm New Password Is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage ="Password Does't Match")]
        public string ConfirmPassword { get; set; }
    }
}
