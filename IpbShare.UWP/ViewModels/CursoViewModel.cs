using IpbShare.Domain.Models;
using IpbShare.Domain.Services;
using IpbShare.Infrastructure;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IpbShare.UWP.ViewModels
{
    public class CursoViewModel : BindableBase
    {
        public CursoViewModel()
        {
            Curso = new Curso();
            Cursos = new ObservableCollection<Curso>();
            CursoService = new CursoService(new UnitOfWork());

            Loading = true;
        }

        public ObservableCollection<Curso> Cursos { get; set; }

        public CursoService CursoService { get; set; }

        private string _nomecurso;

        public string NomeCurso
        {
            get { return _nomecurso; }
            set
            {
                Set(ref _nomecurso, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }

        //FK escola
        private string _nomeescola;

        public string NomeEscola
        {
            get { return _nomeescola; }
            set
            {
                Set(ref _nomeescola, value);
                OnPropertyChanged(nameof(Valid));
                OnPropertyChanged(nameof(Invalid));
            }
        }
       
        public bool Valid
        {
            get
            {
                return !string.IsNullOrWhiteSpace(NomeCurso)
                    && !string.IsNullOrWhiteSpace(NomeEscola);
            }
        }

        public bool Invalid
        {
            get
            {
                return !Valid;
            }
        }

        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set { Set(ref _loading, value); }
        }

        private Curso _curso;

        public Curso Curso
        {
            get { return _curso; }
            set
            {
                Curso = value;
                NomeEscola = _curso.Escola?.NomeEscola;
                NomeCurso = _curso.NomeCurso;
            }
        }

        internal async Task<Curso> AddCursoEscolaAsync()
        {
            Curso.NomeCurso = NomeCurso;
           
            return await CursoService.AddEscolaAsync(NomeEscola, Curso);
        }

        internal async void DeleteCursoAsync(Curso c)
        {
            using (var uow = new UnitOfWork())
            {
                uow.CursoRepository.Delete(c);
                Cursos.Remove(c);
                await uow.SaveAsync();
            }
        }

        internal async Task<ObservableCollection<Escola>> LoadEscolasByNameStartWithAsync(string text)
        {
            ObservableCollection<Escola> res;
            using (var uow = new UnitOfWork())
            {
                var list = await uow.EscolaRepository.FindAllByNameStartWithAsync(text);
                res = new ObservableCollection<Escola>(list);
            }
            return res;
        }

        public async Task LoadAllAsync()
        {
            using (var uow = new UnitOfWork())
            {
                //Loading = true; // carrega os dados 
                var list = await uow.CursoRepository.FindAllAsync();

                Cursos.Clear();
                {
                    foreach (var l in list)
                        Cursos.Add(l); //adiciona o produto à lista
                }

                Loading = false;
            }
        }

    }
}
