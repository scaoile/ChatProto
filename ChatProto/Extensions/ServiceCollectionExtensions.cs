using ChatProto.Handlers;
using ChatProto.Options;
using ChatProto.Services;
using Microsoft.Extensions.Options;

namespace ChatProto.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddGemini(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<GeminiOptions>(configuration.GetSection("Gemini"));

            services.AddTransient<GeminiDelegatingHandler>();

            services.AddHttpClient<GeminiClient>((serviceProvider, httpClient) =>
            {
                var geminiOptions = serviceProvider.GetRequiredService<IOptions<GeminiOptions>>().Value;

                httpClient.BaseAddress = new Uri(geminiOptions.Url);
            })
            .AddHttpMessageHandler<GeminiDelegatingHandler>();
        }
    }
}
