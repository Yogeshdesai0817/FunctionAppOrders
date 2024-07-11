namespace Bussiness.Interfaces
{
    public interface IMessageSenderService
    {
        Task<bool> SendMessageAsync(int messageCount);
    }
}
