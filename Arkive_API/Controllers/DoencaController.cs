using Arkive_API.Data;
using Arkive_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;

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

        [HttpGet]
        [SwaggerOperation(
            Summary = "Lista todas as doenças",
            Description = "Retorna todas as doenças, incluindo a categoria clínica vinculada."
        )]
        public IActionResult GetAllDoencas()
        {
            try
            {
                var resultado = _context.Doenca
                    .Include(x => x.Categoria)
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
            Summary = "Lista doenças ativas",
            Description = "Retorna todas as doenças com status ativo."
        )]
        public IActionResult GetDoencasAtivas()
        {
            try
            {
                var resultado = _context.Doenca
                    .Include(x => x.Categoria)
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
            Summary = "Lista doenças inativas",
            Description = "Retorna todas as doenças com status inativo (excluídas logicamente)."
        )]
        public IActionResult GetDoencasInativas()
        {
            try
            {
                var resultado = _context.Doenca
                    .Include(x => x.Categoria)
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
            Summary = "Busca doença por ID",
            Description = "Retorna uma doença específica pelo seu ID, incluindo a categoria clínica vinculada."
        )]
        public IActionResult GetDoencaById(int id)
        {
            try
            {
                var doenca = _context.Doenca
                    .Include(x => x.Categoria)
                    .FirstOrDefault(x => x.Id == id);

                if (doenca is null)
                    return NotFound();

                return Ok(doenca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("nome/{nome}")]
        [SwaggerOperation(
            Summary = "Busca doenças por nome",
            Description = "Retorna todas as doenças ativas cujo nome contenha o termo informado (busca parcial)."
        )]
        public IActionResult GetDoencaByNome(string nome)
        {
            try
            {
                var resultado = _context.Doenca
                    .Include(x => x.Categoria)
                    .Where(x => x.StAtivo == 'S' && x.Nome.ToLower().Contains(nome.ToLower()))
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

        [HttpGet("categoria/{idCategoria}")]
        [SwaggerOperation(
            Summary = "Lista doenças por categoria",
            Description = "Retorna todas as doenças ativas vinculadas a uma categoria clínica específica."
        )]
        public IActionResult GetDoencaByCategoria(int idCategoria)
        {
            try
            {
                var resultado = _context.Doenca
                    .Include(x => x.Categoria)
                    .Where(x => x.StAtivo == 'S' && x.IdCategoria == idCategoria)
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
            Summary = "Cria uma nova doença",
            Description = "Cadastra uma nova doença no catálogo clínico."
        )]
        public IActionResult CreateDoenca(DoencaEntity model)
        {
            try
            {
                model.StAtivo = 'S';

                _context.Doenca.Add(model);
                _context.SaveChanges();

                return CreatedAtAction(nameof(GetDoencaById), new { id = model.Id }, model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza uma doença",
            Description = "Atualiza os dados de uma doença existente no catálogo."
        )]
        public IActionResult DoencaUpdate(int id, DoencaEntity model)
        {
            try
            {
                var doenca = _context.Doenca
                    .Where(x => x.StAtivo == 'S')
                    .FirstOrDefault(x => x.Id == id);

                if (doenca is null)
                    return NotFound();

                doenca.Nome = model.Nome;
                doenca.IdCategoria = model.IdCategoria;
                doenca.Descricao = model.Descricao;
                doenca.CID = model.CID;
                doenca.Sintomas = model.Sintomas;

                _context.Doenca.Update(doenca);
                _context.SaveChanges();

                return Ok(doenca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("reativar/{id}")]
        [SwaggerOperation(
            Summary = "Reativa uma doença",
            Description = "Restaura uma doença previamente inativada."
        )]
        public IActionResult DoencaReativar(int id)
        {
            try
            {
                var doenca = _context.Doenca
                    .Where(x => x.StAtivo == 'N')
                    .FirstOrDefault(x => x.Id == id);

                if (doenca is null)
                    return NotFound();

                doenca.StAtivo = 'S';

                _context.Doenca.Update(doenca);
                _context.SaveChanges();

                return Ok(doenca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Inativa uma doença",
            Description = "Realiza a exclusão lógica de uma doença (soft delete)."
        )]
        public IActionResult DoencaDelete(int id)
        {
            try
            {
                var doenca = _context.Doenca
                    .Where(x => x.StAtivo == 'S')
                    .FirstOrDefault(x => x.Id == id);

                if (doenca is null)
                    return NotFound();

                doenca.StAtivo = 'N';

                _context.Doenca.Update(doenca);
                _context.SaveChanges();

                return Ok(doenca);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}