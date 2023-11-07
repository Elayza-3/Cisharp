using System;
using System.Collections.Generic;
using System.IO;

public class CakeOrder
{
    private string shape;
    private string size;
    private string flavor;
    private int quantity;
    private string glaze;
    private string decor;

    public void ChooseCake()
    {
        //Console.WriteLine("Выберите форму торта:");
        shape = ArrowMenu.ChooseMenuItem(new List<string> { "Квадрат", "Круг" }, "Выберите форму торта:");

        //Console.WriteLine("Выберите размер торта:");
        size = ArrowMenu.ChooseMenuItem(new List<string> { "Маленький", "Средний", "Большой" }, "Выберите размер торта:");

        //Console.WriteLine("Выберите вкус торта:");
        flavor = ArrowMenu.ChooseMenuItem(new List<string> { "Шоколадный", "Ванильный", "Фруктовый" }, "Выберите вкус торта:");

        Console.WriteLine("Выберите количество тортов:");
        quantity = int.Parse(Console.ReadLine());

        //Console.WriteLine("Выберите глазурь для торта:");
        glaze = ArrowMenu.ChooseMenuItem(new List<string> { "Шоколадная", "Ванильная", "Фруктовая" }, "Выберите глазурь для торта:");

        //Console.WriteLine("Выберите декор для торта:");
        decor = ArrowMenu.ChooseMenuItem(new List<string> { "Цветы", "Фрукты", "Шоколадные украшения" }, "Выберите декор для торта:");

        SaveOrderToFile();
    }

    private void SaveOrderToFile()
    {
        string orderDetails = $"Форма: {shape}, Размер: {size}, Вкус: {flavor}, Количество: {quantity}, Глазурь: {glaze}, Декор: {decor}";
        File.WriteAllText("order.txt", orderDetails);
    }

    public static void ShowTotalOrder()
    {
        string orderDetails = File.ReadAllText("order.txt");
        Console.WriteLine("Итоговый заказ:");
        Console.WriteLine(orderDetails);
    }
}

public static class ArrowMenu
{
    public static string ChooseMenuItem(List<string> menuItems,string msg)
    {
        int selectedIndex = 0;
        while (true)
        {
            Console.Clear();
            Console.WriteLine(msg);
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (i == selectedIndex)
                {
                    Console.Write("-> ");
                }
                else
                {
                    Console.Write("   ");
                }
                Console.WriteLine(menuItems[i]);
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
            {
                return menuItems[selectedIndex];
            }
            else if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                selectedIndex--;
                if (selectedIndex < 0)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                selectedIndex++;
                if (selectedIndex >= menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        bool ordering = true;
        while (ordering)
        {
            CakeOrder cakeOrder = new CakeOrder();
            cakeOrder.ChooseCake();

            Console.WriteLine("Заказ оформлен!");
            CakeOrder.ShowTotalOrder();

            Console.WriteLine("Хотите сделать еще один заказ? (Да/Нет)");
            string answer = Console.ReadLine();
            if (answer.ToLower() != "да")
            {
                ordering = false;
            }
        }
    }
}
