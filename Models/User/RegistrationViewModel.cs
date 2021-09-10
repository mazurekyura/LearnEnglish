using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LearnEnglish.Models.User
{
    public class RegistrationViewModel
    {
        //[Required(ErrorMessageResourceType = typeof(User), ErrorMessageResourceName = typeof(User.Id))]
        [Required(ErrorMessage = "Не указан логин")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароль введен неверно")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public string ReturnUrl { get; set; }
    }
}
