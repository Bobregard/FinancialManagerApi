using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FinancialManager.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        [JsonIgnore]
        public Bank Bank { get; set; }
        public int BankId { get; set; }
        [JsonIgnore]
        public Location Location { get; set; }
        public int LocationId { get; set; }
        [JsonIgnore]
        public User User { get; set; }
        public string UserId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
