using System;

namespace CadastroDeSeries
{
    public class Serie : EntidadeBase
    {
        public Genero Genero { get; set; }
        public string Titulo { get; set; }
        public int Total_Ep { get; set; }
        public int Atual_Ep { get; set; }
        public string Descricao { get; set; }
        public int Ano { get; set; }

        private bool Excluido { get; set; }

        public Serie(Genero genero, string titulo, int total_ep, int atual_ep, int ano)
        {
            this.Genero = genero;
            this.Titulo = titulo;
            this.Total_Ep = total_ep;
            this.Atual_Ep = atual_ep;
            this.Ano = ano;
        }
    }
}