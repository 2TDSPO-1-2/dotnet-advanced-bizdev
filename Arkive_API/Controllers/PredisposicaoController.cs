using Arkive_API.Data;
using Arkive_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as predisposições",
            Description = "Retorna todos os vínculos de predisposição entre espécie, raça e doença."
        )]
        public IActionResult GetAllPredisposicoes()
        {
            try
            {
                var resultado = _context.Predisposicao
                    .Include(x => x.Especie)
                    .Include(x => x.Raca)
                    .Include(x => x.Doenca)
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
            Summary = "Busca predisposição por ID",
            Description = "Retorna um vínculo de predisposição específico pelo seu ID."
        )]
        public IActionResult GetPredisposicaoById(int id)
        {
            try
            {
                var predisposicao = _context.Predisposicao
                    .Include(x => x.Especie)
                    .Include(x => x.Raca)
                    .Include(x => x.Doenca)
                    .FirstOrDefault(x => x.Id == id);

                if (predisposicao is null)
                    return NotFound();

                return Ok(predisposicao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("especie/{idEspecie}")]
        [SwaggerOperation(
            Summary = "Lista predisposições por espécie",
            Description = "Retorna todas as predisposições vinculadas a uma espécie específica."
        )]
        public IActionResult GetPredisposicaoByEspecie(int idEspecie)
        {
            try
            {
                var resultado = _context.Predisposicao
                    .Include(x => x.Especie)
                    .Include(x => x.Raca)
                    .Include(x => x.Doenca)
                    .Where(x => x.IdEspecie == idEspecie)
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

        [HttpGet("raca/{idRaca}")]
        [SwaggerOperation(
            Summary = "Lista predisposições por raça",
            Description = "Retorna todas as predisposições vinculadas a uma raça específica."
        )]
        public IActionResult GetPredisposicaoByRaca(int idRaca)
        {
            try
            {
                var resultado = _context.Predisposicao
                    .Include(x => x.Especie)
                    .Include(x => x.Raca)
                    .Include(x => x.Doenca)
                    .Where(x => x.IdRaca == idRaca)
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

        [HttpGet("doenca/{idDoenca}")]
        [SwaggerOperation(
            Summary = "Lista predisposições por doença",
            Description = "Retorna todas as predisposições vinculadas a uma doença específica."
        )]
        public IActionResult GetPredisposicaoByDoenca(int idDoenca)
        {
            try
            {
                var resultado = _context.Predisposicao
                    .Include(x => x.Especie)
                    .Include(x => x.Raca)
                    .Include(x => x.Doenca)
                    .Where(x => x.IdDoenca == idDoenca)
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
            Summary = "Cria vínculo de predisposição",
            Description = "Cadastra um novo vínculo de predisposição entre espécie/raça e doença."
        )]
        public IActionResult CreatePredisposicao(PredisposicaoEntity model)
        {
            try
            {
                _context.Predisposicao.Add(model);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetPredisposicaoById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Sem PUT — conforme PRD §16.3.5:
        // "o PUT pode ser substituído por remover o vínculo antigo e criar um novo"

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove vínculo de predisposição",
            Description = "Remove o vínculo de predisposição entre espécie/raça e doença."
        )]
        public IActionResult PredisposicaoDelete(int id)
        {
            try
            {
                var predisposicao = _context.Predisposicao.FirstOrDefault(x => x.Id == id);

                if (predisposicao is null)
                    return NotFound();

                _context.Predisposicao.Remove(predisposicao);
                _context.SaveChanges();

                return Ok(predisposicao);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}