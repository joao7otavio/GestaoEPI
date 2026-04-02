using System;

namespace SistemaEPI.Models
{
    public class Funcionario
    {
        public int Id {get;set;}
        public string Nome {get; set;} = string.Empty;
        public string Cpf {get;set;} = string.Empty;
        public string Cargo {get;set;} = string.Empty;
        public string Setor {get;set;} = string.Empty;
        public DateTime DataAdmissao {get;set;}
        public bool IsAtivo {get;set;} = true; 
    }
}