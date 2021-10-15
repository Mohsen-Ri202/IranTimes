using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IranTimes
{
    public interface IMessageSender
    {
        public Task SendEmailAsync(string toEmail ,string Subject, string Message , bool IsMessageHtml=false );
    }
}
