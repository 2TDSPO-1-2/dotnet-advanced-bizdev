using Arkive_API.Data;
using Arkive_API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as espécies",
            Description = "Retorna todas as espécies cadastradas no sistema."
        )]
        public IActionResult GetAllEspecies()
        {
            try
            {
                var resultado = _context.Especie.ToList();

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
            Summary = "Busca espécie por ID",
            Description = "Retorna uma espécie específica pelo seu ID."
        )]
        public IActionResult GetEspecieById(int id)
        {
            try
            {
                var especie = _context.Especie.FirstOrDefault(x => x.Id == id);

                if (especie is null)
                    return NotFound();

                return Ok(especie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria uma nova espécie",
            Description = "Cadastra uma nova espécie no sistema."
        )]
        public IActionResult CreateEspecie(EspecieEntity model)
        {
            try
            {
                _context.Especie.Add(model);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetEspecieById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza uma espécie",
            Description = "Atualiza os dados de uma espécie existente."
        )]
        public IActionResult EspecieUpdate(int id, EspecieEntity model)
        {
            try
            {
                var especie = _context.Especie.FirstOrDefault(x => x.Id == id);

                if (especie is null)
                    return NotFound();

                especie.Especie = model.Especie;

                _context.Especie.Update(especie);
                _context.SaveChanges();

                return Ok(especie);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove uma espécie",
            Description = "Remove uma espécie do sistema."
        )]
        public IActionResult EspecieDelete(int id)
        {
            try
            {
                var especie = _context.Especie.FirstOrDefault(x => x.Id == id);

                if (especie is null)
                    return NotFound();

                _context.Especie.Remove(especie);
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