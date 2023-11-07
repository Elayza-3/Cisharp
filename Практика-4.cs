using System;
using System.CollectionsGeneric;

public class Note
{
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Date { get; set; }
    public bool IsCompleted { get; set; }

    public Note(string title, string description, DateTime date)
    {
        Title = title;
        Description = description;
        Date = date;
        IsCompleted = false;
    }
}



public class DailyPlanner
{
    private List<Note> notes;
    private int currentNoteIndex;
    private DateTime currentDate = DateTime.Now;

    public DailyPlanner()
    {
        notes = new List<Note>
        {
            new Note("Заметка 1", "Описание заметки 1", new DateTime(2022, 1, 1)),
            new Note("Заметка 2", "Описание заметки 2", new DateTime(2022, 1, 2)),
            new Note("Заметка 3", "Описание заметки 3", new DateTime(2022, 1, 3)),
            new Note("Заметка 4", "Описание заметки 4", new DateTime(2022, 1, 4)),
            new Note("Заметка 5", "Описание заметки 5", new DateTime(2022, 1, 5))
        };
        currentNoteIndex = 0;
    }

    public void ShowMenu()
    {
        Console.WriteLine("Ежедневник");
        Console.WriteLine(currentDate.ToShortDateString());
        Console.WriteLine("Нажмите Enter для подробной информации");
        //Console.WriteLine(notes[currentNoteIndex].Title);
    }

    public void ShowDetailedInfo()
    {
        Console.WriteLine("Название: " + notes[currentNoteIndex].Title);
        Console.WriteLine("Описание: " + notes[currentNoteIndex].Description);
        Console.WriteLine("Дата: " + notes[currentNoteIndex].Date.ToShortDateString());
        Console.WriteLine("Выполнена: " + (notes[currentNoteIndex].IsCompleted ? "Да" : "Нет"));
    }

    public void MoveLeft()
    {
        if (currentNoteIndex > 0)
        {
            currentNoteIndex--;
            Console.WriteLine(notes[currentNoteIndex].Title);
        }
        currentDate=currentDate.AddDays(-1);
    }

    public void MoveRight()
    {
        if (currentNoteIndex < notes.Count - 1)
        {
            currentNoteIndex++;
            Console.WriteLine(notes[currentNoteIndex].Title);
        }
        currentDate=currentDate.AddDays(1);
    }
}


class Program
{
    static void Main(string[] args)
    {
        DailyPlanner planner = new DailyPlanner();

        bool running = true;
        while (running)
        {
            planner.ShowMenu();

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            Console.Clear();

            switch (keyInfo.Key)
            {
                case ConsoleKey.LeftArrow:
                    planner.MoveLeft();
                    break;
                case ConsoleKey.RightArrow:
                    planner.MoveRight();
                    break;
                case ConsoleKey.Enter:
                    planner.ShowDetailedInfo();
                    break;
                case ConsoleKey.Escape:
                    running = false;
                    break;
            }
        }
    }
}