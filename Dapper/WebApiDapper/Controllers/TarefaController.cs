using Microsoft.AspNetCore.Mvc;
using WebApiDapper.Controllers.Repository;
using WebApiDapper.Data;

namespace WebApiDapper.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositoryProcedure repository;

        private readonly ILogger<TarefaController> _logger;

        public TarefaController(ILogger<TarefaController> logger, ITarefaRepositoryProcedure repository)
        {
            this.repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            try
            {
                var tarefas = await repository.GetAllAsync();
                return Ok(tarefas);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter tarefas.");
                return StatusCode(500, "Erro ao obter tarefas.");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefaById(int id)
        {
            try
            {
                var tarefa = await repository.GetByIdAsync(id);
                if (tarefa == null)
                {
                    return NotFound();
                }
                return Ok(tarefa);
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter tarefa com ID {id}.");
                return StatusCode(500, "Erro ao obter tarefa.");
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostTarefa([FromBody] Tarefa tarefa)
        {
            try
            {
                await repository.AddAsync(tarefa);
                return CreatedAtAction(nameof(GetTarefaById), new { id = tarefa.Id }, tarefa);
            }
            catch (System.Exception ex)
            {

                _logger.LogError(ex, "Erro ao adicionar nova tarefa.");
                return StatusCode(500, "Erro ao adicionar tarefa.");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutTarefa(int id, [FromBody] Tarefa tarefa)
        {
            try
            {
                if (id != tarefa.Id)
                {
                    return BadRequest();
                }
                await repository.UpdateAsync(tarefa);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Erro ao atualizar tarefa com ID {id}.");
                return StatusCode(500, "Erro ao atualizar tarefa.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTarefa(int id)
        {
            try
            {
                await repository.DeleteAsync(id);
                return NoContent();
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex, $"Erro ao deletar tarefa com ID {id}.");
                return StatusCode(500, "Erro ao deletar tarefa.");
            }
        }
    }
}