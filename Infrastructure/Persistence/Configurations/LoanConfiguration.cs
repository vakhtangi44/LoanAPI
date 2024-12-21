﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Configurations
{
    public class LoanConfiguration : IEntityTypeConfiguration<Loan>
    {
        public void Configure(EntityTypeBuilder<Loan> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Amount).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(l => l.Currency).IsRequired();
            builder.Property(l => l.LoanType).IsRequired();
            builder.Property(l => l.LoanPeriod).IsRequired();
            builder.Property(l => l.Status).IsRequired();
        }
    }
}
