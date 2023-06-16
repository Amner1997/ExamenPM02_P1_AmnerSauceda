using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ExamenPM02_P1_AmnerSauceda.Controllers
{
    public class CamposValidator
    {

        public bool ValidarCampos(ImageSource foto, string latitud, string longitud, string descripcion)
        {
            // Verificar si la foto es nula o vacía
            if (foto == null)
            {
                return false;
            }

            // Verificar si la latitud está vacía
            if (string.IsNullOrWhiteSpace(latitud))
            {
                return false;
            }

            // Verificar si la longitud está vacía
            if (string.IsNullOrWhiteSpace(longitud))
            {
                return false;
            }

            // Verificar si la descripción está vacía
            if (string.IsNullOrWhiteSpace(descripcion))
            {
                return false;
            }

            return true;
        }
    }

    
}
