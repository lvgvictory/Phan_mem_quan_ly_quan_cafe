using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_Cafe.DTO
{
    public class Bill
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dateCheckIn"></param>
        /// <param name="dateCheckOut"></param>
        /// <param name="statuss"></param>
        public Bill(int id, DateTime? dateCheckIn, DateTime? dateCheckOut, int statuss, int discount = 0)
        {
            this.ID = id;
            this.DateCheckIn = dateCheckIn;
            this.DateCheckOut = dateCheckOut;
            this.Statuss = statuss;
            this.Discount = discount;
        }

        public Bill(DataRow row)
        {
            this.ID = (int)row["id"];
            this.DateCheckIn = (DateTime?)row["dateCheckIn"];

            var dateCheckOutTemp = row["dateCheckOut"];
            if (dateCheckOutTemp.ToString() != "")
            {
                this.DateCheckOut = (DateTime?)dateCheckOutTemp;
            }
            
            this.Statuss = (int)row["statuss"];
            this.Discount = (int)row["discount"];
        }

        private int discount;

        public int Discount
        {
            get { return discount; }
            set { discount = value; }
        }

        private int statuss;

        public int Statuss
        {
            get { return statuss; }
            set { statuss = value; }
        }

        private DateTime? dateCheckOut;

        public DateTime? DateCheckOut
        {
            get { return dateCheckOut; }
            set { dateCheckOut = value; }
        }

        private DateTime? dateCheckIn;// có thể null được

        public DateTime? DateCheckIn
        {
            get { return dateCheckIn; }
            set { dateCheckIn = value; }
        } 

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
