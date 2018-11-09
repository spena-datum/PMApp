namespace PMApp.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;


    public class Solicitudes
    {
        public int SolicitudId { get; set; }

        public DateTime Fecha { get; set; }

        public string Usuario { get; set; }

        public string DescripcionPaquete { get; set; }

        public int EstadoId { get; set; }

        public string Imagen64b { get; set; }

        public int SucursalId { get; set; }

        [JsonIgnore]
        public virtual Estados Estados { get; set; }

        [JsonIgnore]
        public virtual Sucursales Sucursales { get; set; }

    }
}