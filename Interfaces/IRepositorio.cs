using System.Collections.Generic;

namespace CadastroDeSeries.Interfaces
{
    public interface IRepositorio<T>
    {
        void Lista();
        T RetornaPorId(int id);
        void Insere(T entidade);
        void Exclui(int id);
        void Atualiza(int id, T entidade);
        int ProximoId();
    }
}