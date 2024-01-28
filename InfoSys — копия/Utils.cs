using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfoSys
{
    static class JSON
    {
        static public T Deserialazer<T>(string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = File.ReadAllText(desktop + "\\" + filename);

            T obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        static public void Serializer<T>(T obj, string filename)
        {
            string desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string json = JsonConvert.SerializeObject(obj);

            File.WriteAllText(desktop + "\\" + filename, json);
        }
    }

    public enum KeyBoard //храним значение Escape клавиши
    {
        F1 = -1,
        F2 = -2,
        F5 = -5,
        F10 = -10,
        Delete = -20,
        Escape = -21,
        Backspace = -22,
    }

    public enum Roles
    {
        Admin,
        Manager,
        WarehouseManager,
        Cashier,
        Accountant,
    }


    static class Arrow
    {
        static private int top;
        static private int bottom;

        static public int Bottom { get { return bottom; } }
        static public int Top { get { return top; } }

        static public void SetLimit(int Top,int Bottom)
        {
            top = Top;
            bottom = Bottom;

        }
        static public void Init(Action action)
        {
            top = Console.CursorTop;

            action();

            bottom = Console.CursorTop - 1;
        }
        static public int SetPos(int position) //Стрелочное меню между верхней (top) границей и нижней (bottom)
        {
            int pos = (top <= position && position <= bottom) ? position : top;

            while (true)
            {
                Console.SetCursorPosition(0, pos);
                Console.WriteLine("->");

                ConsoleKey key = Console.ReadKey(true).Key;

                Console.SetCursorPosition(0, pos);
                Console.WriteLine("  ");

                if (key == ConsoleKey.DownArrow)
                {
                    if (pos == bottom) pos = top;
                    else ++pos;
                    continue;
                }
                else if (key == ConsoleKey.UpArrow)
                {
                    if (pos == top) pos = bottom;
                    else --pos;
                    continue;
                }
                else if (key == ConsoleKey.Enter) return pos - top;

                else if (key == ConsoleKey.F1) return -1;
                else if (key == ConsoleKey.F2) return -2;
                else if (key == ConsoleKey.F5) return -5;
                else if (key == ConsoleKey.F10) return -10;
                else if (key == ConsoleKey.Delete) return -20;
                else if (key == ConsoleKey.Escape) return -21;
                else if(key == ConsoleKey.Backspace) return -22;
                
            }
        }

        static public string Type(int left, string input = "", string mask = null) //Позволяет печатать значения в поле ввода, через label
        {
            Console.CursorTop -= 1;
            Console.CursorLeft = left + input.Length;

            ConsoleKeyInfo key;
            while ((key = Console.ReadKey(true)).Key != ConsoleKey.Enter)
            {
                if (key.Key == ConsoleKey.Backspace)
                {
                    if (input.Length != 0)
                    {
                        Console.Write("\b \b");
                        input = input.Substring(0, input.Length - 1);
                    }

                    continue;
                }

                if (mask == null) Console.Write(key.KeyChar.ToString());
                else Console.Write(mask); //Если указана маска, то вместо символа печаем ее

                input += $"{key.KeyChar}";

            }
            Console.CursorLeft = 0;
            return input;


        }
    }

    static class Utils
    {
        static public string GetRole(int role)
            {
                switch (role)
                {
                    case (int)Roles.Admin: return "Admin";
                    case (int)Roles.Manager: return "Manager";
                    case (int)Roles.WarehouseManager: return "WarehouseManager";
                    case (int)Roles.Cashier: return "Cashier";
                    default: return "Accountant";
                }
            }
    }
    
}

