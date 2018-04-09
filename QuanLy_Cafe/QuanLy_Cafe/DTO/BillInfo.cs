﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_Cafe.DTO
{
    public class BillInfo
    {

        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="billID"></param>
        /// <param name="foodID"></param>
        /// <param name="countt"></param>
        public BillInfo(int id, int billID, int foodID, int countt)
        {
            this.ID = id;
            this.BillID = billID;
            this.FoodID = foodID;
            this.Countt = countt;
        }

        public BillInfo(DataRow row)
        {
            this.ID = (int)row["id"];
            this.BillID = (int)row["idBill"];
            this.FoodID = (int)row["idFood"];
            this.Countt = (int)row["countt"];

            //this.ID = (int)row["id"];
            //this.BillID = (int)row["idBill"];
            //this.FoodID = (int)row["idFood"];
            //this.Countt = (int)row["countt"];
        }

        private int countt;

        public int Countt
        {
            get { return countt; }
            set { countt = value; }
        }

        private int foodID;

        public int FoodID
        {
            get { return foodID; }
            set { foodID = value; }
        }

        private int billID;

        public int BillID
        {
            get { return billID; }
            set { billID = value; }
        }

        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }
    }
}
