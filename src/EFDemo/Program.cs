using EFDemo.Domain;
using EFDemo.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("AppConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    await dbContext.Database.EnsureDeletedAsync();
    await dbContext.Database.EnsureCreatedAsync();

    if (!await dbContext.Stores.AnyAsync())
    {
        var store1 = new Store { Name = "Store1", Address = new Address { Street = "Street 1", City = "City1 " } };
        var store2 = new Store { Name = "Store2", Address = new Address { Street = "Street 2", City = "City2 " } };

        var product1 = new Product { Name = "Product1" };
        var product2 = new Product { Name = "Product2" };

        store1.Products.Add(product1);
        store2.Products.Add(product2);

        dbContext.Add(store1);
        dbContext.Add(store2);

        await dbContext.SaveChangesAsync();
    }

    if (!await dbContext.Customers.AnyAsync())
    {
        var customer = new Customer { Name = "Customer1" };
        dbContext.Customers.Add(customer);

        await dbContext.SaveChangesAsync();
    }
}

app.Run();
