using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy_Cafe.DAO
{
    public class DataProvider
    {

        private static DataProvider instance;

        public static DataProvider Instance
        {
            get 
            { 
                if (instance == null)
	            {
		            instance = new DataProvider();
	            }
                return
                DataProvider.instance; 
            }
            private set { DataProvider.instance = value; }// chỉ trong nội bộ class mới được set dữ liệu vào bên ngoài thi không được phép
        }

        /// <summary>
        /// hàm khởi tạo
        /// </summary>
        private DataProvider() { }

        private string connectionSTR = @"Data Source=DESKTOP-KN98LAA;Initial Catalog=QuanLyQuanCafe123;Integrated Security=True";

        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {
            DataTable data = new DataTable();
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection); // thi hành câu truy vấn


                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@')) // nếu chuỗi item có chứa dấu @ thì chuỗi này có chứa parameter
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }


                SqlDataAdapter adapter = new SqlDataAdapter(command); // trung gian để lấy dữ liệu ra

                adapter.Fill(data); // đổ dữ liệu ra biến data

                connection.Close();
            }

            return data;
        } // trả ra nhưng dòng kết quả


        /// <summary>
        /// hàm trả ra số dòng thanh công
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection); // thi hành câu truy vấn


                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@')) // nếu chuỗi item có chứa dấu @ thì chuỗi này có chứa parameter
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }


                //SqlDataAdapter adapter = new SqlDataAdapter(command); // trung gian để lấy dữ liệu ra

                //adapter.Fill(data); // đổ dữ liệu ra biến data

                data = command.ExecuteNonQuery();

                connection.Close();
            }

            return data;
        } // INSERT, UPDATE, DELETE trả ra số dòng được thực thi

        public object ExecuteScalar(string query, object[] parameter = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionSTR))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(query, connection); // thi hành câu truy vấn


                if (parameter != null)
                {
                    string[] listpara = query.Split(' ');

                    int i = 0;
                    foreach (string item in listpara)
                    {
                        if (item.Contains('@')) // nếu chuỗi item có chứa dấu @ thì chuỗi này có chứa parameter
                        {
                            command.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }


                //SqlDataAdapter adapter = new SqlDataAdapter(command); // trung gian để lấy dữ liệu ra

                //adapter.Fill(data); // đổ dữ liệu ra biến data

                data = command.ExecuteScalar();

                connection.Close();
            }

            return data;
        } // trả ra kết quả của SELECT count(*) FROM Account WHERE UserName = 'admin' AND PassWordd = '1'
    }
}
