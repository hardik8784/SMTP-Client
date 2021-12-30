using System.Collections;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class SimpleEmailSender : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI TextData;
    [SerializeField] UnityEngine.UI.Button SubmitButton;
    [SerializeField] bool Send;

    const string SenderEmail = "hardik8784@gmail.com";
    const string SenderPassword = "***";
    const string ReceiverEmail = "fernando.restituto@georgebrown.ca";


    void Start()
    {
        SubmitButton.onClick.AddListener(delegate {
            if (Send)
            {
                SendAnEmail(TextData.text);
            }
            else
            {
                Debug.Log("Email not sent");
            }
        });
    }

    
    private static void SendAnEmail(string message)
    {
        MailMessage mail = new MailMessage();
        mail.From = new MailAddress(SenderEmail);
        mail.To.Add(ReceiverEmail);
        mail.Subject = "Assignment 2 SMTP Task -From Hardik Dipakbhai Shah(Student ID:101249099) ";
        mail.Body = message;

        SmtpClient SMTPServer = new SmtpClient("smtp.gmail.com");
        SMTPServer.Port = 587;
        SMTPServer.Credentials = new NetworkCredential(
            SenderEmail, SenderPassword) as ICredentialsByHost;
        SMTPServer.EnableSsl = true;
        ServicePointManager.ServerCertificateValidationCallback =
            delegate (object s, X509Certificate certificate,
            X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                Debug.Log("Email success!");
                return true;
            };

        try
        {
            SMTPServer.Send(mail);
        }
        catch (System.Exception Error)
        {
            Debug.Log("Email error: " + Error.Message);
        }
        finally
        {
            Debug.Log("Email sent!");
        }
    }
}
