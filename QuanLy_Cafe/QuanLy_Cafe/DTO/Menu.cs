using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_Cafe.DTO
{
    public class Menu
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="foodNmae"></param>
        /// <param name="countt"></param>
        /// <param name="price"></param>
        /// <param name="totalPrice"></param>
        public Menu(string foodNmae, int countt, float price, float totalPrice = 0)
        {
            this.FoodName = foodName;
            this.Countt = countt;
            this.Price = price;
            this.TotalPrice = totalPrice;
        }
        public Menu(DataRow row)
        {
            this.FoodName = row["name"].ToString();
            this.Countt = (int)row["countt"];
            this.Price = (float)(Convert.ToDouble(row["price"].ToString()));
            this.TotalPrice = (float)(Convert.ToDouble(row["totalPrice"].ToString()));
        }

        private float totalPrice;

        public float TotalPrice
        {
            get { return totalPrice; }
            set { totalPrice = value; }
        }

        private float price;

        public float Price
        {
            get { return price; }
            set { price = value; }
        }

        private int countt;

        public int Countt
        {
            get { return countt; }
            set { countt = value; }
        }

        private string foodName;

        public string FoodName
        {
            get { return foodName; }
            set { foodName = value; }
        }
    }
}
