using Arkive_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkive_API.Controllers
{
    [Route("api/predisposicoes")]
    [ApiController]
    public class PredisposicaoController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public PredisposicaoController(ApplicationContext context)
        {
            _context = context;
        }
    }
}
