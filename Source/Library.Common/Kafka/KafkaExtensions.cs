using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Common.Kafka
{
    /// <summary>
    /// 
    /// </summary>
    public static class KafkaExtensions
    {
        public static void InitKafkaServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KafkaConnection>(configuration.GetSection(nameof(KafkaConnection)));

            services.AddSingleton<IKafkaService, KafkaService>();
        }
    }
}
