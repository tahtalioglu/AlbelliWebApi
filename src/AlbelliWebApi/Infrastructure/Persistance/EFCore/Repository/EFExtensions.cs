using Microsoft.EntityFrameworkCore;
using AlbelliWebApi.Infrastructure.Constant;

namespace AlbelliWebApi.Infrastructure.Persistance.EFCore.Repository
{
	public static class EFExtensions
	{
		public static IServiceCollection UseEFContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AlbelliDataContext>(options =>
			{
				options.UseInMemoryDatabase(configuration.GetSection(Constants.DBName).Value);
			});

			return services;
		}
	}
}
