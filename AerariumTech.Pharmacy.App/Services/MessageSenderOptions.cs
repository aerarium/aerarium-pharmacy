namespace AerariumTech.Pharmacy.App.Services
{
    public class MessageSenderOptions
    {
        public string Domain { get; set; }
        
        public int Port { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        // Deprecated (SendGrid)
        public string SendGridUser { get; set; }
        public string SendGridKey { get; set; }
    }
}