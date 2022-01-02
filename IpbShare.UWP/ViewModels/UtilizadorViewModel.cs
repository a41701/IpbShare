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
    public class UtilizadorViewModel : BindableBase
    {
            public UtilizadorViewModel()
            {
                Utilizador = new Utilizador();
                Utilizadores = new ObservableCollection<Utilizador>();
                UtilizadorService = new UtilizadorService(new UnitOfWork());

                Loading = true;
            }

            public ObservableCollection<Utilizador> Utilizadores { get; set; }

            public UtilizadorService UtilizadorService { get; set; }

            private string _nomeutilizador;

            public string NomeUtilizador
            {
                get { return _nomeutilizador; }
                set
                {
                    Set(ref _nomeutilizador, value);
                    OnPropertyChanged(nameof(Valid));
                    OnPropertyChanged(nameof(Invalid));
                }
            }

            //FK Curso
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
            
            //FK Pais
            private string _nomepais;

            public string NomePais
            {
                get { return _nomepais; }
                set
                {
                    Set(ref _nomepais, value);
                    OnPropertyChanged(nameof(Valid));
                    OnPropertyChanged(nameof(Invalid));
                }
            }

            public bool Valid
                {
                    get
                    {
                        return !string.IsNullOrWhiteSpace(NomeUtilizador)
                            && !string.IsNullOrWhiteSpace(NomePais)
                            && !string.IsNullOrWhiteSpace(NomeCurso);
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

            private Utilizador _utilizador;

            public Utilizador Utilizador
            {
                get { return _utilizador; }
                set
                {
                    Utilizador = value;
                    NomeCurso = _utilizador.Curso?.NomeCurso;
                    NomeUtilizador = _utilizador.Nome;
                    NomePais = _utilizador.Pais?.NomePais;
                }
            }

            internal async Task<Utilizador> AddUtilizadorCursoAsync()
            {
                Utilizador.Nome = NomeUtilizador;
                
                return await UtilizadorService.AddCursoAsync(NomeCurso, Utilizador);
            }

            internal async Task<Utilizador> AddUtilizadorPaisAsync()
            {
                Utilizador.Nome = NomeUtilizador;

                return await UtilizadorService.AddPaisAsync(NomePais, Utilizador);
            }

            internal async void DeleteUtilizadorAsync(Utilizador u)
            {
                using (var uow = new UnitOfWork())
                {
                    uow.UtilizadorRepository.Delete(u);
                    Utilizadores.Remove(u);
                    await uow.SaveAsync();
                }
            }

            internal async Task<ObservableCollection<Curso>> LoadCursosByNameStartWithAsync(string text)
            {
                ObservableCollection<Curso> res;
                using (var uow = new UnitOfWork())
                {
                    var list = await uow.CursoRepository.FindAllByNameStartWithAsync(text);
                    res = new ObservableCollection<Curso>(list);
                }
                return res;
            }

            internal async Task<ObservableCollection<Pais>> LoadPaisByNameStartWithAsync(string text)
            {
                ObservableCollection<Pais> res;
                using (var uow = new UnitOfWork())
                {
                    var list = await uow.PaisRepository.FindAllByNameStartWithAsync(text);
                    res = new ObservableCollection<Pais>(list);
                }
                return res;
            }

            public async Task LoadAllAsync()
                {
                    using (var uow = new UnitOfWork())
                    {
                        //Loading = true; // carrega os dados 
                        var list = await uow.UtilizadorRepository.FindAllAsync();

                        Utilizadores.Clear();
                        {
                            foreach (var l in list)
                                Utilizadores.Add(l); //adiciona o utilizador à lista
                        }

                        Loading = false;
                }
            }

        }
    }
