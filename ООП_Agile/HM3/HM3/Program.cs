namespace HM3;

public class Program
{
    public static void Main()
    {
        var grandPiano = new Piano("Дерево и металл", 88);
        var upright = new Piano("Дерево", 61);
        
        var acousticGuitar = new Guitar("Дерево", 6);
        var bassGuitar = new Guitar("Дерево и металл", 4);
        var electricGuitar = new Guitar("Металл и пластик", 7);

        var instruments = new Instrument[] 
        { 
            grandPiano, 
            upright, 
            acousticGuitar, 
            bassGuitar, 
            electricGuitar 
        };

        foreach (var instrument in instruments)
        {
            Console.WriteLine(instrument.PlayMusic());
        }
    }
}