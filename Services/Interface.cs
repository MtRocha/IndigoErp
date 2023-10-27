namespace IndigoErp.Services
{
    public interface IMailService 
    {
        public void SendRecoveryCode(string email, string subject, string body);
    }
}
