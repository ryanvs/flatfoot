namespace ExceptionHandler.Mail
{
    public interface IPostalWorker
    {
        void SendMail(IMailMessage mailMessage);
    }
}