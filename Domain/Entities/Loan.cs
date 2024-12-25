﻿using Domain.Enums;

namespace Domain.Entities
{
    public class Loan
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public LoanType Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public int Period { get; set; } 
        public LoanStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual User User { get; set; }
    }
}
