using CDC.Models;

using Microsoft.EntityFrameworkCore;

namespace CDC.Data
{
    public class CDCContext : DbContext
    {
        public CDCContext(DbContextOptions<CDCContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
    }
}
