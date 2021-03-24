using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace ShoppingCart.Model
{
    public class PersonModel
    {
        public int PersonId { get; set; }
        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        public string DateOfBirth { get; set; }
        [Required(ErrorMessage = "Date must be selected")]
        public DateTime DOB { get; set; }
        [Required(ErrorMessage = "Select a gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Select a country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Mobile No. is required")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string PassWord { get; set; }

        public int RoleId { get; set; }
    }
}
