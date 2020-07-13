using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Doan1
{
    /// <summary>
    /// Interaction logic for NewFood.xaml
    /// </summary>
    public partial class NewFood : Window
    {
        public Food Food { get; set; }
        public NewFood()
        {
            InitializeComponent();

            this.Loaded += NewFood_Loaded;
            btnFoodName.Click += FoodName_Click;
            btnDes.Click += BtnDes_Click;
            btnIngredients.Click += BtnIngredients_Click;
            btnImage.Click += BtnImage_Click;
            btnDone.Click += BtnDone_Click;
            btnCancel.Click += BtnCancel_Click;
            btnStep.Click += BtnStep_Click;
            btnUrl.Click += BtnUrl_Click;

            this.lvFoodIngredients.PreviewMouseWheel += LvFoodIngredients_PreviewMouseWheel;
            this.lvDetailStep.PreviewMouseWheel += LvDetailStep_PreviewMouseWheel;
        }

        private void LvDetailStep_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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

        private void LvFoodIngredients_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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

        private void BtnUrl_Click(object sender, RoutedEventArgs e)
        {
            var txt = txtUrl.Text;
            if (txt.Length <= 0)
            {
                MessageBox.Show("Nguyên liệu không được để trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Food.Url_Utube = txt;
            txtUrl.Text = "";
        }

        private void BtnStep_Click(object sender, RoutedEventArgs e)
        {
            var txtSp = txtStep.Text;
            if(txtSp.Length <= 0)
            {
                MessageBox.Show("Bạn phải nhập đầy đủ mô tả bước trước!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            var screen = new OpenFileDialog();
            if (screen.ShowDialog() == true)
            {
                var info = new FileInfo(screen.FileName);
                var newName = $"{Guid.NewGuid()}{info.Extension}";
                info.CopyTo($"{AppDomain.CurrentDomain.BaseDirectory}Img\\{newName}");
                Food.DetailedSteps.Add(new DetailStep() { Description = txtSp, Description_Url = newName });
                txtStep.Text = "";
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn hủy bỏ việc thêm?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) != MessageBoxResult.Yes)
            {
                return;
            }
            DialogResult = false;
            this.DataContext = null;
            lvFoodIngredients.ItemsSource = null;
            lvDetailStep.ItemsSource = null;
            this.Close();
        }

        private void BtnDone_Click(object sender, RoutedEventArgs e)
        {
            if(Food.checkConstraint() != null)
            {
                MessageBox.Show(Food.checkConstraint(), "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            DialogResult = true;
            this.Close();
        }

        private void BtnImage_Click(object sender, RoutedEventArgs e)
        {
            var screen = new OpenFileDialog();
            if(screen.ShowDialog() == true)
            {
                var info = new FileInfo(screen.FileName);
                var newName = $"{Guid.NewGuid()}{info.Extension}";
                info.CopyTo($"{AppDomain.CurrentDomain.BaseDirectory}Img\\{newName}");
                Food.Food_Image = newName; 
            }
        }

        private void BtnIngredients_Click(object sender, RoutedEventArgs e)
        {
            var txt = txtIngredients.Text;
            if (txt.Length <= 0)
            {
                MessageBox.Show("Nguyên liệu không được để trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Food.FoodIngredients.Add(txt);
            txtIngredients.Text = "";
        }

        private void BtnDes_Click(object sender, RoutedEventArgs e)
        {
            var txt = txtDes.Text;
            if (txt.Length <= 0)
            {
                MessageBox.Show("Mô tả món ăn không được để trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Food.FoodDescription = txt;
            txtDes.Text = "";
        }

        private void FoodName_Click(object sender, RoutedEventArgs e)
        {
            var txt = txtFoodName.Text;
            if(txt.Length <= 0)
            {
                MessageBox.Show("Tên món ăn không để trống!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            Food.NameFood = txt;
            txtFoodName.Text = "";
        }

        private void NewFood_Loaded(object sender, RoutedEventArgs e)
        {
            Food = new Food();
            Food.Reset();
            this.DataContext = Food;
            lvFoodIngredients.ItemsSource = Food.FoodIngredients;
            lvDetailStep.ItemsSource = Food.DetailedSteps;
        }
    }
}
