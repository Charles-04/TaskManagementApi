using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Infrastructure.Models;

namespace TaskManager.Infrastructure.Interface
{
    public interface IEmailService
    {
        void Send(EmailMessage emailMessage);
    }
}
