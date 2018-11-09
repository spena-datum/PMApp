namespace PackageMail.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Estados
    {
        [Key]
        public int EstadoId { get; set; }
        
        [Display(Name ="Estado")]
        public string Estado { get; set; }

    
        [JsonIgnore]
        public virtual ICollection<Solicitudes> Solicitudes { get; set; }
    }
}