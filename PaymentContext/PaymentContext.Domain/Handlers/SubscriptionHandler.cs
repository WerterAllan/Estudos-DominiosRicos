using System;
using Flunt.Notifications;
using PaymentContext.Domain.Commands;
using PaymentContext.Domain.Repositories;
using PaymentContext.Domain.Converts;
using PaymentContext.Shared.Handlers;
using PaymentContext.Domain.Entities;
using PaymentContext.Domain.Services;

namespace PaymentContext.Domain.Handlers
{
    public class SubscriptionHandler : Notifiable, IHandler<CreateBoletoSubscriptionCommand>
    {
        private readonly IStudentRepository _repository;
        private readonly IEmailService _emailService;

        public SubscriptionHandler(IStudentRepository studentRepository, IEmailService emailService)
        {
            _repository = studentRepository;
            _emailService = emailService;
        }


        public ICommandResult Handler(CreateBoletoSubscriptionCommand command)
        {
            // Fail Fast Validation
            if (CommandIsInvalid(command))
                return new CommandResult(false, "Não foi possível realizar sua assinatura");

            // Verificar se o documento já esta cadastrado
            ChecksIfTheDocumentHasAlreadyBeenRegistered(command);

            // verificar se E-mail já esta cadastrado
            ChecksIfTheEmailHasAlreadyBeenRegistered(command);

            // gerar os VOs
            //GenerateVOs(command);
            // Gerar as Entidades
            var student = command.ToStudentEntity();
            var payment = command.ToBoletoPaymentEntity();
            var subscription = new Subscription(DateTime.Now.AddMonths(1));

            // Relacionamentos
            subscription.AddPayment(payment);
            student.AddSubscriptions(subscription);

            // Agrupar as Validações
            AddNotifications(student, payment, subscription);

            // Checar as validações
            if (Invalid)
                return new CommandResult(false, "Não foi possivel realizar sua assinatura");

            // Salvar as INformações
            _repository.CreateSubscription(student);

            // Enviar E-mail de boas vindas
            _emailService.Send(student.Name.ToString(), student.Email.Address, "Bem vindo ao Werter.IO", "Sua assinatura foi liberada");
            
            // Retonar informações

            return new CommandResult(true, "Assinatura realizada com sucesso");
        }

        private void GenerateVOs(CreateBoletoSubscriptionCommand command)
        {
            throw new NotImplementedException();
        }

        private void ChecksIfTheEmailHasAlreadyBeenRegistered(CreateBoletoSubscriptionCommand command)
        {
            var alreadyRegistered = _repository.EmailExists(command.Email);
            if (alreadyRegistered)
                AddNotification("Email", "Este Email já está em uso");
        }

        private void ChecksIfTheDocumentHasAlreadyBeenRegistered(CreateBoletoSubscriptionCommand command)
        {
            var alreadyRegistered = _repository.DocumentExists(command.Document);
            if (alreadyRegistered)
                AddNotification("Document", "Este CPF já está em uso");
        }

        private bool CommandIsInvalid(CreateBoletoSubscriptionCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return true;

            AddNotifications(command);
            return false;
                
        }
    }
}
