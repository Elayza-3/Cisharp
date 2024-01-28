using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace InfoSys
{

    public class User
    {
        public int ID;
        public string name;
        public string password;
        public int role;

        public User(int ID,string name,string password,int role) 
        { 
            this.ID = ID;
            this.name = name;
            this.password = password;
            this.role = role;
        }

        public void STR()
        {
            Console.WriteLine($"  ID: {ID}");
            Console.WriteLine($"  name: {name}");
            Console.WriteLine($"  password: {password}");
            Console.WriteLine($"  role: {role}");
        }
    }

    public class Worker
    {
        public int ID;
        public string surname;
        public string name;
        public string lastname = "";
        public DateTime birthday;
        public string pasport;
        public string post;
        public double salary;
        public int userID;
        private User user = null;

        public Worker(int ID, string surname, string name, string lastname, DateTime birthday, string pasport, string post, double salary, int userID  = -1)
        {
            this.ID = ID;
            this.surname = surname;
            this.name = name;
            this.lastname = lastname;
            this.birthday = birthday;
            this.pasport = pasport;
            this.post = post;
            this.salary = salary;
            this.userID = userID;

            this.user = JSON.Deserialazer<Dictionary<string, User>>("users.json").Where(x=>x.Value.ID == userID).First().Value;
            
        }

        public void STR()
        {
            Console.WriteLine($"  ID: {ID}");
            Console.WriteLine($"  surname: {surname}");
            Console.WriteLine($"  name: {name}");
            Console.WriteLine($"  lastname: {lastname}");
            Console.WriteLine($"  birthday: {birthday}");
            Console.WriteLine($"  pasport: {pasport}");
            Console.WriteLine($"  post: {post}");
            Console.WriteLine($"  salary: {salary}");
            Console.WriteLine($"  userID: {userID}");
            user.STR();
        }
    }

    public class Product
    {
        public int ID;
        public string label;
        public double price;
        public int count;

        public Product(int ID, string label,double price,int count) 
        {
            this.ID = ID;
            this.label = label;
            this.price = price;
            this.count = count;
        }

        public void STR()
        {
            Console.WriteLine($"  ID: {ID}");
            Console.WriteLine($"  label: {label}");
            Console.WriteLine($"  price: {price}");
            Console.WriteLine($"  count: {count}");
        }
    }

    public class SelectedProduct : Product
    {
        public int selectedcount;

        public SelectedProduct(int ID, string label, double price, int count, int selectedcount) : base( ID,  label,  price,  count)
        {
            this.selectedcount = selectedcount;
        }

        public void STR()
        {
            base.STR();
            Console.WriteLine($"  selectedcount: {selectedcount}");
        }
    }

    public class RecordMoney
    {
        public int ID;
        public string label;
        public double sum_money;
        public DateTime date;
        public bool deduction;

        public RecordMoney(int ID, string label, double sum_money, DateTime date, bool deduction)
        {
            this.ID = ID;
            this.label = label;
            this.sum_money = sum_money;
            this.date = date;
            this.deduction = deduction;
        }

        public void STR()
        {
            Console.WriteLine($"  ID: {ID}");
            Console.WriteLine($"  label: {label}");
            Console.WriteLine($"  sum_money: {sum_money}");
            Console.WriteLine($"  date: {date}");
            Console.WriteLine($"  deduction: {deduction}");
        }
    }



}

