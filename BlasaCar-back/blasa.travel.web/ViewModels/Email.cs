using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tools.Email;
using Microsoft.Extensions.Configuration;
namespace blasa.travel.web.Models
{
    public class EmailSender : Tools.Email.EmailSender
    {

        

            public EmailSender(IConfiguration config)
            {
                _apiKey = config["SendGrid:ApiKey"];
                _fromName = config["SendGrid:FromName"];
                _fromEmail = config["SendGrid:FromEmail"];

            }
        }
}
