using System;
using Flunt.Notifications;
using Flunt.Validations;
using PaymentContext.Domain.Enuns;
using PaymentContext.Shared.Commands;

namespace PaymentContext.Domain.Commands
{
    public class CreateBoletoSubscriptionCommand : Notifiable, ICommand
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Document { get; set; }

        public string Email { get; set; }

        public string BarCode { get; set; }

        public string BoletoNumber { get; set; }

        public string PaymentNumber { get; set; }

        public DateTime PaidDate { get; set; }

        public DateTime ExpireDate { get; set; }

        public decimal Total { get; set; }

        public decimal TotalPaid { get; set; }

        public string Payer { get; set; }

        public string PayerDocument { get; set; }

        public EDocumentType PayerDocumentType { get; set; }

        public string PayerEmail { get; set; }

        public string Street { get; set; }

        public string Number { get; set; }

        public string Neighborhood { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; private set; }

        public string ZipCode { get; private set; }

        public void Validade()
        {
            AddNotifications(new Contract()
                .Requires()
                .HasMinLen(FirstName, 3, "Name.FirstName", "FirstName should be at least 3 characters long")
                .HasMinLen(LastName, 3, "Name.LastName", "LastName should be at least 3 characters long")
                .HasMaxLen(FirstName, 20, "Name.FirstName", "FirstName should be up to 20 characters")
                .HasMaxLen(LastName, 40, "Name.LastName", "LastName should be up to 40 characters")
            );
        }
    }
}
