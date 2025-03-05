 
using WebApiDapper.Data;

namespace WebApiDapper.Controllers.Repository
{
    public interface ITarefaRepositoryProcedure
    {
        Task<IEnumerable<Tarefa>> GetAllAsync();
    Task<Tarefa> GetByIdAsync(int id);
    Task AddAsync(Tarefa tarefa);
    Task UpdateAsync(Tarefa tarefa);
    Task DeleteAsync(int id);
    }
}