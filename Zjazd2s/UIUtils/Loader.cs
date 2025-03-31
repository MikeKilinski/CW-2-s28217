namespace Zjazd2s.UIUtils;

public class Loader
{
    private static bool _works = true;
    private static string? _mssg = null;

    public static void LoadOnShip(Container container, Ship ship)
    {
        Console.Clear();
        if (container.OnShip != null)
        {
            _mssg = $"Container is already on ship {container.OnShip.RegistryNo}, {container.OnShip.Name}.\n" +
                    $"Unload to load in other ship\n" +
                    $"Press ENTER to go back";

        }
        else
        {

            try
            {
                ship.Load(container);
                _mssg = $"Container {container.SerialNumber} loaded on ship {ship.RegistryNo}, {ship.Name}." +
                        "\nPress ENTER to go back";
                
            }
            catch (ShipOverloadException soex)
            {
                _mssg = soex.Message + "\n Press ENTER to go back";
            }
        }

        if (_mssg != null)
        {
            Console.Write(_mssg);
        }

        _mssg = null;

        Console.ReadLine();
    }

    public static void LoadInContainer(Cargo cargo, Container container)
    {
        Console.Clear();
        if (container.OnShip != null)
        {
            _mssg = $"Container is on ship {container.OnShip.RegistryNo}, {container.OnShip.Name}.\n" +
                    $"Unload from ship to load in the container\n" +
                    $"Press ENTER to go back";

        } else
        {

            try
            {
                container.Load(cargo);
                _mssg = $"Container {cargo.Name} loaded on container {container.SerialNumber}." +
                        "\nPress ENTER to go back";
            }
            catch (Exception ex)
            {
                _mssg = ex.Message + "\n Press ENTER to go back";
            }
        }

        if (_mssg != null)
        {
            Console.Write(_mssg);
        }

        _mssg = null;

        Console.ReadLine();
    }

}