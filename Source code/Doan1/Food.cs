using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PropertyChanged;

namespace Doan1
{
    public class Food : INotifyPropertyChanged
    {
        public string id { get; set; }
        public string NameFood { get; set; }
        public string Food_Image { get; set; }
        public string FoodDescription { get; set; }
        public BindingList<string> FoodIngredients { get; set; }
        public BindingList<DetailStep> DetailedSteps { get; set; }
        public string Url_Utube { get; set; }
        public string FavoriteFood { get; set; }

        public Food()
        {
            this.NameFood = "Hiện chưa có món ăn nào ăn nào trong danh sách!!";
            this.Food_Image = "wellcome.jpg";
            this.FoodDescription = "Hãy thêm ngay món ăn bạn thích vào danh sách nhé!";
            this.FoodIngredients = new BindingList<string>();
            this.DetailedSteps = new BindingList<DetailStep>();
            this.FavoriteFood = "White";

        }

        public void Reset()
        {
            this.NameFood = "";
            this.FoodDescription = "";
        }

        public string checkConstraint()
        {
            if(NameFood.Length <= 0)
            {
                return "Bạn có quên điền tên món ăn không?";
            }
            if(Food_Image == "wellcome.jpg")
            {
                return "Bạn có quên chọn mô tả hình ảnh không?";
            }
            if(FoodDescription.Length <=0)
            {
                return "Bạn có quên mô tả món ăn không?";
            }
            if(FoodIngredients.Count <= 0)
            {
                return "Bạn có quên điền nguyên liệu không?";
            }
            if(DetailedSteps.Count <= 0)
            {
                return "Bạn có quên điền các bước thực hiện không?";
            }
            if(Url_Utube.Length <= 0)
            {
                return "Bạn có quên điền link hướng dẫn không?";
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
