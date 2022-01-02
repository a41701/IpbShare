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
   public class ReservaViewModel: BindableBase
    {
            public ReservaViewModel()
            {
                Reserva = new Reserva();
                Reservas = new ObservableCollection<Reserva>();
                ReservaService = new ReservaService(new UnitOfWork());

                Loading = true;
            }

            public ObservableCollection<Reserva> Reservas { get; set; }

            public ReservaService ReservaService { get; set; }

            private string _nomeequipamento;

            //Equipamento
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

            //Utilizador
            private string _emailu;

            public string EmailU
             {
                get { return _emailu; }
                set
                {
                    Set(ref _emailu, value);
                    OnPropertyChanged(nameof(Valid));
                    OnPropertyChanged(nameof(Invalid));
                }
            }

            private int _idreserva;

            public int IdReserva
            {
                get { return _idreserva; }
                set
                {
                    Set(ref _idreserva, value);
                    OnPropertyChanged(nameof(Valid));
                    OnPropertyChanged(nameof(Invalid));
                }
            }

        public bool Valid
            {
                get
                {
                    string IdReserva = Convert.ToString(_idreserva); //converter o id para string para poder utiliza-lo no return
                    return !string.IsNullOrWhiteSpace(NomeEquipamento)
                        && !string.IsNullOrWhiteSpace(EmailU)
                        && !string.IsNullOrWhiteSpace(IdReserva);
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

            private Reserva _reserva;

            public Reserva Reserva
            {
                get { return _reserva; }
                set
                {
                    Reserva = value;
                    IdReserva = _reserva.Id; 
                    NomeEquipamento = _reserva.Equipamento?.NomeEquipamento;
                    EmailU= _reserva.Utilizador?.Email;
                }
            }

            internal async Task<Reserva> AddReservaAsync()
            {
                Reserva.Id = IdReserva;

            return await ReservaService.AddReservaAsync(EmailU, NomeEquipamento, Reserva);
            }


            internal async void DeleteReservaAsync(Reserva r)
            {
                using (var uow = new UnitOfWork())
                {
                    uow.ReservaRepository.Delete(r);
                    Reservas.Remove(r);
                    await uow.SaveAsync();
                }
            }

            internal async Task<ObservableCollection<Equipamento>> LoadEquipamentosByNameStartWithAsync(string text)
            {
                ObservableCollection<Equipamento> res;
                using (var uow = new UnitOfWork())
                {
                    var list = await uow.EquipamentoRepository.FindAllByNameStartWithAsync(text);
                    res = new ObservableCollection<Equipamento>(list);
                }
                return res;
            }

            internal async Task<ObservableCollection<Utilizador>> LoadUtilizadoresByNameStartWithAsync(string text)
            {
                ObservableCollection<Utilizador> res;
                using (var uow = new UnitOfWork())
                {
                    var list = await uow.UtilizadorRepository.FindAllByNameStartWithAsync(text);
                    res = new ObservableCollection<Utilizador>(list);
                }
                return res;
            }

            internal async Task<ObservableCollection<Reserva>> FindProductsTakeByUserAsync(int UserId) {
            ObservableCollection<Reserva> res;
            using (var uow = new UnitOfWork())
            {
                var list = await uow.ReservaRepository.FindProductsTakeByUserAsync(UserId);
                res = new ObservableCollection<Reserva>(list);
            }
                return res;
             }

        internal async Task<ObservableCollection<Reserva>> ReservasOrdenadasporDataReservaAsync(DateTime datareserva)
        {
            ObservableCollection<Reserva> res;
            using (var uow = new UnitOfWork())
            {
                var list = await uow.ReservaRepository.HistoricAsync(datareserva);
                res = new ObservableCollection<Reserva>(list);
            }
            return res;
        }

        internal async Task<ObservableCollection<Reserva>> ReservasUtilizadorAsync(int UserId)
        {
            ObservableCollection<Reserva> res;
            using (var uow = new UnitOfWork())
            {
                var list = await uow.ReservaRepository.UserHistoricAsync(UserId);
                res = new ObservableCollection<Reserva>(list);
            }
            return res;
        }

        /*internal Reserva LastProductRequisitedByUser(int UserId) {
            using (var uow = new UnitOfWork()) {
               Reserva lastproduct = uow.ReservaRepository.FindLastProductRequisitedByUser(UserId);
               return lastproduct;
            }
        } */ 
        public async Task LoadAllAsync()
            {
                using (var uow = new UnitOfWork())
                {
                    //Loading = true; // carrega os dados 
                    var list = await uow.ReservaRepository.FindAllAsync();

                    Reservas.Clear();
                    {
                        foreach (var l in list)
                            Reservas.Add(l); //adiciona o produto à lista
                    }

                    Loading = false;
                }
            }

        }
    }
