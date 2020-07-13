using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;
using System.Windows;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Doan1
{
    class FoodList
    {
        public BindingList<Food> foods { get; set; }

        string folder = AppDomain.CurrentDomain.BaseDirectory;
        string dataFile = "";
        private Random _rng = new Random();


        public FoodList()
        {
            foods = new BindingList<Food>();
        }


        public void Savefile()
        {
            dataFile = $"{folder}Foods.txt";
            string json = JsonConvert.SerializeObject(this.foods);
            File.WriteAllText(dataFile, json);
        }

        public Food randomFood()
        {
            var indexFood = _rng.Next(this.foods.Count());
            return this.foods[indexFood];
        }

        public BindingList<Food> FilterFood(string FilterName)
        {
            var p = foods.Where(fl => fl.NameFood.Contains(FilterName));
            return new BindingList<Food>(new List<Food>(p));
        }

        public static string convertToUnSign3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public BindingList<Food> getData()
        {
            dataFile = $"{folder}Foods.txt";
            try
            {
                using (StreamReader r = new StreamReader(dataFile))
                {
                    string json = r.ReadToEnd();
                    this.foods = JsonConvert.DeserializeObject<BindingList<Food>>(json);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //this.foods = new List<Food>();
            //Food f1 = new Food()
            //{
            //    id = 1,
            //    NameFood = "Trứng rim mắm hành",
            //    Food_Image = "trungrimmamhanh.jpg",
            //    FoodDescription = "Trứng Rim Mắm Hành, món ăn khá đơn giả nhưng lại cực kì hao cơm, với cách chế biến nhanh chóng là đã có ngay món trứng rim thơm ngon với lớp vỏ ngoài giòn dai, có màu nâu đẹp thấm đượm vị. Hãy cùng Cooky bắt tay vào làm ngay món trứng rim mắm hành này để đãi gia đình nhưng lại không mất quá nhiều thời gian của bạn nhé.",
            //    FoodIngredients = new List<string>() { "Trứng gà 6 quả", "Nước mắm 1 muỗng", "Tiêu xay 1/2 Muỗng cà phê", "Hành lá 50gr", "Đường 1,5 muỗng canh", "Bột ngọt 1/2 Muỗng cà phê", "Tương ớt 2 muỗng canh", "Ớt hiểm 2 trái" },
            //    DetailedSteps = new List<DetailStep>() { new DetailStep() { StepNumber = 1, Description = "Làm nước sốt mắm: Cho vào tô nhỏ hỗn hợp 1.5 muỗng canh đường, 1 muỗng canh nước mắm,1/2 muỗng cà phê bột ngọt, 1/2 muỗng cà phê tiêu, 2 muỗng canh tương ớt, dùng muỗng khuấy đều hỗn hợp.", Description_Url = "trungrimmamhanh1.jpg" },
            //        new DetailStep() { StepNumber = 2, Description = "Cho 1 muỗng cà phê muối vào nồi nước sôi, sau đó thả 6 quả trứng gà vào nồi và luộc chín rồi cắt làm đôi.", Description_Url = "trungrimmamhanh2.jpg" },
            //        new DetailStep() { StepNumber = 3, Description = "Bắc chảo lên bếp và cho lượng dầu vừa phải vào chảo. Cho trứng vào chảo rán, sau khi vàng 1 mặt thì lật mặt còn lại rán nên lật nhẹ nhành để không bị vỡ trứng.", Description_Url = "trungrimmamhanh3.jpg" },
            //        new DetailStep() { StepNumber = 4, Description = "Cho nước sốt vào và đun với lửa vừa, khi nước sốt gần cạn thì cho tép khô, hành lá, ớt thái lát vào, đảo cho chín. Cuối cùng rắc hành phi lên là hoàn tất.", Description_Url = "trungrimmamhanh4.jpg" },
            //        new DetailStep() { StepNumber = 5, Description = "Món ăn có thể dùng với cơm nóng hoặc bánh mì cũng rất ngon.", Description_Url = "trungrimmamhanh5.jpg" } },
            //    FavoriteFood = false,
            //    Url_Utube = "https://www.youtube.com/watch?v=cuDG6KQg_OI"
            //};
            //Food f2 = new Food()
            //{
            //    id = 2,
            //    NameFood = "Chân Giò Hầm Coca",
            //    Food_Image = "changiohamcoca.jpg",
            //    FoodDescription = "Chân Giò Hầm Coca món ăn ngon dùng cơm, món ngon đãi tiệc hay nhăm nhi món nhậu đều rất hợp vị. Thịt chân giò mềm, lớp da dai béo ngậy nước sốt sóng sánh vị ngon cuốn hút. Món ăn ngon dùng với cơm nóng rất bắt cơm thích hợp vào những ngày thu se lạnh dù là trẻ con hay người già đều có thể thưởng thức. Đích thị là món ngon gắn kết bữa cơm sum vầy gia đình bạn. ",
            //    FoodIngredients = new List<string>() { "Chân Giò 1 kg", "Ngũ vị hương 1 gói", "Nước mắm 2 muỗng canh", "Đường 2 muỗng canh", "Muối 1 muỗng cà phê", "Lá nguyệt quế khô 3 lá", "Hành lá quấn bó 2 Cọng", "Coca Cola 1 lon", "Hành Băm 10 gram", "Hạt Nêm 2 muỗng canh", "Bột Ngọt 2 muỗng canh", "Dầu ăn 2 muỗng canh", "Hoa Hồi 3 cái" },
            //    DetailedSteps = new List<DetailStep>() { new DetailStep() { StepNumber = 1, Description = "Bạn chọn chân giò trước vì sẽ ngon hơn chân sau, cắt khoanh vừa ăn rửa sạch với nước và muối dùng tay bóp cho sạch. Sau đó trụng sơ chân giò tầm 3 phút có hành, gừng và chút xíu giấm để khử bớt mùi hôi. Vớt ra ngâm vào nước đá lạnh và rửa sạch. Ướp chân giò với Ngũ Vị Hương, Hành băm, Hạt nêm, Đường, Bột Ngọt, Nước mắm, Muối. Trộn đều gia vị cho chân giò ngấm 1 tiếng.", Description_Url = "changiohamcoca1.jpg" },
            //        new DetailStep() { StepNumber = 2, Description = "Bắt chảo lên bếp, dầu nóng bạn cho Hoa hồi, lá nguyệt quê khô và hành quấn bó vào khử dầu nhé. Đến khi dậy mùi thơm thì cho chân giò đã ướp vào xào thật săn. Xào chân giò trên lửa lớn khoảng 5 phút cho rút ngấm hết gia vị thì cho coca và nước vào sấp mặt thịt, chân giò hầm càng lâu càng mềm càng ngon nên bạn để nhiều nhiều nước chút nha...", Description_Url = "changiohamcoca2.jpg" },
            //        new DetailStep() { StepNumber = 3, Description = "Lúc này các bạn vớt bỏ bọt, sau đó đậy nắp hầm chân giò với lửa riu riu thêm 30 phút. Đến khi chân giò mềm, lớp da dai dảo béo ngậy và nước sánh lại là được.", Description_Url = "changiohamcoca3.jpg" },
            //        new DetailStep() { StepNumber = 4, Description = "Qua cách thực hiện nhanh chóng, là bạn đã có ngay món Chân giò hầm Coca rồi. Món ngon, màu sắc sóng sánh, hương vị đậm đà nếm 1 miếng là lại muốn ăn thêm 1 miếng. Bạn thưởng thức khi nóng sẽ ngon hơn, bạn dọn thêm 1 dĩa rau dưa 1 chén cơm nóng là hết sẩy. Bạn nhanh tay lưu vào sổ tay món ngon nhé.", Description_Url = "changiohamcoca4.jpg" } },
            //    FavoriteFood = false,
            //    Url_Utube = "https://www.youtube.com/watch?v=_qLBuwXnhQQ"
            //};

            //Food f3 = new Food()
            //{
            //    id = 3,
            //    NameFood = "Canh chua nghêu",
            //    Food_Image = "canhchuangeu.jpg",
            //    FoodDescription = "Làm phong phú hơn thực đơn cho bé yêu nhà bạn với món canh chua nghêu đậu hũ chua ngọt bắt vị, rất thích hợp để nấu cho bé ăn vào những ngày thời tiết nắng nóng. Món canh với thịt nghêu dai ngọt bổ dưỡng, cung cấp thêm chất sắt và giúp tăng cường hệ miễn dịch cho bé, giúp bé cao lớn khỏe mạnh hơn. Ngoài nghêu ra còn có đậu hũ non mềm mịn, cùng các loại rau được cắt nhỏ nhắn vừa miệng, giúp bé tập nhai và cung cấp thêm nhiều chất xơ tốt cho hệ tiêu hóa của bé.",
            //    FoodIngredients = new List<string>() { "Nghêu", "Thơm", "Đậu hũ non", "Hành tím", "Nước cốt me", "Đường trắng", "Cà chua", "Bạc hà", "Sả", "Dầu ăn", "Hạt nêm Maggi" },
            //    DetailedSteps = new List<DetailStep>() { new DetailStep() { StepNumber = 1, Description = "Nghêu mua về ngâm với nước muối có vài trái ớt để nghêu nhả bớt cát. Cho nghêu vào nồi cùng với 15gr sả cắt khúc, 200ml nước lọc, đun sôi. Luộc đến khi nghêu mở nắp thì tắt bếp, tách lấy thịt nghêu. Nước luộc nghêu để cho lắng bớt cát nếu có rồi lọc lấy phần nước sạch.", Description_Url = "canhchuangeu1.jpg" },
            //        new DetailStep() { StepNumber = 2, Description = "30gr thơm, 30gr cà chua bỏ hạt, 30gr bạc hà tước vỏ, đem cắt các nguyên liệu thành những miếng nhỏ. Bắc nồi lên bếp, làm nóng 1 muỗng canh dầu ăn, cho 10gr hành tím vào phi thơm. Tiếp theo cho cà chua và thơm vào xào, đến khi cà chua và thơm mềm thì chế vào nồi 200ml nước luộc nghêu và 100ml nước lọc.", Description_Url = "canhchuangeu2.jpg" },
            //        new DetailStep() { StepNumber = 3, Description = "Đun sôi nồi canh, nêm vào 1 muỗng canh hạt nêm nấm hương chay Maggi, 1 muỗng canh nước cốt me và 1 muỗng canh đường cho gia vị vừa miệng.", Description_Url = "canhchuangeu3.jpg" },
            //        new DetailStep() { StepNumber = 4, Description = "Sau khi nêm nếm thì cho thêm 100gr đậu hũ non cắt miếng vuông nhỏ, bạc hà, thịt nghêu đã bóc và 1 ít ngò gai, ngò om cắt nhỏ vào nồi. Đun cho canh sôi lại là hoàn tất.", Description_Url = "canhchuangeu4.jpg" } ,
            //    new DetailStep() { StepNumber = 4, Description = "Dọn canh chua nghêu ra ăn cùng cơm nóng thôi. Món canh có hương vị chua ngọt nhẹ nhàng kích thích vị giác, thịt nghêu mềm dai thơm ngọt tự nhiên, bổ sung nhiều chất sắt tốt cho sức khỏe của trẻ nhỏ.", Description_Url = "canhchuangeu5.jpg" }},
            //    FavoriteFood = false,
            //    Url_Utube = "https://www.youtube.com/watch?v=_qLBuwXnhQQ"
            //};
            //this.foods.Add(f1);
            //this.foods.Add(f2);
            //this.foods.Add(f3);
            //this.Savefile();
            return this.foods;
        }
    }
}
