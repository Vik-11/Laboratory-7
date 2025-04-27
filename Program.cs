using Задания_1_10;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Enter path to file with extension .txt for Ex1");
        string path1 = TextFile.IsPathValid();
        TextFile.RunEx1(path1);
        Console.WriteLine();

        Console.WriteLine("Enter path to file with extension .txt for Ex2");
        string path2 = TextFile.IsPathValid();
        TextFile.RunEx2(path2);
        Console.WriteLine();

        Console.WriteLine("Enter path to output file with extension .txt for Ex3");
        string path3 = TextFile.IsPathValid();
        TextFile.RunEx3(path3);

        Console.WriteLine("Enter path to file with extension .txt for Ex4");
        string path4 = TextFile.IsPathValid();
        TextFile.RunEx4(path4);

        Console.WriteLine("Enter path to file with extension .txt for Ex5");
        string path5 = TextFile.IsPathValid();
        TextFile.RunEx5(path5);

        TextFile.RunEx6();

        TextFile.RunEx7();

        TextFile.RunEx8();

        Console.WriteLine("Enter path to file with extension .txt for Ex9");
        string path9 = TextFile.IsPathValid();
        TextFile.RunEx9(path9);

        Console.WriteLine("Enter path to file with extension .txt for Ex10");
        string path10 = TextFile.IsPathValid();
        TextFile.RunEx10(path10);
    }
}