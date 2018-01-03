using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Truck.Services
{
    public interface IMailService
    {
        bool SendMail(string to, string subject, string body, string cc = null, string bcc = null,
            bool isBodyHtml = true, string[] attachments = null, bool async = false);

        bool SendBug(string controller, string action, Exception exp);
    }
}
