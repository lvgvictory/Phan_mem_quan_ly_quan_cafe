using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_Cafe.DTO
{
    public class Account
    {
        /// <summary>
        /// Hàm khởi tạo
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="displayName"></param>
        /// <param name="typee"></param>
        /// <param name="passWordd"></param>
        public Account(string userName, string displayName, int typee, string password = null)
        {
            this.UserName = userName;
            this.DisplayName = displayName;
            this.Typee = typee;
            this.Password = password;
        }

        public Account(DataRow row)
        {
            this.UserName = row["userName"].ToString();
            this.DisplayName = row["displayName"].ToString();
            this.Typee = (int)row["typee"];
            this.Password = row["passwordd"].ToString();
        }

        private int typee;

        public int Typee
        {
            get { return typee; }
            set { typee = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string displayName;

        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        private string userName;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
