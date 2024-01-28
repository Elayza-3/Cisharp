using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using InfoSys;

namespace InfoSys
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Authorization.HI();
            Authorization.Login();
        }
    }
}


static class Authorization
{
    public static string username = "";
    public static string password = "";
    public static string post = "";

    public static Dictionary<int, string> data = new Dictionary<int, string>()
    {
        [0] = "  логин: ",
        [1] = "  пароль: ",
        [2] = "  авторизоваться"
    };

    static public void Login() //Авторизация пользователя
    {
        

        Arrow.Init(() => {foreach (string item in data.Values)
        {
            Console.WriteLine(item);
        } });

        do
        {


            int ind = Arrow.SetPos( Console.CursorTop);
            switch (ind)
            {
                case 0:
                    username = Arrow.Type(data[ind].Length, username); break;
                case 1:
                    password = Arrow.Type(data[ind].Length, password, "*"); break;
                case 2:
                    authorization(); break;
                case (int)KeyBoard.Escape:
                    return;
            }
        }
        while (true);
    }


    static public void authorization()
    {
        Dictionary<string, User> users = JSON.Deserialazer<Dictionary<string, User>>("users.json");
        Dictionary<string,Worker> workers = JSON.Deserialazer<Dictionary<string, Worker>>("employee.json");

        KeyValuePair<string,User> pair= users.Where(x=> x.Value.name == username && x.Value.password == password).FirstOrDefault();

        if (pair.Value != null)
        {
            User user = pair.Value;

                try
                {
                    Worker worker = workers.Where(x => x.Value.userID == user.ID).First().Value;
                    post = worker.post;
                    
                }
                catch { }

                switch (user.role)
                {
                    case (int)Roles.Admin: new Admin(pair.Key);break;
                    case (int)Roles.Manager: new Admin(pair.Key); break;
                    case (int)Roles.WarehouseManager: new Admin(username); break;
                    case (int)Roles.Cashier: new Admin(username); break;
                    case (int)Roles.Accountant: new Admin(username); break;
                }

                Console.Clear();
                username = password = post = "";
                HI();

                Arrow.Init(() => { foreach (string item in data.Values)
                    {
                        Console.WriteLine(item);
                    }
                });
                return;
            
        }
        Console.WriteLine("\nError authorization(((");

    }

    static public void HI()
    {
        Console.WriteLine($"Wellcome\t\t{(username!= "" ? username : "anonymous")}\t\t\t{(post != "" ? $"Post : {post}" : post)}");
        Console.WriteLine(string.Concat(Enumerable.Repeat("-", Console.WindowWidth-1)));//во всю ширину окна печатаем "-"
    }
}


