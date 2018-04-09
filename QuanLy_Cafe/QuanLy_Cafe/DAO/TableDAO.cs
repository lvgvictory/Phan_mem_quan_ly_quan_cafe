using QuanLy_Cafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_Cafe.DAO
{
    public class TableDAO
    {
        private static TableDAO instance;

        public static TableDAO Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new TableDAO();
                }
                return TableDAO.instance; 
            }

            private set { TableDAO.instance = value; }
        }

        public static int TableWidth = 85;
        public static int TableHeght = 85;

        /// <summary>
        /// hàm khở tạo
        /// </summary>
        private TableDAO() { }


        public void SwitchTable(int id1, int id2)
        {
            string query = "USP_SwitchTable @idTable1 , @idTable2";
            DataProvider.Instance.ExecuteQuery(query, new object[]{id1, id2});
        }

        public List<Table> LoadTableList()
        {
            List<Table> tableList = new List<Table>();
            string query = "USP_GetTableList";
            DataTable data = DataProvider.Instance.ExecuteQuery(query);

            foreach (DataRow item in data.Rows)
            {
                Table table = new Table(item);
                tableList.Add(table);
            }

            return tableList;
        }

    }
}
