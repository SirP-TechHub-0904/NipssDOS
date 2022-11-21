using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NipssDOS.Services
{
    public interface ISecServices
    {
        Task<string> SendSms(string recipient, string message);
        Task<bool> SendEmail(string recipient, string message, string title);

    }
}
