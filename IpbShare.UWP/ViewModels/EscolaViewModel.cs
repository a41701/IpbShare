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
    public class EscolaViewModel : BindableBase
    {
        public ObservableCollection<Escola> Escolas { get; set; } 

        private Escola _escola;
        public Escola Escola
        {
            get { return _escola; }
            set
            {
                _escola = value;
                NomeEscola = _escola?.NomeEscola;
            }
        }

        private string _nomeescola;
        public string NomeEscola
        {
            get { return _nomeescola; }
            set
            {
                Set(ref _nomeescola, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }

        public EscolaViewModel()
        {
            Escola = new Escola();
            Escolas = new ObservableCollection<Escola>();
        }


        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrWhiteSpace(NomeEscola); //é válido quando quando a Escola não é nula
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }


        internal async Task<Escola> UpsertAsync()
        {
            Escola res = null;
            using (var uow = new UnitOfWork())
            {
                Escola.NomeEscola = NomeEscola;
                res = await uow.EscolaRepository.UpsertAsync(Escola);
                await uow.SaveAsync();
            }
            return res;
        }

        internal async void DeleteAsync(Escola e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.EscolaRepository.Delete(e);
                Escolas.Remove(e);
                await uow.SaveAsync();
            }
        }

        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.EscolaRepository.FindAllAsync();

                Escolas.Clear(); //não criar elementos repetidos ao voltar a carregar a pagina
                foreach (var item in list)
                {
                    Escolas.Add(item);
                }
            }
        }
    }
}
