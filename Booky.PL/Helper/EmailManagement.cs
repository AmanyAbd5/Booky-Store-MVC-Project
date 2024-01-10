using Booky.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Booky.PL.Helper

{
    public static class EmailManagement
    {
        public static void SendEmail(Email email)
        {
            var client =new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl=true;
            client.Credentials = new NetworkCredential("test1project2024@gmail.com", "mpdxptagbztvaskd");
            client.Send("test1project2024@gmail.com", email.To, email.Subject, email.Body);
        }
    }
}
