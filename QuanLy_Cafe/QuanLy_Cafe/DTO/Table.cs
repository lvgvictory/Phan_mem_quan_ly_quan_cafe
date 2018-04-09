using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_Cafe.DTO
{
    // lưu trữ thông tin và thuộc tính của Table
    public class Table
    {
        private string statuss;

        public string Statuss
        {
            get { return statuss; }
            set { statuss = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private int iD;

        public int ID
        {
            get { return iD; }
            set { iD = value; }
        }

        public Table(int id, string name, string statuss)
        {
            this.ID = id;
            this.Name = name;
            this.Statuss = statuss;
        }

        public Table(DataRow row)
        {
            this.ID = (int)row["id"];
            this.Name = row["name"].ToString();
            this.Statuss = row["statuss"].ToString();
        }
    }
}
