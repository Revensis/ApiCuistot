using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ApiCuistot.Models
{
    public class DatabaseContext: DbContext
    {
        private readonly string _connectionString;
        public DatabaseContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        private MySqlConnection GetConnection()
        {
            Console.WriteLine(_connectionString);
            return new MySqlConnection(_connectionString);
        }
        public List<Conso> getConso()
        {
            List<Conso> conso = new List<Conso>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("select * from CONSO", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        conso.Add(new Conso()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("nom"),
                        });
                    }
                }
            }
            return conso;
        }
    }
}
