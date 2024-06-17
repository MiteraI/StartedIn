namespace CrossCutting.DTOs.Email
{
    public class Smtp
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseCredential { get; set; }
    }
}
