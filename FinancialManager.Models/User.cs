using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace FinancialManager.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountNumber { get; set; }
        public string Address { get; set; }
        [JsonIgnore]
        public List<Transaction> Transactions { get; set; }
    }
}