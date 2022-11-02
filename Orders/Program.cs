using Data;
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
    .ConfigureServices(services => {
        string connectionString = Environment.GetEnvironmentVariable("DefaultConnection")!;

        services.AddDbContext<DbContext, DataContext>(opts => opts.UseSqlServer(connectionString));

        services.AddTransient<IOrderRepository, OrderRepository>();
        services.AddTransient<IUserRepository, UserRepository>();

        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IUserService, UserService>();

        services.AddAutoMapper(typeof(MappingProfile));
    })
    .Build();

host.Run();
