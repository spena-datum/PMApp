namespace PackageMail.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Sucursales
    {
        [Key]
        public int SucursalId { get; set; }

        [Display(Name ="Sucursal")]
        public string Sucursal { get; set; }

        [Display(Name ="Dirección")]
        public string Direccion { get; set; }

        [Display(Name ="Latitud")]
        public string Latitud { get; set; }
        [Display(Name ="Longitud")]
        public string Longitud { get; set; }
        [JsonIgnore]
        public virtual ICollection<Solicitudes> Solicitudes { get; set; }
    }
}