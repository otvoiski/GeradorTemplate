using Microsoft.Extensions.Configuration;
using System.IO;

namespace GeradorTemplate
{
    public interface IConstante
    {
        string GetFullPath(string tipoProjeto, string nomeProjeto);
        string GetPathOutput();
        string GetPathTemplate();
        string GetPathEnd();
        string GetNameTemplate();
        string GetIdentifier();
    }

    public class Constante : IConstante
    {
        IConfigurationRoot Configuration { get; set; }

        public Constante()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("_Config/appsettings.json");

            Configuration = builder.Build();
        }

        public string GetNameTemplate()
            => Configuration.GetSection("Configuration:NameTemplate").Value;

        public string GetPathTemplate()
            => Configuration.GetSection("Configuration:PathTemplate").Value;

        private string GetMicroService()
            => Configuration.GetSection("Configuration:Name:Microservice").Value;

        private string GetWebService()
            => Configuration.GetSection("Configuration:Name:Webservice").Value;

        public string GetPathEnd()
            => Configuration.GetSection("Configuration:PathEnd").Value;

        public string GetPathOutput()
            => Configuration.GetSection("Configuration:PathOutput").Value;

        public string GetIdentifier()
            => Configuration.GetSection("Configuration:Identifier").Value;

        private string GetFullPath()
            => Configuration.GetSection("Configuration:FullPath").Value;

        public string GetFullPath(string tipoProjeto, string nomeProjeto)
        {
            if (tipoProjeto == Program.MICRO_SERVICE)
                return $"{GetFullPath()}{GetMicroService()}{nomeProjeto}{GetPathEnd()}";
            else
                return $"{GetFullPath()}{GetWebService()}{nomeProjeto}{GetPathEnd()}";
        }
    }
}
