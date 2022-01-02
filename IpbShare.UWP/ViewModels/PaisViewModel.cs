using IpbShare.Domain.Models;
using IpbShare.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.UWP.ViewModels
{
    public class PaisViewModel : BindableBase
    {
        public ObservableCollection<Pais> Paises { get; set; } //Lista observável (tunada)

        private Pais _pais;
        public Pais Pais
        {
            get { return _pais; }
            set
            {
                _pais = value;
                NomePais = _pais?.NomePais; 
            }
        }

        private string _nomepais;
        public string NomePais
        {
            get { return _nomepais; }
            set
            {
                Set(ref _nomepais, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }

        public PaisViewModel()
        {
            Pais = new Pais();
            Paises = new ObservableCollection<Pais>(); //obtém a coleção dos paises
        }


        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrWhiteSpace(NomePais); //é válido quando quando o Pais não é nulo
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }


        internal async Task<Pais> UpsertAsync()
        {
            Pais res = null;
            using (var uow = new UnitOfWork())
            {
                Pais.NomePais = NomePais;
                res = await uow.PaisRepository.UpsertAsync(Pais);
                await uow.SaveAsync();
            }
            return res;
        }

        internal async void DeleteAsync(Pais p)
        {
            using (var uow = new UnitOfWork())
            {
                uow.PaisRepository.Delete(p);
                Paises.Remove(p);
                await uow.SaveAsync();
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.PaisRepository.FindAllAsync();

                Paises.Clear(); //não criar elementos repetidos ao voltar a carregar a pagina
                foreach (var item in list)
                {
                    Paises.Add(item);
                }
            }
        }
    }
}
