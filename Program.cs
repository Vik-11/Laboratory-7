using Задания_1_10;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter path to file with extension .txt for Ex1");
        string path1 = Console.ReadLine();
        TextFile ex1 = new TextFile(path1);
        ex1.RunEx1();
        Console.WriteLine();

        Console.WriteLine("Enter path to file with extension .txt for Ex2");
        string path2 = Console.ReadLine();
        TextFile ex2 = new TextFile(path2);
        ex2.RunEx2();
        Console.WriteLine();

        Console.WriteLine("Enter path to output file with extension .txt for Ex3");
        string path3 = Console.ReadLine();
        TextFile ex3 = new TextFile(path3);
        ex3.RunEx3();

        Console.WriteLine("Enter path to file with extension .txt for Ex4");
        string path4 = Console.ReadLine();
        TextFile ex4 = new TextFile(path4);
        ex4.RunEx4();

        TextFile ex5 = new TextFile();
        ex5.RunEx5();

        TextFile ex6 = new TextFile();
        ex6.RunEx6();

        TextFile ex7 = new TextFile();
        ex7.RunEx7();

        TextFile ex8 = new TextFile();
        ex8.RunEx8();

        Console.WriteLine("Enter path to file with extension .txt for Ex9");
        string path9 = Console.ReadLine();
        TextFile ex9 = new TextFile(path9);
        ex9.RunEx9();

        TextFile ex10 = new TextFile();
        ex10.RunEx10();
    }
}
