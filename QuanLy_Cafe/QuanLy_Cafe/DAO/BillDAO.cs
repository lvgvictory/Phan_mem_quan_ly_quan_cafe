using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLy_Cafe.DTO;

namespace QuanLy_Cafe.DAO
{
    // lấy ra cái Bill từ id Table
    public class BillDAO
    {
        private static BillDAO instance;

        public static BillDAO Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new BillDAO();
                }
                return BillDAO.instance; 
            }
            private set { BillDAO.instance = value; }
        }

        private BillDAO() { }

        /// <summary>
        /// lấy ra cái ID UncheckOut của bảng TableFood
        /// Thành công: bill.ID
        /// Thất bại: -1
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int GetUncheckBillIDByTableID(int id)
        {
            string query = "SELECT * FROM dbo.Bill WHERE idTable = "+ id +" AND statuss = 0";
            DataTable table = DataProvider.Instance.ExecuteQuery(query);

            if (table.Rows.Count > 0)
            {
                Bill bill = new Bill(table.Rows[0]);
                return bill.ID;
            }
            return -1; // không có thằng nào
        }

        public void CheckOut(int id, int discount, float totalPrice)
        {
            string query = "UPDATE dbo.Bill SET dateCheckOut = GETDATE(), statuss = 1, " + "discount = " + discount + ", totalPrice = "+ totalPrice + " WHERE id = " + id;

            DataProvider.Instance.ExecuteNonQuery(query);
        }

        public void InsertBill(int id)
        {
            DataProvider.Instance.ExecuteNonQuery("EXEC USP_InsertBill @idTable", new object[] { id });
        }

        public DataTable GetBillListByDate(DateTime checkIn, DateTime checkOut)
        {
            string query = "EXEC USP_GetListBillByDate @checkIn , @checkOut";
            return DataProvider.Instance.ExecuteQuery(query, new object[] { checkIn, checkOut });
        }


        public int GetMaxIDBill()
        {
            try // cố gắng làm chuyện này
            {
                return (int)DataProvider.Instance.ExecuteScalar("SELECT MAX(id) FROM dbo.Bill");
            }
            catch // nếu như không thành công thì return 1
            {
                return 1;
            }
            
        }
    }
}
