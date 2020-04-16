using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Repositories;

namespace PaymentContext.Tests.Moks
{
    public class FakeStudentRepository : IStudentRepository
    {
        public void CreateSubscription(Student student)
        {
        }

        public bool DocumentExists(string document)
        {
            return document == "99999999999";
        }

        public bool EmailExists(string email)
        {
            return email == "ricardo.caiuba@gmail.com";
        }
    }
}
