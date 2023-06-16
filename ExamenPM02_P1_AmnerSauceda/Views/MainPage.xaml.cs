using SQLite;
using System;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;
using ExamenPM02_P1_AmnerSauceda.Models;
using Plugin.Media;
using ExamenPM02_P1_AmnerSauceda.Views;
using Plugin.Geolocator;
using ExamenPM02_P1_AmnerSauceda.Controllers;

namespace ExamenPM02_P1_AmnerSauceda
{
    public partial class MainPage : ContentPage
    {
        Plugin.Media.Abstractions.MediaFile photo = null;

        private SQLiteAsyncConnection _database;

        public MainPage()
        {
            InitializeComponent();
            var locator = CrossGeolocator.Current;
            bool isGpsEnabled = locator.IsGeolocationEnabled;

            if (isGpsEnabled)
            {
                // El GPS está activo
                GetLocation();
                _database = new SQLiteAsyncConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "sitios.db3"));
                _database.CreateTableAsync<Sitio>().Wait();
            }
            else
            {
                // El GPS no está activo
                DisplayAlert("Error", "El GPS esta Inactivo por favor salga de la app y activelo", "OK");
  
            }
  
        }

        private async void GetLocation()
        {
            var location = await Geolocation.GetLastKnownLocationAsync();
            if (location != null)
            {
                latitudEntry.Text = location.Latitude.ToString();
                longitudEntry.Text = location.Longitude.ToString();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo obtener la ubicación", "OK");
            }
        }

        public String Getimage64()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    String Base64 = Convert.ToBase64String(fotobyte);

                    return Base64;
                }
            }

            return null;
        }

        public byte[] GetimageBytes()
        {
            if (photo != null)
            {
                using (MemoryStream memory = new MemoryStream())
                {
                    Stream stream = photo.GetStream();
                    stream.CopyTo(memory);
                    byte[] fotobyte = memory.ToArray();

                    return fotobyte;
                }

            }

            return null;
        }


        private async void btnfoto_Clicked(object sender, EventArgs e)
        {
            photo = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "MYAPP",
                Name = "Foto.jpg",
                SaveToAlbum = true
            });

            if (photo != null)
            {
                foto.Source = ImageSource.FromStream(() => { return photo.GetStream(); });
            }
        }

        private async void AgregarRegistro_Clicked(object sender, EventArgs e)
        {
            CamposValidator validator = new CamposValidator();
            bool camposValidos = validator.ValidarCampos(foto.Source, latitudEntry.Text, longitudEntry.Text, descripcionEntry.Text);

            if (camposValidos) {

                var sitio = new Models.Sitio
                {
                    Latitud = double.Parse(latitudEntry.Text),
                    Longitud = double.Parse(longitudEntry.Text),
                    Descripcion = descripcionEntry.Text,
                    foto = GetimageBytes()
                };

                if (await App.Instancia.AddSitio(sitio) > 0)
                {
                    await DisplayAlert("Aviso", "Se ha registrado correctamente", "OK");

                    //limpiar campos
                    foto.Source = null;
                    descripcionEntry.Text = string.Empty;
                }
                else
                    await DisplayAlert("Aviso", "a ocurrido un error", "OK");
            }
            else
            {
                await DisplayAlert("Error", "Por favor, complete todos los campos", "OK");
            }

        }


        private void SalirApp_Clicked(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }

       

        private async void ListaSitios_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PageGridSitios());
        }
    }
}
