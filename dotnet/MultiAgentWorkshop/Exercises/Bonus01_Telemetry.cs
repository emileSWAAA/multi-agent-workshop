using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MultiAgentWorkshop.Exercises
{
    /// <summary>
    /// BONUS01: Application Insights Telemetry Integration
    ///
    /// Workshop Instructions:
    /// - Integrate Azure Application Insights telemetry into the application.
    /// - Implement an extension method AddAppInsightsTelemetry for IServiceCollection.
    /// - The method should:
    ///   - Read the Application Insights connection string from configuration ("ApplicationInsights").
    ///   - Throw an exception if the connection string is missing.
    ///   - Create a ResourceBuilder with the service name "MultiAgentWorkshop".
    ///   - Enable model diagnostics with sensitive data by setting the AppContext switch:
    ///     "Microsoft.SemanticKernel.Experimental.GenAI.EnableOTelDiagnosticsSensitive" to true.
    ///   - Configure OpenTelemetry tracing:
    ///     - Add source "Microsoft.SemanticKernel*"
    ///     - Add Azure Monitor trace exporter with the connection string.
    ///   - Configure OpenTelemetry metrics:
    ///     - Add meter "Microsoft.SemanticKernel*"
    ///     - Add Azure Monitor metric exporter with the connection string.
    ///   - Configure logging:
    ///     - Add OpenTelemetry logging with the resource builder.
    ///     - Add Azure Monitor log exporter with the connection string.
    ///     - Include formatted messages and scopes.
    ///     - Set minimum log level to Information.
    ///   - Register the logger factory as a singleton in the service collection.
    /// - Return the modified IServiceCollection.
    ///
    /// Setup:
    /// - https://learn.microsoft.com/en-us/semantic-kernel/concepts/enterprise-readiness/observability/?pivots=programming-language-csharp
    /// - Register this extension method in your Program.cs when building the host.
    /// - Ensure the Application Insights connection string is available in configuration.
    /// </summary>
    internal static class Bonus01_Telemetry
    {
        internal static IServiceCollection AddAppInsightsTelemetry(this IServiceCollection services, HostBuilderContext context)
        {
            // Implement the AddAppInsightsTelemetry extension method here.
            throw new NotImplementedException("Implement the AddAppInsightsTelemetry method as per the instructions.");
        }
    }
}
