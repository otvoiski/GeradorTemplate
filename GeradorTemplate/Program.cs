using GeradorTemplate.Facade;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace GeradorTemplate
{
    public class Program
    {
        public const string WEB_SERVICE = "-ws";
        public const string MICRO_SERVICE = "-ms";

        static void Main(string[] args)
        {
            var tipoProjeto = args[0];
            var nomeProjeto = args[1];

            if (args[0] == "-h" || args[0] == "-help")
            {
                Console.WriteLine(" Comandos:");
                Console.WriteLine("     [tipo projeto] [nome projeto]");
                Console.WriteLine("         [tipo projeto] => -ws | cria um template usado para web services");
                Console.WriteLine("         [tipo projeto] => -ms | cria um template usado para micro serviço");
                Console.WriteLine("         [nome projeto] => NOME | cria um template com o nome NOME no namespace, o NOME é de sua sujestão.");
                Console.WriteLine(" OBS: O projeto será criado onde se localiza seu workspace no tfs, ajuste no appsettings.json antes de usar a aplicação.", ConsoleColor.Yellow);
            }
            else
            {
                if (string.IsNullOrEmpty(tipoProjeto))
                {
                    Console.WriteLine("Tipo do projeto é inválido.");
                    Console.WriteLine($"     Projetos válidos => {WEB_SERVICE}, {MICRO_SERVICE}");
                }
                else if (string.IsNullOrEmpty(nomeProjeto))
                {
                    Console.WriteLine("Nome do projeto é inválido.");
                    Console.WriteLine("     Nome válido => NOME");
                }
                else
                {
                    // Carrega dependencias.
                    IServiceProvider serviceProvider = Dependencias.LoadDependencies();

                    var facade = serviceProvider.GetRequiredService<ICreateTemplateFacade>();

                    if (facade.Execute(tipoProjeto, nomeProjeto).GetAwaiter().GetResult())
                        Console.WriteLine(" Projeto criado com sucesso.", ConsoleColor.Green);
                    else
                        Console.WriteLine(" Falha ao cria o projeto.", ConsoleColor.Red);
                }
            }
        }
    }
}
