using Arkive_API.Data;
using Arkive_API.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todos os feedbacks NPS",
            Description = "Retorna todos os feedbacks de satisfação registrados."
        )]
        public IActionResult GetAllFeedbacks()
        {
            try
            {
                var resultado = _context.FeedbackNPS.ToList();

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
            Summary = "Busca feedback NPS por ID",
            Description = "Retorna um feedback de satisfação específico pelo seu ID."
        )]
        public IActionResult GetFeedbackById(int id)
        {
            try
            {
                var feedback = _context.FeedbackNPS.FirstOrDefault(x => x.Id == id);

                if (feedback is null)
                    return NotFound();

                return Ok(feedback);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nota/{nota}")]
        [SwaggerOperation(
            Summary = "Lista feedbacks por nota",
            Description = "Retorna todos os feedbacks com a nota NPS informada (0 a 10)."
        )]
        public IActionResult GetFeedbackByNota(int nota)
        {
            try
            {
                var resultado = _context.FeedbackNPS
                    .Where(x => x.Nota == nota)
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

        [HttpGet("responsavel/{idResponsavel}")]
        [SwaggerOperation(
            Summary = "Lista feedbacks por responsável",
            Description = "Retorna todos os feedbacks vinculados a um responsável específico."
        )]
        public IActionResult GetFeedbackByResponsavel(int idResponsavel)
        {
            try
            {
                var resultado = _context.FeedbackNPS
                    .Where(x => x.IdResponsavel == idResponsavel)
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

        [HttpGet("animal/{idAnimal}")]
        [SwaggerOperation(
            Summary = "Lista feedbacks por animal",
            Description = "Retorna todos os feedbacks vinculados a um animal específico."
        )]
        public IActionResult GetFeedbackByAnimal(int idAnimal)
        {
            try
            {
                var resultado = _context.FeedbackNPS
                    .Where(x => x.IdAnimal == idAnimal)
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

        [HttpGet("clinica/{idClinica}")]
        [SwaggerOperation(
            Summary = "Lista feedbacks por clínica",
            Description = "Retorna todos os feedbacks vinculados a uma clínica específica."
        )]
        public IActionResult GetFeedbackByClinica(int idClinica)
        {
            try
            {
                var resultado = _context.FeedbackNPS
                    .Where(x => x.IdClinica == idClinica)
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

        [HttpGet("data/{data}")]
        [SwaggerOperation(
            Summary = "Lista feedbacks por data",
            Description = "Retorna todos os feedbacks registrados em uma data específica. Formato esperado: yyyy-MM-dd."
        )]
        public IActionResult GetFeedbackByData(string data)
        {
            try
            {
                if (!DateTime.TryParse(data, out DateTime dataParsed))
                    return BadRequest("Formato de data inválido. Use o formato yyyy-MM-dd.");

                var resultado = _context.FeedbackNPS
                    .Where(x => x.Resposta.Date == dataParsed.Date)
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
            Summary = "Registra um novo feedback NPS",
            Description = "Registra um feedback de satisfação vinculado a responsável, animal, clínica ou consulta."
        )]
        public IActionResult CreateFeedback(FeedbackNPSEntity model)
        {
            try
            {
                _context.FeedbackNPS.Add(model);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetFeedbackById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Sem PUT — feedback NPS é um registro imutável por natureza

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Remove um feedback NPS",
            Description = "Remove um registro de feedback NPS do sistema."
        )]
        public IActionResult FeedbackDelete(int id)
        {
            try
            {
                var feedback = _context.FeedbackNPS.FirstOrDefault(x => x.Id == id);

                if (feedback is null)
                    return NotFound();

                _context.FeedbackNPS.Remove(feedback);
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