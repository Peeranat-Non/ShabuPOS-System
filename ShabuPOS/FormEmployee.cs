using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using BuffetAPI.Data;   // ดึง DbContext มาใช้
using BuffetAPI.Models; // ดึง Model (Employee, Shop) มาใช้

namespace ShabuPOS // เปลี่ยนเป็นชื่อโปรเจกต์ของคุณ
{
    public partial class FormEmployee : Form
    {
        public FormEmployee()
        {
            InitializeComponent();
        }

        // ตัวแปรเก็บ ID ที่กำลังแก้ไข (เนื่องจากรหัสพนักงานเป็น string เช่น "E01" จึงใช้ค่าเริ่มต้นเป็นค่าว่าง)
        private string _selectedEmployeeId = "";

        private void ResetToAddMode()
        {
            labelMode.Text = @"โหมด: เพิ่มข้อมูล";
            labelMode.BackColor = Color.Green;
            labelMode.ForeColor = Color.White;
            _selectedEmployeeId = "";
            textEmployId.Enabled = true; // โหมดเพิ่มข้อมูล ต้องให้พิมพ์รหัสพนักงานได้
            LoadEmployees(); // รีโหลดตาราง
            dgvEmployees.ClearSelection();
            ClearForm();    // ล้างหน้าจอ
        }

        private void ResetToEditOrDeleteMode()
        {
            labelMode.Text = @"โหมด: แก้ไข/ลบข้อมูล";
            labelMode.BackColor = Color.Yellow;
            labelMode.ForeColor = Color.Black;
            textEmployId.Enabled = false; // โหมดแก้ไข ห้ามแก้รหัสพนักงาน (Primary Key)
        }

        // ✅ 2. ปรับปรุง LoadEmployees ให้รองรับการ Filter และ Search [cite: 2026-03-08]
        private void LoadEmployees()
        {
            try
            {
                // 1. ถ้ายังไม่ได้เลือกตำแหน่ง ให้ตารางว่างเปล่า [cite: 2026-03-08]
                if (cbRoleFilter.SelectedIndex == 0)
                {
                    dgvEmployees.DataSource = null;
                    return;
                }

                using (var db = DbConfig.GetDbContext())
                {
                    string selectedRole = cbRoleFilter.Text;
                    string searchId = txtSearchId.Text.Trim().ToLower();    // รหัสพนักงาน [cite: 2026-03-08]
                    string searchName = txtSearchName.Text.Trim().ToLower(); // ชื่อพนักงาน [cite: 2026-03-08]

                    var query = db.Employees.Include(e => e.Shop).AsQueryable();

                    // 2. กรองตามตำแหน่ง [cite: 2026-03-08]
                    if (selectedRole != "แสดงพนักงานทั้งหมด")
                    {
                        query = query.Where(e => e.Role == selectedRole);
                    }

                    // 3. กรองตาม "รหัสพนักงาน" (เฉพาะถ้ามีการพิมพ์) [cite: 2026-03-08]
                    if (!string.IsNullOrEmpty(searchId))
                    {
                        query = query.Where(e => e.EmployId.ToLower().Contains(searchId));
                    }

                    // 4. กรองตาม "ชื่อพนักงาน" (เฉพาะถ้ามีการพิมพ์) [cite: 2026-03-08]
                    if (!string.IsNullOrEmpty(searchName))
                    {
                        query = query.Where(e => e.EmployName.ToLower().Contains(searchName));
                    }

                    var employees = query.Select(e => new
                    {
                        Id = e.EmployId,
                        Shop = e.Shop.ShopName,
                        Name = e.EmployName,
                        Position = e.EmployPosition,
                        Role = e.Role,
                        StartDate = e.EmploySdate
                    }).ToList();

                    dgvEmployees.DataSource = employees;
                    dgvEmployees.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error");
            }
        }

        private void InitShopComboBox()
        {
            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var shops = db.Shops
                        .Select(s => new { s.ShopId, s.ShopName })
                        .ToList();

                    comboShop.DataSource = shops;
                    comboShop.DisplayMember = "ShopName"; // สิ่งที่ตาเห็น
                    comboShop.ValueMember = "ShopId";     // ค่าที่เก็บจริง
                    comboShop.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidateInput()
        {
            // กฎข้อที่ 1: ห้ามรหัสพนักงานว่าง
            if (string.IsNullOrWhiteSpace(textEmployId.Text))
            {
                MessageBox.Show("กรุณาระบุรหัสพนักงาน");
                return false;
            }

            // กฎข้อที่ 2: ห้ามสาขาว่าง
            if (comboShop.SelectedValue == null)
            {
                MessageBox.Show("กรุณาเลือกสาขา (Shop)");
                return false;
            }

            // กฎข้อที่ 3: ห้ามชื่อพนักงานว่าง
            if (string.IsNullOrWhiteSpace(textEmployName.Text))
            {
                MessageBox.Show("กรุณาระบุชื่อพนักงาน");
                return false;
            }

            return true;
        }

        private void FormEmployee_Load(object sender, EventArgs e)
        {
            dgvEmployees.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEmployees.ReadOnly = true;
            dgvEmployees.MultiSelect = false;

            InitShopComboBox();
            ResetToAddMode(); // เริ่มต้นด้วยโหมดเพิ่มข้อมูล
            InitRoleFilterComboBox(); // ✅ เพิ่ม: ตั้งค่าตัวกรองตำแหน่ง

            // ✅ เรียกใช้ ThemeConfig เพื่อตกแต่งตาราง!
            ThemeConfig.FormatMinimalistDataGridView(dgvEmployees);

            // ✅ ตกแต่งปุ่มพร้อมใส่ Hover Effect
            ThemeConfig.StyleButtonAdd(buttonAdd);
            ThemeConfig.StyleButtonEdit(buttonEdit);
            ThemeConfig.StyleButtonDelete(buttonDelete);

            ThemeConfig.ApplyGlobalFont(this);
        }

        // ✅ 1. ตั้งค่า ComboBox สำหรับ Filter ตำแหน่ง [cite: 2026-03-08]
        private void InitRoleFilterComboBox()
        {
            cbRoleFilter.Items.Clear();
            // Index 0: ตัวเลือกเริ่มต้น (ไม่โชว์ข้อมูล)
            // Index 1-4: กรองตามตำแหน่ง
            // Index 5: แสดงทั้งหมด
            cbRoleFilter.Items.AddRange(new string[] {
                "-- กรุณาเลือกตำแหน่ง --",
                "แสดงพนักงานทั้งหมด",
                "ผู้ดูแลระบบ",
                "ผู้จัดการ",
                "เจ้าหน้าที่บัญชี",
                "พนักงานการเงิน",
                "พนักงานแคชเชียร์",
                "เจ้าหน้าที่จัดซื้อ",
                "พนักงานต้อนรับ",
                "พนักงานครัว",
                "เจ้าหน้าที่คลังสินค้า",
                "เจ้าหน้าที่การตลาด",
            });
            cbRoleFilter.SelectedIndex = 0;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            // สไตล์อาจารย์: ถ้าเลือกรายการอยู่ (มี ID) ให้ปุ่ม Add ทำหน้าที่เคลียร์ฟอร์ม
            if (!string.IsNullOrEmpty(_selectedEmployeeId))
            {
                ResetToAddMode();
                textEmployId.Focus();
                return;
            }

            // ตรวจสอบ (Validation)
            if (!ValidateInput()) { return; }

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    // เช็คก่อนว่ารหัสพนักงานซ้ำไหม
                    bool isExist = db.Employees.Any(emp => emp.EmployId == textEmployId.Text.Trim());
                    if (isExist)
                    {
                        MessageBox.Show("รหัสพนักงานนี้มีในระบบแล้ว กรุณาใช้รหัสอื่น", "แจ้งเตือน");
                        return;
                    }

                    // --- การเพิ่มข้อมูล (A) ---
                    var newEmployee = new Employee
                    {
                        EmployId = textEmployId.Text.Trim(),
                        ShopId = comboShop.SelectedValue.ToString(),
                        EmployName = textEmployName.Text.Trim(),
                        EmployPosition = textEmployPosition.Text.Trim(),
                        EmploySdate = DateOnly.FromDateTime(dtpEmploySdate.Value) // แปลงวันที่ให้ตรงกับ DateOnly
                    };

                    db.Employees.Add(newEmployee);
                    db.SaveChanges();
                }

                MessageBox.Show("เพิ่มข้อมูลสำเร็จ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvEmployees_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvEmployees.Rows[e.RowIndex];
                _selectedEmployeeId = row.Cells["Id"].Value?.ToString();

                textEmployId.Text = row.Cells["Id"].Value?.ToString();
                textEmployName.Text = row.Cells["Name"].Value?.ToString();
                textEmployPosition.Text = row.Cells["Position"].Value?.ToString();

                if (DateTime.TryParse(row.Cells["StartDate"].Value?.ToString(), out DateTime sDate))
                {
                    dtpEmploySdate.Value = sDate;
                }

                // ดึงข้อมูลใหม่เพื่อให้ชัวร์ว่าเลือก ComboBox ถูกสาขา
                using (var db = DbConfig.GetDbContext())
                {
                    var emp = db.Employees.Find(_selectedEmployeeId);
                    if (emp != null)
                        comboShop.SelectedValue = emp.ShopId;
                }

                ResetToEditOrDeleteMode(); // เปลี่ยนแถบสถานะเป็นสีเหลือง
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedEmployeeId))
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการแก้ไข");
                return;
            }

            // 1. ตรวจสอบ (Validation)
            if (!ValidateInput()) { return; }

            try
            {
                using (var db = DbConfig.GetDbContext())
                {
                    // --- โหมดแก้ไข ---
                    var emp = db.Employees.Find(_selectedEmployeeId);
                    if (emp != null)
                    {
                        emp.ShopId = comboShop.SelectedValue.ToString();
                        emp.EmployName = textEmployName.Text.Trim();
                        emp.EmployPosition = textEmployPosition.Text.Trim();
                        emp.EmploySdate = DateOnly.FromDateTime(dtpEmploySdate.Value);
                    }
                    db.SaveChanges();
                }

                MessageBox.Show("แก้ไขข้อมูลสำเร็จ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetToAddMode();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_selectedEmployeeId))
            {
                MessageBox.Show("กรุณาเลือกรายการที่ต้องการลบ");
                return;
            }

            try
            {
                if (MessageBox.Show("ยืนยันการลบ?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (var db = DbConfig.GetDbContext())
                    {
                        // (ถ้าอนาคตมีประวัติพนักงานให้บริการ สามารถเช็คตรงนี้ได้เหมือนที่อาจารย์สอนเรื่องประวัติการขาย)

                        var emp = db.Employees.Find(_selectedEmployeeId);
                        if (emp != null)
                        {
                            db.Employees.Remove(emp);
                            db.SaveChanges();
                            MessageBox.Show("ลบข้อมูลสำเร็จ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetToAddMode();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"เกิดข้อผิดพลาด: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearForm()
        {
            textEmployId.Text = "";
            textEmployName.Text = "";
            textEmployPosition.Text = "";
            comboShop.SelectedIndex = -1;
            dtpEmploySdate.Value = DateTime.Now;
        }

        // ✅ 4. Event เมื่อเปลี่ยนค่าใน ComboBox Filter [cite: 2026-03-08]
        private void cbRoleFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadEmployees(); // กรองทันทีที่เปลี่ยนตำแหน่ง
        }

        private void txtSearchId_TextChanged(object sender, EventArgs e)
        {
            LoadEmployees();
        }

        private void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            LoadEmployees();
        }
    }
}