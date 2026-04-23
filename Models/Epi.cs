using System;

namespace SistemaEPI.Models
{
    public class Epi
    {
        public int Id {get;set;}
        public string Nome {get;set;} = string.Empty;
        public string NumeroCA {get;set;} = string.Empty;
        public DateTime ValidadeCA {get;set;}
        public int PeriodicidadeTrocaDias {get;set;}
        public bool IsAtivo { get; set; }
        public DateTime ? DeletedAt { get; set; }
    }
}
