using DejlienApp.Framework;
using Microsoft.AspNet.Identity.EntityFramework;


namespace DejlienApp.Models
{
    public class UserAccount:IdentityUser, IEntity<string>
    {
        //[Key]
        //public override string Id { get; set; }

        //[Required(ErrorMessage = "Email is required.")]
        //[RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3]\.)|(([\w-]+\.)+))([a-zA-Z{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter valid email.")]
        //public override string Email { get; set; }

        // Är dennan ödvändig?
        //[Required(ErrorMessage = "Username is required.")]
        //public string Username { get; set; }

        //[Required(ErrorMessage = "Password is required.")]
        //[DataType(DataType.Password)]
        //public string Password { get; set; }

        //[Required(ErrorMessage = "Confirm password.")]
        //[DataType(DataType.Password)]
        //public string ConfirmPassword { get; set; }

        public Profile Profile { get; set; }
    }
}