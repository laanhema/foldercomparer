using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Collections.Generic;

/// @author Lauri Makkonen
/// @version 26.10.2017
/// <summary>
/// Ohjelma, mikä vertailee kahden kansion tiedostoja.
/// </summary>
public class TammoneTylsaOhjelma
{
    /// <summary>
    /// 
    /// </summary>
    public static void Main()
    {
        Console.WriteLine("Write the path of the first folder:");
        Console.Write(">");
        string a = Console.ReadLine();
        Directory.SetCurrentDirectory(a);
        int totalfilesA = Directory.GetFiles(a, "*.*", SearchOption.AllDirectories).Length;
        string[] listA = new string[totalfilesA];
        string[] listANoPath = new string[totalfilesA];
        listA = Directory.GetFiles(a, "*.*", SearchOption.AllDirectories);
        for (int i = 0; i < listA.Length; i++)
        {
            listANoPath[i] = Path.GetFileName(listA[i]);
            Console.WriteLine(listANoPath[i]);
        }
        Console.WriteLine("");
        Console.WriteLine("Contains " + totalfilesA + " files.");
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Write the path of the second folder:");
        Console.Write(">");
        string b = (Console.ReadLine());
        Directory.SetCurrentDirectory(b);
        int totalfilesB = Directory.GetFiles(b, "*.*", SearchOption.AllDirectories).Length;
        string[] listB = new string[totalfilesB];
        listB = Directory.GetFiles(b, "*.*", SearchOption.AllDirectories);
        string[] listBNoPath = new string[totalfilesB];
        for (int i = 0; i < listB.Length; i++)
        {
            listBNoPath[i] = Path.GetFileName(listB[i]);
            Console.WriteLine(listBNoPath[i]);
        }
        Console.WriteLine("");
        Console.WriteLine("Contains " + totalfilesB + " files.");
        Console.WriteLine("------------------------------------------------");

        List<string> list1 = new List<string> {};
        for (int i = 0; i < listANoPath.Length; i++)
        {
            list1.Add(listANoPath[i]);
        }
        List<string> list2 = new List<string> {};
        for (int i = 0; i < listBNoPath.Length; i++)
        {
            list2.Add(listBNoPath[i]);
        }
        int biggest = WhichIsBigger(totalfilesA, totalfilesB);
        List<string> missingfromA = new List<string> {};
        List<string> missingfromB = new List<string> {};
        for (int i = 0; i < listANoPath.Length; i++)
        {
            if
            (list2.Contains(listANoPath[i]))
            { i++; }
            else
            {
                missingfromB.Add(list1[i]);
                i++;
            }
        }
        for (int i = 0; i < listBNoPath.Length; i++)
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


    }
    public static int WhichIsBigger(int totalfilesA, int totalfilesB)
    {
        if (totalfilesA > totalfilesB)
            return totalfilesA;
        else
            return totalfilesB;
    }
}
