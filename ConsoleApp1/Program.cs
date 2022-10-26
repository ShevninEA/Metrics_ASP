namespace ConsoleApp1
{
    public partial class App
    {
        public string Name { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            App1 app = new App1();
            app.Print();
        }
    }
}