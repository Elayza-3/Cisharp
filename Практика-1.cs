using System;


namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            int choice;
            double num1, num2, result;
            do
            {
                Console.WriteLine("Выберите операцию:");
                Console.WriteLine("1. Сложение");
                Console.WriteLine("2. Вычитание");
                Console.WriteLine("3. Умножение");
                Console.WriteLine("4. Деление");
                Console.WriteLine("5. Возведение в степень");
                Console.WriteLine("6. Квадратный корень");
                Console.WriteLine("7. 1% от числа");
                Console.WriteLine("8. Факториал");
                Console.WriteLine("9. Выход");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("Введите первое число: ");
                        num1 = double.Parse(Console.ReadLine());
                        Console.Write("Введите второе число: ");
                        num2 = double.Parse(Console.ReadLine());
                        result = num1 + num2;
                        Console.WriteLine($"Результат: {result}");
                        break;
                    case 2:
                        Console.Write("Введите первое число: ");
                        num1 = double.Parse(Console.ReadLine());
                        Console.Write("Введите второе число: ");
                        num2 = double.Parse(Console.ReadLine());
                        result = num1 - num2;
                        Console.WriteLine($"Результат: {result}");
                        break;
                    case 3:
                        Console.Write("Введите первое число: ");
                        num1 = double.Parse(Console.ReadLine());
                        Console.Write("Введите второе число: ");
                        num2 = double.Parse(Console.ReadLine());
                        result = num1 * num2;
                        Console.WriteLine($"Результат: {result}");
                        break;
                    case 4:
                        Console.Write("Введите первое число: ");
                        num1 = double.Parse(Console.ReadLine());
                        Console.Write("Введите второе число: ");
                        num2 = double.Parse(Console.ReadLine());
                        if (num2 == 0)
                        {
                            Console.WriteLine("Ошибка: деление на ноль");
                        }
                        else
                        {
                            result = num1 / num2;
                            Console.WriteLine($"Результат: {result}");
                        }
                        break;
                    case 5:
                        Console.Write("Введите число: ");
                        num1 = double.Parse(Console.ReadLine());
                        Console.Write("Введите степень: ");
                        int power = int.Parse(Console.ReadLine());
                        result = Math.Pow(num1, power);
                        Console.WriteLine($"Результат: {result}");
                        break;
                    case 6:
                        Console.Write("Введите число: ");
                        num1 = double.Parse(Console.ReadLine());
                        if (num1 < 0)
                        {
                            Console.WriteLine("Ошибка: квадратный корень из отрицательного числа");
                        }
                        else
                        {
                            result = Math.Sqrt(num1);
                            Console.WriteLine($"Результат: {result}");
                        }
                        break;
                    case 7:
                        Console.Write("Введите число: ");
                        num1 = double.Parse(Console.ReadLine());
                        result = num1 / 100;
                        Console.WriteLine($"Результат: {result}");
                        break;
                    case 8:
                        Console.Write("Введите число: ");
                        num1 = double.Parse(Console.ReadLine());
                        result = 1;
                        for (int i = 2; i <= num1; i++)
                        {
                            result *= i;
                        }
                        Console.WriteLine($"Результат: {result}");
                        break;
                    case 9:
                        Console.WriteLine("Выход");
                        break;
                    default:
                        Console.WriteLine("Ошибка: неверный выбор операции");
                        break;
                }
            } while (choice != 9);
        }
    }
}