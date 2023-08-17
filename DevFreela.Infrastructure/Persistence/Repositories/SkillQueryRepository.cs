using Dapper;
using DevFreela.Core.Entities;
using DevFreela.Core.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DevFreela.Infrastructure.Persistence.Repositories
{
    public class SkillQueryRepository : ISkillQueryRepository
    {

        private readonly string _connectionString;
        public SkillQueryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DevFreelaCs") ?? throw new ArgumentNullException();
        }

        public async Task<List<Skill>> GetAllAsync()
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                var script = "SELECT id as Id, description as Description FROM Skills";
                var skills = await sqlConnection.QueryAsync<Skill>(script);
                return skills.ToList();
            }
        }
        
    }
}
