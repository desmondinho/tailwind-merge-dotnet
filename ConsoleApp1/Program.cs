using TailwindMerge;

public class Program
{
    public static void Main( string[] args )
    {
        Console.ReadKey();

        var result = new TwMerge().Merge( "bg-red-500 bg-green-300" );
        Console.WriteLine( result );

        Console.ReadKey();

        //var summary = BenchmarkRunner.Run<Benchmark>();
    }
}
