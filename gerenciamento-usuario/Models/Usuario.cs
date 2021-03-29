using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gerenciamento_usuario.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        
    }
}