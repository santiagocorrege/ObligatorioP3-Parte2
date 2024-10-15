using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SistemaAutenticacion.Entidades;
using SistemaAutenticacion.ValueObject;

namespace Gestor.AccesoDatos.Configuraciones
{
    public class UsuarioConfiguracion : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            var emailConverter = new ValueConverter<Email, string>
                (
                    e => e.Valor,
                    e => new Email(e)
                );
            builder.Property(u => u.Email).HasConversion(emailConverter);
        }
    }
}
