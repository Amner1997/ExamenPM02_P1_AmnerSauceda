
using Plugin.Geolocator;
using Plugin.Media.Abstractions;
using System;
using System.IO;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace ExamenPM02_P1_AmnerSauceda
{
    public partial class App : Application
    {
        
        public App()
        {
            InitializeComponent();
            
            MainPage = new MainPage();
        }

        static Controllers.DBProc dBProc;
        public static Controllers.DBProc Instancia
        {
            get
            {
                if (dBProc == null)
                {
                    String dbname = "Proc.db3";
                    String dbpath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    String dbfulp = Path.Combine(dbpath, dbname);
                    dBProc = new Controllers.DBProc(dbfulp);
                }

                return dBProc;
            }
        }

        protected async override void OnStart()
        {
            MainPage = new NavigationPage(new MainPage());
            base.OnStart();

            // Verificar si los permisos de GPS están concedidos
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            if (status != PermissionStatus.Granted)
            {
                // Los permisos no están concedidos, solicitarlos al usuario
                status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (status == PermissionStatus.Granted)
            {
                // Los permisos están concedidos, puedes iniciar la funcionalidad relacionada con el GPS
                Console.WriteLine("Los permisos estan concedidos");
            }
            else
            {
                // El usuario denegó los permisos, puedes mostrar un mensaje o tomar alguna acción alternativa
                Console.WriteLine("El usuario denego los permisos");
            }

        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
