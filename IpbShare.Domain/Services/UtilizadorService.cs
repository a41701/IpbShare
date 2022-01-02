using IpbShare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Services
{
    public class UtilizadorService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public UtilizadorService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //TODO : posteriormente fazer igual ao método do Reserva Service
        public async Task<Utilizador> AddCursoAsync(string NomeCurso, Utilizador Utilizador)
        {
            var curso = new Curso(NomeCurso);
            var cursoUpdated = await _unitOfWork.CursoRepository.FindOrCreatAsync(curso);
            await _unitOfWork.SaveAsync();

            Utilizador.Curso = cursoUpdated;
            var utilizadorUpdated = await _unitOfWork.UtilizadorRepository.UpsertAsync(Utilizador);
            await _unitOfWork.SaveAsync();

            return utilizadorUpdated;
        }
       
        public async Task<Utilizador> AddPaisAsync(string NomePais, Utilizador Utilizador)
        {
            var pais = new Pais(NomePais);
            var paisUpdated = await _unitOfWork.PaisRepository.FindOrCreatAsync(pais);
            await _unitOfWork.SaveAsync();

            Utilizador.Pais = paisUpdated;
            var utilizadorUpdated = await _unitOfWork.UtilizadorRepository.UpsertAsync(Utilizador);
            await _unitOfWork.SaveAsync();

            return utilizadorUpdated;
        }
    }
}
