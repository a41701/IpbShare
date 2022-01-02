using IpbShare.UWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// O modelo de item de Página em Branco está documentado em https://go.microsoft.com/fwlink/?LinkId=234238

namespace IpbShare.UWP.Views.Categoria
{
    /// <summary>
    /// Uma página vazia que pode ser usada isoladamente ou navegada dentro de um Quadro.
    /// </summary>
    public sealed partial class CategoriaFormPage : Page
    {
        public CategoriaViewModel CategoriaViewModel { get; set; }

        public CategoriaFormPage()
        {
            this.InitializeComponent();
            CategoriaViewModel = new CategoriaViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                CategoriaViewModel = e.Parameter as CategoriaViewModel;
            }

            base.OnNavigatedTo(e);
        }


        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (await CategoriaViewModel.UpsertAsync() != null)
            {
                Frame.GoBack();
            }
            else
            {
                FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
            }



        }

        //voltar para a pagina anterior, quando a categoria for cancelada
        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

    }
}
