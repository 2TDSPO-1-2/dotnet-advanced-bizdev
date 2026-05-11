using Arkive_API.Data;
using Arkive_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as raças",
            Description = "Retorna todas as raças, incluindo a espécie vinculada."
        )]
        public IActionResult GetAllRacas()
        {
            try
            {
                var resultado = _context.Raca
                    .Include(x => x.Especie)
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

        [HttpGet("ativos")]
        [SwaggerOperation(
            Summary = "Lista raças ativas",
            Description = "Retorna todas as raças com status ativo."
        )]
        public IActionResult GetRacasAtivas()
        {
            try
            {
                var resultado = _context.Raca
                    .Include(x => x.Especie)
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
            Summary = "Lista raças inativas",
            Description = "Retorna todas as raças com status inativo (excluídas logicamente)."
        )]
        public IActionResult GetRacasInativas()
        {
            try
            {
                var resultado = _context.Raca
                    .Include(x => x.Especie)
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
            Summary = "Busca raça por ID",
            Description = "Retorna uma raça específica pelo seu ID, incluindo a espécie vinculada."
        )]
        public IActionResult GetRacaById(int id)
        {
            try
            {
                var raca = _context.Raca
                    .Include(x => x.Especie)
                    .FirstOrDefault(x => x.Id == id);

                if (raca is null)
                    return NotFound();

                return Ok(raca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("especie/{idEspecie}")]
        [SwaggerOperation(
            Summary = "Lista raças por espécie",
            Description = "Retorna todas as raças ativas vinculadas a uma espécie específica."
        )]
        public IActionResult GetRacasByEspecie(int idEspecie)
        {
            try
            {
                var resultado = _context.Raca
                    .Include(x => x.Especie)
                    .Where(x => x.StAtivo == 'S' && x.IdEspecie == idEspecie)
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

        [HttpPost]
        [SwaggerOperation(
            Summary = "Cria uma nova raça",
            Description = "Cadastra uma nova raça vinculada a uma espécie."
        )]
        public IActionResult CreateRaca(RacaEntity model)
        {
            try
            {
                model.StAtivo = 'S';

                _context.Raca.Add(model);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetRacaById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza uma raça",
            Description = "Atualiza os dados de uma raça existente."
        )]
        public IActionResult RacaUpdate(int id, RacaEntity model)
        {
            try
            {
                var raca = _context.Raca
                    .Where(x => x.StAtivo == 'S')
                    .FirstOrDefault(x => x.Id == id);

                if (raca is null)
                    return NotFound();

                raca.Raca = model.Raca;
                raca.IdEspecie = model.IdEspecie;
                raca.Porte = model.Porte;

                _context.Raca.Update(raca);
                _context.SaveChanges();

                return Ok(raca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reativar/{id}")]
        [SwaggerOperation(
            Summary = "Reativa uma raça",
            Description = "Restaura uma raça previamente inativada."
        )]
        public IActionResult RacaReativar(int id)
        {
            try
            {
                var raca = _context.Raca
                    .Where(x => x.StAtivo == 'N')
                    .FirstOrDefault(x => x.Id == id);

                if (raca is null)
                    return NotFound();

                raca.StAtivo = 'S';

                _context.Raca.Update(raca);
                _context.SaveChanges();

                return Ok(raca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Inativa uma raça",
            Description = "Realiza a exclusão lógica de uma raça (soft delete)."
        )]
        public IActionResult RacaDelete(int id)
        {
            try
            {
                var raca = _context.Raca
                    .Where(x => x.StAtivo == 'S')
                    .FirstOrDefault(x => x.Id == id);

                if (raca is null)
                    return NotFound();

                raca.StAtivo = 'N';

                _context.Raca.Update(raca);
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