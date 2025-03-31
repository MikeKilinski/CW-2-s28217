namespace Zjazd2s.UIUtils;

public class Browser
{
    private static bool _works = true;
    private static string? _mssg = null;

    public static void BrowseAll(string type)
    {
        Console.Clear();
        var thisType = "";
        if (type == "S")
        {
            thisType = "ship";
        } else if (type == "C")
        {
            thisType = "container";
        }

        while (_works)
        {
            Console.WriteLine("Browser");
            Console.WriteLine($"There are {Program.ShipsInPort.Count} {thisType}s.\n"+
                              $"choose the number of a {thisType} you want to see.\n"+
                              $"or type 'Q' to go back.\n");
            switch(thisType){
                case "ship":
                    for (int i = 0; i < Program.ShipsInPort.Count; i++)
                    {
                        Console.WriteLine($"{i+1}. {Program.ShipsInPort[i].ToString(true)}");
                    }

                    break;
                default:
                    for (int i = 0; i < Program.ContainersInPort.Count; i++)
                    {
                        Console.WriteLine($"{i+1}. {Program.ContainersInPort[i].toString()}");
                    }
                    break;
                    }
            
            Console.WriteLine("\nType number from the list to show item.\nType 'Q' to go back");
            if (_mssg != null)
            {
                Console.WriteLine(_mssg);
            }

            _mssg = null;
            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                Console.WriteLine(result);
                if (result > Program.ShipsInPort.Count || result < 1)
                {
                    _mssg = "Invalid number - no such ship on the list";
                }
                else
                {
                    switch (thisType)
                    {
                        case "ship":
                            Shower.ShowFromAll("S", result - 1);
                            break;
                        case "container":
                            Shower.ShowFromAll("C", result - 1);
                            break;
                    }
                }
            }
            else if (input != null && input.ToLower() == "q")
            {
                _works = false;
            }
            else
            {
                _mssg = "Invalid input";
            }



            Console.Clear();
            Console.WriteLine(input);
        }
        
        
        _works = true;
    }


    
    public static void BrowseContainersOnShip(Ship ship)
    {
        Console.Clear();
        var list = ship.Containers;
        while (_works)
        {
            var conCount = list.Count;
            Console.WriteLine("Browser");
            if (conCount != 0)
            {
                Console.WriteLine($"There are {conCount} containers.\n" +
                                  $"choose the number of a container you want to see.\n" +
                                  $"or type 'Q' to go back.\n");

                for (var i = 0; i < list.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {list[i].toString()}");
                }

                Console.WriteLine("\nType number from the list to show item.\nType 'Q' to go back");
            }
            else
            {
                Console.WriteLine($"No containers on ship {ship.RegistryNo}, {ship.Name}.\n" +
                                  $"Type 'Q' to go back.\n");
            }

            if (_mssg != null)
            {
                Console.WriteLine(_mssg);
            }

            _mssg = null;
            var input = Console.ReadLine();
            if (int.TryParse(input, out int result))
            {
                Console.WriteLine(result);
                if (result > list.Count || result < 1)
                {
                    _mssg = "Invalid number - no such container on the list";
                }
                else
                {
                    Shower.ShowFromShip(result-1, ship);
                }
            }
            else if (input != null && input.ToLower() == "q")
            {
                _works = false;
            }
            else
            {
                _mssg = "Invalid input";
            }



            Console.Clear();
            Console.WriteLine(input);
        }
        
        
        _works = true;
    }

    public static void BrowseCargo(Container container)
    {
        
        while (_works)
        {
            Console.Clear();
            Console.WriteLine("Browser");
            Console.WriteLine($"Cargo in container {container.SerialNumber}");
            var list = container.Cargos;
            var i = 1;
            foreach (var cargo in list)
            {
                Console.WriteLine($"{i}. {cargo.Name}, {cargo.Mass}");
            }
            Console.WriteLine($"Type 'Q' to go back");
            var input = Console.ReadLine();
           
            if (input != null && input.ToLower() == "q")
            {
       
                    _works = false;
            } else {
                _mssg = "Invalid input";
            }
        }
        
        _works = true;
    }

    public static void BrowseContainerToLoad(Ship ship)
    {
        Console.Clear();
        

        while (_works)
        {
            Console.Clear();
            Console.WriteLine("Browser\n+" +
                              "Choose the container to load.");
            var list = Program.ContainersInPort;
            var i = 1;
            foreach (var unit in list)
            {
                if (unit.OnShip == null)
                {
                    Console.WriteLine($"{i++}. {unit.toString()}");
                }
            }
            Console.WriteLine("\nType number from the list to choose.\nType 'Q' to go back");
            var input = Console.ReadLine();
            if (_mssg != null)
            {
                Console.WriteLine(_mssg);
            }
            _mssg = null;
            
            if (int.TryParse(input, out int result))
            {
                Console.WriteLine(result);
                if (result > list.Count || result < 1)
                {
                    _mssg = "Invalid number - no such container on the list";
                }
                else
                {
                    Loader.LoadOnShip(list[result-1], ship);
                    _works = false;
                }
            }
            else if (input != null && input.ToLower() == "q")
            {
                _works = false;
            }
            else
            {
                _mssg = "Invalid input";
            }

        }
        _works = true;
    }
}