using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CoffeeContact.Web.Services
{
    public interface IMessageService {
        Task SendMessage(Dictionary<string, string> responses);
    }
}