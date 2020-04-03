using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore2.API.Services
{
    public interface IMailService
    {
        string MailToAddress { get; }
        string MailFromAddress { get; }
        bool SendMail(string subjec, string mailContent);
    }
}
