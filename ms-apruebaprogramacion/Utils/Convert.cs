using saff_core.constantes;

namespace ms_apruebaprogramacion.Utils
{
    public class Convert
    {
        public UrlService Url { get; } = LoadParametersUrlApiConfig();

        private static UrlService LoadParametersUrlApiConfig()
        {
            string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
            string appconfig = string.IsNullOrEmpty(environment) ? "appsettings.json" : $"appsettings.{environment}.json";
            var builder = new ConfigurationBuilder()
                .AddJsonFile(appconfig, optional: true, reloadOnChange: true);
            var config = builder.Build();
            return config.GetSection("Parametros").Get<UrlService>(); ;
        }
    }
}
