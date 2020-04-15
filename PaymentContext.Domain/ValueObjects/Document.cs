using Flunt.Validations;
using PaymentContext.Domain.Enuns;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects
{
    public class Document : ValueObject
    {
        public Document(string number, EDocumentType type)
        {
            Number = number;
            Type = type;

            AddNotifications(new Contract()
                .Requires()
                .IsTrue(this.Validade(), "Document.Number", "Invalid docment")
            );
        }

        public string Number { get; private set; }
        public EDocumentType Type { get; private set; }

        private bool Validade()
        {
            if (this.Type.Equals(EDocumentType.CNPJ) && Number.Length == 14)
                return true;

            if (this.Type.Equals(EDocumentType.CPF) && Number.Length == 11)
                return true;

            return false;
        }
    }
}
