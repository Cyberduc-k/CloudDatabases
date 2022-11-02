using Data;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository;
using Repository.Interfaces;
using Service;
using Service.Interfaces;
using Service.Mapping;

IHost host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((ctx, services) => {
        string connectionString = Environment.GetEnvironmentVariable("DefaultConnection")!;

        services.AddDbContext<DbContext, DataContext>(opts => opts.UseSqlServer(connectionString));

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IProductRepository, ProductRepository>();
        services.AddTransient<IOrderRepository, OrderRepository>();

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IOrderService, OrderService>();

        services.AddScoped<IImageService, ImageService>();

        services.AddAutoMapper(typeof(MappingProfile));
    })
    .ConfigureOpenApi()
    .Build();

await host.RunAsync();
