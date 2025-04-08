using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Email wajib diisi")]
        [EmailAddress(ErrorMessage = "Format email tidak valid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username wajib diisi")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password wajib diisi")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Konfirmasi Password wajib diisi")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password dan konfirmasi tidak cocok")]
        public string ConfirmPassword { get; set; }
    }
}
