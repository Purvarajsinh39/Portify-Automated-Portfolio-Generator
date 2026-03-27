using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Portify.Models
{
    public class EmailService
    {
        public static void SendOtpEmailBackgroundJob(string toEmail, string otpCode, string purpose)
        {
            // Note: Background jobs must be static string-based, or non-static if registered appropriately. Static is simplest.
            var service = new EmailService();
            service.SendOtpEmail(toEmail, otpCode, purpose);
        }

        public void SendOtpEmail(string toEmail, string otpCode, string purpose)
        {
            string subject = purpose == "Registration" ? "Welcome to Portify - Verify Your Email" : "Portify - Password Reset Code";
            string title = purpose == "Registration" ? "Verify Your Email" : "Reset Your Password";
            string bodyText = purpose == "Registration" 
                ? "Thank you for registering on Portify! Use the following OTP to verify your email address and complete your registration."
                : "We received a request to reset your Portify password. Use the OTP below to proceed. If you didn't request this, you can safely ignore this email.";

            string htmlBody = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #f4f7f6;
            margin: 0;
            padding: 0;
            color: #333;
        }}
        .container {{
            max-width: 600px;
            margin: 40px auto;
            background: #ffffff;
            border-radius: 8px;
            overflow: hidden;
            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
        }}
        .header {{
            background-color: #0d6efd;
            padding: 30px;
            text-align: center;
            color: #ffffff;
        }}
        .header h1 {{
            margin: 0;
            font-size: 24px;
        }}
        .content {{
            padding: 30px;
            text-align: center;
        }}
        .content p {{
            font-size: 16px;
            line-height: 1.5;
            color: #555;
        }}
        .otp-box {{
            margin: 30px auto;
            border: 2px dashed #0d6efd;
            background-color: #f8fbff;
            display: inline-block;
            padding: 15px 30px;
            font-size: 32px;
            font-weight: bold;
            letter-spacing: 5px;
            color: #0d6efd;
            border-radius: 8px;
        }}
        .footer {{
            background-color: #f4f7f6;
            padding: 20px;
            text-align: center;
            font-size: 12px;
            color: #aaa;
        }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1>{title}</h1>
        </div>
        <div class='content'>
            <p>Hi there,</p>
            <p>{bodyText}</p>
            <div class='otp-box'>
                {otpCode}
            </div>
            <p>This code is valid for <strong>5 minutes</strong>. Please do not share this code with anyone.</p>
        </div>
        <div class='footer'>
            &copy; {DateTime.Now.Year} Portify. All rights reserved.<br>
            If you have any questions, contact us at portify.support@gmail.com.
        </div>
    </div>
</body>
</html>";

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("portify.support@gmail.com", "Portify Verification");
                mail.To.Add(toEmail);
                mail.Subject = subject;
                mail.Body = htmlBody;
                mail.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = new NetworkCredential("portify.support@gmail.com", "csonemwbeculdpjb");
                    smtp.EnableSsl = true;
                    smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    smtp.Send(mail);
                }
            }
        }
    }
}
