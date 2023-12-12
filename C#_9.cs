using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Mime;

public static class TaskManager
    
{
    private static int x = 0;
    private static int limit_top;
    private static int limit_bottom;


    public static Process ShowProcessList()
    {
        
        Console.WriteLine("                   Выберите процесс:");
        Console.WriteLine("---------------------------------------------------------------------");

        limit_top = x = Console.CursorTop;
        List<Process> processes = Process.GetProcesses().ToList();
        for (int i = 0; i < processes.Count; i++)
        {
            Process process = processes[i];
            try
            {
                
                Console.WriteLine($"   {process.ProcessName} - Оперативка: {process.PagedMemorySize64/1024/1024}мб; Физ память: {process.WorkingSet64/1024/1024}мб");
            }
            catch { processes.RemoveAt(i); }
        }
        

        limit_bottom = processes.Count-1 + limit_top;
        int position = x;
        while (true)
        {
            Console.SetCursorPosition(0, position);
            Console.Write("->");
            ConsoleKey key = Console.ReadKey(true).Key;
            position = Handler(key);
            if (position == -1) return processes[x - limit_top];
            else if (position == -2) return null;
            

        }


    }
    private static int Handler(ConsoleKey key)
    {
        Console.SetCursorPosition(0, x);
        Console.Write("  ");

        if (key == ConsoleKey.UpArrow)
        {
            if (x == limit_top) return x = limit_bottom;
            return --x;
        }
        else if (key == ConsoleKey.DownArrow)
        {
            if (x == limit_bottom) return x = limit_top;
            return ++x;
        }
        else if (key == ConsoleKey.Enter) { return -1; }
        else if(key == ConsoleKey.Escape) { return -2; }
        return x;
    }

    public static int ShowProcessDetails(Process process)
    {
        Console.Clear();
        try
        {
            Console.WriteLine($"Список процессов: {process.ProcessName}");
            Console.WriteLine($"ID: {process.Id}");
            Console.WriteLine($"Использование процессора: {process.TotalProcessorTime}");
            Console.WriteLine($"Использование памяти: {process.WorkingSet64}");
        }
        catch (Exception)
        {
            Console.WriteLine("Доступ к процессу запрещен.");
        }

        while (true)
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.D:return -1;
                case ConsoleKey.Delete: return -2;
                case ConsoleKey.Backspace: return -3;
                default: continue;
            }
        }
        
    }

    public static void TerminateProcess(Process process)
    {
        Console.Clear();
        try
        {
            process.Kill();
            Console.WriteLine("Процесс успешно завершен.");
        }
        catch (Exception)
        {
            Console.WriteLine("Доступ к завершению процесса запрещен.");
        }
        finally { Console.ReadKey(true); }
    }

    public static void TerminateProcessesByName(string processName)
    {
        Console.Clear();
        try
        {
            Process[] processes = Process.GetProcessesByName(processName);
            foreach (Process process in processes)
            {
                process.Kill();
            }
            Console.WriteLine($"Все процессы с именем '{processName}' завершены успешно.");
        }
        catch (Exception)
        {
            Console.WriteLine("Доступ к завершающим процессам запрещен.");
        }
        finally { Console.ReadKey(true); }
    }
}

public enum MenuOption
{
    D = -1,
    Delete = -2,
    Backspace = -3
}

public class Program
{
    public static void Main()
    {


        while (true)
        {
            Console.Clear();
            Process process = TaskManager.ShowProcessList();

            if(process == null) { break; }

            int command = TaskManager.ShowProcessDetails(process);

            switch (command)
            {
                case (int)MenuOption.D:
                    TaskManager.TerminateProcess(process);
                    break;
                case (int)MenuOption.Delete:
                    TaskManager.TerminateProcessesByName(process.ProcessName);
                    break;
                case (int)MenuOption.Backspace:
                    continue;
            }
        }
    }
}
