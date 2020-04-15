using System;
using PaymentContext.Domain.ValueObjects;

namespace PaymentContext.Domain.Entities
{
    public class BoletoPayment : Payment
    {
        public BoletoPayment
        (
            string barCode,
            string boletoNumber,
            DateTime paidDate,
            DateTime expireDate,
            decimal total,
            string payer,
            Document document,
            decimal totalPaid,
            Address address,
            Email email
        )
        : base
        (
            paidDate,
            expireDate,
            total,
            payer,
            document,
            totalPaid,
            address,
            email
        )
        {
            BarCode = barCode;
            BoletoNumber = boletoNumber;
        }
        public string BarCode { get; private set; }
        public string BoletoNumber { get; private set; }
    }
}
