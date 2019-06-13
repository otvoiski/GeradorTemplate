namespace GeradorTemplate
{
    public interface IConstante
    {
        string GetPathOutput(string tipoProjeto);
        string GetPathTemplate();
        string GetNameTemplate();
    }

    public class Constante : IConstante
    {
        public string GetNameTemplate()
        {
            return "TEMPLATE";
        }

        public string GetPathTemplate()
        {
            return @"C:\TESTE\Template\Fontes";
        }

        public string GetPathOutput(string tipoProjeto)
        {
            if (tipoProjeto == Program.MICRO_SERVICE)
                return @"Testando\";
            else
                return @"Testando\";
        }
    }
}
