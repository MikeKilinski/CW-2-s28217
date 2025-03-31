
// See https://aka.ms/new-console-template for more information

using Zjazd2s;
using Zjazd2s.UIUtils;

class Program
{
    public static List<Container> ContainersInPort {get;} = new List<Container>();
    public static List <Ship> ShipsInPort {get;}= new List<Ship>();
     
     
     
    static void Main()
    {
        var cointenerL1 = new ContainerL(259.1, 243.8, 605.8, 1270.0);
        var cointenerL2 = new ContainerL(259.1, 243.8, 605.8, 1270.0);
        var cointenerL3 = new ContainerL(259.1, 243.8, 605.8, 1270.0);
        var cointenerG1 = new ContainerG(259.1, 243.8, 605.8, 1270.0);
        var cointenerG2 = new ContainerG(259.1, 243.8, 605.8, 1270.0);
        var cointenerG3 = new ContainerG(259.1, 243.8, 605.8, 1270.0);
        var containerC1 = new ContainerC(259.1, 243.8, 605.8, 1270.0, 28000, 0.0);
        var containerC2 = new ContainerC(259.1, 243.8, 605.8, 1270.0, 28000, 5.5);
        var containerC3 = new ContainerC(259.1, 243.8, 605.8, 1270.0, 28000, -100.0);
        
        ContainersInPort.Add(cointenerL1);
        ContainersInPort.Add(cointenerL2);
        ContainersInPort.Add(cointenerL3);
        ContainersInPort.Add(cointenerG1);
        ContainersInPort.Add(cointenerG2);
        ContainersInPort.Add(cointenerG3);
        ContainersInPort.Add(containerC1);
        ContainersInPort.Add(containerC2);
        ContainersInPort.Add(containerC3);

        var ship1 = new Ship("Lady", 22.5, 200000.0, 20000);
        var ship2 = new Ship("Captain", 22.5, 20000.0, 2000);
        var ship3 = new Ship("Lieutenant", 7, 500.0, 50);
        var ship4 = new Ship("General", 10, 100.0, 10);
        var ship5 = new Ship("Colonel", 15, 20.0, 2);
        
        ShipsInPort.Add(ship1);
        ShipsInPort.Add(ship2);
        ShipsInPort.Add(ship3);
        ShipsInPort.Add(ship4);
        ShipsInPort.Add(ship5);
        
        StartScreen.Go();
        Console.ReadLine();
        Console.Clear();
        MainMenu.Go();
        Console.Clear();
    }
    
    
    
    


}