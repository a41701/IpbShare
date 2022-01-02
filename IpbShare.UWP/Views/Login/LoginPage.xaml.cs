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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IpbShare.UWP.Views.Login
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        //TODO implementar login
        private async void BtnLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var dialog = new MessageDialog("Login");
            await dialog.ShowAsync();

            //navegar para pagina dos equipamentos
        }

        private void LoginRegister_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegistoPage));
        }
    }
}
