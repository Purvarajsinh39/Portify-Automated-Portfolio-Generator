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
            SendEmail(toEmail, subject, htmlBody);
        }

        public void SendBlockNotification(string toEmail, string userName, string reason)
        {
            string htmlBody = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: 'Segoe UI', sans-serif; background-color: #f4f7f6; margin: 0; padding: 0; color: #333; }}
        .container {{ max-width: 600px; margin: 40px auto; background: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); border-top: 4px solid #ef4444; }}
        .header {{ background-color: #ef4444; padding: 25px; text-align: center; color: #ffffff; }}
        .content {{ padding: 30px; }}
        .reason-box {{ margin: 20px 0; background-color: #fff1f2; border-left: 4px solid #ef4444; padding: 15px; font-style: italic; color: #991b1b; }}
        .footer {{ background-color: #f4f7f6; padding: 20px; text-align: center; font-size: 12px; color: #aaa; border-top: 1px solid #eee; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1 style='margin:0; font-size:20px;'>Account Access Suspended</h1>
        </div>
        <div class='content'>
            <p>Dear {userName},</p>
            <p>We are writing to inform you that your Portify account has been suspended by an administrator.</p>
            <p><strong>Reason for suspension:</strong></p>
            <div class='reason-box'>
                {reason}
            </div>
            <p>While your account is suspended, you will not be able to log in or manage your portfolios. If you believe this is a mistake, please reach out to our support team.</p>
        </div>
        <div class='footer'>
            &copy; {DateTime.Now.Year} Portify. All rights reserved.
        </div>
    </div>
</body>
</html>";
            SendEmail(toEmail, "Account Access Suspended - Portify", htmlBody);
        }

        public void SendUnblockNotification(string toEmail, string userName)
        {
            string htmlBody = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: 'Segoe UI', sans-serif; background-color: #f4f7f6; margin: 0; padding: 0; color: #333; }}
        .container {{ max-width: 600px; margin: 40px auto; background: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1); border-top: 4px solid #22c55e; }}
        .header {{ background-color: #22c55e; padding: 25px; text-align: center; color: #ffffff; }}
        .content {{ padding: 30px; }}
        .footer {{ background-color: #f4f7f6; padding: 20px; text-align: center; font-size: 12px; color: #aaa; border-top: 1px solid #eee; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1 style='margin:0; font-size:20px;'>Account Access Restored</h1>
        </div>
        <div class='content'>
            <p>Hi {userName},</p>
            <p>Good news! Your Portify account access has been restored by an administrator.</p>
            <p>You can now log in and continue building your professional portfolios.</p>
            <div style='text-align: center; margin-top: 30px;'>
                <a href='#' style='background-color: #22c55e; color: white; padding: 12px 25px; text-decoration: none; border-radius: 6px; font-weight: bold;'>Login to Portify</a>
            </div>
        </div>
        <div class='footer'>
            &copy; {DateTime.Now.Year} Portify. All rights reserved.
        </div>
    </div>
</body>
</html>";
            SendEmail(toEmail, "Account Access Restored - Portify", htmlBody);
        }

        public void SendTemplateNotification(string toEmail, string userName, string templateName)
        {
            string htmlBody = $@"
<!DOCTYPE html>
<html>
<head>
    <style>
        body {{ font-family: 'Segoe UI', sans-serif; background-color: #f4f7f6; margin: 0; padding: 0; color: #333; }}
        .container {{ max-width: 600px; margin: 40px auto; background: #ffffff; border-radius: 12px; overflow: hidden; box-shadow: 0 10px 25px rgba(0, 0, 0, 0.05); border-top: 6px solid #a855f7; }}
        .header {{ background: linear-gradient(135deg, #a855f7 0%, #9333ea 100%); padding: 40px 20px; text-align: center; color: #ffffff; }}
        .content {{ padding: 40px; text-align: center; }}
        .template-card {{ background-color: #faf5ff; border: 1px solid #f3e8ff; border-radius: 12px; padding: 25px; margin: 25px 0; }}
        .footer {{ background-color: #f8fafc; padding: 25px; text-align: center; font-size: 13px; color: #64748b; border-top: 1px solid #f1f5f9; }}
        .btn {{ background: #a855f7; color: white; padding: 14px 30px; text-decoration: none; border-radius: 50px; font-weight: bold; display: inline-block; transition: all 0.3s ease; }}
    </style>
</head>
<body>
    <div class='container'>
        <div class='header'>
            <h1 style='margin:0; font-size:24px; font-family: serif; font-style: italic;'>New Template Available</h1>
        </div>
        <div class='content'>
            <p style='font-size: 18px; color: #1e293b;'>Hi {userName},</p>
            <p>Exciting news! A brand new portfolio template has just been added to Portify.</p>
            
            <div class='template-card'>
                <h2 style='margin:0; color: #9333ea; font-size: 20px;'>{templateName}</h2>
                <p style='color: #64748b; margin-top: 10px;'>Ready for your next professional showcase.</p>
            </div>

            <p style='margin-bottom: 35px;'>Log in now to explore the new design and update your portfolio!</p>
            
            <a href='https://portify-demo.azurewebsites.net/Dashboard/Explore' class='btn'>Explore Templates</a>
        </div>
        <div class='footer'>
            &copy; {DateTime.Now.Year} Portify. Helping you build your digital identity.<br>
            <span style='font-size: 11px; opacity: 0.7;'>You received this email because you're a registered Portify user. You can manage your notification settings in your profile.</span>
        </div>
    </div>
</body>
</html>";
            SendEmail(toEmail, "New Portfolio Template Available! - Portify", htmlBody);
        }

        private void SendEmail(string toEmail, string subject, string htmlBody)
        {
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("portify.support@gmail.com", "Portify Support");
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
