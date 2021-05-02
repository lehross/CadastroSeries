using System;
using System.Collections.Generic;
using CadastroDeSeries.Interfaces;
using MySqlConnector;
using CadastroDeSeries;

namespace CadastroDeSeries
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        private List<Serie> listaSerie = new List<Serie>(); //conterá todas as séries

        public void Atualiza(int id, Serie entidade)
        {
            listaSerie[id] = entidade;
        }

        public void Exclui(int id)
        {
            listaSerie[id].Excluir();
        }

        public void Insere(Serie entidade)
        {
            using (var conn = new MySqlConnection(Connection().ConnectionString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO `series` (`gender`, `name`, `total_ep`, `current_ep`, `year`) VALUES ({(int) entidade.Genero}, '{entidade.Titulo}', {entidade.Total_Ep}, {entidade.Atual_Ep}, {entidade.Ano})";

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Lista()
        {
            using (var conn = new MySqlConnection(Connection().ConnectionString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = "SELECT * FROM series";

                    using (var reader = command.ExecuteReader())
                    {
                        //exibir somente se existirem séries já cadastradas
                        if (reader.Read())
                        {
                            do
                            {
                                Console.WriteLine($"#ID {reader["id"]}: {reader["name"]} - {((int) reader["total_ep"] == (int) reader["current_ep"] ? "Em dia" : "Atrasada")}");
                            } while (reader.Read());
                        }

                        else
                            Console.WriteLine("Nenhuma série cadastrada!");
                        
                    }
                }
            }
        }

        public int ProximoId()
        {
            return listaSerie.Count;
        }

        public Serie RetornaPorId(int id)
        {
            return listaSerie[id];
        }

        public MySqlConnectionStringBuilder Connection()
        {
            return new MySqlConnectionStringBuilder
            {
                Server = "localhost",
                Database = "shows_movies-controll",
                UserID = "root",
                Password = "123456"
            };
        }
    }
}