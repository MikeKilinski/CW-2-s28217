namespace Zjazd2s.UIUtils;

public class MainMenu
{
    public static string? _mssg = null;
    public static void Go()
    {
        var works = true;
        while (works)
        {
            Console.Clear();
            Console.WriteLine("MainMenu");
            Console.WriteLine(
                $"There are {Program.ShipsInPort.Count} ships and {Program.ContainersInPort.Count} containers in the port\n");
            Console.WriteLine("Choose what you want to do:\n" +
                              "1. Browse ships\n" +
                              "2. Add a ship\n" +
                              "3. Browse containers\n" +
                              "4. Add a container\n" +
                              "5. Exit\n"
            );
            
            switch (Console.ReadLine())

            {

                case "1": Console.Clear(); Browser.BrowseAll("S"); break;
                case "2": Console.Clear(); Adder.AddShip();
                    _mssg = "Ship added";
                    break;
                case "3": Console.Clear(); Browser.BrowseAll("C"); break;
                case "4": Console.Clear();
                    Adder.AddContainer(); 
                    _mssg = "Container added";
                    break;
                case "5": Console.Clear(); Console.WriteLine("Exit");
                    works = false; break;
                default: Console.Clear(); Console.WriteLine("Invalid input"); break;
            }

            if (_mssg != null)
            {
                Console.WriteLine(_mssg + "\nPress ENTER to go back");
                Console.ReadLine();
            }
            _mssg = null;

        }

    }
}