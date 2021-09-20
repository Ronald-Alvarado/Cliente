﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cliente.Entidades
{
    public class Citas
    {
        public int CitasId { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }
        public DateTime FechaCita { get; set; }
        public string HoraCita { get; set; }


        public Citas()
        {
            CitasId = 0;
            Nombres = string.Empty;
            Apellidos = string.Empty;
            Telefono = string.Empty;
            Direccion = string.Empty;
            FechaCita = DateTime.Now;
            HoraCita = string.Empty;
        }
    }
}
