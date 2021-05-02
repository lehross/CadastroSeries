using System.Collections.Generic;

namespace CadastroDeSeries.Interfaces
{
    public interface IRepositorio<T>
    {
        void Lista();
        void RetornaPorTitulo(string titulo);
        void Insere(T entidade);
        void Exclui(string titulo);
        void Atualiza(string titulo, T entidade);
    }
}