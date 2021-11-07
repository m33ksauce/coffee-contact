using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CoffeeContact.Web.Services
{
    public interface IMessageService {
        Task SendMessage<T>(T message) where T : class;
    }
}