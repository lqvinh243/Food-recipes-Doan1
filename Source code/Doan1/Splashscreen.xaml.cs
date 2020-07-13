using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Doan1
{
    /// <summary>
    /// Interaction logic for Splashscreen.xaml
    /// </summary>
    public partial class Splashscreen : Window
    {
        DispatcherTimer dt = new DispatcherTimer();
        int n = 1;
        static string folder = AppDomain.CurrentDomain.BaseDirectory;
        string dataFile = $"{folder}config.txt";

        public Splashscreen()
        {
            InitializeComponent();

            if (System.IO.File.Exists(dataFile))
            {
                string str;
                str = System.IO.File.ReadAllText(dataFile);
                n = int.Parse(str);
            }
            else
            {
                Console.WriteLine("File does not exist");
            }
            if (n == 1)
            {
                dt.Tick += new EventHandler(dt_Tick);
                dt.Interval = new TimeSpan(0, 0, 5);
                dt.Start();
            }
            else
            {
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
            }
            this.Loaded += Splashscreen_Loaded;

            this.Closing += Splashscreen_Closing;
        }

        private void Splashscreen_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (ckbShow.IsChecked == true)
            {
                n = 0;
            }
            System.IO.File.WriteAllText(dataFile, n.ToString());
        }

        private void Splashscreen_Loaded(object sender, RoutedEventArgs e)
        {
            Food foodshow = new Food();
            FoodList foods = new FoodList();
            foods.getData();
            if (foods.foods.Count > 0)
            {
                foodshow = foods.randomFood();
            }
            this.DataContext = foodshow;
        }

        private void dt_Tick(object sender, EventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();

            dt.Stop();
            this.Close();
        }
    }
}
