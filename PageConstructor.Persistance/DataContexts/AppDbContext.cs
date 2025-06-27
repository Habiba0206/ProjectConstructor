using Microsoft.EntityFrameworkCore;
using PageConstructor.Domain.Entities;

namespace PageConstructor.Persistence.DataContexts;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions)
{
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Page> Pages => Set<Page>();
    public DbSet<Font> Fonts => Set<Font>();
    public DbSet<FontWeight> FontWeights => Set<FontWeight>();
    public DbSet<Meta> Metas => Set<Meta>();
    public DbSet<Script> Scripts => Set<Script>();
    public DbSet<Block> Blocks => Set<Block>();
}
