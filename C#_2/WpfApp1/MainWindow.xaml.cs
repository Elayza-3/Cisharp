using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        string[] signs = new string[2] { "o", "x" };
        public MainWindow()
        {
            InitializeComponent();

            Runner.map = new Button[][] { 
                new Button[]{ field1, field2, field3 },
                new Button[]{ field4, field5, field6 },
                new Button[]{ field7, field8, field9 } 
            };

            if( new Random().Next(0, 1) == 1) signs = signs.Reverse().ToArray();

            Runner.SetSign(signs);


        }

        private void start_Click(object sender, RoutedEventArgs e)
        {
            lbl.Content = "Игра";

            Runner.CleanField(true);

            signs = signs.Reverse().ToArray();
            Runner.SetSign(signs);

        }

        private void field_Click(object sender, RoutedEventArgs e)
        {
            Button clicked = sender as Button;

            clicked.Content = Runner.user;
            clicked.IsEnabled = false;

            if (Runner.GetWinner( Runner.user))
            {
                Runner.CleanField(false,0);
                lbl.Content = "Победа!";

            }
            else
            {
                try
                {
                    if (Runner.RunBot())
                    {
                        Runner.CleanField(false, 0);
                        lbl.Content = "Поражение";

                    }
                }
                catch (ArgumentOutOfRangeException)
                {
                    lbl.Content = "Ничья";
                }
            }
        }
    }
}


static class Runner
{
    static public string user;
    static public string bot;

    static public Button[][] map;


    static public void SetSign(string[] signs)
    {
        (user, bot) = (signs[0], signs[1]);
    }

    static public bool GetWinner(string sign)
    {
        int left_d = 0;
        int right_d = 0;

        for (int i = 0; i<map.Length; ++i)
        {
            if (map[i].Count(btn => btn.Content == sign) == 3) return true;
            if (map.Count(row=> row[i].Content == sign) == 3) return true;

            if (map[i][i].Content == sign) left_d++;
            if (map[i][2-i].Content == sign) right_d++;

        }

        return left_d == 3 || right_d == 3;
    }

    static public void CleanField( bool enabled, int content = 1)
    {

        _iter((btn) =>
        {
            btn.IsEnabled = enabled;
            btn.Content = (content == 1) ? "" : btn.Content;
        });
    }

    static public bool RunBot()
    {
        List<Button> Buttonlist = new List<Button>();

        _iter( (btn) =>
        {
            if(btn.IsEnabled)
            {
                Buttonlist.Add(btn);
            }
            
        });

        int index = (new Random()).Next(0, Buttonlist.Count);

        Button bot_btn = Buttonlist[index];
        bot_btn.Content = bot;
        bot_btn.IsEnabled = false;

        return GetWinner( bot);
    }

    static private void _iter( Action<Button> func)
    {
        foreach (Button[] row in map)
        {
            foreach (Button btn in row)
            {
                func(btn);
            }

        }
    }
}