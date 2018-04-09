using QuanLy_Cafe.DAO;
using QuanLy_Cafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy_Cafe
{
    public partial class fAdmin : Form
    {
        BindingSource foodList = new BindingSource();

        BindingSource accountList = new BindingSource();
        public fAdmin()
        {
            InitializeComponent();
            Loadd();
            
        }
        #region Methords

        /// <summary>
        /// Hàm hiển thị danh sách món ăn sau khi tìm kiếm thành công
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        List<Food> SearchFoodByName(string name)
        {
            List<Food> listFood = new List<Food>();

            listFood = FoodDAO.Instance.SearchFoodByName(name);

            return listFood;
        }

        void Loadd()
        {
            dtgvFood.DataSource = foodList;
            dtgvAccount.DataSource = accountList;

            //LoadAccountList();
            LoadDatePickerBill();
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
            LoadListFood();
            LoadAccount();
            LoadCategoryIntoCombobox(cbFoodCategory);
            AddFoodBinding();
            AddAccountBiding();
        }
        /// <summary>
        /// bindings form Account
        /// </summary>
        void AddAccountBiding()
        {
            txbUserName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "UserName", true, DataSourceUpdateMode.Never));
            txbDisplayName.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "DisplayName", true, DataSourceUpdateMode.Never));
            txbAccountType.DataBindings.Add(new Binding("Text", dtgvAccount.DataSource, "Typee", true, DataSourceUpdateMode.Never));

        }


        void LoadAccount()
        {
            accountList.DataSource = AccountDAO.Instance.GetListAccount();
        }

        /// <summary>
        /// Đặt lại ngày
        /// </summary>
        void LoadDatePickerBill()
        {
            DateTime today = DateTime.Now;
            dtpkFromDate.Value = new DateTime(today.Year, today.Month, 1);
            dtpkToDate.Value = dtpkFromDate.Value.AddMonths(1).AddDays(-1);
        }

        /// <summary>
        /// Hiển thị danh sách hóa đơn theo ngày
        /// </summary>
        /// <param name="checkIn"></param>
        /// <param name="checkOut"></param>
        void LoadListBillByDate(DateTime checkIn, DateTime checkOut)
        {
            dtgvBill.DataSource = BillDAO.Instance.GetBillListByDate(checkIn, checkOut);
        }

        /// <summary>
        /// Binding data cho ListFood
        /// </summary>
        void AddFoodBinding()
        {
            txbFoodName.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "name", true, DataSourceUpdateMode.Never));
            txbFoodID.DataBindings.Add(new Binding("Text", dtgvFood.DataSource, "id", true, DataSourceUpdateMode.Never));
            nmFoodPrice.DataBindings.Add(new Binding("value", dtgvFood.DataSource, "price", true, DataSourceUpdateMode.Never));
        }

        void LoadCategoryIntoCombobox(ComboBox cb)
        {
            cb.DataSource = CategoryDAO.Instance.GetListCategory();
            cb.DisplayMember = "name";
        }

        /// <summary>
        /// Hiển thị danh sách các món ăn
        /// </summary>
        void LoadListFood()
        {
            foodList.DataSource = FoodDAO.Instance.GetListFood();
        }
        
        #endregion

        #region Events

        private void btnViewBill_Click(object sender, EventArgs e)
        {
            LoadListBillByDate(dtpkFromDate.Value, dtpkToDate.Value);
        }

        private void btnShowFood_Click(object sender, EventArgs e)
        {
            LoadListFood();
        }

        private void txbFoodID_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (dtgvFood.SelectedCells.Count > 0)
                {
                    int id = (int)dtgvFood.SelectedCells[0].OwningRow.Cells["CategoryID"].Value;

                    Category cateogory = CategoryDAO.Instance.GetCategoryByID(id);
                    cbFoodCategory.SelectedItem = cateogory;

                    int index = -1;
                    int i = 0;

                    foreach (Category item in cbFoodCategory.Items)
                    {
                        if (item.ID == cateogory.ID)
                        {
                            index = i;
                            break;
                        }
                        i++;
                    }

                    cbFoodCategory.SelectedIndex = index;
                }
            }
            catch { }
        }

        private void btnAddFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;

            if (FoodDAO.Instance.InsertFood(name, categoryID, price))
            {
                MessageBox.Show("Thêm món thành công", "Thông báo");
                LoadListFood();
                if (insertFood != null)
                {
                    insertFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi thêm món", "Thông báo");
            }
        }

        private void btnEditFood_Click(object sender, EventArgs e)
        {
            string name = txbFoodName.Text;
            int categoryID = (cbFoodCategory.SelectedItem as Category).ID;
            float price = (float)nmFoodPrice.Value;
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.UpdateFood(id, name, categoryID, price))
            {
                MessageBox.Show("Sửa món thành công", "Thông báo");
                LoadListFood();
                if(updateFood != null)
                {
                    updateFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi sửa món", "Thông báo");
            }
        }

        private void btnDeleteFood_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(txbFoodID.Text);

            if (FoodDAO.Instance.DeleteFood(id))
            {
                MessageBox.Show("Xóa món thành công", "Thông báo");
                LoadListFood();
                if (deleteFood != null)
                {
                    deleteFood(this, new EventArgs());
                }
            }
            else
            {
                MessageBox.Show("Có lỗi khi Xóa món", "Thông báo");
            }
        }

        private event EventHandler insertFood;
        public event EventHandler InsertFood
        {
            add { insertFood += value; }
            remove { insertFood -= value; }
        }

        private event EventHandler deleteFood;
        public event EventHandler DeleteFood
        {
            add { deleteFood += value; }
            remove { deleteFood -= value; }
        }

        private event EventHandler updateFood;
        public event EventHandler UpdateFood
        {
            add { updateFood += value; }
            remove { updateFood -= value; }
        }

        #endregion

        private void btnSearchFood_Click(object sender, EventArgs e)
        {
           foodList.DataSource = SearchFoodByName(txbSearchFoodName.Text);
        }

        private void btnShowAccount_Click(object sender, EventArgs e)
        {
            LoadAccount();
        }



        
        

        /// <summary>
        /// Hàm show ra danh sách thức ăn
        /// </summary>
        //void LoadListFood()
        //{
        //    string query = "SELECT * FROM Food";

        //    //DataProvider provider = new DataProvider();
        //    dtgvFood.DataSource = DataProvider.Instance.ExecuteQuery(query);
        //}

        /// <summary>
        /// Hàm show ra danh sách tài khoản
        /// </summary>
        //void LoadAccountList()
        //{
            
        //    //string connectionSTR = @"Data Source=DESKTOP-KN98LAA;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";
        //    //SqlConnection connection = new SqlConnection(connectionSTR);

        //    //connection.Open();
        //    //string query = "SELECT * FROM Account"; // câu lệnh truy vấn
            
        //    //SqlCommand command = new SqlCommand(query, connection); // thi hành câu truy vấn
            
        //    //DataTable data = new DataTable();

        //    //SqlDataAdapter adapter = new SqlDataAdapter(command); // trung gian để lấy dữ liệu ra

        //    //adapter.Fill(data); // đổ dữ liệu ra biến data
        //    //dtgvAccount.DataSource = data;

        //    //connection.Close();

        //    //string query = "SELECT * FROM Account"; // câu lệnh truy vấn
        //   //string query = "SELECT DisplayName as [Tên hiển thi] FROM Account";

        //    string query = "EXEC dbo.USP_GetAccountByUserName @userName"; 

        //    //DataProvider provider = new DataProvider();
        //    dtgvAccount.DataSource = DataProvider.Instance.ExecuteQuery(query, new object[] { "staff" });
        //}
       
    }
}
