using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;
using BuffetAPI.Models;

namespace ShabuPOS
{
    public partial class FormMenu : Form
    {
        public FormMenu()
        {
            InitializeComponent();
        }

        private string _selectedMenuId = "";
        private string _sourceImagePath = "";

        // ⚠️ อย่าลืมเช็ค Path นี้ให้ตรงกับเครื่องคุณ
        private readonly string webPublicFolder = @"C:\Users\peera\Downloads\buffet-ordering-app\public";

        private void ResetToAddMode()
        {
            labelMode.Text = @"โหมด: เพิ่มข้อมูล";
            labelMode.BackColor = Color.Green;
            labelMode.ForeColor = Color.White;
            _selectedMenuId = "";
            textMenuId.Enabled = true;

            LoadMenus();
            dgvMenus.ClearSelection();
            ClearForm();
        }

        private void ResetToEditOrDeleteMode()
        {
            labelMode.Text = @"โหมด: แก้ไข/ลบข้อมูล";
            labelMode.BackColor = Color.Yellow;
            labelMode.ForeColor = Color.Black;
            textMenuId.Enabled = false;
        }

        private void ClearForm()
        {
            textMenuId.Text = "";
            textMenuName.Text = "";
            textMenuPrice.Text = "";
            comboProduct.SelectedIndex = -1; // ล้างช่องเลือกสต๊อก
            picMenuImage.Image = null;
            _sourceImagePath = "";
        }

        // ✅ โหลดรายชื่อสินค้า/วัตถุดิบ (Product) ลง ComboBox
        private void InitProductComboBox()
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var proList = db.Products
                        .Select(p => new
                        {
                            ProId = p.Pro_id,
                            FullName = p.Pro_id + " : " + p.Pro_name
                        }).ToList();

                    comboProduct.DataSource = proList;
                    comboProduct.DisplayMember = "FullName";
                    comboProduct.ValueMember = "ProId";
                    comboProduct.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("โหลดข้อมูลสต๊อกไม่สำเร็จ: " + ex.Message);
            }
        }

        public class MenuGridDisplay
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Status { get; set; }
            public string LinkedStock { get; set; } // โชว์ว่าตัดสต๊อกตัวไหน
            //public Image FoodImage { get; set; }
        }

        private void LoadMenus()
        {
            // ป้องกันการ Error ตอนโหลดหน้าฟอร์มครั้งแรก
            if (cbPackageFilter.Items.Count == 0 || cbPackageFilter.SelectedIndex < 0)
            {
                dgvMenus.DataSource = null;
                return;
            }

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    // ✅ 1. รับค่าจาก ComboBox
                    // รับข้อความที่แสดงบนหน้าจอ เพื่อใช้เช็คเงื่อนไข (เช่น "แสดงทั้งหมด", "Silver")
                    string packageText = cbPackageFilter.Text;
                    // รับรหัสจาก ValueMember เพื่อใช้ค้นหาในฐานข้อมูล
                    string packageValue = cbPackageFilter.SelectedValue?.ToString() ?? "";

                    string statusText = cbStatusFilter.Text;

                    // ✅ 2. สร้าง Query แบบ AsQueryable 
                    var query = db.Menus.Include(m => m.Product)
                                        .Include(m => m.Packages)
                                        .AsQueryable();

                    // ✅ 3. เงื่อนไขการกรองตามแพ็กเกจ
                    // ถ้า 'ข้อความ' ไม่ใช่คำว่าแสดงทั้งหมด/กรุณาเลือก และ 'รหัส' ไม่เป็นค่าว่าง ค่อยทำการกรอง
                    if (packageText != "แสดงทั้งหมด" && packageText != "-- กรุณาเลือกแพ็กเกจ --" && !string.IsNullOrEmpty(packageValue))
                    {
                        query = query.Where(m => m.Packages.Any(p => p.Package_ID == packageValue));
                    }

                    // ✅ 4. เงื่อนไขการกรองตามสถานะ 
                    if (statusText == "พร้อมขาย")
                    {
                        query = query.Where(m => m.MenuStatus == "Available");
                    }
                    else if (statusText == "หมดชั่วคราว")
                    {
                        query = query.Where(m => m.MenuStatus == "OutOfStock");
                    }

                    // ✅ 5. สั่งรัน Query
                    var rawMenus = query.ToList();

                    var displayList = rawMenus.Select(m =>
                    {
                        // --- (Logic การโหลดรูปภาพเดิมของคุณ) ---
                        Image loadedImage = null;
                        if (!string.IsNullOrEmpty(m.MenuImage))
                        {
                            string cleanPath = m.MenuImage.Replace("/", "\\");
                            string imgPath = webPublicFolder + cleanPath;
                            if (File.Exists(imgPath))
                            {
                                try
                                {
                                    using (var bmpTemp = new Bitmap(imgPath))
                                    { loadedImage = new Bitmap(bmpTemp); }
                                }
                                catch { }
                            }
                        }

                        return new MenuGridDisplay
                        {
                            Id = m.MenuId,
                            Name = m.MenuName,
                            Price = m.MenuPrice ?? 0,
                            // ✅ แสดงสถานะใน Grid [cite: 2026-03-08]
                            Status = m.MenuStatus == "Available" ? "พร้อมขาย" : "หมดชั่วคราว",
                            LinkedStock = m.Product != null ? m.Product.Pro_name : "ไม่ตัดสต๊อก",
                            // FoodImage = loadedImage
                        };
                    }).ToList();

                    dgvMenus.DataSource = displayList;

                    // --- (การตั้งค่า Grid เดิมของคุณ) ---
                    dgvMenus.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
                    dgvMenus.AllowUserToResizeRows = false;
                    foreach (DataGridViewRow row in dgvMenus.Rows) { row.Height = 60; }
                    if (dgvMenus.Columns.Contains("Price")) { dgvMenus.Columns["Price"].DefaultCellStyle.Format = "N2"; }
                    dgvMenus.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error");
            }
        }

        private bool ValidateInput(out decimal price)
        {
            price = 0;
            if (string.IsNullOrWhiteSpace(textMenuId.Text)) { MessageBox.Show("ระบุรหัสเมนู"); return false; }
            if (string.IsNullOrWhiteSpace(textMenuName.Text)) { MessageBox.Show("ระบุชื่อเมนู"); return false; }
            if (!decimal.TryParse(textMenuPrice.Text, out price) || price < 0) { MessageBox.Show("ราคาต้องเป็นตัวเลข >= 0"); return false; }
            if (comboProduct.SelectedValue == null) { MessageBox.Show("กรุณาเลือกสินค้าที่ต้องการตัดสต๊อก"); return false; }
            return true;
        }

        private string SaveImageFile(string menuId)
        {
            if (string.IsNullOrEmpty(_sourceImagePath)) return null; // ไม่เซฟรูปใหม่
            try
            {
                string destFolder = Path.Combine(webPublicFolder, "images");
                if (!Directory.Exists(destFolder)) Directory.CreateDirectory(destFolder);

                string extension = Path.GetExtension(_sourceImagePath);
                string newFileName = menuId + extension;
                string destPath = Path.Combine(destFolder, newFileName);

                File.Copy(_sourceImagePath, destPath, true);
                return "/images/" + newFileName;
            }
            catch (Exception ex) { MessageBox.Show("เซฟรูปไม่สำเร็จ: " + ex.Message); return null; }
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            dgvMenus.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMenus.ReadOnly = true;
            dgvMenus.MultiSelect = false;

            InitProductComboBox(); // โหลดสต๊อกเข้า ComboBox
            ResetToAddMode();
            InitFilters();
            //InitFilterItems();
            InitEditPanelControls(); // ✅ เพิ่ม: โหลดรายชื่อแพ็กเกจลง CheckedListBox

            // ✅ เรียกใช้ ThemeConfig เพื่อตกแต่งตาราง!
            ThemeConfig.FormatMinimalistDataGridView(dgvMenus);

            // ✅ ตกแต่งปุ่มพร้อมใส่ Hover Effect
            ThemeConfig.StyleButtonAdd(buttonAdd);
            ThemeConfig.StyleButtonEdit(buttonEdit);
            ThemeConfig.StyleButtonDelete(buttonDelete);

            ThemeConfig.ApplyGlobalFont(this);
            LoadMenus();
        }

        // ✅ 1. โหลดรายชื่อแพ็กเกจที่มีทั้งหมดใน DB มาใส่ CheckedListBox ด้านขวา [cite: 2026-03-08]
        private void InitEditPanelControls()
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var packages = db.Packages.ToList();
                    clbPackages.Items.Clear();
                    foreach (var p in packages)
                    {
                        clbPackages.Items.Add(p.Package_Name);
                    }
                }
                cbEditStatus.Items.Clear();
                cbEditStatus.Items.AddRange(new string[] { "พร้อมขาย", "หมดชั่วคราว" });
            }
            catch (Exception ex) { MessageBox.Show("โหลดข้อมูลแพ็กเกจไม่สำเร็จ: " + ex.Message); }
        }

        // --- ฟังก์ชันช่วยเหลืออื่นๆ ---
        private void DisplayImage(string imgRelativePath)
        {
            if (!string.IsNullOrEmpty(imgRelativePath))
            {
                string imgPath = webPublicFolder + imgRelativePath.Replace("/", "\\");
                if (File.Exists(imgPath))
                {
                    using (var bmpTemp = new Bitmap(imgPath)) { picMenuImage.Image = new Bitmap(bmpTemp); }
                    return;
                }
            }
            picMenuImage.Image = null;
        }

        // ✅ แก้ไขฟังก์ชัน InitFilters ให้รองรับทั้งข้อมูลจาก DB และตัวเลือกพิเศษ
        private void InitFilters()
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    // 1. ดึงรายชื่อแพ็กเกจจริงจากฐานข้อมูล
                    var packages = db.Packages.Select(p => new {
                        ID = p.Package_ID,
                        Name = p.Package_Name
                    }).ToList();

                    // 2. สร้าง List ใหม่เพื่อรวมตัวเลือกพิเศษเข้ากับข้อมูลจาก DB [cite: 2026-03-08]
                    var filterList = new List<object>();
                    filterList.Add(new { ID = "", Name = "-- กรุณาเลือกแพ็กเกจ --" });

                    foreach (var p in packages)
                    {
                        filterList.Add(p);
                    }

                    filterList.Add(new { ID = "ALL", Name = "แสดงทั้งหมด" });

                    // 3. ผูกข้อมูลเข้ากับ ComboBox (เมื่อใช้ DataSource แล้ว ห้ามใช้ Items.Clear เด็ดขาด)
                    cbPackageFilter.DataSource = filterList;
                    cbPackageFilter.DisplayMember = "Name";
                    cbPackageFilter.ValueMember = "ID";
                }

                // สำหรับ Status Filter ถ้าไม่ได้ใช้ DataSource สามารถใช้ AddRange ได้ปกติ [cite: 2026-03-08]
                cbStatusFilter.Items.Clear();
                cbStatusFilter.Items.AddRange(new string[] {
            "-- กรุณาเลือกสถานะ --",
            "พร้อมขาย",
            "หมดชั่วคราว",
            "แสดงทั้งหมด"
        });

                cbPackageFilter.SelectedIndex = 0;
                cbStatusFilter.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("โหลดข้อมูล Filter ผิดพลาด: " + ex.Message);
            }
        }

        // ❌ ลบหรือคอมเมนต์ฟังก์ชัน InitFilterItems() ทิ้งไปเลยครับ เพราะมันทำให้เกิด Error
        /*
        private void InitFilterItems()
        {
            // โค้ดส่วนนี้จะทำให้ Error เพราะไปขัดกับ DataSource ของ InitFilters
        }
        */

        // สร้างฟังก์ชันแยกเพื่อความเป็นระเบียบ
        //private void InitFilterItems()
        //{
        //    // ใส่ข้อมูลให้ Package Filter
        //    cbPackageFilter.Items.Clear();
        //    cbPackageFilter.Items.AddRange(new string[] { "-- กรุณาเลือกแพ็กเกจ --", "Silver 299", "Gold 399", "แสดงทั้งหมด" });

        //    // ใส่ข้อมูลให้ Status Filter
        //    cbStatusFilter.Items.Clear();
        //    cbStatusFilter.Items.AddRange(new string[] { "-- กรุณาเลือกสถานะ --", "พร้อมขาย", "หมดชั่วคราว", "แสดงทั้งหมด" });

        //    // ✅ เมื่อมีข้อมูลแล้ว การสั่ง SelectedIndex = 0 จะไม่ Error อีกต่อไปครับ
        //    cbPackageFilter.SelectedIndex = 0;
        //    cbStatusFilter.SelectedIndex = 0;
        //}

        private void btnBrowseImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _sourceImagePath = ofd.FileName;
                    using (var bmpTemp = new Bitmap(_sourceImagePath)) { picMenuImage.Image = new Bitmap(bmpTemp); }
                }
            }
        }

        private void dgvMenus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvMenus.Rows[e.RowIndex];
                _selectedMenuId = row.Cells["Id"].Value?.ToString();

                using (var db = DbConfig.GetDbContext())
                {
                    // ต้อง Include Packages เพื่อเช็คว่าเมนูนี้อยู่แพ็กเกจไหนบ้าง
                    var menu = db.Menus.Include(m => m.Packages).FirstOrDefault(m => m.MenuId == _selectedMenuId);
                    if (menu != null)
                    {
                        textMenuId.Text = menu.MenuId;
                        textMenuName.Text = menu.MenuName;
                        textMenuPrice.Text = menu.MenuPrice?.ToString();
                        comboProduct.SelectedValue = menu.Pro_id ?? "";

                        // ตั้งค่าสถานะใน ComboBox แก้ไข
                        cbEditStatus.Text = menu.MenuStatus == "Available" ? "พร้อมขาย" : "หมดชั่วคราว";

                        // ✅ ติ๊กเลือกแพ็กเกจใน CheckedListBox [cite: 2026-03-08]
                        for (int i = 0; i < clbPackages.Items.Count; i++)
                        {
                            string pkgName = clbPackages.Items[i].ToString();
                            bool isChecked = menu.Packages.Any(p => p.Package_Name == pkgName);
                            clbPackages.SetItemChecked(i, isChecked);
                        }

                        DisplayImage(menu.MenuImage);
                    }
                }
                ResetToEditOrDeleteMode();
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(_selectedMenuId)) { ResetToAddMode(); textMenuId.Focus(); return; }

            decimal price = 0;
            if (!ValidateInput(out price)) { return; }

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    if (db.Menus.Any(m => m.MenuId == textMenuId.Text.Trim()))
                    { MessageBox.Show("รหัสซ้ำ", "แจ้งเตือน"); return; }

                    // เซฟรูปและเช็คว่าได้ชื่อไฟล์ไหม
                    string newFileName = SaveImageFile(textMenuId.Text.Trim());
                    // ถ้าผู้ใช้ไม่ได้เลือกรูปใหม่ ให้ใช้เป็นค่าว่างแทน
                    if (string.IsNullOrEmpty(_sourceImagePath)) { newFileName = null; }

                    var newMenu = new Menu
                    {
                        MenuId = textMenuId.Text.Trim(),
                        MenuName = textMenuName.Text.Trim(),
                        MenuPrice = price,
                        Pro_id = comboProduct.SelectedValue.ToString(), // ✅ เซฟรหัสสต๊อก
                        MenuImage = newFileName
                    };

                    db.Menus.Add(newMenu);
                    db.SaveChanges();
                }
                MessageBox.Show("เพิ่มข้อมูลสำเร็จ");
                ResetToAddMode();
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        // ✅ 3. ปรับปรุงปุ่มบันทึกการแก้ไข (buttonEdit) ให้รองรับ Many-to-Many [cite: 2026-03-08]
        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMenuId)) return;
            if (!ValidateInput(out decimal price)) return;

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var menu = db.Menus.Include(m => m.Packages).FirstOrDefault(m => m.MenuId == _selectedMenuId);
                    if (menu != null)
                    {
                        menu.MenuName = textMenuName.Text.Trim();
                        menu.MenuPrice = price;
                        menu.Pro_id = comboProduct.SelectedValue.ToString();

                        // อัปเดตสถานะ [cite: 2026-03-08]
                        menu.MenuStatus = cbEditStatus.Text == "พร้อมขาย" ? "Available" : "OutOfStock";

                        // ✅ อัปเดตความสัมพันธ์ Many-to-Many กับ Package
                        menu.Packages.Clear(); // ล้างของเก่า
                        foreach (var item in clbPackages.CheckedItems)
                        {
                            string pkgName = item.ToString();
                            var pkg = db.Packages.FirstOrDefault(p => p.Package_Name == pkgName);
                            if (pkg != null) menu.Packages.Add(pkg);
                        }

                        string savedFileName = SaveImageFile(_selectedMenuId);
                        if (savedFileName != null) menu.MenuImage = savedFileName;

                        db.SaveChanges();
                        MessageBox.Show("แก้ไขข้อมูลและแพ็กเกจสำเร็จ", "Success");
                        ResetToAddMode();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show($"Error: {ex.Message}"); }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedMenuId)) return;
            if (MessageBox.Show("ยืนยันการลบ?", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var menu = db.Menus.Find(_selectedMenuId);
                    if (menu != null)
                    {
                        db.Menus.Remove(menu);
                        db.SaveChanges();
                        MessageBox.Show("ลบสำเร็จ");
                        ResetToAddMode();
                    }
                }
            }
        }

        private void dgvMenus_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cbPackageFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMenus();
        }

        private void cbStatusFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadMenus();
        }
    }
}