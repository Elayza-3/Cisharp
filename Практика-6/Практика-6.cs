using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Serialization;

public class Book
{
    public string Title;
    public string Author;
    public int Pages;

    public Book() { }
    public Book(string title, string author, int pages)
    {
        Title = title;
        Author = author;
        Pages = pages;
    }
}

class Program
{
    static string filePath;

    static void Main(string[] args)
    {
        Console.WriteLine("Нажмите F1 для сохранения файла, Escape для выхода.");
        Console.WriteLine("___________________________________________________");
        Console.WriteLine("Введите путь к файлу:");
        filePath = Console.ReadLine();
        Console.Clear();
        if (File.Exists(filePath))
        {
            FileManager.LoadFile(filePath);
            
            ConsoleKeyInfo keyInfo;

            FileManager.print();

            do
            {
                keyInfo = Console.ReadKey(true);
                Arrow.move(keyInfo.Key);
                if (keyInfo.Key == ConsoleKey.F1)
                {
                    Console.Clear();
                    string path = Console.ReadLine();
                    FileManager.SaveFile(path);
                 }
            } while (keyInfo.Key != ConsoleKey.Escape);
        }
        else
        {
            Console.WriteLine("Файл не найден.");
        }
    }

    

    
}

static class FileManager
{
    static public Book book;
    static public string[] text;
    static public void SaveFile(string filePath)
        {
            string fileExtension = Path.GetExtension(filePath).ToLower();
            switch (fileExtension)
            {
                case ".txt":
                {
                    string data = "";
                    foreach (var item in FileManager.text)
                    {
                        data += item + "\n";
                    }
                    File.WriteAllText(filePath,data.Trim('\n') );
                    break; 
                }
                case ".json":
                {
                    string jsonData = JsonConvert.SerializeObject(book);
                    File.WriteAllText(filePath, jsonData);
                    break;
                }
                case ".xml":
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Book));
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
                    {
                        serializer.Serialize(fs, book);
                    }
                    break;
                }
                default:
                    Console.WriteLine("Формат файла не поддерживается.");
                    break;
            }
    }

    static public void LoadFile(string filePath)
    {
        string fileExtension = Path.GetExtension(filePath).ToLower();
        Book book;
        switch (fileExtension)
        {
            case ".txt":
                text = File.ReadAllLines(filePath);
                getBook(text);
                break;
            case ".json":
                string jsonData = File.ReadAllText(filePath);
                book = JsonConvert.DeserializeObject<Book>(jsonData);
                getText(book);
                break;
            case ".xml":
                XmlSerializer serializer = new XmlSerializer(typeof(Book));
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
                {
                    book = (Book)serializer.Deserialize(fileStream);
                }
                getText(book);
                break;
            default:
                Console.WriteLine("Формат файла не поддерживается.");
                break;
        }
         
    }
    
    static private void getText(Book obj)
    {
        text =new string[] {$"{obj.Title}",$"{obj.Author}",$"{obj.Pages}"};
    }

    static public void getBook(string[] text)
    {
        book = new Book(text[0], text[1], Convert.ToInt32(text[2]));
    }

    static public void print()
    {
        foreach (var item in text)
        {
            Console.WriteLine("  " + item);
        }
    }
}

static class Arrow
{
    static int currentPos = 0;

    static public void move(ConsoleKey key)
    {
        
        switch (key)
        {
            case ConsoleKey.UpArrow:
                {
                    if(currentPos == 0)
                    {
                        currentPos = FileManager.text.Length - 1;
                    }
                    else
                    {
                        --currentPos;
                    }
                    break;
                }
            case ConsoleKey.DownArrow:
                {
                    if (currentPos == FileManager.text.Length-1)
                    {
                        currentPos = 0;
                    }
                    else
                    {
                        ++currentPos;
                    }
                    break;
                }
            case ConsoleKey.Enter:
                {
                    Console.Clear ();
                    Console.WriteLine("Введите новую строчку:");

                    string newline = Console.ReadLine();
                    FileManager.text[currentPos] = newline;
                    FileManager.getBook(FileManager.text);
                    
                    break;
                }
        }
        Console.Clear();
        FileManager.print();
        Console.SetCursorPosition(0, currentPos);
        Console.Write("->");
    }

    
}