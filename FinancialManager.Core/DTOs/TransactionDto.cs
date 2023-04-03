using FinancialManager.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialManager.Core.DTOs
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public int BankId { get; set; }
        public int LocationId { get; set; }
        public int CategoryId { get; set; }
    }

    public class TransactionCreateDto
    {
        [Required]
        public decimal Amount { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
        public int BankId { get; set; }
        [Required]
        public int LocationId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }

    public class TransactionSummary
    {
        public string BankName { get; set; }
        public string CategoryName { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
