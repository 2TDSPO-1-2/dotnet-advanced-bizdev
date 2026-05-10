using Arkive_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkive_API.Controllers
{
    [Route("api/feedbacks-nps")]
    [ApiController]
    public class FeedbackNPSController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public FeedbackNPSController(ApplicationContext context)
        {
            _context = context;
        }
    }
}
