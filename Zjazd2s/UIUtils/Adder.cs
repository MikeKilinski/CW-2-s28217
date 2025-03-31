using System.Linq.Expressions;

namespace Zjazd2s.UIUtils;

public class Adder
{
    private static string? _mssg = null;

    public static Cargo AddCargo(Container container)
    {
        Console.Clear();
        Cargo? cargo = null;
        string? name;
        double massd;
        double volumed;
        bool isIt;
        name = GetData("Cargo's name:");
        massd = GetDouble("Cargo's mass:");
        switch (container.ContainerType)
        {
            case "L":
                isIt = GetBool("Is cargo hazardous? (yes/no)");
                volumed = GetDouble("Cargo's volume:");

                cargo = new CargoL(name, volumed, massd, isIt);
                break;
            case "G":
                volumed = GetDouble("Cargo's volume:");

                cargo = new CargoG(name, volumed, massd);
                break;
            case "C":
                string? prod = null;
                Products prodP;
                prodP = GetProd("Product kind (bananas, chocolate, fish, meat, ice cream, frozen pizza, cheese, sausages, butter, eggs):");

                cargo = new CargoC(name, massd, prodP);
                break;
        }

        Console.Clear();

        return cargo;
    }

    public static void AddShip()
    {
        Console.Clear();
        Ship? ship;
        string? name;
        double maxSpeedd;
        double maxLoadd;
        int maxQtyd;
        name = GetData("Ship's name: ");
        maxSpeedd = GetDouble("Ship's max speed:");
        maxLoadd = GetDouble("Ship's max load:");
        maxQtyd = (int)GetDouble("Ship's max qty:");
        ship = new Ship(name, maxSpeedd, maxLoadd, maxQtyd);
        Program.ShipsInPort.Add(ship);
    }

    public static void AddContainer()
    {
        Console.Clear();
        Container? container;
        string? type;
        string? question = null;
        type = GetType("Container type (liquid, refrigerator, gas):");
        var h = GetDouble("Container's height:");
        var w = GetDouble("Container's width:");
        var l = GetDouble("Container's length:");
        var weight = GetDouble("Container's weight: ");
        switch (type)
        {
            case "L":
                container = new ContainerL(h, w, l, weight);
                Program.ContainersInPort.Add(container);
                break;
            case "G":
                container = new ContainerG(h, w, l, weight);
                Program.ContainersInPort.Add(container);
                break;
            case "C":
                var capacity = GetDouble("Container's capacity:");
                var temp = GetDouble("Container's temperature:");
                container = new ContainerC(h, w, l, weight, capacity, temp);
                Program.ContainersInPort.Add(container);
                break;
        }
    }


    public static string GetData(String mssg)
    {   
        Console.Clear();
        Console.WriteLine(mssg);
        var kind = Console.ReadLine();
        
        if (kind == null || kind.Length == 0)
        {
            Console.Clear();
            kind = GetData(mssg + "\nField Can't be empty");
        }

        return kind;
    }

    public static bool GetBool(String mssg)
    {
        Console.Clear();
        Console.WriteLine(mssg);
        var output = false;
        var kind = Console.ReadLine();
        var mssgI = mssg;
        if (kind == null || kind.Length == 0)
        {
            GetBool("\nField Can't be empty");
        }

        if (kind.ToLower() != "yes" && kind.ToLower() != "no")
        {
            GetBool("\nonly yes or no");
        }

        if (kind.ToLower() == "yes")
        {
            output = true;
        }
        return output;
    }
    
    public static Products GetProd(String mssg)
    {
        Console.Clear();
        Console.WriteLine(mssg);
        var output = Products.Bananas;
        var kind = Console.ReadLine();
        if (kind == null || kind.Length == 0)
        {
            GetBool("\nField Can't be empty");
        }

        if (kind.ToLower() != "bananas" && kind.ToLower() != "chocolate" && kind.ToLower() != "fish" && kind.ToLower() != "meat"  && kind.ToLower() != "ice cream"  && kind.ToLower() != "frozen pizza"  && kind.ToLower() != "cheese" && kind.ToLower() != "sausages" && kind.ToLower() != "butter" && kind.ToLower() != "eggs" )
        {
            GetBool("\nonly listed products");
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
    
    public static double GetDouble(String mssg)
    {
        Console.Clear();
        Console.WriteLine(mssg);
        var output = 0.0d;
        var kind = Console.ReadLine();
        if (kind == null || kind.Length == 0)
        {
            output = GetDouble(mssg + "\nField Can't be empty");
        } else if (double.TryParse(kind, out output))
        {
            return output;
        }
        else
        {
            output = GetDouble(mssg + "\nmust be a number");
        }
        return output;
    }

    public static string GetType(String mssg)
    {
        Console.Clear();
        Console.WriteLine(mssg);
        var output = "";
        var kind = Console.ReadLine();
        if (kind == null || kind.Length == 0)
        {
            kind = GetType("\nField Can't be empty");
        }
        if (kind.ToLower() != "liquid" && kind.ToLower() != "refrigerator" && kind.ToLower() != "gas")
        {
            
            kind = GetType("\nonly liquid, refrigerator or gas");
        }

        switch (kind.ToLower())
        {
            case "liquid":
                output = "L";
                break;
            case "refrigerator":
                output = "C";
                break;
            case "gas":
                output = "G";
                break;
        }
        return output;
    }
}