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
    public class EquipamentoViewModel : BindableBase
    {
       
        public EquipamentoViewModel()
            {
                Equipamento = new Equipamento();
                Equipamentos = new ObservableCollection<Equipamento>();
                EquipamentoService = new EquipamentoService(new UnitOfWork());

                Loading = true;
            }

            public ObservableCollection<Equipamento> Equipamentos { get; set; }

            public EquipamentoService EquipamentoService { get; set; }

            //FK Categoria
            private string _nomecategoria;
            
            public string NomeCategoria
            {
                get { return _nomecategoria; }
                set
                {
                    Set(ref _nomecategoria, value);
                    OnPropertyChanged(nameof(Valid));
                    OnPropertyChanged(nameof(Invalid));
                }
            }

            //FK Escola
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

            private string _nomeequipamento;

            public string NomeEquipamento
            {
                get { return _nomeequipamento; }
                set
                {
                    Set(ref _nomeequipamento, value);
                    OnPropertyChanged(nameof(Valid));
                    OnPropertyChanged(nameof(Invalid));
                }
            }
            
            //Para inserir imagens 
            private byte[] _thumb;

            public byte[] Thumb
            {
                get { return _thumb; }
                set { Set(ref _thumb, value); }
            }
            public bool Valid
            {
                get
                {
                    return !string.IsNullOrWhiteSpace(NomeEquipamento)
                        && !string.IsNullOrWhiteSpace(NomeCategoria) 
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

            private Equipamento _equipamento;

            public Equipamento Equipamento
            {
                get { return _equipamento; }
                set
                {
                    Equipamento = value;
                    NomeCategoria = _equipamento.Categoria?.NomeCategoria; // Categoria? ->no caso da categoria ser null, nao devolve exceção
                    NomeEquipamento = _equipamento.NomeEquipamento;
                    NomeEscola = _equipamento.Escola?.NomeEscola;
                    Thumb = _equipamento?.Thumb;
                }
            }

            internal async Task<Equipamento> AddEquipamentoCategoriaAsync()
            {
                Equipamento.NomeEquipamento = NomeEquipamento;
                Equipamento.Thumb = Thumb;

                return await EquipamentoService.AddCategoriaAsync(NomeCategoria,Equipamento);
            }

            internal async Task<Equipamento> AddEquipemantoEscolaAsync()
            {
                Equipamento.NomeEquipamento = NomeEquipamento;
                Equipamento.Thumb = Thumb;

                return await EquipamentoService.AddCategoriaAsync(NomeEscola, Equipamento);
            }

            internal async void DeleteEquipamentotAsync(Equipamento eq)
                {
                    using (var uow = new UnitOfWork())
                    {
                        uow.EquipamentoRepository.Delete(eq);
                        Equipamentos.Remove(eq);
                        await uow.SaveAsync();
                    }
                }

            internal async Task<ObservableCollection<Categoria>> LoadCategoriasByNameStartWithAsync(string text)
            {
                ObservableCollection<Categoria> res;
                using (var uow = new UnitOfWork())
                {
                    var list = await uow.CategoriaRepository.FindAllByNameStartWithAsync(text);
                    res = new ObservableCollection<Categoria>(list);
                }
                return res;
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
                        var list = await uow.EquipamentoRepository.FindAllAsync();

                        Equipamentos.Clear();
                        {
                            foreach (var l in list)
                                Equipamentos.Add(l); //adiciona o produto à lista
                        }

                        Loading = false;
                    }
                }

            }
        }
