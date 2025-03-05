

using System.Data.SqlClient;
using Dapper;
using WebApiDapper.Data;

namespace WebApiDapper.Controllers.Repository
{
    public class TarefaRepositoryProcedure : ITarefaRepositoryProcedure
    {
        private readonly string _connectionString;

        public TarefaRepositoryProcedure(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Tarefa>> GetAllAsync()
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryAsync<Tarefa>("GetAllTarefa", commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task<Tarefa> GetByIdAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                return await conn.QueryFirstAsync<Tarefa>("GetTarefaById", new { Id = id }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task AddAsync(Tarefa tarefa)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                await conn.ExecuteAsync("AddTarefa", new { tarefa.Nome, tarefa.Concluida }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }
        public async Task UpdateAsync(Tarefa tarefa)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                await conn.ExecuteAsync("UpdateTarefa", new { tarefa.Id, tarefa.Nome, tarefa.Concluida }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                await conn.ExecuteAsync("DeleteTarefa", new { Id = id }, commandType: System.Data.CommandType.StoredProcedure);
            }
        }


    }
}