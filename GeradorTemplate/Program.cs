namespace GeradorTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = @"C:\Template\Fontes";

            var directories = Directory.GetDirectories(path, "*.*", SearchOption.AllDirectories);

            directories = directories.Select(x => x.Replace("WSGEM", args[0])).ToArray();

            foreach (var item in directories)
            {
                var newPath = item.Replace(@"Fontes\", @"Testando\");

                Directory.CreateDirectory(newPath);

            }


            var ponto = ".".ToCharArray();



            var filesChanged = from files in Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories)
                               where files.Split(ponto[0]).Last() != "suo"
                               select new
                               {
                                   File = files,
                               };


            foreach (var item in filesChanged)
            {
                var newPath = item.File.Replace(@"Fontes\", @"Testando\").Replace("WSGEM", args[0]);

                File.Copy(item.File, newPath, true);

                File.WriteAllText(newPath, File.ReadAllText(newPath).Replace("WSGEM", args[0]));

            }
        }
    }
}
