using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExamenPM02_P1_AmnerSauceda.Models
{
    public class Sitio
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        //public string Imagen { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public string Descripcion { get; set; }

        public byte[] foto { get; set; }
    }
}
