using Microsoft.AspNetCore.Identity;
using System;

namespace AspNetCoreIdentity.Models
{
    public class Usuario : IdentityUser<Guid>
    {
        public string Imagem { get; set; }

        public bool Ativo { get; set; }
        public DateTime? DataCadastro { get; set; }
        public DateTime? DataAlteracao { get; set; }

    }


}
