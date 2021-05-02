using System;
using MySqlConnector;

namespace CadastroDeSeries
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario =  ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;
                    default:
                        Console.WriteLine("Opção inválida");
                        break;
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Listar séries");

            repositorio.Lista();
        }

        private static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            var dados = InformarDadosSerie();

			Serie novaSerie = new Serie(id: repositorio.ProximoId(),
										genero: dados.Item1,
										titulo: dados.Item2,
                                        total_ep: dados.Item3,
                                        atual_ep: dados.Item4,
										ano: dados.Item5,
										descricao: dados.Item6);

			repositorio.Insere(novaSerie);
        }

        private static void AtualizarSerie()
        {
            Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

            //checar se a série está cadastrada
            // if (indiceSerie > repositorio.Lista().Count)
            // {
            //     Console.WriteLine("Série não cadastrada");
            //     return ;
            // }

            var dados = InformarDadosSerie();

            Serie atualizaSerie = new Serie(id: indiceSerie,
										genero: dados.Item1,
										titulo: dados.Item2,
                                        total_ep: dados.Item3,
                                        atual_ep: dados.Item4,
										ano: dados.Item5,
										descricao: dados.Item6);

			repositorio.Atualiza(indiceSerie, atualizaSerie);
        }

        private static void ExcluirSerie()
        {
            Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());

            //checar se a série está cadastrada
            // if (indiceSerie > repositorio.Lista().Count)
            // {
            //     Console.WriteLine("Série não cadastrada");
            //     return ;
            // }

			repositorio.Exclui(indiceSerie);
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o id da série: ");
			int indiceSerie = int.Parse(Console.ReadLine());
            Console.WriteLine();

            //checar se a série está cadastrada
            // if (indiceSerie > repositorio.Lista().Count)
            // {
            //     Console.WriteLine("Série não cadastrada");
            //     return ;
            // }

			var serie = repositorio.RetornaPorId(indiceSerie);

			Console.WriteLine(serie);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Time4TV a seu dispor!");
            Console.WriteLine("Informe a opção desejada:");

            Console.WriteLine("1 - Listar séries");
            Console.WriteLine("2 - Inserir nova série");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Vizualizar série");
            Console.WriteLine("X - Sair");
            Console.WriteLine();
        
            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            //Console.Clear();
            return opcaoUsuario;
        }

        private static (Genero, string, int, int, int, string) InformarDadosSerie()
        {
            //listar id dos gêneros
			foreach (int i in Enum.GetValues(typeof(Genero)))
				Console.WriteLine($"{i}-{Enum.GetName(typeof(Genero), i)}");
			
            Console.WriteLine();

			Console.Write("Digite o gênero entre as opções acima: ");
			int entradaGenero = int.Parse(Console.ReadLine());

			Console.Write("Digite o Título da Série: ");
			string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o número toal de episódios: ");
			int entradaTotalEp = int.Parse(Console.ReadLine());

            Console.Write("Digite quantos episódios foram assistidos: ");
			int entradaAtualEp = int.Parse(Console.ReadLine());

			Console.Write("Digite o Ano de Início da Série: ");
			int entradaAno = int.Parse(Console.ReadLine());

			Console.Write("Digite a Descrição da Série: ");
			string entradaDescricao = Console.ReadLine();

            return ((Genero)entradaGenero, entradaTitulo, entradaTotalEp, entradaAtualEp, entradaAno, entradaDescricao);
        }
    }
}
