using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enuns;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Services;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler :
        Notifiable,
        IHandler<CreateBoletoSubscriptionCommand>,
        IHandler<CreatePayPalSubscriptionCommand>
    {

        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository repository, IEmailService emailService)
        {
            _repository = repository;
            _emailService = emailService;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand cmd)
        {

            // Fail Fast Validations
            cmd.Validade();
            if (cmd.Invalid)
            {
                AddNotifications(cmd);
                return new CommandResult(false, "Couldn't complete your subscription");
            }

            // Verificar se documento já está cadastrado
            if (_repository.DocumentExists(cmd.Document))
                AddNotification("Document", "This CPF is already in use");

            // Verificar se E-mail já está cadastrado
            if (_repository.EmailExists(cmd.Email))
                AddNotification("Document", "This CPF is already in use");

            // Gerar os VOs
            Name name = new Name(cmd.FirstName, cmd.LastName);
            Document document = new Document(cmd.Document, EDocumentType.CPF);
            Email email = new Email(cmd.Email);
            Address address = new Address(cmd.Street, cmd.Number, cmd.Neighborhood, cmd.City, cmd.State, cmd.Country, cmd.ZipCode);

            // Gerar as entidades
            Student student = new Student(name, document, email);
            Subscription subscription = new Subscription(DateTime.Now.AddMonths(1));
            BoletoPayment payment = new BoletoPayment(
                cmd.BarCode,
                cmd.BoletoNumber,
                cmd.PaidDate,
                cmd.ExpireDate,
                cmd.Total,
                cmd.Payer,
                new Document(cmd.PayerDocument, cmd.PayerDocumentType),
                cmd.TotalPaid,
                address,
                email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Checar as notificações
            if (Invalid)
                return new CommandResult(false, "Couldn't complete your subscription");
            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(
                student.Name.ToString(),
                student.Email.Address,
                "Welcome to the rrsantos.com.br",
                "Your subscription has been created"
            );

            return new CommandResult(true, "Subscription successful!");

        }

        public ICommandResult Handle(CreatePayPalSubscriptionCommand cmd)
        {
            // Verificar se documento já está cadastrado
            if (_repository.DocumentExists(cmd.Document))
                AddNotification("Document", "This CPF is already in use");

            // Verificar se E-mail já está cadastrado
            if (_repository.EmailExists(cmd.Email))
                AddNotification("Document", "This CPF is already in use");

            // Gerar os VOs
            Name name = new Name(cmd.FirstName, cmd.LastName);
            Document document = new Document(cmd.Document, EDocumentType.CPF);
            Email email = new Email(cmd.Email);
            Address address = new Address(cmd.Street, cmd.Number, cmd.Neighborhood, cmd.City, cmd.State, cmd.Country, cmd.ZipCode);

            // Gerar as entidades
            Student student = new Student(name, document, email);
            Subscription subscription = new Subscription(DateTime.Now.AddMonths(1));
            PayPalPayment payment = new PayPalPayment(
                cmd.TransactionCode,
                cmd.PaidDate,
                cmd.ExpireDate,
                cmd.Total,
                cmd.Payer,
                new Document(cmd.PayerDocument, cmd.PayerDocumentType),
                cmd.TotalPaid,
                address,
                email
            );

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            // Agrupar as Validações
            AddNotifications(name, document, email, address, student, subscription, payment);

            // Salvar as informações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(
                student.Name.ToString(),
                student.Email.Address,
                "Welcome to the rrsantos.com.br",
                "Your subscription has been created"
            );

            return new CommandResult(true, "Subscription successful!");

        }
    }
}
