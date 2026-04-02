using System;

namespace SistemaEPI.Models
{
    public class EntregaEpi
    {
        public int Id {get;set;}
        public int EpiId {get;set;}
        public Epi? Epi {get;set;}
        public int FuncionarioId {get;set;}
        public Funcionario? Funcionario {get;set;}
        public DateTime DataEntrega {get;set;}
        public DateTime ProximaTroca {get;set;}
        public string? AssinaturaDigitalHash {get;set;}
        public int ResponsavelRegistroId {get;set;}
        public Usuario? ResponsavelRegistro {get; set;}
        public bool IsAtivo { get; set; } = true;
    }
}