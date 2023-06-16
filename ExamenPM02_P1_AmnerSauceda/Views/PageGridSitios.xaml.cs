using ExamenPM02_P1_AmnerSauceda.Models;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using Xamarin.Essentials;



namespace ExamenPM02_P1_AmnerSauceda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageGridSitios : ContentPage
    {
        public PageGridSitios()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            listaSitios.ItemsSource = await App.Instancia.GetAllSitios();
        }

        private void listaSitios_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Sitio sitioSeleccionado)
            {
                double latitud = sitioSeleccionado.Latitud;
                double longitud = sitioSeleccionado.Longitud;

                // Utiliza las variables latitud y longitud como desees, por ejemplo, mostrarlas en etiquetas o realizar acciones adicionales.
                Console.WriteLine(latitud + " = " + longitud);
            }

        }

        private async void LoadSitios()
        {
            // Obtiene los sitios de la base de datos SQLite y los asigna
            var sitios = await App.Instancia.GetAllSitios(); // Implementa el método GetAllSitios() para obtener los sitios de la base de datos
            listaSitios.ItemsSource = sitios;
        }


        private async void Button_eliminarCasa(object sender, EventArgs e)
        {
            // Verificar si hay un elemento seleccionado
            if (listaSitios.SelectedItem is Sitio sitioSeleccionado)
            {
                await App.Instancia.DeleteSitio(sitioSeleccionado);
                await DisplayAlert("Aviso", "Se ha elimanado el sitio", "OK");
                LoadSitios();
            }
            else
            {
                await DisplayAlert("Aviso", "Seleccione el sitio a eliminar", "OK");
            }
        }

        
        private async void Button_verMapa(object sender, EventArgs e)
        {
            if (listaSitios.SelectedItem is Sitio sitioSeleccionado)
            {
                await Navigation.PushAsync(new PageMapSitios(sitioSeleccionado.Latitud, sitioSeleccionado.Longitud, sitioSeleccionado.Descripcion));
                PageMapSitios mapSitios = new PageMapSitios(sitioSeleccionado.Latitud, sitioSeleccionado.Longitud, sitioSeleccionado.Descripcion);
                mapSitios.PasarFoto(sitioSeleccionado.foto);
            }
            else
            {
                await DisplayAlert("Aviso", "Seleccione el sitio para mirar en el mapa", "OK");
            }

        }
    }
}