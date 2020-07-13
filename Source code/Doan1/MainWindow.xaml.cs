using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Doan1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static FoodList fl = new FoodList();
        BindingList<Food> f = fl.getData();
        BindingList<Food> foodshow;
        BindingList<Food> favbin;
        List<string> imgtemp = new List<string>();
        Config cfg = new Config();
        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;
            this.btnClose.Click += BtnClose_Click;
            this.btnNext.Click += BtnNext_Click;
            this.btnPre.Click += BtnPre_Click;
            this.addNewFood.Click += AddNewFood_Click;
            this.txtSeach.TextChanged += TxtSeach_TextChanged;
            this.btnUnFilter.Click += BtnUnFilter_Click;

            this.del_Item.Click += Del_Item_Click;
        }

        private void Del_Item_Click(object sender, RoutedEventArgs e)
        {
            var foodSelect = listFood.SelectedItem as Food;
            f.Remove(foodSelect);
            favbin.Remove(foodSelect);
            if (f.Count % 2 == 0)
            {
                cfg.PageTotal -= 1;

            }
            if (cfg.PageTotal == 1)
            {
                cfg.next = "False";
                cfg.PageNow = 1;
            }
            else if(f.Count % 2 ==0)
            {
                cfg.PageNow -= 1;
            }

            if(cfg.PageNow == 1)
            {
                cfg.pre = "False";
            }
            
            setView(cfg.PageNow);
        }

        private void BtnUnFilter_Click(object sender, RoutedEventArgs e)
        {
            if (txtSeach.Text.Length > 0)
            {
                txtSeach.Text = "";
                txtSeachRs.Text = "";
                f = fl.getData();
                setView(1);
                cfg.PageNow = 1;
                cfg.pre = "False";
                cfg.next = "False";
                cfg.PageTotal = (int)Math.Ceiling(f.Count() / (double)cfg.CountView);
                if (cfg.PageNow < cfg.PageTotal)
                {
                    cfg.next = "True";
                }
            }
        }

        private void TxtSeach_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtSeachRs.Text = "";
            var txt = (sender as TextBox).Text;
            f = fl.FilterFood(txt);
            setView(1);
            cfg.PageNow = 1;
            cfg.pre = "False";
            cfg.next = "False";
            cfg.PageTotal = (int)Math.Ceiling(f.Count() / (double)cfg.CountView);
            if (f.Count <= 0)
            {
                txtSeachRs.Text = "Không có kết quả phù hợp";
                cfg.PageNow = 0;
            }
            
            if (cfg.PageNow < cfg.PageTotal)
            {
                cfg.next = "True";
            }

        }

        private void AddNewFood_Click(object sender, RoutedEventArgs e)
        {
            NewFood newFood = new NewFood();
            newFood.ShowDialog();
            var result = newFood.DialogResult;
            if (result == false)
            {
                imgtemp.Add(newFood.Food.Food_Image);
                for (int i = 0; i < newFood.Food.DetailedSteps.Count; i++)
                {
                    imgtemp.Add(newFood.Food.DetailedSteps[i].Description_Url);
                }
            }
            else
            {
                var g = Guid.NewGuid();
                newFood.Food.id = g.ToString();
                f.Add(newFood.Food);
                cfg.PageTotal = (int)Math.Ceiling(f.Count() / (double)cfg.CountView);
                if (cfg.PageNow < cfg.PageTotal)
                {
                    cfg.next = "True";
                }
            }
        }

        private void BtnPre_Click(object sender, RoutedEventArgs e)
        {
            cfg.PageNow--;
            cfg.next = "True";
            if (cfg.PageNow == 1)
            {
                cfg.pre = "False";
            }
            setView(cfg.PageNow);
        }

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            cfg.PageNow++;
            if (cfg.PageNow == cfg.PageTotal)
            {
                cfg.next = "False";
            }
            cfg.pre = "True";
            setView(cfg.PageNow);

        }

        public void setView(int PageNow)
        {
            int Key = (PageNow - 1) * cfg.CountView;
            var p = f.Skip(Key).Take(cfg.CountView);
            var l = new List<Food>(p);
            var g = new BindingList<Food>(l);

            foodshow.Clear();
            for (int i = 0; i < g.Count(); i++)
            {
                foodshow.Add(g[i]);
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn muốn thoát?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                this.Close();
            }
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            for (int i = 0; i < imgtemp.Count; i++)
            {
                var filePath = $"{AppDomain.CurrentDomain.BaseDirectory}Img\\{imgtemp[i]}";
                if (File.Exists(filePath))
                {
                    System.GC.Collect();
                    System.GC.WaitForPendingFinalizers();
                    File.Delete(filePath);
                }
            }
            fl.Savefile();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {

            cfg.PageNow = 1;
            cfg.CountView = 2;
            cfg.PageTotal = (int)Math.Ceiling(f.Count() / (double)cfg.CountView);
            cfg.pre = "False";
            cfg.next = "False";
            if (cfg.PageTotal > 1) cfg.next = "True";


            int Key = (cfg.PageNow - 1) * cfg.CountView;
            var p = f.Skip(Key).Take(cfg.CountView);
            var l = new List<Food>(p);
            foodshow = new BindingList<Food>(l);
            listFood.ItemsSource = foodshow;
            this.DataContext = cfg;

            var favorite = f.Where(i => i.FavoriteFood == "Pink");
            var fav = new List<Food>(favorite);
            favbin = new BindingList<Food>(fav);
            listFavoriteFood.ItemsSource = favbin;

        }


        private void Favorite_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var i = listFavoriteFood.SelectedIndex;
            var f = favbin[i];
            DetailFood detailFood = new DetailFood(f);
            detailFood.ShowDialog();
        }

        private void HomeCard_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var i = listFood.SelectedIndex;
            var f = foodshow[i];
            DetailFood detailFood = new DetailFood(f);
            detailFood.ShowDialog();
        }

        private void btnHeart_Click(object sender, RoutedEventArgs e)
        {
            var i = (((sender as Button).Parent as Grid).Parent as StackPanel);
            var foodC = i.DataContext as Food;
            var message = foodC.FavoriteFood == "White" ? "Bạn muốn thêm món này vào danh sách món ăn yêu thích?" : "Bạn muốn bỏ món này khỏi danh sách món ăn yêu thích?";
            if (MessageBox.Show(message, "Thông báo", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                foodC.FavoriteFood = foodC.FavoriteFood == "White" ? "Pink" : "White";
                if (foodC.FavoriteFood == "Pink")
                {
                    favbin.Add(foodC);
                }
                else
                {
                    favbin.Remove(foodC);
                }
            }
        }
    }
}
