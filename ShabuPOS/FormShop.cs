using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;   // ดึง DbContext
using BuffetAPI.Models; // ดึง Model

namespace ShabuPOS // ⚠️ เปลี่ยนให้ตรงกับชื่อโปรเจกต์ของคุณ
{
    public partial class FormShop : Form
    {
        public FormShop()
        {
            InitializeComponent();
        }

        // ตัวแปรเก็บรหัสร้านค้าที่กำลังเลือก
        private string _selectedShopId = "";

        private void ResetToAddMode()
        {
            labelMode.Text = @"โหมด: เพิ่มข้อมูล";
            labelMode.BackColor = Color.Green;
            labelMode.ForeColor = Color.White;
            _selectedShopId = "";
            textShopId.Enabled = true; // เปิดให้พิมพ์รหัสร้านได้
            LoadShops();
            dgvShops.ClearSelection();
            ClearForm();
        }

        private void ResetToEditOrDeleteMode()
        {
            labelMode.Text = @"โหมด: แก้ไข/ลบข้อมูล";
            labelMode.BackColor = Color.Yellow;
            labelMode.ForeColor = Color.Black;
            textShopId.Enabled = false; // โหมดแก้ไข ห้ามแก้รหัสร้าน
        }

        private void ClearForm()
        {
            textShopId.Text = "";
            textShopName.Text = "";
            textShopAddress.Text = "";
            textShopPhone.Text = "";
        }

        private void LoadShops()
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    // --- R: อ่านข้อมูลร้านค้ามาแสดงตาราง ---
                    var shops = db.Shops
                        .Select(s => new
                        {
                            Id = s.ShopId,
                            Name = s.ShopName,
                            Phone = s.ShopPhone,
                            Address = s.ShopAddress
                        }).ToList();

                    dgvShops.DataSource = shops;
                    dgvShops.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(textShopId.Text))
            {
                MessageBox.Show("กรุณาระบุรหัสสาขา");
                return false;
            }
            if (string.IsNullOrWhiteSpace(textShopName.Text))
            {
                MessageBox.Show("กรุณาระบุชื่อสาขา");
                return false;
            }
            return true;
        }

        private void FormShop_Load(object sender, EventArgs e)
        {
            dgvShops.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvShops.ReadOnly = true;
            dgvShops.MultiSelect = false;

            ResetToAddMode();

            // ✅ เรียกใช้ ThemeConfig เพื่อตกแต่งตาราง!
            ThemeConfig.FormatMinimalistDataGridView(dgvShops);

            // ✅ ตกแต่งปุ่มพร้อมใส่ Hover Effect
            ThemeConfig.StyleButtonAdd(buttonAdd);
            ThemeConfig.StyleButtonEdit(buttonEdit);
            ThemeConfig.StyleButtonDelete(buttonDelete);

            ThemeConfig.ApplyGlobalFont(this);
        }

        private void dgvShops_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvShops.Rows[e.RowIndex];
                _selectedShopId = row.Cells["Id"].Value?.ToString();

                // โยนข้อมูลกลับไปที่ TextBox
                textShopId.Text = row.Cells["Id"].Value?.ToString();
                textShopName.Text = row.Cells["Name"].Value?.ToString();
                textShopPhone.Text = row.Cells["Phone"].Value?.ToString();
                textShopAddress.Text = row.Cells["Address"].Value?.ToString();

                ResetToEditOrDeleteMode(); // เปลี่ยนเป็นแถบสีเหลือง
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // ถ้าแถบเป็นสีเหลืองอยู่ (มีข้อมูลเลือกไว้) การกด Add จะทำหน้าที่เคลียร์ฟอร์มให้เป็นสีเขียว
            if (!string.IsNullOrEmpty(_selectedShopId))
            {
                ResetToAddMode();
                textShopId.Focus();
                return;
            }

            if (!ValidateInput()) { return; }

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    // เช็คว่ารหัสสาขาซ้ำไหม
                    bool isExist = db.Shops.Any(s => s.ShopId == textShopId.Text.Trim());
                    if (isExist)
                    {
                        MessageBox.Show("รหัสสาขานี้มีในระบบแล้ว กรุณาใช้รหัสอื่น", "แจ้งเตือน");
                        return;
                    }

                    // --- A: เพิ่มข้อมูล ---
                    var newShop = new Shop
                    {
                        ShopId = textShopId.Text.Trim(),
                        ShopName = textShopName.Text.Trim(),
                        ShopAddress = textShopAddress.Text.Trim(),
                        ShopPhone = textShopPhone.Text.Trim()
                    };

                    db.Shops.Add(newShop);
                    db.SaveChanges();
                }

                MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "Success");
                ResetToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedShopId))
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการแก้ไข");
                return;
            }

            if (!ValidateInput()) { return; }

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    // --- U: แก้ไขข้อมูล ---
                    var shop = db.Shops.Find(_selectedShopId);
                    if (shop != null)
                    {
                        shop.ShopName = textShopName.Text.Trim();
                        shop.ShopAddress = textShopAddress.Text.Trim();
                        shop.ShopPhone = textShopPhone.Text.Trim();
                    }
                    db.SaveChanges();
                }

                MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "Success");
                ResetToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedShopId))
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการลบ");
                return;
            }

            try
            {
                if (MessageBox.Show("ยืนยันการลบ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    using (var db = DbConfig.GetDbContext())
                    {
                        // ⚠️ เช็คก่อนว่ามีพนักงานสังกัดร้านนี้อยู่ไหม? (สไตล์อาจารย์)
                        bool hasEmployees = db.Employees.Any(emp => emp.ShopId == _selectedShopId);
                        if (hasEmployees)
                        {
                            MessageBox.Show("ไม่สามารถลบสาขานี้ได้ เพราะมีพนักงานสังกัดอยู่\nกรุณาย้ายหรือลบพนักงานก่อน", "ลบไม่ได้", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        // --- D: ลบข้อมูล ---
                        var shop = db.Shops.Find(_selectedShopId);
                        if (shop != null)
                        {
                            db.Shops.Remove(shop);
                            db.SaveChanges();
                            MessageBox.Show("ลบข้อมูลสำเร็จ", "Success");
                            ResetToAddMode();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error");
            }
        }
    }
}