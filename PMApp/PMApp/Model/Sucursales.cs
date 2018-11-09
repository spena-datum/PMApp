namespace PMApp.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class Sucursales
    {
        public int SucursalId { get; set; }

        public string Sucursal { get; set; }

        public string Direccion { get; set; }

        public string Latitud { get; set; }
        public string Longitud { get; set; }
        [JsonIgnore]
        public virtual ICollection<Solicitudes> Solicitudes { get; set; }
    }
}