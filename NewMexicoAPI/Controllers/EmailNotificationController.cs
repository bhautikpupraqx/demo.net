using Microsoft.AspNetCore.Mvc;
using NewMexicoAPI.Models;
using NewMexicoAPI.Repositories;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace NewMexicoAPI.Controllers
{
    public class EmailNotificationController : Controller
    {
        private readonly EmailNotificationRepository _repository;
        public EmailNotificationController(EmailNotificationRepository repository)
        {
            _repository = repository;
        }

        [HttpPost("Email_Template")]
        public async Task<ActionResult> EmailTemplateForUser(string role, string userid)
        {
            return Ok(await _repository.EmailNotificationTemplateForUsers(role,userid));
        }

        [HttpPost("SaveEmail_Template")]
        public async Task<ActionResult> SaveEmailTemplateForUser(SaveEmailTemplate saveEmailTemplate)
        {
            return Ok(await _repository.SaveEmailnotificationtemplatedata(saveEmailTemplate));
        }

    }
}
