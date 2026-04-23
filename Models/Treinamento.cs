using System;

namespace SistemaEPI.Models
{
    public class Treinamento
    {
        public int Id {get;set;}
        public int FuncionarioId {get;set;}
        public Funcionario? Funcionario {get; set;}
        public string NomeNR {get;set;} = string.Empty;
        public DateTime DataConclusao {get;set;}
        public int ValidadeMeses {get;set;}
        public string StatusVencimento {get;set;} = string.Empty;
        public int ResponsavelRegistroId {get;set;}
        public Usuario? ResponsavelRegistro { get; set; }
        public DateTime? DeletedAt { get; set; }
         public bool IsAtivo { get; set; } = true;
    }
}