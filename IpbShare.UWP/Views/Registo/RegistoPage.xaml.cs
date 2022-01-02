using IpbShare.UWP.Views.Login;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IpbShare.UWP.Views.Registo
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistoPage : Page
    {
        public RegistoPage()
        {
            this.InitializeComponent();
        }

        //TODO fazer registo
        private async void BtnRegisto_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dialog = new MessageDialog("Registo");
            await dialog.ShowAsync();

            //navegar para pagina de equipamentos
        }

        private void RegisterLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }
    }
}
