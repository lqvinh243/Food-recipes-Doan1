using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Doan1
{
    /// <summary>
    /// Interaction logic for DetailFood.xaml
    /// </summary>
    public partial class DetailFood : Window
    {
        public Food food { get; set; }
        public DetailFood(Food f)
        {
            InitializeComponent();
            food = f;

            this.Loaded += DetailFood_Loaded;
            FoodIngredients.PreviewMouseWheel += FoodIngredients_PreviewMouseWheel;
            DetailStep.PreviewMouseWheel += DetailStep_PreviewMouseWheel;
            btnClose.Click += BtnClose_Click;
            this.Closing += DetailFood_Closing;
        }

        private void DetailFood_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.DataContext = null;
            FoodIngredients.ItemsSource = null;
            DetailStep.ItemsSource = null;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DetailStep_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void FoodIngredients_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        private void DetailFood_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = food;
            FoodIngredients.ItemsSource = food.FoodIngredients;
            DetailStep.ItemsSource = food.DetailedSteps;
        }
    }
}
