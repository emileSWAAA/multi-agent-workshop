using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.SemanticKernel;

namespace MultiAgentWorkshop
{
    internal static class SemanticKernelSettings
    {
        // This method configures Semantic Kernel and plugins
        internal static IServiceCollection AddSemanticKernel(this IServiceCollection services, HostBuilderContext context)
        {
            services.Configure<AzureOpenAISettings>(context.Configuration.GetSection("AzureOpenAISettings"));
            var azureOpenAISettings = context.Configuration.GetSection("AzureOpenAISettings").Get<AzureOpenAISettings>();
            ArgumentNullException.ThrowIfNull(azureOpenAISettings, nameof(azureOpenAISettings));

            // Add the Azure OpenAI Chat Completion service
            services.AddAzureOpenAIChatCompletion(
                deploymentName: azureOpenAISettings.ChatModelDeployment,
                endpoint: azureOpenAISettings.Endpoint,
                apiKey: azureOpenAISettings.ApiKey
            );

            services.AddKernel();
            return services;
        }
    }
}
