using Arkive_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkive_API.Controllers
{
    [Route("api/doencas")]
    [ApiController]
    public class DoencaController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public DoencaController(ApplicationContext context)
        {
            _context = context;
        }
    }
}
