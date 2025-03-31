namespace Zjazd2s;

public class NewClass(string name, int age)
{
    public string Name { get; set; } = name;
    public int Age { get; set; } = age;

    public override string ToString()
    {
        return name + " " + age;
    }
}