using System;
using System.Collections.Generic;
using CadastroDeSeries.Interfaces;
using MySqlConnector;
using CadastroDeSeries;

namespace CadastroDeSeries
{
    public class SerieRepositorio : IRepositorio<Serie>
    {
        public void Atualiza(string titulo, Serie entidade)
        {
            using (var conn = new MySqlConnection(Connection().ConnectionString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = $"UPDATE `series` SET `gender` = {(int) entidade.Genero}, `name` = '{entidade.Titulo}', `total_ep` = {entidade.Total_Ep}, `current_ep` = {entidade.Atual_Ep}, `year` = {entidade.Ano} WHERE (`name` = '{titulo}')";

                    command.ExecuteNonQuery();
                }
            }

            Console.WriteLine($"Série {titulo} atualizada!");
        }

        public void Exclui(string titulo)
        {
            using (var conn = new MySqlConnection(Connection().ConnectionString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = $"DELETE FROM series WHERE (`name` = '{titulo}')";

                    command.ExecuteNonQuery();
                    Console.WriteLine("Série excluída!");
                }
            }
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

        public void RetornaPorTitulo(string titulo)
        {
            using (var conn = new MySqlConnection(Connection().ConnectionString))
            {
                conn.Open();

                using (var command = conn.CreateCommand())
                {
                    command.CommandText = $"SELECT * FROM series WHERE (`name` = '{titulo}')";

                    using (var reader = command.ExecuteReader())
                    {
                        //exibir somente se existirem séries já cadastradas
                        if (reader.Read())
                        {
                            Console.WriteLine($"#ID {reader["id"]}: {reader["name"]} - {((int) reader["total_ep"] == (int) reader["current_ep"] ? "Em dia" : $"Atrasada - Episódios faltando: {(int) reader["total_ep"] - (int) reader["current_ep"]}")}");   
                        }

                        else
                            Console.WriteLine("Série não cadastrada!");
                        
                    }
                }
            }
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