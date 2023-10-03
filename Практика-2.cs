
using System;

class Program
{
    static void Main(string[] args)
    
    {
        while (true)
        {
            Console.WriteLine("Выберите программу для запуска:");
            Console.WriteLine("1. Программа 1");
            Console.WriteLine("2. Программа 2");
            Console.WriteLine("3. Программа 3");
            Console.WriteLine("4. Выйти");

            int choice = Convert.ToInt32(Console.ReadLine());

            if (choice == 1)
            {
                RunProgram1();
            }
            else if (choice == 2)
            {
                RunProgram2();
            }
            else if (choice == 3)
            {
                RunProgram3();
            }
            else if(choice == 4)
            {
                break;
            }
            {
                Console.WriteLine("Такой команды нет"); 
                
            }



        }

    
    }

    private static void RunProgram1()
    {
        Random random = new Random();
        int random_number = random.Next(0, 101);
        Console.WriteLine("Угадай число от 0 до 100 (включительно)");
        int user_number = Convert.ToInt32(Console.ReadLine());

        while(true)
        {
            if (user_number>random_number)
            {
                Console.WriteLine("Надо меньше");
            }
            else if (user_number<random_number)
            {
                Console.WriteLine("Надо больше");
            }
            else
            {
                Console.WriteLine("Ебать попадание");
                break;
            }
            user_number = Convert.ToInt32(Console.ReadLine());
        }
    }

    private static void RunProgram2()
    {
        int[,] table = new int[9,9];
        for(int number_row = 1;number_row<=9;number_row++) { 
            for(int number_col = 1; number_col<=9; number_col++)
            {
                table[number_row-1, number_col-1]= number_row*number_col;
            }
        }
        for(int row = 0; row<9; row++)
        {
            for (int col = 0; col<9; col++)
            {
                Console.Write($"{table[row,col]}\t");
            }
            Console.WriteLine();
        }
        
        
    }

    private static void RunProgram3()
    {
        int number = Convert.ToInt32(Console.ReadLine());
        for(int counter = 1; counter<=number; counter++)
        {
            if (number%counter==0)
            {
                Console.Write($"{counter}  ");
            }
        }
        Console.WriteLine();
    }
}
