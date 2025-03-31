using Zjazd2s.UIUtils;

namespace Zjazd2s;



public abstract class Container : IContainer
{
    public abstract double Height { get; set; }
    public abstract string ContainerType { get; set; }
    public abstract double Weight { get; set; }
    public abstract double Depth { get; set; }
    public abstract double Width { get; set; }
    public abstract string SerialNumber { get; set; }
    public abstract double Capacity { get; set; }
    public abstract List<Cargo> Cargos { get; set; }
    public abstract Ship? OnShip { get; set; }
    

    public abstract void Load(Cargo cargo);
    public abstract void Unload();
    public abstract double TotalMass();

    public string toString()
    {
        return SerialNumber;
    }
}

public class ContainerL (double height, double width, double depth, double weight): Container, IHazardNotifier
{
    
    private static int NextNumber { get; set; } = 1;
    public override double Height { get; set; } = height;
    public override string ContainerType { get; set; } = "L";
    public override double Weight { get; set; } = weight;
    public override double Depth { get; set; } = depth;
    public override double Width { get; set; } = width;
    public override string SerialNumber { get; set; } = "KON-" + "L" + "-" + NextNumber++;
    public override double Capacity { get; set; } = (height * width * depth)/1000;
    public override List<Cargo> Cargos { get; set; } = new List<Cargo>();
    public override Ship? OnShip { get; set; } = null;

    public override void Load(Cargo cargo)
    {
        if (Cargos.Count > 0)
        {
            Notify("Container needs to be empty");
        }
        else if (cargo.GetType() != "L")
        {
            var message = $"Cargo expected {ContainerType} but got {cargo.GetType()}";
            Notify(message, new WrongCargoException(message));
        }
        else if (((CargoL)cargo).IsHazard)
        {
            if (((CargoL)cargo).CargoVolume > Capacity * 0.5)
            {
                var message = $"Max load {Capacity * 0.5}L, cargo is {((CargoL)cargo).CargoVolume}L";
                Notify(message, new OverfillException(message));
            }
            else
            {
                Cargos.Add(cargo);
                cargo.OnContainer = this;
                Notify("Cargo: " + (CargoL)cargo + " loaded");
            }
        }
        else
        {
            if (((CargoL)cargo).CargoVolume > Capacity * 0.9)
            {
                var message = $"Max load {Capacity * 0.9}L, cargo is {((CargoL)cargo).CargoVolume}L";
                Notify(message, new OverfillException(message));
            }
            else
            {
                Cargos.Add(cargo);
                cargo.OnContainer = this;
                Notify("Cargo: " + (CargoL)cargo + " loaded");
            }
        }

    }

    public override void Unload()
    {
        Cargos[0].OnContainer = null;
        Cargos.Clear();
        Notify("Container unloaded");
    }

    public override double TotalMass()
    {
        if (Cargos.Count == 0)
        {
            return Weight;
        }
        else
        {
            var totalMass = 0.0;
            foreach (var item in Cargos)
            {
                totalMass += item.Mass;
            }
            return totalMass+Weight;
        }
    }

    public void Notify(string message)
    {
        Console.WriteLine(message);
    }

    public void Notify(string message, Exception ex)
    {
        Console.WriteLine(message);
        throw ex;
    }
}

public class ContainerG(double height, double width, double depth, double weight) : Container, IHazardNotifier
{   
    private static int NextNumber { get; set; } = 1;
    public override double Height { get; set; } = height;
    public override string ContainerType { get; set; } = "G";
    public override double Weight { get; set; } = weight;
    public override double Depth { get; set; } = depth;
    public override double Width { get; set; } = width;
    public override string SerialNumber { get; set; } = "KON-" + "G" + "-" + NextNumber++;
    public override double Capacity { get; set; } = (height * width * depth)/1000;
    public override List<Cargo> Cargos { get; set; } = new List<Cargo>();
    public override Ship? OnShip { get; set; } = null;
    public double Residues { get; set; } = 0.0d;
    
    public override void Load(Cargo cargo)
    {
        if (Cargos.Count > 0)
        {
            Notify("Container needs to be empty");
        } else if (cargo.GetType() != "G")
        {
            var message = "Cargo expected G but got " + cargo.GetType();
            Notify(message, new WrongCargoException(message));
        } else if (((CargoG)cargo).CargoVolume > Capacity * (1-Residues))
        {
            var message = $"Max load {Capacity * (1-Residues)}L, cargo is {((CargoG)cargo).CargoVolume}L";
            Notify(message, new OverfillException(message));
        }
        else
        {
            Cargos.Add(cargo);
            cargo.OnContainer = this;
            Notify("Cargo: " + (CargoG)cargo + " loaded");
        }
    }

    public override void Unload()
    {
        Cargos[0].OnContainer = null;
        Cargos.Clear();
        Notify("Container unloaded");
        Residues = 0.05d;
    }

    public override double TotalMass()
    {
        if (Cargos.Count == 0)
        {
            return Weight;
        }
        else
        {
            var totalMass = 0.0;
            foreach (var item in Cargos)
            {
                totalMass += item.Mass;
            }
            return totalMass+Weight;
        }
    }

    public void Notify(string message)
    {
        Console.WriteLine(message);
    }

    public void Notify(string message, Exception ex)
    {
        Console.WriteLine(message);
        throw ex;
    }
}

public class ContainerC(double height, double width, double depth, double weight, double capacity, double temperature) : Container, IHazardNotifier
{
    private static int NextNumber { get; set; } = 1;
    public double Temperature { get; set; } = temperature;
    public Products? Product { get; set; } 
    public override double Height { get; set; } = height;
    public override string ContainerType { get; set; } = "C";
    public override double Weight { get; set; } = weight;
    public override double Depth { get; set; } = depth;
    public override double Width { get; set; } = width;
    public override string SerialNumber { get; set; } = "KON-" + "C" + "-" + NextNumber++;
    public override double Capacity { get; set; } = capacity;
    public override List<Cargo> Cargos { get; set; } = new List<Cargo>();
    public override Ship? OnShip { get; set; } = null;


    public override void Load(Cargo cargo)
    {
        if (Product != null && ((CargoC)cargo).Product != Product)
        {
            var message = $"Cargo expected {Product} but got {((CargoC)cargo).Product}";
            Notify(message, new WrongCargoException(message));
        } else
        {
            var cargoMass = 0.0;
            foreach (var item in Cargos)
            {
                cargoMass += item.Mass;
            }
            if (cargoMass + cargo.Mass > Capacity)
            {
                var message = $"Max load {Capacity}kg, cargo is {cargoMass + cargo.Mass}kg";
                Notify(message, new OverfillException(message));
            }
            else if (Temperature*10 < (int)((CargoC)cargo).Product)
            {
                var message = $"Temperature expected {(int)((CargoC)cargo).Product}\u00b0C but is {Temperature}\u00b0C";
                Notify(message, new TemperatureTooLowException(message));
            }
            else
            {
                Cargos.Add(cargo);
                cargo.OnContainer = this;
                Notify("Cargo: " + (CargoC)cargo + " loaded");
            }

        }
    }

    public override void Unload()
    {
        foreach (var cargo in Cargos)
        {
            cargo.OnContainer = null;
        }

        Cargos.Clear();
        Notify("Container unloaded");
        Product = null;
    }

    public void Unload(int cargoNumber)
    {
        if (Cargos.Count == 1)
        {
            Unload();
        }
        else
        {
            Cargos[cargoNumber].OnContainer = null;
            Cargos.RemoveAt(cargoNumber);
            Notify("Cargo: " + (CargoC)Cargos[cargoNumber] + " unloaded");
        }
    }
    
    public override double TotalMass()
    {
        if (Cargos.Count == 0)
        {
            return Weight;
        }
        else
        {
            var totalMass = 0.0;
            foreach (var item in Cargos)
            {
                totalMass += item.Mass;
            }
            return totalMass+Weight;
        }
    }

    public void Notify(string message)
    {
        Console.WriteLine(message);
    }

    public void Notify(string message, Exception ex)
    {
        Console.WriteLine(message);
        throw ex;
    }
}

interface IContainer
{
    void Load(Cargo cargo);
    void Unload();
    double TotalMass();
}

interface IHazardNotifier
{
    void Notify(string message);
}
