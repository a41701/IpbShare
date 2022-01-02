using IpbShare.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace IpbShare.Domain.Models
{
    public class Utilizador : Entity
    {
        public string Email { get; set; }
        public string Nome { get; set; }
        public bool IsAdministrator { get; set; }

        private string _password;

        public string Password
        {
            get { return _password; }
            set
            {
                var data = Encoding.UTF8.GetBytes(value);
                var hashData = new SHA1Managed().ComputeHash(data);

                _password = BitConverter.ToString(hashData).Replace("-", "").ToUpper();
            }
        }

        //FK
        public int PaisId { get; set; }
        public Pais Pais { get; set; }

        //FK
        public int CursoId { get; set; }
        public Curso Curso { get; set; }


        public List<Reserva> ReservaList { get; set; }

        public Utilizador()
        {

        }
        public Utilizador(string email)
        {
            Email = email;
        }

        public Utilizador(int id)
        {
            Id = id;
        }
    }
}
