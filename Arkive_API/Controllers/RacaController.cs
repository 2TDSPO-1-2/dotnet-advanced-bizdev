using Arkive_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkive_API.Controllers
{
    [Route("api/racas")]
    [ApiController]
    public class RacaController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public RacaController(ApplicationContext context)
        {
            _context = context;
        }
    }
}
