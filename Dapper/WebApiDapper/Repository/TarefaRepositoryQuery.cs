
using System.Data.SqlClient;
using Dapper;
using WebApiDapper.Data;

namespace WebApiDapper.Controllers.Repository
{
    public class TarefaRepository 
    {
        private readonly string _connectionString;

        public TarefaRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<Tarefa>("SELECT * FROM Tarefas");
            }
        }

        public async Task<Tarefa> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstOrDefaultAsync<Tarefa>("SELECT * FROM Tarefas WHERE Id = @Id", new { Id = id });
            }
        }

        public async Task AddAsync(Tarefa tarefa)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "INSERT INTO Tarefas (Nome, Concluida) VALUES (@Nome, @Concluida)";
                await conn.ExecuteAsync(sql, tarefa);
            }
        }

        public async Task UpdateAsync(Tarefa tarefa)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "UPDATE Tarefas SET Nome = @Nome, Concluida = @Concluida WHERE Id = @Id";
                await conn.ExecuteAsync(sql, tarefa);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "DELETE FROM Tarefas WHERE Id = @Id";
                await conn.ExecuteAsync(sql, new { Id = id });
            }
        }
    }
}