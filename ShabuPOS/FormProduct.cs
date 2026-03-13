using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http.Json;
using System.Windows.Forms;
using BuffetAPI.Data;
using BuffetAPI.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ShabuPOS
{
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
        }

        private string ApiBaseUrl = "http://localhost:5000";
        private string _selectedProId = "";
        private string _sourceImagePath = "";

        // ⚠️ อย่าลืมเปลี่ยน Path นี้ให้ตรงกับโฟลเดอร์เว็บของคุณ
        private readonly string webPublicFolder = @"C:\Users\peera\Downloads\buffet-ordering-app\public";

        private void ResetToAddMode()
        {
            labelMode.Text = @"โหมด: เพิ่มข้อมูล";
            labelMode.BackColor = Color.Green;
            labelMode.ForeColor = Color.White;
            _selectedProId = "";
            textProId.Enabled = true;

            //LoadProducts();
            dgvProducts.ClearSelection();
            ClearForm();
        }

        private void ResetToEditOrDeleteMode()
        {
            labelMode.Text = @"โหมด: แก้ไข/ลบข้อมูล";
            labelMode.BackColor = Color.Yellow;
            labelMode.ForeColor = Color.Black;
            textProId.Enabled = false;
        }

        private void ClearForm()
        {
            textProId.Text = "";
            textProName.Text = "";
            textProQuan.Text = "0";
            textProStock.Text = "0";
            textProUnit.Text = "";
            comboPR.SelectedIndex = -1;
            comboPO.SelectedIndex = -1;
            picProImage.Image = null;
            _sourceImagePath = "";
        }

        private void InitComboBoxes()
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    // โหลด PR เข้า ComboBox
                    var prList = db.PurchaseRequisitions.Select(pr => new { pr.PR_ID }).ToList();
                    comboPR.DataSource = prList;
                    comboPR.DisplayMember = "PR_ID";
                    comboPR.ValueMember = "PR_ID";
                    comboPR.SelectedIndex = -1;

                    // โหลด PO เข้า ComboBox
                    var poList = db.PurchaseOrders.Select(po => new { po.Po_id }).ToList();
                    comboPO.DataSource = poList;
                    comboPO.DisplayMember = "Po_id";
                    comboPO.ValueMember = "Po_id";
                    comboPO.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("โหลดข้อมูล ComboBox ไม่สำเร็จ: " + ex.Message);
            }
        }

        // คลาสจำลองสำหรับแสดงในตาราง (รวมรูปภาพ)
        public class ProductGridDisplay
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public int Stock { get; set; }
            public string Unit { get; set; }
            //public Image ProImage { get; set; }
        }

        private void LoadProducts()
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var rawProducts = db.Products.ToList();

                    var displayList = rawProducts.Select(p =>
                    {
                        Image loadedImage = null;

                        if (!string.IsNullOrEmpty(p.Pro_image))
                        {
                            string cleanPath = p.Pro_image.Replace("/", "\\");
                            string imgPath = webPublicFolder + cleanPath;

                            if (File.Exists(imgPath))
                            {
                                try
                                {
                                    using (var bmpTemp = new Bitmap(imgPath))
                                    {
                                        loadedImage = new Bitmap(bmpTemp);
                                    }
                                }
                                catch { }
                            }
                        }

                        return new ProductGridDisplay
                        {
                            Id = p.Pro_id,
                            Name = p.Pro_name,
                            Stock = p.Pro_stock ?? 0,
                            Unit = p.Pro_unit,
                            //ProImage = loadedImage
                        };
                    }).ToList();

                    dgvProducts.DataSource = displayList;

                    // จัดรูปแบบตาราง
                    dgvProducts.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                    dgvProducts.AllowUserToResizeRows = false;

                    foreach (DataGridViewRow row in dgvProducts.Rows)
                    {
                        row.Height = 60; // ปรับความสูงแถว
                    }

                    //if (dgvProducts.Columns.Contains("ProImage"))
                    //{
                    //    dgvProducts.Columns["ProImage"].HeaderText = "รูปสินค้า";
                    //    DataGridViewImageColumn imgCol = (DataGridViewImageColumn)dgvProducts.Columns["ProImage"];
                    //    imgCol.ImageLayout = DataGridViewImageCellLayout.Zoom;
                    //    dgvProducts.Columns["ProImage"].Width = 80;
                    //}

                    dgvProducts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error");
            }
        }

        private string SaveImageFile(string proId)
        {
            if (string.IsNullOrEmpty(_sourceImagePath)) return null;

            try
            {
                string destFolder = Path.Combine(webPublicFolder, "images");
                if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);

                string extension = Path.GetExtension(_sourceImagePath);
                string newFileName = "P_" + proId + extension; // ตั้งชื่อไฟล์หลบเมนูอาหาร เช่น P_PRO01.jpg
                string destPath = Path.Combine(destFolder, newFileName);

                File.Copy(_sourceImagePath, destPath, true);
                return "/images/" + newFileName;
            }
            catch (Exception ex)
            {
                MessageBox.Show("เซฟรูปไม่สำเร็จ: " + ex.Message);
                return null;
            }
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            dgvProducts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProducts.ReadOnly = true;
            dgvProducts.MultiSelect = false;

            InitComboBoxes();
            ResetToAddMode();

            // ✅ เรียกใช้ ThemeConfig เพื่อตกแต่งตาราง!
            ThemeConfig.FormatMinimalistDataGridView(dgvProducts);
            dgvProducts.DataSource = null;

            // ✅ ตกแต่งปุ่มพร้อมใส่ Hover Effect
            ThemeConfig.StyleButtonAdd(buttonAdd);
            ThemeConfig.StyleButtonEdit(buttonEdit);
            ThemeConfig.StyleButtonDelete(buttonDelete);

            ThemeConfig.ApplyGlobalFont(this);
        }

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _sourceImagePath = ofd.FileName;
                    using (var bmpTemp = new Bitmap(_sourceImagePath))
                    {
                        picProImage.Image = new Bitmap(bmpTemp);
                    }
                }
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvProducts.Rows[e.RowIndex];
                _selectedProId = row.Cells["Id"].Value?.ToString();

                using (var db = DbConfig.GetDbContext())
                {
                    var pro = db.Products.Find(_selectedProId);
                    if (pro != null)
                    {
                        textProId.Text = pro.Pro_id;
                        textProName.Text = pro.Pro_name;
                        textProQuan.Text = pro.Pro_quan?.ToString();
                        textProStock.Text = pro.Pro_stock?.ToString();
                        textProUnit.Text = pro.Pro_unit;
                        //comboPR.SelectedValue = pro.PR_ID ?? "";
                        //comboPO.SelectedValue = pro.Po_id ?? "";

                        // โหลดรูปภาพ
                        if (!string.IsNullOrEmpty(pro.Pro_image))
                        {
                            string cleanPath = pro.Pro_image.Replace("/", "\\");
                            string imgPath = webPublicFolder + cleanPath;
                            if (File.Exists(imgPath))
                            {
                                using (var bmpTemp = new Bitmap(imgPath))
                                {
                                    picProImage.Image = new Bitmap(bmpTemp);
                                }
                            }
                            else { picProImage.Image = null; }
                        }
                        else { picProImage.Image = null; }
                    }
                }
                ResetToEditOrDeleteMode();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedProId))
            {
                ResetToAddMode();
                textProId.Focus();
                return;
            }

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    if (db.Products.Any(p => p.Pro_id == textProId.Text.Trim()))
                    {
                        MessageBox.Show("รหัสสินค้านี้มีในระบบแล้ว", "แจ้งเตือน");
                        return;
                    }

                    string savedFileName = SaveImageFile(textProId.Text.Trim());

                    var newPro = new Product
                    {
                        Pro_id = textProId.Text.Trim(),
                        Pro_name = textProName.Text.Trim(),
                        Pro_quan = int.TryParse(textProQuan.Text, out int q) ? q : 0,
                        Pro_stock = int.TryParse(textProStock.Text, out int s) ? s : 0,
                        Pro_unit = textProUnit.Text.Trim(),
                        //PR_ID = comboPR.SelectedValue?.ToString(),
                        //Po_id = comboPO.SelectedValue?.ToString(),
                        Pro_image = savedFileName
                    };

                    db.Products.Add(newPro);
                    db.SaveChanges();
                }
                MessageBox.Show("เพิ่มข้อมูลสำเร็จ");
                ResetToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProId)) return;

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var pro = db.Products.Find(_selectedProId);
                    if (pro != null)
                    {
                        string savedFileName = SaveImageFile(_selectedProId);

                        pro.Pro_name = textProName.Text.Trim();
                        pro.Pro_quan = int.TryParse(textProQuan.Text, out int q) ? q : 0;
                        pro.Pro_stock = int.TryParse(textProStock.Text, out int s) ? s : 0;
                        pro.Pro_unit = textProUnit.Text.Trim();
                        //pro.PR_ID = comboPR.SelectedValue?.ToString();
                        //pro.Po_id = comboPO.SelectedValue?.ToString();

                        // อัปเดตรูปเฉพาะเมื่อมีการเลือกรูปใหม่
                        if (savedFileName != null)
                        {
                            pro.Pro_image = savedFileName;
                        }
                    }
                    db.SaveChanges();
                }
                MessageBox.Show("แก้ไขข้อมูลสำเร็จ");
                ResetToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedProId))
            {
                MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการลบจากตารางก่อนครับ");
                return;
            }

            if (MessageBox.Show($"ยืนยันการลบสินค้า {_selectedProId} ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    using (var db = DbConfig.GetDbContext())
                    {
                        var pro = db.Products.Find(_selectedProId);
                        if (pro != null)
                        {
                            db.Products.Remove(pro);
                            db.SaveChanges();
                            MessageBox.Show("ลบข้อมูลสำเร็จแล้วครับ");
                            ResetToAddMode();
                        }
                        else
                        {
                            MessageBox.Show("ไม่พบข้อมูลสินค้าในระบบ");
                        }
                    }
                }
                catch (Exception ex)
                {
                    // ✅ จุดสำคัญ: โชว์ข้อความ Error จาก Database [cite: 2026-02-26]
                    MessageBox.Show("ไม่สามารถลบได้ เนื่องจาก: " + ex.InnerException?.Message ?? ex.Message);
                }
            }
        }

        private void btnExecuteSearch_Click(object sender, EventArgs e)
        {
            string category = cboStockCategoryFilter.Text; // เช่น "เนื้อสัตว์"
            string keyword = txtSearchProductName.Text.Trim();

            // 1. กำหนด Connection String (ใช้ตัวที่คุณเทสผ่านในหน้า Add Connection) [cite: 2026-03-07]
            string connString = DbConfig.ConnectionString;

            // 2. เขียนคำสั่ง SQL ที่ใช้ LIKE และ AND เพื่อกรองข้อมูล [cite: 2026-02-26]
            string sql = @"
        SELECT 
            Pro_id AS [Id], 
            Pro_name AS [Name], 
            Pro_stock AS [Stock], 
            Pro_unit AS [Unit]
        FROM Product
        WHERE (Pro_category = @cat OR @cat = 'ทั้งหมด')
        AND (Pro_name LIKE @key OR @key = '')";

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    // 3. เตรียม Command และใส่ Parameter เพื่อป้องกัน SQL Injection
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@cat", category);
                    cmd.Parameters.AddWithValue("@key", "%" + keyword + "%");

                    // 4. ดึงข้อมูลใส่ DataTable
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    // 5. นำข้อมูลไปโชว์ในตาราง (DataGridView)
                    dgvProducts.DataSource = dt;

                    // ตกแต่งตาราง
                    ThemeConfig.FormatMinimalistDataGridView(dgvProducts);

                    if (dt.Rows.Count == 0) MessageBox.Show("ไม่พบข้อมูลที่ค้นหา");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("เกิดข้อผิดพลาดในการ SQL: " + ex.Message);
            }
        }

        public class ProductItem
        {
            public int Product_ID { get; set; }
            public string Product_Name { get; set; }
            public string Category { get; set; }
            public int Stock_Qty { get; set; }
            public decimal Price { get; set; }
        }
    }
}