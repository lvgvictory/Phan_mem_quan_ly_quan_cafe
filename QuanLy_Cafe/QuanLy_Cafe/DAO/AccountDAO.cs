using QuanLy_Cafe.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_Cafe.DAO
{
    public class AccountDAO
    {
        private static AccountDAO instance;

        public static AccountDAO Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance; 
            }
            private set { instance = value; }
        }

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        private AccountDAO() { }

        public bool Login(string username, string password)
        {
            string query = "USP_Login @userName , @passWord";
            DataTable result = DataProvider.Instance.ExecuteQuery(query, new object[] { username, password }); // trả ra nhưng dòng kết quả
            return result.Rows.Count > 0;
        }

        /// <summary>
        /// Cập nhật thông thin tài khoản trong form thông tin tài khoản
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="displayName"></param>
        /// <param name="pass"></param>
        /// <param name="newPass"></param>
        /// <returns></returns>
        public bool UpdateAccount(string userName, string displayName, string pass, string newPass)
        {
            int result = DataProvider.Instance.ExecuteNonQuery("EXEC USP_UpdateAccount @userName , @displayName , @passwordd , @newPassword", new object[] { userName, displayName, pass, newPass });
            return result > 0; // nếu như số dòng thay đổi > 0 tức là có Update
        }

        public DataTable GetListAccount()
        {
            string query = "SELECT UserName, DisplayName, Typee FROM dbo.Account";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public Account GetAccountByUserName(string userName)
        {
            DataTable data = DataProvider.Instance.ExecuteQuery("Select * from account where userName = '" + userName + "'");

            foreach (DataRow item in data.Rows)
            {
                return new Account(item);
            }

            return null;
        }
    }
}
