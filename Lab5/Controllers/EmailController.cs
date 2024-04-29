using EmailApp.Entities;
using EmailApp.Methods;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System;
using System.Net;
using EmailApp.Login;

namespace EmailApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        ///catalin.p2002@gmail.com
        /// kmpa alvc brdy pqyq
        //misterco2002@gmail.com
        //vtht tuyh rktv drbr

        [HttpPost("SendEmailsv2")]
        public async Task SendNewEmail2([FromBody] SendEmail emailParameters) //, IFormFile attachment)
        {
            var credentials = Logger.Instance.GetCredentials();
            try
            {
                await SendEmail2.SendEmail(credentials, emailParameters);
            }
            catch (Exception ex)
            {
                return;
            }
        }


        [HttpGet("GetEmailIMAP2")]
        public async Task<List<EmailParameters>> GetIMAP()
        {
            var credentials = Logger.Instance.GetCredentials();
            try
            {
                return await IMAPEmails.GetEmails(credentials);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("GetEmaiPOP3")]
        public async Task<List<EmailParameters>> GetPOP3()
        {
            var credentials = Logger.Instance.GetCredentials();
            try
            {
                return await POP3Emails.GetEmails(credentials);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<bool> Login(Credentials credentials)
        {
            Logger.Instance.SetCredentials(credentials);
            return true;
        }
    }
}