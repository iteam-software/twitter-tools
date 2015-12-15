using ITeamSoftware.Tools.Twitter.Services;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ITeamSoftware.Tools.Twitter
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddTwitterTools(this IServiceCollection collection, Action<TwitterToolsOptions> config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var options = new TwitterToolsOptions();
            config(options);

            collection.AddTransient<ITwitterApplicationOnlyService, TwitterApplicationOnlyService>();
            collection.AddSingleton(o => options);

            return collection;
        }
    }
}
