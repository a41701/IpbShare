using IpbShare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Services
{
    public class ReservaService
    {
        private IUnitOfWork _unitOfWork { get; set; }
        public ReservaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Reserva> AddReservaAsync(string emailU, string NomeEquipamento, Reserva Reserva)
        {
            var utilizador = new Utilizador(emailU);
            var utilizadorUpdated = await _unitOfWork.UtilizadorRepository.FindOrCreatAsync(utilizador);
            await _unitOfWork.SaveAsync();

            var equipamento = new Equipamento(NomeEquipamento);
            var equipamentoUpdated = await _unitOfWork.EquipamentoRepository.FindOrCreatAsync(equipamento);
            await _unitOfWork.SaveAsync();

            Reserva.Utilizador = utilizadorUpdated;
            Reserva.Equipamento = equipamentoUpdated;
            var reservaUpdated = await _unitOfWork.ReservaRepository.UpsertAsync(Reserva);
            await _unitOfWork.SaveAsync();

            return reservaUpdated;
        }
    }
}
