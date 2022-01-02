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
    public class CategoriaViewModel: BindableBase
    {
        public ObservableCollection<Categoria> Categorias { get; set; } //Lista observável (tunada)

        private Categoria _categoria;
        public Categoria Categoria
        {
            get { return _categoria; }
            set
            {
                _categoria = value;
                NomeCategoria = _categoria?.NomeCategoria; //verifica se a categoria existe, caso não exista não retorna exceção
            }
        }

        private string _nomecategoria;
        public string NomeCategoria
        {
            get { return _nomecategoria; }
            set
            {
                Set(ref _nomecategoria, value);
                OnPropertyChanged(nameof(Invalid));
                OnPropertyChanged(nameof(Valid));
            }
        }

        public CategoriaViewModel()
        {
            Categoria = new Categoria();
            Categorias = new ObservableCollection<Categoria>(); //obtém a coleção das categorias
        }

        //verifica se uma categoria é valida ou nao
        public bool Valid
        {
            get
            {
                bool res = !string.IsNullOrWhiteSpace(NomeCategoria); //é válido quando a Categoria não é nula
                return res;
            }
        }

        public bool Invalid
        {
            get { return !Valid; }
        }

        //Atualiza uma categoria
        internal async Task<Categoria> UpsertAsync()
        {
            Categoria res = null;
            using (var uow = new UnitOfWork())
            {
                Categoria.NomeCategoria = NomeCategoria;
                res = await uow.CategoriaRepository.UpsertAsync(Categoria);
                await uow.SaveAsync();
            }
            return res;
        }

        //Apaga uma categoria à colecao Categorias 
        internal async void DeleteAsync(Categoria e)
        {
            using (var uow = new UnitOfWork())
            {
                uow.CategoriaRepository.Delete(e);
                Categorias.Remove(e);
                await uow.SaveAsync();
            }
        }

        //Adiciona uma categoria à colecao Categorias 
        public async void LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                var list = await uow.CategoriaRepository.FindAllAsync();

                Categorias.Clear(); //não criar elementos repetidos ao voltar a carregar a pagina
                foreach (var item in list)
                {
                    Categorias.Add(item);
                }
            }
        }
    }
}
