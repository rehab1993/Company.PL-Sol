using System.Collections;
using System.Collections.Generic;

namespace Company.PL.ViewModels
{
    public class UsersViewModel
    {
        public string Id {  get; set; }

        public string FName { get; set; }
        public string LName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
