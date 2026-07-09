using System.Net;
using System.Net.Mail;

namespace Wisdom.Services.Implementation;

public class EmailServices
{
    // Send email methods
    public async static void SendEmail(string email, string subject, string body)
    {
        // Create smtp protocol
        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("divademadpro160@gmail.com", "?"),
            EnableSsl = true
        };
        
        // Create email
        var message = new MailMessage
        {
            From = new MailAddress("divademadpro160@gmail.com"),
            To = {email},
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        // Send email
        await smtpClient.SendMailAsync(message);
    }
    
    // Generate verification email method
    public static string GenerateVerificationEmail(int code)
    {
        return $@" <!DOCTYPE html> 
                   <html lang='en'> 
                   <head>
                        <meta charset='UTF-8'> 
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'> 
                        <title>Email Verification</title> 
                   </head> 
                   <body style='margin:0;padding:0;background-color:#f4f4f4;font-family:Arial,Helvetica,sans-serif;'> 
                       <table width='100%' cellpadding='0' cellspacing='0' style='background-color:#f4f4f4;padding:40px 0;'> 
                            <tr> 
                                <td align='center'> 
                                    <table width='600' cellpadding='0' cellspacing='0' style='background-color:#ffffff;border-radius:12px;overflow:hidden; box-shadow:0 4px 15px rgba(0,0,0,0.1);'> 
                                        <tr> 
                                            <td align='center' style='background-color:#2563eb;padding:30px;color:#ffffff;'> 
                                                <h1 style='margin:0;font-size:28px;'> Welcome to Wisdom System </h1> 
                                            </td> 
                                        </tr> 
                                        <tr> 
                                            <td style='padding:40px 35px;color:#333333;'> 
                                                <h2 style='margin-top:0;'> Hello, </h2> 
                                                <p style='font-size:16px;line-height:1.7;'> Thank you for registering. To complete your account setup, please use the verification code below: </p> 
                                                <div style='text-align:center;margin:35px 0;'> 
                                                    <span style='display:inline-block; background-color:#f3f4f6; border:2px dashed #2563eb; padding:18px 35px; font-size:32px; font-weight:bold; letter-spacing:8px; color:#2563eb; border-radius:10px;'> {code} </span> 
                                                </div> 
                                                <p style='font-size:16px;line-height:1.7;'> This verification code will expire in <strong>10 minutes</strong>. </p> 
                                                <p style='font-size:16px;line-height:1.7;'> If you did not create an account, you can safely ignore this email. </p> 
                                                <hr style='border:none;border-top:1px solid #e5e7eb;margin:30px 0;'> 
                                                <p style='font-size:14px;color:#6b7280;text-align:center;'> © 2026 Wisdom. All rights reserved. </p> 
                                            </td> 
                                        </tr> 
                                    </table> 
                                </td> 
                            </tr> 
                        </table> 
                    </body> 
                    </html>";
    }
}