namespace PMApp.Models
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    

    public class Estados
    {
        public int EstadoId { get; set; }
        
        public string Estado { get; set; }
            
        [JsonIgnore]
        public virtual ICollection<Solicitudes> Solicitudes { get; set; }
    }
}