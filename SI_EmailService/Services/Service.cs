using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Emailservice;
using Google.Protobuf.Collections;
using Grpc.Core;
using SI_Exam_Monolithic_Flight_Application.Facade;

namespace SI_EmailService.Services
{
    public class Service : Emailservice.EmailService.EmailServiceBase
    {
        public override Task<ListOfEmails> GetEmails(Query request, ServerCallContext context)
        {
            var res = UserFacade.Singleton().GetUsersBasedOnParameter(request.Field, request.Filter);
            RepeatedField<Email> emails = new RepeatedField<Email>();
            foreach (var tuple in res)
            {
                var email = tuple.Item1;
                var name = tuple.Item2;
                emails.Add(new Email
                {
                    MailAddress = email,
                    Name = name
                });
            }
            var listOfEmails = new ListOfEmails
            {
                Emails = { emails}
            };
            return Task.FromResult(listOfEmails);
        }
    }
}
