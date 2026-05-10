using Arkive_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkive_API.Controllers
{
    [Route("api/especies")]
    [ApiController]
    public class EspecieController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public EspecieController(ApplicationContext context)
        {
            _context = context;
        }
    }
}
