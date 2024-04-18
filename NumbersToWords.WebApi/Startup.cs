using System.Diagnostics.CodeAnalysis;
using Microsoft.Net.Http.Headers;

namespace NumbersToWords.WebApi
{
    [ExcludeFromCodeCoverage]
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            NumbersConverter.Container.Register(services);

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = Path.Combine(AppContext.BaseDirectory, "ui");
            });
        }

        public void Configure(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Map("/app", client =>
            {
                client.UseSpaStaticFiles();

                client.UseSpa(spa =>
                {
                    spa.Options.SourcePath = Path.Combine(AppContext.BaseDirectory, "ui");

                    spa.Options.DefaultPageStaticFileOptions = new StaticFileOptions
                    {
                        OnPrepareResponse = ctx =>
                        {
                            var headers = ctx.Context.Response.GetTypedHeaders();

                            headers.CacheControl = new CacheControlHeaderValue()
                            {
                                NoCache = true,
                                NoStore = true,
                                MustRevalidate = true
                            };
                        }
                    };
                });
            });
        }
    }
}