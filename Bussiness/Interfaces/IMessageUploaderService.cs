namespace Bussiness.Interfaces
{
    public interface IMessageUploaderService
    {
        Task<bool> UpLoadMessageAsync(string message);
    }
}
