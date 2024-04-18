using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using NumbersConverter.DigitsConverters;

namespace NumbersConverter
{
    [ExcludeFromCodeCoverage]
    public class Container
    {
        public static void Register(IServiceCollection services)
        {
            services.AddSingleton<OneDigitConverter>();
            services.AddSingleton<TwoDigitsConverter>();
            services.AddSingleton<ThreeDigitsConverter>();
            services.AddSingleton<Converter>();
            services.AddSingleton(provider => (IConverter)provider.GetService(typeof(Converter))!);
        }
    }
}