using EFDemo.Domain;
using EFDemo.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;

namespace EFDemo.Infrastructure;

public class AppDbContext : DbContext
{
	public DbSet<Store> Stores => Set<Store>();
	public DbSet<Product> Products => Set<Product>();
	public DbSet<Customer> Customers => Set<Customer>();


    public AppDbContext(DbContextOptions<AppDbContext> options)
		: base(options)
	{
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.ApplyConfigurationsFromAssembly(typeof(StoreConfiguration).Assembly);
    }
}
