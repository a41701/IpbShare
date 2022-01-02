using IpbShare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Services
{
    public class CursoService
    {
        private IUnitOfWork _unitOfWork { get; set; }

        public CursoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Curso> AddEscolaAsync(string NomeEscola, Curso Curso)
        {
            var escola = new Escola(NomeEscola);
            var escolaUpdated = await _unitOfWork.EscolaRepository.FindOrCreatAsync(escola);
            await _unitOfWork.SaveAsync();

            Curso.Escola = escolaUpdated;
            var cursoUpdated = await _unitOfWork.CursoRepository.UpsertAsync(Curso);
            await _unitOfWork.SaveAsync();

            return cursoUpdated;
        }
    }
}
