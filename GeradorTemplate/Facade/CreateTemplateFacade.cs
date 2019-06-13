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

        private async Task<bool> Execute(string tipoProjeto, string nomeProjeto)
        {
            if (tipoProjeto == Program.WEB_SERVICE)
                return await ExecuteWebService(tipoProjeto, nomeProjeto);
            else
                return await ExecuteMicroService(tipoProjeto, nomeProjeto);
        }

        private async Task<bool> ExecuteMicroService(string tipoProjeto, string nomeProjeto)
        {
            try
            {
                CreateDirectories(nomeProjeto);

                var ponto = ".".ToCharArray();

                var filesChanged =
                    from files in DirectoryService.EnumerateFiles()
                    where files.Split(ponto[0]).Last() != "suo"
                    select new
                    {
                        File = files,
                    };


                foreach (var item in filesChanged)
                {
                    var newPath = item.File.Replace(@"Fontes\", Constante.GetPathOutput()).Replace(Constante.GetNameTemplate(), nomeProjeto);
                    File.Copy(item.File, newPath, false);
                    await File.WriteAllTextAsync(newPath, File.ReadAllText(newPath).Replace(Constante.GetNameTemplate(), nomeProjeto));
                }

                Process.Start("cmd.exe", $"Config/open.bat {Constante.GetPathOutput()}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gerado Error: {ex.Message}, InnerException: {ex.InnerException.Message}");
                return false;
            }
        }

        private async Task<bool> ExecuteWebService(string tipoProjeto, string nomeProjeto)
        {
            try
            {
                CreateDirectories(nomeProjeto);

                var ponto = ".".ToCharArray();

                var filesChanged =
                    from files in DirectoryService.EnumerateFiles()
                    where files.Split(ponto[0]).Last() != "suo"
                    select new
                    {
                        File = files,
                    };


                foreach (var item in filesChanged)
                {
                    var newPath = item.File.Replace(@"Fontes\", Constante.GetPathOutput()).Replace(Constante.GetNameTemplate(), nomeProjeto);
                    File.Copy(item.File, newPath, false);
                    await File.WriteAllTextAsync(newPath, File.ReadAllText(newPath).Replace(Constante.GetNameTemplate(), nomeProjeto));
                }

                Process.Start("cmd.exe", $"Config/open.bat {Constante.GetPathOutput()}");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Gerado Error: {ex.Message}, InnerException: {ex.InnerException.Message}");
                return false;
            }
        }

        private void CreateDirectories(string nomeProjeto)
        {
            foreach (var item in DirectoryService.GetDirectories(nomeProjeto))
            {
                var newPath = item.Replace(@"Fontes\", Constante.GetPathOutput());
                DirectoryService.CreateDirectory(newPath);
            }
        }
    }
}
