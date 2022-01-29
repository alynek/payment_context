using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Enums;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.ValueObjects;
using PaymentContext.Shared.Commands;
using PaymentContext.Shared.Handlers;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable<Notification>, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _studentRepository;

        public SubscriptionHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public ICommandResult Handle(CreateBoletoSubscriptionCommand command)
        {
            //Validations
            command.Validate();

            if(!command.IsValid)
            {
                AddNotifications(command);
                return new CommandResult(true, "Unable to subscribe");
            }

            if(_studentRepository.DocumentExists(command.Document)) AddNotification("Document", "This Document is already in use");

            if(_studentRepository.EmailExists(command.Email)) AddNotification("Email", "This Email is already in use");

            //Generate VOS
            var name = new Name(command.FirstName, command.LastName);
            var document = new Document(command.Document,  command.PayerDocumentType);
            var email = new Email(command.Email);
            var address = new Address(command.Street, command.Number, command.Neighborhood, command.City, command.State, command.Country, command.ZipCode);

            //Generate Entities
            var student = new Student(name, document, email);
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            var payment = new BoletoPayment(
                command.BarCode, 
                command.BoletoNumber, 
                command.PaidDate, 
                command.ExpireDate, 
                command.Total, 
                command.TotalPaid, 
                address, 
                new Document(command.PayerDocument, command.PayerDocumentType), 
                command.Payer,
                email
            );

            subscription.AddPayment(payment);
            student.AddSubscription(subscription);

            AddNotifications(name, document, email, address, student, subscription, payment);

            _studentRepository.CreateSubscription(student);

            return new CommandResult(true, "Successful subscription!");
        }
    }
}