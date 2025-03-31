using System.Linq.Expressions;

namespace Zjazd2s.UIUtils;

public class Adder
{
    private static string? _mssg = null;

    public static Cargo AddCargo(Container container)
    {
        Console.Clear();
        Cargo? cargo = null;
        string? name = null;
        string? mass = null;
        double massd = 0.0;
        double volumed = 0.0;
        string? isHazard = null;
        bool isIt = false;
        string? volume = null;
        GetData(name, "Cargo's name:");
        massd = GetDouble(mass, "Cargo's mass:");
        switch (container.ContainerType)
        {
            case "L":
                isIt = GetBool(isHazard, "Is cargo hazardous? (yes/no)");
                volumed = GetDouble(volume, "Cargo's volume:");

                cargo = new CargoL(name, volumed, massd, isIt);
                break;
            case "G":
                volumed = GetDouble(volume, "Cargo's volume:");

                cargo = new CargoG(name, volumed, massd);
                break;
            case "C":
                string? prod = null;
                Products prodP = Products.Bananas;
                prodP = GetProd(prod, "Product kind (bananas, chocolate, fish, meat, ice cream, frozen pizza, cheese, sausages, butter, eggs):");

                cargo = new CargoC(name, massd, prodP);
                break;
        }

        Console.Clear();

        return cargo;
    }

    public static void AddShip()
    {
        Console.Clear();
        Ship? ship = null;
        string? name = null;
        string? maxSpeed = null;
        double maxSpeedd = 0.0;
        string? maxLoad = null;
        double maxLoadd = 0.0;
        string? maxQty = null;
        int maxQtyd = 0;
        GetData(name, "Ship's name: ");
        maxSpeedd = GetDouble(maxSpeed, "Ship's max speed:");
        maxLoadd = GetDouble(name, "Ship's max load:");
        maxQtyd = (int)GetDouble(name, "Ship's max qty:");
        ship = new Ship(name, maxSpeedd, maxLoadd, maxQtyd);
        Program.ShipsInPort.Add(ship);
    }

    public static void AddContainer()
    {
        
    }


    public static void GetData(string? kind, String mssg)
    {
        Console.Clear();
        Console.WriteLine(mssg);
        if (kind == null)
        {
            Console.Clear();
            GetData(kind, mssg + "\nField Can't be empty");
        }

        kind = Console.ReadLine();
    }

    public static bool GetBool(string? kind, String mssg)
    {
        var output = false;
        Console.Clear();
        Console.WriteLine(mssg);
        if (kind == null )
        {
            Console.Clear();
            GetBool(kind, mssg + "\nField Can't be empty");
        }

        if (kind.ToLower() != "yes" && kind.ToLower() != "no")
        {
            Console.Clear();
            GetBool(kind, mssg + "\nonly yes or no");
        }

        if (kind.ToLower() == "yes")
        {
            output = true;
        }
        return output;
    }
    
    public static Products GetProd(string? kind, String mssg)
    {
        var output = Products.Bananas;
        Console.Clear();
        Console.WriteLine(mssg);
        if (kind == null )
        {
            Console.Clear();
            GetBool(kind, mssg + "\nField Can't be empty");
        }

        if (kind.ToLower() != "bananas" && kind.ToLower() != "chocolate" && kind.ToLower() != "fish" && kind.ToLower() != "meat"  && kind.ToLower() != "ice cream"  && kind.ToLower() != "frozen pizza"  && kind.ToLower() != "cheese" && kind.ToLower() != "sausages" && kind.ToLower() != "butter" && kind.ToLower() != "eggs" )
        {
            Console.Clear();
            GetBool(kind, mssg + "\nonly listed products");
        }

        switch (kind.ToLower())
        {
            case "bananas":
                output = Products.Bananas;
                break;
            case "chocolate":
                output = Products.Chocolate;
                break;
            case "fish":
                output = Products.Fish;
                break;
            case "meat":
                output = Products.Meat;
                break;
            case "ice cream":
                output = Products.IceCream;
                break;
            case "frozen pizza":
                output = Products.FrozenPizza;
                break;
            case "cheese":
                output = Products.Cheese;
                break;
            case "sausages":
                output = Products.Sausages;
                break;
            case "butter":
                output = Products.Butter;
                break;
            case "eggs":
                output = Products.Eggs;
                break;
        }

        return output;
    }
    
    public static double GetDouble(string? kind, String mssg)
    {
        var output = 0.0d;
        Console.Clear();
        Console.WriteLine(mssg);
        if (kind == null )
        {
            Console.Clear();
            output = GetDouble(kind, mssg + "\nField Can't be empty");
        }

        try
        {
            double.TryParse(kind, out output);
        }
        catch (Exception ex)
        { 
            Console.Clear();
           output =  GetDouble(kind, mssg + "\nmust be a number");
        }

        
        return output;
    }
}