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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace IpbShare.UWP.Views.Categoria
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageCategoriaPage : Page
    {
        public CategoriaViewModel CategoriaViewModel { get; set; }

        public ManageCategoriaPage()
        {
            InitializeComponent();
            CategoriaViewModel = new CategoriaViewModel();
        }

        private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        //Ação ao clicar no botão "Adicionar"
        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(CategoriaFormPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            CategoriaViewModel.LoadAllAsync();
            base.OnNavigatedTo(e);
        }

        //Ação ao clicar no botão "Deletar/Apagar"
        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteDialog = new ContentDialog
            {
                Title = "Delete Category Permanently?",
                Content = "If you delete this item, you can´t recovery it. Are you sure?",
                PrimaryButtonText = "Delete",
                CloseButtonText = "Cancel"
            };

            ContentDialogResult result = await deleteDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                if (sender is FrameworkElement fe && fe.DataContext is IpbShare.Domain.Models.Categoria c)
                {
                    if (c.Equipamentos.Any())
                    {
                        FlyoutBase.ShowAttachedFlyout(fe);
                    }
                    else
                    {
                        CategoriaViewModel.DeleteAsync(c);
                    }
                }
            }
        }

        //Ação ao clicar no botão "Editar"
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement fe && fe.DataContext is IpbShare.Domain.Models.Categoria c)
            {
                CategoriaViewModel.Categoria = c;
                Frame.Navigate(typeof(CategoriaFormPage), CategoriaViewModel);
            }
        }
    }
}
