using IpbShare.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoriaRepository CategoriaRepository { get; }
        ICursoRepository CursoRepository { get; }
        IEquipamentoRepository EquipamentoRepository { get; }
        IEscolaRepository EscolaRepository { get; }
        IPaisRepository PaisRepository { get; }
        IReservaRepository ReservaRepository { get; }
        IUtilizadorRepository UtilizadorRepository { get; }

        Task SaveAsync();
    }
}
