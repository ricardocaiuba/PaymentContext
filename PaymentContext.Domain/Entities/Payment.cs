using System;
using Flunt.Validations;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Entities;

namespace PaymentContext.Domain.Entities
{
    public abstract class Payment : Entity
    {
        protected Payment
        (
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            string payer,
            Document document,
            decimal totalPaid,
            Address address,
            Email email
        )
        {
            Number = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 10).ToUpper();
            PaidDate = paidDate;
            ExpireDate = expireDate;
            Total = total;
            Payer = payer;
            Document = document;
            TotalPaid = totalPaid;
            Address = address;
            Email = email;

            AddNotifications(new Contract()
                .Requires()
                .IsLowerOrEqualsThan(0, Total, "Payment.Total", "Total cannot be zero")
                .IsGreaterOrEqualsThan(Total, TotalPaid, "Payment.TotalPaid", "TotalPaid cannot be less than the value of the boleto")
            );
        }

        public string Number { get; private set; }
        public DateTime PaidDate { get; private set; }

        public DateTime ExpireDate { get; private set; }

        public decimal Total { get; private set; }

        public string Payer { get; private set; }

        public Document Document { get; private set; }

        public decimal TotalPaid { get; private set; }

        public Address Address { get; private set; }

        public Email Email { get; private set; }
    }
}
