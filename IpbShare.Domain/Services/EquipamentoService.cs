using IpbShare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Services
{
    public class EquipamentoService
    {

        private IUnitOfWork _unitOfWork { get; set; }

        public EquipamentoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;   
        }

        public async Task<Equipamento> AddCategoriaAsync(string NomeCategoria, Equipamento Equipamento)
        {
            var categoria  = new Categoria(NomeCategoria);
            var categoriaUpdated = await _unitOfWork.CategoriaRepository.FindOrCreatAsync(categoria);
            await _unitOfWork.SaveAsync();

            Equipamento.Categoria = categoriaUpdated;
            var equipamentoUpdated = await _unitOfWork.EquipamentoRepository.UpsertAsync(Equipamento);
            await _unitOfWork.SaveAsync();

            return equipamentoUpdated;
        }

        public async Task<Equipamento> AddEscolaAsync(string NomeEscola, Equipamento Equipamento)
        {
            var escola = new Escola(NomeEscola);
            var escolaUpdated = await _unitOfWork.EscolaRepository.FindOrCreatAsync(escola);
            await _unitOfWork.SaveAsync();

            Equipamento.Escola =  escolaUpdated;
            var equipamentoUpdated = await _unitOfWork.EquipamentoRepository.UpsertAsync(Equipamento);
            await _unitOfWork.SaveAsync();

            return equipamentoUpdated;
        }

    }
}
