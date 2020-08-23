using System;
using System.Collections.Generic;

namespace GreatDesafioOracle.Models
{
    public partial class Usuario
    {
        public decimal? Id { get; set; }
        public string Nome { get; set; }
        public string Rg { get; set; }
        public DateTime? Datanascimento { get; set; }
        public string Nomemae { get; set; }
        public DateTime? Datacadastro { get; set; }
        public string Cpf { get; set;}
    }
}
