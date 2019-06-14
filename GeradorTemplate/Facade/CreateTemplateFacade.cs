using GeradorTemplate.Service;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GeradorTemplate.Facade
{
    public interface ICreateTemplateFacade
    {
        Task<bool> Execute(string tipoProjeto, string nomeProjeto);
    }
    public class CreateTemplateFacade : ICreateTemplateFacade
    {
        IConstante Constante;
        IDirectoryService DirectoryService;

        public CreateTemplateFacade(
            IConstante Constante,
            IDirectoryService DirectoryService
        )
        {
            this.Constante = Constante;
            this.DirectoryService = DirectoryService;
        }

        public async Task<bool> Execute(string tipoProjeto, string nomeProjeto)
        {
            try
            {
                foreach (var item in DirectoryService.GetDirectories(nomeProjeto))
                {
                    var newPath = item.Replace(Constante.GetIdentifier(), Constante.GetPathOutput());
                    DirectoryService.CreateDirectory(newPath);
                }
                
                foreach (var item in from files in DirectoryService.EnumerateFiles()
                                     where files.Split('.').Last() != "suo"
                                     select new { File = files }
                )
                {
                    var newPath = item.File.Replace(
                        Constante.GetIdentifier(),
                        Constante.GetPathOutput()
                    ).Replace(
                        Constante.GetNameTemplate(),
                        nomeProjeto
                    );

                    File.Copy(item.File, newPath, false);
                    await File.WriteAllTextAsync(
                        newPath, 
                        File.ReadAllText(newPath)
                        .Replace(
                            Constante.GetNameTemplate(),
                            nomeProjeto
                        )
                    );
                }

                DirectoryService.MoveDirectory(tipoProjeto,nomeProjeto);

                Process.Start(
                    $"_Config/open.bat",
                    $"{Constante.GetFullPath(tipoProjeto, nomeProjeto)}\\{Constante.GetIdentifier()}"
                );

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gerado Error: {ex.Message}", ConsoleColor.Red);
                return false;
            }
        }
    }
}
