namespace CoffeeContact.Web.Services
{
    using System.Threading.Tasks;

    public interface IMessageService
    {
        Task SendMessage<T>(T message)
        where T : class;
    }
}