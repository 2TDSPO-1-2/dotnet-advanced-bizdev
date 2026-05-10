using Arkive_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arkive_API.Controllers
{
    [Route("api/categorias-doenca")]
    [ApiController]
    public class CategoriaDoencaController : ControllerBase
    {
        private readonly ApplicationContext _context;

        public CategoriaDoencaController(ApplicationContext context)
        {
            _context = context;
        }

    }
}
