using Zjazd2s.UIUtils;

namespace Zjazd2s;

public class Ship (string name, double maxSpeed, double maxLoadMass, int maxLoadQnty)
{
    public static int NextNumber = 1;
    public string RegistryNo { get; set; } = "FS-" + NextNumber++;
    public string Name { get; set; } = name;
    public double MaxSpeed { get; set; } = maxSpeed;
    public double MaxLoadMass { get; set; } = maxLoadMass;
    public int MaxLoadQnty { get; set; } = maxLoadQnty;
    public List<Container> Containers { get; set; } = new List<Container>();

    public void Load(Container container)
    {
        if (MaxLoadQnty <= (Containers.Count))
        {
            var mssg = $"Max load {MaxLoadQnty} containers already reached";
            Notify(mssg, new ShipOverloadException(mssg));
        }
        else
        {
            var massLeft = MaxLoadMass*1000;
            foreach (var item in Containers)
            {
                massLeft -= item.TotalMass();
            }
            if (massLeft < container.TotalMass())
            {
                var mssg = $"there is {massLeft}kg left in ship. Container is {container.TotalMass()}kg";
                Notify(mssg, new ShipOverloadException(mssg));
            }
            else
            {
                massLeft = MaxLoadMass;
                foreach (var item in Containers)
                {
                    massLeft -= item.TotalMass()/1000;
                }
                Containers.Add(container);
                container.OnShip = this;
                Notify("Container: " + container + " loaded \n" +
                       $"{MaxLoadQnty - Containers.Count} more can be loaded \n"+
                       $"{massLeft}T capacity left");
            }

        }
    }

    public void Load(List<Container> containersToLoad)
    {
        var massTotal = 0.0;
        foreach (var item in containersToLoad)
        {
            massTotal += item.TotalMass();
        }


        var massLeft = MaxLoadMass * 1000 ;
        
        foreach (var item in Containers)
        {
            massLeft -= item.TotalMass();
        }

        if (massLeft < massTotal)
        {
            var mssg = $"there is {massLeft}kg left in ship. Containers are together {massTotal}kg";
            Notify(mssg, new ShipOverloadException(mssg));
        }
        else if (containersToLoad.Count > MaxLoadQnty - Containers.Count)
        {
            var mssg = $"Max load left {MaxLoadQnty - Containers.Count} containers. Can't load {containersToLoad.Count} more";
            Notify(mssg, new ShipOverloadException(mssg));
        } else 
        {
            massLeft = MaxLoadMass;
            foreach (var item in Containers)
            {
                massLeft -= item.TotalMass()/1000;
            }
            Containers.AddRange(containersToLoad);
            foreach (var item in containersToLoad)
            {
                item.OnShip = this;
            }

            Notify("Containers: " + containersToLoad + " loaded \n" +
                   $"{MaxLoadQnty - Containers.Count} more can be loaded \n"+
                   $"{massLeft}T capacity left");
        }
    }

    public void Unload()
    {
        foreach (var item in Containers)
        {
            item.OnShip = null;
        }

        Containers.Clear();
        Notify("Ship unloaded");
    }

    public Container Unload(int containerNumber)
    {
        if (Containers.Count < containerNumber + 1)
        {
            var mssg = "There is no such a notainer";
            Notify(mssg, new NoSuchContainerException(mssg));
            return default;
        }
        
        {
            var container = Containers[containerNumber];
            container.OnShip = null;
            Containers.RemoveAt(containerNumber);
            Notify("Container: " + container + " unloaded");
            return container;
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
    public override string ToString()
    {
        return RegistryNo;
    }

    public string ToString(bool full = false)
    {
        if (full)
        {
            var massLeft = MaxLoadMass - Containers.Sum(x => x.TotalMass()/1000);
            return $"{RegistryNo}, {Name}, {Containers.Count}/{MaxLoadQnty} containers, {massLeft}/{MaxLoadMass}T capacity left";
        }
        else
        {
            return ToString();
        }
    }

    public void ShowCargo()
    {
        var i = 1;
        foreach (var item in Containers)
        {
            var mssg = $"{i}. {item}";
            Notify(mssg);
            i++;
        }
    }

    public double ShowCargoMass()
    {
        if (Containers.Count != 0)
        {
            return Containers.Sum(x => x.TotalMass() / 1000);
        } 
        return 0;
    }

}