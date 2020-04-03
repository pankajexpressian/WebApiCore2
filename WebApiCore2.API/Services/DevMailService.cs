using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiCore2.API.Services
{
    public class DevMailService : IMailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DevMailService> _logger;

        public DevMailService(IConfiguration configuration, ILogger<DevMailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public string MailToAddress => _configuration["MailSettings:MailTo"];

        public string MailFromAddress => _configuration["MailSettings:MailFrom"];

        public bool SendMail(string subjec, string mailContent)
        {

            _logger.LogInformation($" Mail Sent From Dev : {MailFromAddress} to : {MailToAddress} with subject : {subjec}  ");
            return true;
        }
    }
}
