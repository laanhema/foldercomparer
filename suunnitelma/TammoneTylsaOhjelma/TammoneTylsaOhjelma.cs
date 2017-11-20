using System;
using System.IO;
using System.Collections.Generic;

/// @author Lauri Makkonen
/// @version 20.11.2017
/// <summary>
/// Ohjelma, mikä vertailee kahden kansion tiedostoja.
/// </summary>
public class TammoneTylsaOhjelma

{
    const string quote = "\"";
    public static void Main()
    {
        string folder1 = "";
        string[] a = Directory.GetLogicalDrives();
        Console.WriteLine(a);
        do
        {
            Console.WriteLine("Write the path of the first folder:");
            Console.WriteLine(@"Example " + quote + @"X:\XXXX\XXXX\XXXX\..." + quote);
            Console.Write(">");
            folder1 = Console.ReadLine();
            if (!Directory.Exists(folder1) == true)
                Console.WriteLine("Invalid path!");
        }
        while (!Directory.Exists(folder1) == true);
        int folder1AmountOfFilesInside = Directory.GetFiles(folder1, "*.*", SearchOption.AllDirectories).Length;
        string[] folder1Array = new string[folder1AmountOfFilesInside];
        string[] folder1ArrayNoPath = new string[folder1AmountOfFilesInside];
        folder1Array = Directory.GetFiles(folder1, "*.*", SearchOption.AllDirectories);

        for (int i = 0; i < folder1ArrayNoPath.Length; i++)
        {
            folder1ArrayNoPath[i] = Path.GetFileName(folder1Array[i]);
            Console.WriteLine(folder1ArrayNoPath[i]);
        }
        Console.WriteLine("");
        Console.WriteLine("Contains " + folder1AmountOfFilesInside + " files.");
        Console.WriteLine("------------------------------------------------");




        string folder2 = "";
        do
        {
            Console.WriteLine("Write the path of the second folder:");
            Console.WriteLine(@"Example " + quote + @"X:\XXXX\XXXX\XXXX\..." + quote);
            Console.Write(">");
            folder1 = Console.ReadLine();
            if (!Directory.Exists(folder2) == true)
                Console.WriteLine("Invalid path!");
        }
        while (!Directory.Exists(folder2) == true);
        int folder2AmountOfFilesInside = Directory.GetFiles(folder2, "*.*", SearchOption.AllDirectories).Length;
        string[] folder2Array = new string[folder2AmountOfFilesInside];
        string[] folder2ArrayNoPath = new string[folder2AmountOfFilesInside];
        folder2Array = Directory.GetFiles(folder1, "*.*", SearchOption.AllDirectories);

        for (int i = 0; i < folder2ArrayNoPath.Length; i++)
        {
            folder2ArrayNoPath[i] = Path.GetFileName(folder2Array[i]);
            Console.WriteLine(folder2ArrayNoPath[i]);
        }
        Console.WriteLine("");
        Console.WriteLine("Contains " + folder2AmountOfFilesInside + " files.");
        Console.WriteLine("------------------------------------------------");









        List<string> list1 = new List<string> {};
        for (int i = 0; i < folder1ArrayNoPath.Length; i++)
        {
            list1.Add(folder1ArrayNoPath[i]);
        }
        List<string> list2 = new List<string> {};
        for (int i = 0; i < folder2Array.Length; i++)
        {
            list2.Add(folder2Array[i]);
        }
        List<string> missingfromA = new List<string> {};
        List<string> missingfromB = new List<string> {};
        for (int i = 0; i < folder1ArrayNoPath.Length; i++)
        {
            if
            (list2.Contains(folder1ArrayNoPath[i]))
            { i++; }
            else
            {
                missingfromB.Add(list1[i]);
                i++;
            }
        }
        for (int i = 0; i < folder2Array.Length; i++)
            {
            if
            (list1.Contains(list2[i]))
            { i++; }
            else
            {
                missingfromA.Add(list2[i]);
                i++;
            }
        }
        Console.WriteLine("Where would you like to create the txt.files?");
        Console.Write(">");
        string c = Console.ReadLine();
        File.WriteAllLines(c + @"\missingfromB.txt", missingfromB);
        File.WriteAllLines(c + @"\missingfromA.txt", missingfromA);

        //Aliohjelma joka etsii folderin (tarvitaan sekä folder 1 sekä 2 mutta samat koodit)

    }
}
