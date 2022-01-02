using IpbShare.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.Domain.Services
{
    public class EscolaService
    {

        private IUnitOfWork _unitOfWork { get; set; }

        public EscolaService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        


    }
}

