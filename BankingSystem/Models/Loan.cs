using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingSystem.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateAdded { get; set; }
        public decimal InterestRate { get; set; }
        public int MonthFrequency { get; set; }
        public string LoanName { get; set; }

        public bool TaxLevied { get; set; }
        public decimal TaxRate { get; set; }

    }
}
