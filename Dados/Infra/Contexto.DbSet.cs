using Microsoft.EntityFrameworkCore;

namespace Dados.Infra
{
    public partial class Contexto : DbContext
    {
        public DbSet<ContaCorrente> Contas { get; set; }
    }
}
