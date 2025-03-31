using Zjazd2s.UIUtils;

namespace Zjazd2s;

public abstract class Cargo
{
    public abstract string Name { get; set; }
    public abstract double Mass { get; set; }
    public abstract Container? OnContainer { get; set; }

    public new abstract string? ToString();
    public abstract string GetType();
}

public class CargoL(string name, double volume, double mass, bool isHazard, Container? onContainer = null ) : Cargo
{
    private static int NextNumber { get; set; } = 1;
    public string SerialNumber { get; set; } = "CAR-" + CargoType + "-" + NextNumber++;
    public static readonly string CargoType = "L";
    public override string Name { get; set; } = name;
    public override double Mass { get; set; } = mass;
    public override Container? OnContainer { get; set; } = onContainer;
    public bool IsHazard { get; set; } = isHazard;
    public double CargoVolume { get; set; } = volume;
    
    public override string ToString()
    {
        var hazard = IsHazard ? "HAZARD" : "NO HAZARD";
        return $"{SerialNumber}, {Name}, {CargoVolume}L, {hazard}";
    }

    public override string GetType()
    {
        return CargoType;
    }
}

public class CargoG(string name, double volume, double mass, Container? onContainer = null) : Cargo
{   
    private static int NextNumber { get; set; } = 1;
    public string SerialNumber { get; set; } = "CAR-" + CargoType + "-" +NextNumber++;
    public static readonly string CargoType = "G";
    public override string Name { get; set; } = name;
    public override double Mass { get; set; } = mass;
    public override Container? OnContainer { get; set; } = onContainer;
    public double CargoVolume { get; set; } = volume;
    
    public override string ToString()
    {
        return $"{SerialNumber}, {Name}, {CargoVolume}L";
    }

    public override string GetType()
    {
        return CargoType;
    }
}

public class CargoC(string name, double mass, Products product, Container? onContainer = null) : Cargo
{
    private static int NextNumber { get; set; } = 1;
    public string SerialNumber { get; set; } = "CAR-" + CargoType + "-" + NextNumber++;
    public static readonly string CargoType = "C";
    public override string Name { get; set; } = name;
    public override double Mass { get; set; } = mass;
    public Products Product { get; set; } = product;
    public override Container? OnContainer { get; set; } = onContainer;
    
    public override string? ToString()
    {
        return $"{SerialNumber}, {Product}, {Name}, {Mass}kg";
    }

    public override string GetType()
    {
        return CargoType;
    }

}

public enum Products
{
    Bananas = 133,
    Chocolate = 180,
    Fish = 20,
    Meat = -150,
    IceCream = -180,
    FrozenPizza = -300,
    Cheese = 72,
    Sausages = 50,
    Butter = 205,
    Eggs = 190
}