using System;
using System.IO;
using System.Collections.Generic;

/// @author Lauri Makkonen
/// @version 19.11.2019
/// <summary>
/// A program that compares contents of two specified folders.
/// </summary>
public class FolderComparer
{
    public static void Main()
    {

        /// TODO: 
        /// -   muuta ohjelma käyttämään jotain nopeampaa tietorakennetta (esim. <String>TreeMap)
        ///     taulukon sijasta. Verratessa isompia kansioita suoritukseen kuluu paljon aikaa...
        /// -   "Access to the path 'C:\test.txt' is denied." kuulostaa oudolta
        string folder1Path = "";
        string folderNumber = "first";
        do
        {
            folder1Path = AskPath(folderNumber);
        } while (ExceptionsAllDirectories(folder1Path));

        string[] folder1Array = FilesToArray(folder1Path);

        string folder2Path = "";
        folderNumber = "second";
        do
        {
            folder2Path = AskPath(folderNumber);
        } while (ExceptionsAllDirectories(folder2Path));

        string[] folder2Array = FilesToArray(folder2Path);
        

        Console.WriteLine("Comparing folders...");

        List<string> folder1List = ListFromArray(folder1Array);
        List<string> folder2List = ListFromArray(folder2Array);

        List<string> missingFromFolder2 = CompareFolders(folder1Array, folder1List, folder2List);
        List<string> missingFromFolder1 = CompareFolders(folder2Array, folder2List, folder1List);

        for (int i = 0; i < folder1Array.Length; i++)
        {
            if
            (folder2List.Contains(folder1Array[i])) { }
            else missingFromFolder2.Add(folder1List[i]);
        }

        for (int i = 0; i < folder2Array.Length; i++)
        {
            if
            (folder1List.Contains(folder2Array[i])) { }
            else missingFromFolder1.Add(folder2List[i]);

        }

        string[] folder1PathEnding = folder1Path.Split('\\');
        string[] folder2PathEnding = folder2Path.Split('\\');
        string textFilesPath = "";

        do
        {
            Console.WriteLine("Where would you like to create the .txt-files?");
            Console.Write(">");
            textFilesPath = Console.ReadLine();

        } while (InvalidPath(textFilesPath) || ExceptionsTopDirectoryOnly(textFilesPath));
        File.WriteAllLines(textFilesPath + @"\Missing from " + folder2PathEnding[folder2PathEnding.Length - 1] + ".txt", missingFromFolder2);
        File.WriteAllLines(textFilesPath + @"\Missing from " + folder1PathEnding[folder1PathEnding.Length - 1] + ".txt", missingFromFolder1);
        Console.WriteLine("DONE!");
    }


    /// <summary>
    /// Subprogram that asks the user to write a path to the folder which he or she wants to examine. 
    /// It reads the user's input and makes sure the path exists before returning it to the main program. 
    /// If the path doesn't exist subprogram asks the user to input a new path. 
    /// </summary>
    /// <param name="folderNumber">
    /// Changes what subprogram writes before user input
    /// For ex. "first" makes it write "Write the path of the first folder:"
    /// </param>
    /// <returns>Path to a folder</returns>
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
    /// Subprogram that checks if exceptions occur when trying to examine a certain folder and all it's subdirectories.
    /// </summary>
    /// <param name="path">Path to a folder</param>
    /// <returns>
    /// "True" when exceptions occur
    /// "False" when no exceptions occur
    /// </returns>
    public static bool ExceptionsAllDirectories(string path)
    {
        try
        {
            Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return true;
        }

        return false;
    }


    /// <summary>
    /// Subprogram that gets all files under a folder and all it's subdirectories and then puts all of those files in an array. 
    /// Also prints the content of the folders.
    /// </summary>
    /// <param name="path">Path to a folder</param>
    /// <returns>Array of the files under a folder and it's subdirectories</returns>
    public static string[] FilesToArray(string path)
    {
        Console.WriteLine("------------------------------------------------");
        Console.WriteLine("Folder content:");
        Console.WriteLine("------------------------------------------------");
        string[] pathArray = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
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
    /// Subprogram that returns "True" if a certain folder path doesn't exist.
    /// </summary>
    /// <param name="path">Path to a folder</param>
    /// <returns>
    /// "True" when path doesn't exist
    /// "False" when path exists
    /// </returns>
    /// <example>
    /// <pre name="test">
    /// string path = "kissa";
    /// FolderComparer.InvalidPath(path) === true;
    /// path = "X:\\kissa\\koira\\kana";
    /// FolderComparer.InvalidPath(path) === true;
    /// </pre>
    /// </example>
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
    /// Subprogram that checks if exceptions occur when trying to examine a certain folder path. 
    /// (Only the very top directory)
    /// In case of exception subprogram prevents the mainprogram from crashing and tells the user what's wrong. 
    /// </summary>
    /// <param name="path">Path of a folder</param>
    /// <returns>
    /// "True" when exceptions occur
    /// "False" when no exceptions occur
    /// </returns>
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
            File.WriteAllText(path + @"\test.txt", "test");
            File.Delete(path + @"\test.txt");
        }
        catch (UnauthorizedAccessException noAccess)
        {
            Console.WriteLine(noAccess.Message);
            return true;
        }

        return false;
    }


    /// <summary>
    /// Subprogram that makes a list based on the array it was given. 
    /// </summary>
    /// <param name="array">Array from which the list is made</param>
    /// <returns>A list</returns>
    public static List<string> ListFromArray(string[] array)
    {
        List<string> list = new List<string> { };
        foreach (string item in array)
        {
            list.Add(item);
        }
        return list;
    }


    /// <summary>
    
    /// </summary>
    /// <param name="folderXArray"></param>
    /// <param name="folder1List"></param>
    /// <param name="folder2List"></param>
    /// <returns></returns>
    public static List<string> CompareFolders(string[] folderXArray, List<string> folder1List, List<string> folder2List)
    {

        List<string> missingFromFolderX = new List<string> { };
        for (int i = 0; i < folderXArray.Length; i++)
        {
            if (!folder2List.Contains(folderXArray[i])) missingFromFolderX.Add(folder1List[i]);
        }
        return missingFromFolderX;
    }

        



}

