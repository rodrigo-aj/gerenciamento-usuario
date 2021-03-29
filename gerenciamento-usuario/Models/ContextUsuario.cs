using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace gerenciamento_usuario.Models
{
    public class ContextUsuario : DbContext
    {
        public ContextUsuario() : base("mysql_connection") { }

        public DbSet<Usuario> Usuario { get; set; }

    }

}