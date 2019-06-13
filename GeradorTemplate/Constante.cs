namespace GeradorTemplate
{
    public interface IConstante
    {
        string GetPathOutput();
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
            return @"C:\Template\Fontes";
        }

        public string GetPathOutput()
        {
            return @"Testando\";
        }
    }
}
