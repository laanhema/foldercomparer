using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

/// @author Lauri Makkonen
/// @version 04.12.2017
/// <summary>
/// A program that compares content of two specified folders.
/// </summary>
public class TammoneTylsaOhjelma
{
    public static void Main()
    {
        string folder1Path = "";
        string folderNumber = "first";
        do
        {
            folder1Path = AskPath(folderNumber);
        } while (!ExceptionsAllDirectories(folder1Path) == false);

        StringBuilder[] folder1Array = FilesToArray(folder1Path);


        string folder2Path = "";
        folderNumber = "second";
        do
        {
            folder2Path = AskPath(folderNumber);
        } while (!ExceptionsAllDirectories(folder2Path) == false);

        string[] folder2Array = FilesToArray(folder2Path);

        /*
        for (int i = 0; i < folder1Array.Length; i++)
        {
            foreach (string item in folder1Array)
            {
                if (string.Equals(folder1Array[i], item) == true) { }
                else missingFromFolder2.Add(folder1Array[i]);
            }
        }


        for (int i = 0; i < folder2Array.Length; i++)
        {
            foreach (string item in folder2Array)
            {
                if (string.Equals(folder2Array[i], item) == true) { }
                else missingFromFolder1.Add(folder2Array[i]);
            }
        }
        */

        Console.WriteLine("Comparing folders...");
        List<string> folder1List = new List<string> { };
        foreach (string item in folder1Array)
        {
            folder1List.Add(item);
        }

        List<string> folder2List = new List<string> { };
        foreach (string item in folder2Array)
        {
            folder2List.Add(item);
        }

        List<string> missingFromFolder2 = new List<string> { };
        List<string> missingFromFolder1 = new List<string> { };

        for (int i = 0; i < folder1Array.Length; i++)
        {
            if
            (folder2List.Contains(folder1Array[i])) { }
            else missingFromFolder2.Add(folder1List[i]);
        }

        for (int i = 0; i < folder2Array.Length; i++)
        {
            if
            (folder1List.Contains(folder2List[i])) { }
            else missingFromFolder1.Add(folder2List[i]);

        }

        string textFilesPath = "";
        do
        {
            Console.WriteLine("Where would you like to create the .txt-files?");
            Console.Write(">");
            textFilesPath = Console.ReadLine();

        } while (!InvalidPath(textFilesPath) == false || !ExceptionsTopDirectoryOnly(textFilesPath) == false);
        File.WriteAllLines(textFilesPath + @"\MissingFromFolder2.txt", missingFromFolder2);
        File.WriteAllLines(textFilesPath + @"\MissingFromFolder1.txt", missingFromFolder1);

    }


    /// <summary>
    /// Subprogram that asks the user to write a path where 
    /// </summary>
    /// <param name="folderNumber"></param>
    /// <returns>User's written path</returns>
    public static string AskPath(string folderNumber)
    {
        Directory.SetCurrentDirectory("/");
        string path = "";
        do
        {
            Console.WriteLine("Write the path of the " + folderNumber + " folder:");
            Console.WriteLine(@"Example X:\XXXX\XXXX\XXXX\...");
            Console.Write(">");
            path = Console.ReadLine();

            if (Directory.Exists(path) == false) Console.WriteLine("Invalid path!");

        }
        while (Directory.Exists(path) == false);

        return path;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool ExceptionsAllDirectories(string path)
    {
        try
        {
            Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        }
        catch (UnauthorizedAccessException noAccess)
        {
            Console.WriteLine(noAccess.Message);
            return true;
        }

        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static StringBuilder[] FilesToArray(string path)
    {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Folder content:");
        Console.WriteLine("------------------------------------------------");
        StringBuilder[] pathArray = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        for (int i = 0; i < pathArray.Length; i++)
        {
            pathArray[i] = Path.GetFileName(pathArray[i]);
            Console.WriteLine(pathArray[i]);
        }
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Contains " + pathArray.Length + " files.");
        Console.WriteLine("------------------------------------------------");
        return pathArray;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool InvalidPath(string path)
    {
        if (Directory.Exists(path) == false)
        {
            Console.WriteLine("Invalid path!");
            return true;
        }
        return false;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public static bool ExceptionsTopDirectoryOnly(string path)
    {
        try
        {
            Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
        }
        catch (UnauthorizedAccessException noAccess)
        {
            Console.WriteLine(noAccess.Message);
            return true;
        }

        try
        {
            File.WriteAllText(path + @"\MissingFromFolder1.txt", "test");

        }
        catch (UnauthorizedAccessException noAccess)
        {
            Console.WriteLine(noAccess.Message);
            return true;
        }
        return false;
    }


}

