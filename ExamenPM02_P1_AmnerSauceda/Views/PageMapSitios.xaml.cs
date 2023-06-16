using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using ImageFromXamarinUI;
using ExamenPM02_P1_AmnerSauceda.Models;
using Plugin.Geolocator;

namespace ExamenPM02_P1_AmnerSauceda.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageMapSitios : ContentPage
    {
        Sitio sitio = new Sitio();
        public PageMapSitios(double latitud, double longitud, string descripcion)
        {
            InitializeComponent();

            var locator = CrossGeolocator.Current;
            bool isGpsEnabled = locator.IsGeolocationEnabled;

            if (isGpsEnabled)
            {
                // El GPS está activo
                DisplayAlert("Aviso", "El GPS esta Activado", "OK");
                var pin = new Pin
                {
                    Position = new Position(latitud, longitud),
                    Label = "Pin: " + descripcion
                };

                mapaSitio.Pins.Add(pin);
                mapaSitio.MoveToRegion(MapSpan.FromCenterAndRadius(pin.Position, Distance.FromKilometers(1)));

            }
            else
            {
                // El GPS no está activo
                DisplayAlert("Error", "El GPS esta Inactivo por favor activelo", "OK");
            }

        }

        public async void PasarFoto(byte[] foto)
        {
            if (foto != null)
            {
                var imagenSitio = foto;

                // Guardar la imagen en un archivo temporal
                var filePath = Path.Combine(FileSystem.CacheDirectory, "sitio.png");
                using (var fileStream = File.OpenWrite(filePath))
                {
                    await fileStream.WriteAsync(imagenSitio, 0, imagenSitio.Length);
                }

                // Compartir la imagen utilizando Xamarin.Essentials
                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Imagen del sitio",
                    File = new ShareFile(filePath)
                });

                
            }
            else
            {
                // Manejo de error o mensaje de que no hay imagen disponible
                await DisplayAlert("Aviso", "no hay imagen disponible", "OK");
            }
        }

        public async void CompartirUbicacion(object sender, EventArgs e)
        {

               var mapaImage = await mapa.CaptureImageAsync();

              // Guardar la imagen en un archivo temporal
              var filePath = Path.Combine(FileSystem.CacheDirectory, "mapa.png");
              using (var fileStream = File.OpenWrite(filePath))
              {
                  await mapaImage.CopyToAsync(fileStream);
              }

              // Compartir la imagen utilizando Xamarin.Essentials
              await Share.RequestAsync(new ShareFileRequest
              {
                  Title = "Captura de pantalla del mapa",
                  File = new ShareFile(filePath)
              });
        }

    }
    
}