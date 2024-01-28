using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InfoSys
{
    

    internal interface CRUD
    {
        void Create();
        void Read(string id);
        void Update(string id);
        void Delete(string id);
        dynamic Search();
    }

    public class Admin : CRUD 
    {
        static private Dictionary<string, User> users;
        
        static private Dictionary<int, string> data = new Dictionary<int, string>()
        {
            [0] = "  ID: ",
            [1] = "  Логин: ",
            [2] = "  Пароль: ",
            [3] = "  Роль: "
        };

        private string username;
        private Worker worker;


        public Admin(string id)
        {
            
            users = JSON.Deserialazer<Dictionary<string, User>>("users.json");
            this.username = users[id].name;
            this.worker = GetUser(Convert.ToInt32(id));
            All();
            
            
        }

        public void All()
        {
            while (true)
            {
                Console.Clear();
                Authorization.HI();
                Console.WriteLine("{0,15} {1,25} {2,25} {3,25}", "ID", "Login", "Password", "Role");


                Arrow.Init(() =>
                {
                    foreach (string item in users.Keys)
                    {
                        Console.WriteLine("{0,15} {1,25} {2,25} {3,25}", item, users[item].name, users[item].password, Utils.GetRole(users[item].role));
                    }
                });

                int ind = Arrow.SetPos(Console.CursorTop);
                try
                {
                    Read(users.ElementAt(ind).Key);
                    continue;
                }
                catch { }

                switch (ind)
                {
                    case (int)KeyBoard.F1: Create(); break;
                    case (int)KeyBoard.F2: users = Search(); break;
                    case (int)KeyBoard.Backspace: users = JSON.Deserialazer<Dictionary<string, User>>("users.json");break;
                    case (int)KeyBoard.Escape:
                        return;
                }
            }
        }

        public void Create() { 
            Console.Clear();
            Arrow.Init(() =>
            {
                foreach (string item in data.Values)
                {
                    Console.WriteLine(item);
                }
            });
            string name = "";
            string ID = "";
            string password = "";
            string role = "";
            while (true)
            {
                int ind = Arrow.SetPos(Console.CursorTop);
                switch (ind)
                {
                    case 0:
                        ID = Arrow.Type(data[ind].Length, ID); break;
                    case 1:
                        name = Arrow.Type(data[ind].Length, name); break;
                    case 2:
                        password = Arrow.Type(data[ind].Length, password); break;
                    case 3:
                        role = Arrow.Type(data[ind].Length, role); break;
                    case (int)KeyBoard.Escape:
                        return;
                    case (int)KeyBoard.F5:
                        try
                        {
                            Check(name, Convert.ToInt32(ID));

                            User new_user = new User(Convert.ToInt32(ID), name, password, Convert.ToInt32(role));
                            users[$"{new_user.ID}"] = new_user;
                            JSON.Serializer(users, "users.json");
                            return;
                        }
                        catch { 
                            Console.CursorTop = Arrow.Bottom+2;
                            Console.WriteLine("Error");
                            };
                        break;
                }
            }            
        }

        public void Delete(string id)
        {
            users.Remove(id);
            JSON.Serializer(users,"users.json");
        }



        public void Read(string id)
        {
            Console.Clear();
            User admin = users[id];
            Arrow.Init(() => { admin.STR(); });
            while (true)
            {
                int ind = Arrow.SetPos(-1);
                switch (ind)
                {
                    case (int)KeyBoard.Delete: Delete(id);return;
                    case (int)KeyBoard.F10: Update(id);break;
                    case (int)KeyBoard.Escape: return;
                }
            }
        }

        public dynamic Search()
        {
            Console.Clear();
            Arrow.Init(() =>
            {
                foreach (string item in data.Values)
                {
                    Console.WriteLine(item);
                }
            });
            int end;
            int ind = Arrow.SetPos(end = Console.CursorTop);
            Console.CursorTop = end + 3;
            Console.WriteLine("Enter search value: ");
            string value = Console.ReadLine();
            switch (ind)
            {
                case 0: return users.Where(x => x.Value.ID == Convert.ToInt32(value)).ToDictionary(p=> p.Key,p=>p.Value) ;
                case 1:return users.Where(x => x.Value.name == value).ToDictionary(p => p.Key, p => p.Value);
                case 2:return users.Where(x => x.Value.password == value).ToDictionary(p => p.Key, p => p.Value);
                case 3:return users.Where(x => x.Value.role == Convert.ToInt32(value)).ToDictionary(p => p.Key, p => p.Value);
                default: return users;
            }

        }

        public void Update(string id)
        {
            User admin = users[id];
            while (true)
            {
                try
                {
                    int ind = Arrow.SetPos(Console.CursorTop);
                    switch (ind)
                    {
                        case 0:
                            admin.ID = Convert.ToInt32(Arrow.Type(6, $"{admin.ID}")); break;
                        case 1:
                            admin.name = Arrow.Type(8, admin.name); break;
                        case 2:
                            admin.password = Arrow.Type(12, admin.password); break;
                        case 3:
                            admin.role = Convert.ToInt32(Arrow.Type(8, $"{admin.role}")); break;
                        case (int)KeyBoard.Escape: return;
                        case (int)KeyBoard.F5:
                            Check(admin.name,admin.ID,true);

                            users[$"{admin.ID}"] = admin;
                            JSON.Serializer(users, "users.json");
                            return;
                    }
                }
                catch {
                    Console.CursorTop = Arrow.Bottom + 2;
                    Console.WriteLine("Error");
                }
            }
        }

        static void Check(string name, int id, bool exist = false)
        {


            if (users.Select(x => x.Value.name).ToList().Count(x => x == name) <= (exist? 1:0) &&
                users.Select(x => x.Value.ID).ToList().Count(x => x == id) <= (exist ? 1 : 0)) return;
            throw new Exception();
        }

        static Worker GetUser(int id)
        {
            return JSON.Deserialazer<Dictionary<string, Worker>>("employee.json").Where(x => x.Value.userID == id).FirstOrDefault().Value;
        }
    }



    /*public class Manager : CRUD 
    {
        static private Dictionary<string, Worker> workers;
        static private Dictionary<string, User> users;

        private string username;
        private string post;

        static private Dictionary<int, string> data = new Dictionary<int, string>()
        {
            [0] = "  ID: ",
            [1] = "  surname: ",
            [2] = "  name: ",
            [3] = "  lastname: ",
            [4] = "  birthday: ",
            [5] = "  pasport: ",
            [6] = "  post: ",
            [7] = "  salary: ",
            [8] = "  userID: "
        };



        public Manager(string id)
        {
            this.username = username;
            workers = JSON.Deserialazer<Dictionary<string, Worker>>("employee.json");
            users = JSON.Deserialazer<Dictionary<string, User>>("users.json");

            All();


        }

        public void All()
        {
            while (true)
            {
                Console.Clear();
                Authorization.HI();
                Console.WriteLine("{0,15} {1,25} {2,25} {3,25} {4,15}", "ID", "Surname", "Name", "Lastname","Post");


                Arrow.Init(() =>
                {
                    foreach (string item in workers.Keys)
                    {
                        Console.WriteLine("{0,15} {1,25} {2,25} {3,25} {4,15}", item, workers[item].surname, workers[item].name, workers[item].lastname, workers[item].post);
                    }
                });

                int ind = Arrow.SetPos(Console.CursorTop);
                try
                {
                    Read(workers.ElementAt(ind).Key);
                    continue;
                }
                catch { }

                switch (ind)
                {
                    case (int)KeyBoard.F1: Create(); break;
                    case (int)KeyBoard.F2: users = Search(); break;
                    case (int)KeyBoard.Backspace: workers = JSON.Deserialazer<Dictionary<string, Worker>>("employee.json"); break;
                    case (int)KeyBoard.Escape:
                        return;
                }
            }
        }


        public void Create()
        {
            Console.Clear();
            Arrow.Init(() =>
            {
                foreach (string item in data.Values)
                {
                    Console.WriteLine(item);
                }
            });
            string ID ="";
            string surname = "";
            string name = "";
            string lastname = "";
            string birthday = "";
            string pasport = "";
            string post = "";
            string salary = "";
            string userID = "";
            while (true)
            {
                int ind = Arrow.SetPos(Console.CursorTop);
                switch (ind)
                {
                    case 0:
                        ID = Arrow.Type(data[ind].Length, ID); break;
                    case 1:
                        surname = Arrow.Type(data[ind].Length, surname); break;
                    case 2:
                        name = Arrow.Type(data[ind].Length, name); break;
                    case 3:
                        lastname = Arrow.Type(data[ind].Length, lastname); break;
                    case 4:
                        birthday = Arrow.Type(data[ind].Length, birthday); break;
                    case 5:
                        pasport = Arrow.Type(data[ind].Length, pasport); break;
                    case 6:
                        post = Arrow.Type(data[ind].Length, post); break;
                    case 7:
                        salary = Arrow.Type(data[ind].Length, salary); break;
                    case 8:
                        userID = Arrow.Type(data[ind].Length, userID); break;
                    case (int)KeyBoard.Escape:
                        return;
                    case (int)KeyBoard.F5:
                        try
                        {
                            //Check(name, Convert.ToInt32(ID));

                            User new_user = new User(Convert.ToInt32(ID), name, password, Convert.ToInt32(role));
                            users[$"{new_user.ID}"] = new_user;
                            JSON.Serializer(users, "users.json");
                            return;
                        }
                        catch
                        {
                            Console.CursorTop = Arrow.Bottom + 2;
                            Console.WriteLine("Error");
                        };
                        break;
                }
            }
        }

        public void Delete(string name)
        {
            throw new NotImplementedException();
        }

        public void Read(string id)
        {
            Console.Clear();
            Worker worker = workers[id];
            Arrow.Init(() => { worker.STR(); });
            while (true)
            {
                int ind = Arrow.SetPos(-1);
                switch (ind)
                {
                    case (int)KeyBoard.Delete: Delete(id); return;
                    case (int)KeyBoard.F10: Update(id); break;
                    case (int)KeyBoard.Escape: return;
                }
            }
        }

        public dynamic Search()
        {
            throw new NotImplementedException();
        }

        public void Update(string name)
        {
            throw new NotImplementedException();
        }

        
    }*/
}
