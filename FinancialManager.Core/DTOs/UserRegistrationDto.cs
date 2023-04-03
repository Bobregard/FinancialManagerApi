using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Models.DTOs
{
    public class UserRegistrationDto
    {
        public string? FirstName { get; init; }
        public string? LastName { get; init; }

        [Required(ErrorMessage = "Username is required")]
        public string? UserName { get; init; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; init; }
        public string? Email { get; init; }
        public string? PhoneNumber { get; init; }

        [Required(ErrorMessage = "Account number is required")]
        public string? AccountNumber { get; init; }

        [Required(ErrorMessage = "Address is required")]
        public string? Address { get; init; }

    }
}
