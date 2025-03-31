using Zjazd2s.UIUtils;

namespace Zjazd2s;

public class Shower
{
    private static bool _works = true;
    private static string? _mssg = null;

    public static void ShowFromAll(String type, int number)
    {
        while (_works)
        {
            Console.Clear();
            
                switch (type)
                {
                    case "S":
                        var ship = Program.ShipsInPort[number];
                        Console.Write($"Youre watching the ship {ship.RegistryNo}, {ship.Name}.\n" +
                                      $"Max speed: {ship.MaxSpeed} knots\n" +
                                      $"Mass loaded: {ship.ShowCargoMass()}T\n" +
                                      $"Total mass loaded available: {ship.MaxLoadMass}T\n" +
                                      $"Cointainers loaded: {ship.Containers.Count} pcs\n" +
                                      $"Total containers loaded available: {ship.MaxLoadQnty} pcs\n" +
                                      $"\nWhat do you want to do?\n\n" +
                                      $"1. Show & edit cargo\n" +
                                      $"2. Load a container\n" +
                                      $"3. Unload all containers\n" +
                                      $"4. Delete the ship\n" +
                                      $"5. Go back\n");
                        break;
                    case "C":
                        var con = Program.ContainersInPort[number];
                        string? conType = con.ContainerType;
                        
                        switch (conType)
                        {
                            case "L":
                                con = (ContainerL)con;
                                Console.WriteLine($"Youre watching the liquid container {con.SerialNumber} in Port.\n" +
                                                  $"Dimensions: w={con.Width}, l={con.Depth}, h={con.Height}\n" +
                                                  $"Capacity: {con.Capacity}L\n");
                                if (con.Cargos == null || con.Cargos.Count == 0)
                                {
                                    Console.WriteLine($"No cargos on the ship.\n+" +
                                                      $"Self mass: {con.Weight}kg");
                                }
                                else
                                {
                                    Console.WriteLine($"Self mass: {con.Weight}kg, Cargo mass: {con.Cargos[0].Mass}kg, Total mass: {con.TotalMass()}");
                                }
                                break;
                            case "C":
                                ContainerC conC = (ContainerC)con;
                                Console.WriteLine($"Youre watching the refrigerator container {conC.SerialNumber} in Port.\n" +
                                                  $"Dimensions: w={conC.Width}, l={conC.Depth}, h={conC.Height}\n" +
                                                  $"Capacity: {conC.Capacity}kg\n" +
                                                  $"Temperature: {conC.Temperature}");
                                if (conC.Cargos == null || conC.Cargos.Count == 0)
                                {
                                    Console.WriteLine($"No cargos on the ship.\n+" +
                                                      $"Self mass: {conC.Weight}kg");
                                }
                                else
                                {
                                    var cMass = 0.0;
                                    foreach (var cargo in conC.Cargos)
                                    {
                                        cMass += cargo.Mass;
                                    }

                                    Console.WriteLine($"Self mass: {conC.Weight}kg, Cargo mass: {cMass}kg, Total mass: {conC.TotalMass()}");
                                }
                                break;
                            case "G":
                                ContainerG conG = (ContainerG)con;
                                con = (ContainerL)con;
                                Console.WriteLine($"Youre watching the liquid container {conG.SerialNumber} in Port.\n" +
                                                  $"Dimensions: w={conG.Width}, l={conG.Depth}, h={conG.Height}\n" +
                                                  $"Capacity: {conG.Capacity}L\n");
                                if (conG.Cargos == null || conG.Cargos.Count == 0)
                                {
                                    Console.WriteLine($"No cargos on the ship.\n+" +
                                                      $"Self mass: {conG.Weight}kg");
                                }
                                else
                                {
                                    Console.WriteLine($"Self mass: {conG.Weight}kg, Cargo mass: {conG.Cargos[0].Mass}kg, Total mass: {conG.TotalMass()}");
                                }

                                break;
                        }
                        
                        Console.WriteLine($"\nWhat do you want to do?\n\n" +
                                          $"1. Show & edit cargo\n" +
                                          $"2. Load cargo\n" +
                                          $"3. Unload all cargo\n" +
                                          $"4. Delete the container\n" +
                                          $"5. Load on ship\n" +
                                          $"6. Go back\n");

                        break;
                }

                if (_mssg != null)
                {
                    Console.WriteLine(_mssg);
                }

                _mssg = null;

                var query = Console.ReadLine();
                switch (type)
                {
                    case "S":
                        switch (query)
                        {
                            case "1":
                                var ship1 = Program.ShipsInPort[number];
                                if (ship1.Containers == null || ship1.Containers.Count == 0)
                                {
                                    _mssg = "No cargos on the ship.";
                                }
                                else
                                {
                                    Browser.BrowseContainersOnShip(ship1);
                                }
                                break;
                            case "2":
                                var ship2 = Program.ShipsInPort[number];
                                Browser.BrowseContainerToLoad(ship2);
                                break;
                            case "3":
                                var ship3 = Program.ShipsInPort[number];
                                try
                                {
                                    ship3.Unload();
                                }
                                catch (Exception e)
                                {
                                    _mssg = "All containers unloaded";
                                }
                                break;
                            case "4":
                                var ship4 = Program.ShipsInPort[number];
                                try
                                {
                                    ship4.Unload();
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine("All containers unloaded");
                                }
                                Program.ShipsInPort.RemoveAt(number);
                                Console.WriteLine("Ship deleted.\nPress ENTER to continue...");
                                Console.ReadLine();
                                _works = false;
                                break;
                            case "5":
                                _works = false;
                                break;
                            default:
                                _mssg = "Invalid input";
                                break;
                        }

                        break;
                    case "C":
                        var cont = Program.ContainersInPort[number];
                        switch (query)
                        {
                            case "1":
                                if (cont.Cargos == null || cont.Cargos.Count == 0)
                                {
                                    _mssg = "No cargos on the ship.";
                                }
                                else
                                {
                                    Browser.BrowseCargo(cont);
                                }

                                break;
                            case "2":
                                Loader.LoadInContainer(Adder.AddCargo(cont), cont);
                                _mssg = "Cargo added.";
                                break;
                            case "3":
                                cont.Unload();
                                break;
                            case "4":
                                Program.ContainersInPort.RemoveAt(number);
                                _works = false;
                                break;
                            case "5":
                                _works = false;
                                break;
                            default:
                                _mssg = "Invalid input";
                                break;
                        }

                        break;
                }

            

        }
        
        _works = true;
    }

    public static void ShowFromShip(int number, Ship ship)
    {
        while (_works)
        {
            Console.Clear();
            var container = ship.Containers[number];
            if (container is ContainerL)
            {
                container = (ContainerL)container;
                Console.WriteLine($"Youre watching the liquid container {container.SerialNumber} on ship {ship.RegistryNo}, {ship.Name}.\n" +
                                  $"Dimensions: w={container.Width}, l={container.Depth}, h={container.Height}\n" +
                                  $"Capacity: {container.Capacity}L\n");
                if (container.Cargos == null || container.Cargos.Count == 0)
                {
                    Console.WriteLine($"No cargos on the ship.\n+" +
                                      $"Self mass: {container.Weight}kg");
                }
                else
                {
                    Console.WriteLine($"Self mass: {container.Weight}kg, Cargo mass: {container.Cargos[0].Mass}kg, Total mass: {container.TotalMass()}");
                }
            }

            
            
                          
            Console.WriteLine($"\nWhat do you want to do?\n\n" +
                                      $"1. Show & edit cargo\n" +
                                      $"2. Load cargo\n" +
                                      $"3. Unload all cargo\n" +
                                      $"4. Delete the container\n" +
                                      $"5. Load on other ship\n" +
                                      $"6. Go back\n");


                if (_mssg != null)
                {
                    Console.WriteLine(_mssg);
                }

                _mssg = null;

                var query = Console.ReadLine();
                switch (query)
                {
                    case "1":
                        if (container.Cargos == null || container.Cargos.Count == 0)
                        {
                            _mssg = "No cargos on the ship.";
                        }
                        else
                        {
                            Browser.BrowseCargo(container);
                        }

                        break;
                    case "2":
                        Loader.LoadInContainer(Adder.AddCargo(container), container);
                        _mssg = "Cargo added.";
                        break;
                    case "3":
                        container.Unload();
                        break;
                    case "4":
                        ship.Containers.RemoveAt(number);
                        _works = false;
                        break;
                    case "5":
                        _works = false;
                        break;
                    default:
                        _mssg = "Invalid input";
                        break;
                }

        }
        
        _works = true;
    }

    public static void ShowCargo(Container container, int result)
    {
        
    }

}