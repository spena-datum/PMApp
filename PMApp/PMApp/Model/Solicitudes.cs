namespace PackageMail.Models
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Solicitudes
    {
        [Key]
        public int SolicitudId { get; set; }

        [Display(Name ="Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name ="Usuario")]
        public string Usuario { get; set; }

        [Display(Name ="Descripción del paquete")]
        public string DescripcionPaquete { get; set; }

        [Display(Name ="Estado")]
        public int EstadoId { get; set; }

        [Display(Name ="Imagen del paquete entregado")]
        public string Imagen64b { get; set; }

        [Display(Name ="Sucursal")]
        public int SucursalId { get; set; }

        [JsonIgnore]
        public virtual Estados Estados { get; set; }

        [JsonIgnore]
        public virtual Sucursales Sucursales { get; set; }

    }
}