using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace ApiCuistot.Models
{
    public class DatabaseContext : DbContext
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
        //permet d'aaficher toutes les conso
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
                            Name = reader.GetString("nom")
                        });
                    }
                }
            }
            return conso;
        }
        //permet d'afficher toutes les commandes
        public List<Commande> getOrder()
        {
            List<Commande> commande = new List<Commande>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("select * from Commande", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        commande.Add(new Commande()
                        {
                            Id = reader.GetInt32("id"),
                            Name = reader.GetString("nom"),
                        });
                    }
                }
            }
            return commande;
        }
        //pemret d'afficher toute les commandes ainsi que leur état
        public List<Contenir> getContenir()
        {
            List<Contenir> contenir = new List<Contenir>();
            using (MySqlConnection connection = GetConnection())
            {
                connection.Open();
                MySqlCommand cmd = new MySqlCommand("select * from contenir", connection);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        contenir.Add(new Contenir()
                        {
                            ContenirConso = reader.GetInt32("contenir_conso"),
                            ContenirCommande = reader.GetInt32("contenir_commande"),
                            Etat = reader.GetString("etat")
                        });
                    }
                }
            }
            return contenir;
        }
        //permet d'ajouter des conso dans la bdd
        public int insertConso(string nom)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Conso VALUES(0,@nom)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", nom);
                return cmd.ExecuteNonQuery();
            }
        }
        //permet de d'initialisé une commande et de la nommée
        public int startNewOrder(string nom)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO Commande VALUES(0,@nom)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@nom", nom);
                return cmd.ExecuteNonQuery();
            }
        }
        //permet de crée la commande avec le plat souhaité
        public int createOrder(int idcommande, int idconso, string etat)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "INSERT INTO contenir VALUES(@idcommande, @idconso, @etat)";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idcommande", idcommande);
                cmd.Parameters.AddWithValue("@idconso", idconso);
                cmd.Parameters.AddWithValue("@etat", etat);
                return cmd.ExecuteNonQuery();
            }
        }
        //permet de modifier l'etat de la commande et du plat
        public int updateCommandStatus(int idcommande, int idconso, string etat)
        {
            using (MySqlConnection conn = GetConnection())
            {
                conn.Open();
                string query = "UPDATE contenir SET etat = @etat WHERE contenir_commande = @idcommande and contenir_conso = @idconso;";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@idcommande", idcommande);
                cmd.Parameters.AddWithValue("@idconso", idconso);
                cmd.Parameters.AddWithValue("@etat", etat);
                return cmd.ExecuteNonQuery();
            }
        }
    }
}