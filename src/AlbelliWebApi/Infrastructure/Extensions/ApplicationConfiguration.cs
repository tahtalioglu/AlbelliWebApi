using AlbelliWebApi.Data.Repositories;
using AlbelliWebApi.Infrastructure.Persistance.EFCore.Repository;
using AlbelliWebApi.Services;
namespace AlbelliWebApi.Infrastructure.Extensions
{
	public static class ApplicationConfiguration
	{
		public static IServiceCollection AddApplicationRepositories(this IServiceCollection services)
		{
			services.AddTransient<IProductRepository, ProductRepository>();
			services.AddTransient<IOrderRepository, OrderRepository>();
			services.AddTransient<IOrderItemRepository, OrderItemRepository>();
			return services;
		}

		public static IServiceCollection AddApplicationServices(this IServiceCollection services)
		{
			services.AddScoped<IOrderService, OrderService>();

			return services;
		}
	}
}
