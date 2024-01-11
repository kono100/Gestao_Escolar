using Gestao.Models;
using Microsoft.EntityFrameworkCore;

namespace Gestao.Data
{
    public class IESContext : DbContext
    {
        public IESContext(DbContextOptions<IESContext> options) : base(options)
        {
        }

        public DbSet<Departamento> Departamento { get; set;}

        public DbSet<Instituicao> Instituicao { get; set;}
    }
}
