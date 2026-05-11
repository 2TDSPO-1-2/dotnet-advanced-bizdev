using Arkive_API.Data;
using Arkive_API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as categorias de doença",
            Description = "Retorna todas as categorias clínicas cadastradas no sistema."
        )]
        public IActionResult GetAllCategorias()
        {
            try
            {
                var resultado = _context.CategoriaDoenca.ToList();

                if (!resultado.Any())
                    return NoContent();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("ativos")]
        [SwaggerOperation(
            Summary = "Lista categorias de doença ativas",
            Description = "Retorna todas as categorias clínicas com status ativo."
        )]
        public IActionResult GetCategoriasAtivas()
        {
            try
            {
                var resultado = _context.CategoriaDoenca
                    .Where(x => x.StAtivo == 'S')
                    .ToList();

                if (!resultado.Any())
                    return NoContent();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("inativos")]
        [SwaggerOperation(
            Summary = "Lista categorias de doença inativas",
            Description = "Retorna todas as categorias clínicas com status inativo (excluídas logicamente)."
        )]
        public IActionResult GetCategoriasInativas()
        {
            try
            {
                var resultado = _context.CategoriaDoenca
                    .Where(x => x.StAtivo == 'N')
                    .ToList();

                if (!resultado.Any())
                    return NoContent();

                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Busca categoria de doença por ID",
            Description = "Retorna uma categoria clínica específica pelo seu ID."
        )]
        public IActionResult GetCategoriaById(int id)
        {
            try
            {
                var categoria = _context.CategoriaDoenca
                    .FirstOrDefault(x => x.Id == id);

                if (categoria is null)
                    return NotFound();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria uma nova categoria de doença",
            Description = "Cadastra uma nova categoria clínica no sistema."
        )]
        public IActionResult CreateCategoria(CategoriaDoencaEntity model)
        {
            try
            {
                model.StAtivo = 'S';

                _context.CategoriaDoenca.Add(model);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetCategoriaById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza uma categoria de doença",
            Description = "Atualiza os dados de uma categoria clínica existente."
        )]
        public IActionResult CategoriaUpdate(int id, CategoriaDoencaEntity model)
        {
            try
            {
                var categoria = _context.CategoriaDoenca
                    .Where(x => x.StAtivo == 'S')
                    .FirstOrDefault(x => x.Id == id);

                if (categoria is null)
                    return NotFound();

                categoria.Nome = model.Nome;

                _context.CategoriaDoenca.Update(categoria);
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reativar/{id}")]
        [SwaggerOperation(
            Summary = "Reativa uma categoria de doença",
            Description = "Restaura uma categoria clínica previamente inativada."
        )]
        public IActionResult CategoriaReativar(int id)
        {
            try
            {
                var categoria = _context.CategoriaDoenca
                    .Where(x => x.StAtivo == 'N')
                    .FirstOrDefault(x => x.Id == id);

                if (categoria is null)
                    return NotFound();

                categoria.StAtivo = 'S';

                _context.CategoriaDoenca.Update(categoria);
                _context.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Inativa uma categoria de doença",
            Description = "Realiza a exclusão lógica de uma categoria clínica (soft delete)."
        )]
        public IActionResult CategoriaDelete(int id)
        {
            try
            {
                var categoria = _context.CategoriaDoenca
                    .Where(x => x.StAtivo == 'S')
                    .FirstOrDefault(x => x.Id == id);

                if (categoria is null)
                    return NotFound();

                categoria.StAtivo = 'N';

                _context.CategoriaDoenca.Update(categoria);
                _context.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}