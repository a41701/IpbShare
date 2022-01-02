using IpbShare.UWP.Views.Categoria;
using IpbShare.UWP.Views.Equipamento;
using IpbShare.UWP.Views.Login;
using IpbShare.UWP.Views.Registo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace IpbShare.UWP
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        public Frame AppFrame => frame; //alteração de páginas

        private void NvMain_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var selectedItem = args.InvokedItemContainer as NavigationViewItem;
            if (selectedItem != null)
            {
                switch(selectedItem.Tag) //analisa as tags dos items criados no navigation
                {
                    case "Equipamentos":
                        AppFrame.Navigate(typeof(ManageEquipamentoPage));
                        break;
                    case "Categorias":
                        AppFrame.Navigate(typeof(ManageCategoriaPage));
                        break;
                    
                        
                }
            }
        }

        private void NvMain_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (AppFrame.CanGoBack)
            {
                AppFrame.GoBack();
            }
        }

        private void NavLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AppFrame.Navigate(typeof(LoginPage));
        }

        private async void NavLogout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dialog = new MessageDialog("Logout");
            await dialog.ShowAsync();
        }

        private void NavRegister_Tapped(object sender, TappedRoutedEventArgs e)
        {
            AppFrame.Navigate(typeof(RegistoPage));
        }
    }
}
