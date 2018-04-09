using QuanLy_Cafe.DAO;
using QuanLy_Cafe.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLy_Cafe
{
    public partial class fTableManager : Form
    {
        private Account loginAccount;

        public Account LoginAccount
        {
            get { return loginAccount; }
            set { loginAccount = value; ChangeAccount(loginAccount.Typee); }
        }
        public fTableManager(Account acc)
        {
            InitializeComponent();

            this.LoginAccount = acc;

            LoadTable();
            LoadCategory();
            LoadComboBoxTable(cbSwitchTable);
        }
        #region Methord

        /// <summary>
        /// xử lý phân quyền
        /// </summary>
        /// <param name="typee"></param>
        void ChangeAccount(int type)
        {
            adminToolStripMenuItem.Enabled = type == 1;
            thôngTinTàiKhoảnToolStripMenuItem.Text += " (" + LoginAccount.DisplayName + ")";
        }


        void LoadCategory()
        {
            List<Category> listCategory = CategoryDAO.Instance.GetListCategory();
            cbCategory.DataSource = listCategory;
            cbCategory.DisplayMember = "name";
            

        }

        void LoadFoodByCategoryID(int id)
        {
            List<Food> listFood = FoodDAO.Instance.GetFoodByCategoryID(id);
            cbFood.DataSource = listFood;
            cbFood.DisplayMember = "name";
            //txbPrice.DataBindings.Add(new Binding("Text", cbFood.DataSource, "Price"));
            //txbPrice.DataBindings.Add("DataSource", cbFood.SelectedItem, "listFood");
        }

        /// <summary>
        /// Hàm load danh sách bàn
        /// </summary>
        void LoadTable()
        {
            flpTable.Controls.Clear();
            List<Table> tablelist = TableDAO.Instance.LoadTableList();
            foreach (Table item in tablelist)
            {
                Button btn = new Button() {Width = TableDAO.TableHeght, Height = TableDAO.TableHeght };
                btn.Text = item.Name + Environment.NewLine + item.Statuss;
                btn.Click += btn_Click;
                btn.Tag = item; // thẻ Tag có kiểu dữ liệu là Obj
                switch(item.Statuss)
                {
                    case "Trống":
                        btn.BackColor = Color.Aqua;
                        break;
                    default:
                        btn.BackColor = Color.DeepPink;
                        break;
                }

                flpTable.Controls.Add(btn);
            }
        }

        void ShowBill(int id)
        {
            lsvBill.Items.Clear();
            
            List<QuanLy_Cafe.DTO.Menu> listBillInfo = MenuDAO.Instance.GetListMenuByTable(id);

            float totalPrice = 0;

            foreach (QuanLy_Cafe.DTO.Menu item in listBillInfo)
            {
                ListViewItem lsvItem = new ListViewItem(item.FoodName.ToString());

                lsvItem.SubItems.Add(item.Countt.ToString());
                lsvItem.SubItems.Add(item.Price.ToString());
                lsvItem.SubItems.Add(item.TotalPrice.ToString());

                totalPrice += item.TotalPrice;

                lsvBill.Items.Add(lsvItem);
            }
            CultureInfo culture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentCulture = culture;
            //txbTotalPrice.Text = totalPrice.ToString("c", culture);
            txbTotalPrice.Text = totalPrice.ToString("c");
            
        }

        void LoadComboBoxTable(ComboBox cb)
        {
            cb.DataSource = TableDAO.Instance.LoadTableList();
            cb.DisplayMember = "name";
        }

        #endregion

        /////////////////////////////////////////////////////////////////////////////////////////////
        
        #region Events

        void btn_Click(object sender, EventArgs e)
        {
            int TableID = ((sender as Button).Tag as Table).ID;
            lsvBill.Tag = (sender as Button).Tag;
            ShowBill(TableID);
        }
        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void thôngTinCáNhânToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAccountProfile f = new fAccountProfile(LoginAccount);
            f.UpdateAccount += f_UpdateAccount;
            f.ShowDialog();
        }

        /// <summary>
        /// đưa dữ liệu từ from con về from cha
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void f_UpdateAccount(object sender, AccountEvent e)
        {
            thôngTinTàiKhoảnToolStripMenuItem.Text = "Thông tin tài khoản (" + e.Acc.DisplayName + ")";
        }

        private void adminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            fAdmin f = new fAdmin();
            f.InsertFood += f_InsertFood;
            f.DeleteFood += f_DeleteFood;
            f.UpdateFood += f_UpdateFood;
            f.ShowDialog();
        }

        void f_UpdateFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }
        }

        void f_DeleteFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }
            LoadTable();
            
        }

        void f_InsertFood(object sender, EventArgs e)
        {
            LoadFoodByCategoryID((cbCategory.SelectedItem as Category).ID);
            if (lsvBill.Tag != null)
            {
                ShowBill((lsvBill.Tag as Table).ID);
            }
            //LoadTable();
        }

        private void cbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            int id = 0;

            ComboBox cb = sender as ComboBox;
            if (cb.SelectedItem == null)
            {
                return;
            }
            Category selected = cb.SelectedItem as Category;
            id = selected.ID;

            LoadFoodByCategoryID(id);
        }

        /// <summary>
        /// Hàm thêm thức ăn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddFood_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            if (table == null)
            {
                MessageBox.Show("Mời bạn chòn bàn cần thêm", "Thông báo");
                return;
            }
            int idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);

            int idFood = (cbFood.SelectedItem as Food).ID;
            int countt = (int)nmFoodCount.Value;

            if (idBill == -1) // nếu chưa tòn tại Bill thì tạo Bill mới và tạo BillInfo
            {
                BillDAO.Instance.InsertBill(table.ID);
                BillInfoDAO.Instance.InsertBillInfo(BillDAO.Instance.GetMaxIDBill(), idFood, countt);
            }
            else // nếu Bill đã tồn tại thì tạo BillInfo
            {
                BillInfoDAO.Instance.InsertBillInfo(idBill, idFood, countt);
            }
            ShowBill(table.ID);
            LoadTable();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            Table table = lsvBill.Tag as Table;

            int idBill = 0;
            try
            {
                idBill = BillDAO.Instance.GetUncheckBillIDByTableID(table.ID);
            }
            catch (Exception)
            {

                return;
            }
            

            int discount = (int)nmDiscount.Value;
            float totalPeice = (float)(Convert.ToDouble(txbTotalPrice.Text.Split(',')[0]));//cắt chuỗi theo ký tự ',' và lấy ra phần đầu tiên
            float finalTotalPrice = totalPeice - (totalPeice / 100) * discount;

            if (idBill != -1) // nếu như đang tồn tại Bill thi thanh toán
            {
                if (MessageBox.Show(string.Format("Bạn có chắc là muốn thanh toán hóa đơn cho {0}\nTổng tiền - (Tổng tiền / 100) x Giảm giá = {1} - ({1} / 100) x {2} = {3}\nSố tiền cần thanh toán là {3}", table.Name, totalPeice, discount, finalTotalPrice), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    BillDAO.Instance.CheckOut(idBill, discount, finalTotalPrice);
                    ShowBill(table.ID);
                    LoadTable();
                }
            }
        }

        private void btnSwitchTable_Click(object sender, EventArgs e)
        {
            int id1 = (lsvBill.Tag as Table).ID;
            int id2 = (cbSwitchTable.SelectedItem as Table).ID;

            if (MessageBox.Show(string.Format("Bạn có thật sự muốn chuyển bàn {0} sang bàn {1}", (lsvBill.Tag as Table).Name, (cbSwitchTable.SelectedItem as Table).Name), "Thông báo", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                TableDAO.Instance.SwitchTable(id1, id2);

                LoadTable();
            }
        }
        #endregion










    }
}
