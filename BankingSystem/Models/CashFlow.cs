using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class CashFlow
    {
        [Key] public int CashFlowId { get; set; }

        public string CashFlowName { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

        public DateTime DateofPayment { get; set; }

        public int InterestDays { get; set; }

        public decimal TaxAmount { get; set; }

    }
}
