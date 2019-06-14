using GeradorTemplate.Facade;
using GeradorTemplate.Service;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeradorTemplate
{
    public class Dependencias
    {
        internal static IServiceProvider LoadDependencies()
        {
            var serviceCollection = new ServiceCollection()
                .AddSingleton<IConstante, Constante>()
                .AddTransient<IDirectoryService, DirectoryService>()
                .AddTransient<ICreateTemplateFacade, CreateTemplateFacade>()
                ;

            return serviceCollection.BuildServiceProvider();
        }
    }
}
