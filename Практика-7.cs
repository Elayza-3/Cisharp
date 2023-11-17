using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
namespace ConsoleTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ArrowMenu.ChooseFile();

        }
    }
}


public static class FileExplorer

{
    private static string CurrentPath = null;
    public static List<string[]> Content(string path=null)
    {
        CurrentPath = path;
        switch(path)
        {

            case null: {
                    return ShowDrives(); 
                }
            default: {
                    if (!File.GetAttributes(path).HasFlag(FileAttributes.Directory)) { 
                        OpenFile(path);
                        CurrentPath = NavigateUp();
                    }

                    return ShowDirectoryContent(path); }
        }
    }

    public static List<string[]> ShowDrives()
    {
        List<string[]> list = new List<string[]>();
        foreach (DriveInfo d in DriveInfo.GetDrives())
        {

            list.Add(new string[] { $"  {d.Name}\tTotal available space: {(d.TotalFreeSpace / 1024 / 1024 / 1024)} GB\tTotal size of drive: {(d.TotalSize / 1024 / 1024 / 1024)} GB" ,d.Name});
        }
        return list;
    }

    public static List<string[]> ShowDirectoryContent(string path)
    {
        
        List<string[]> list = new List<string[]>();
        try
        {
            foreach (string item in Directory.GetDirectories(path))
            {

                list.Add(new string[] { $"  {Path.GetFileName(item)}\t|time_create:{Directory.GetCreationTime(item)}|", item });
            }
        }
        catch { }
        foreach (string item in Directory.GetFiles(path))
        {
            list.Add(new string[] { $"  {Path.GetFileName(item)}\t|time_create:{File.GetCreationTime(item)}|\textension:{Path.GetExtension(item)}|", item});
        }
        return list;
        
    }

    private static void OpenFile(string filePath)
    {
        try
        {
            Process.Start(filePath);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public static string NavigateUp()
    {
        try
        {
            DirectoryInfo directory = Directory.GetParent(CurrentPath);
            if (directory != null)
            {

                return directory.FullName;
            }
        }
        catch { }
        return null;
    }
}


public static class ArrowMenu
{
    public static void ChooseFile()
    {
        List<string[]> files = FileExplorer.Content(null);
        int selectedIndex = 0;
        while (true)
        {

            Console.Clear();
            for (int i = 0; i < files.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.Write("-> ");
                }
                else
                {
                    Console.Write("   ");
                }

                Console.WriteLine(files[i][0]);
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                files = FileExplorer.Content(files[selectedIndex][1]);
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = files.Count - 1;
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedIndex++;
                if (selectedIndex >= files.Count)
                {
                    selectedIndex = 0;
                }
            }
            else if(keyInfo.Key == ConsoleKey.LeftArrow)
            {
                files = FileExplorer.Content(FileExplorer.NavigateUp());
            }
            else if (keyInfo.Key == ConsoleKey.Escape) return;
        }
    }
}