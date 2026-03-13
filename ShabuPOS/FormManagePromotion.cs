using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BuffetAPI.Data;
using BuffetAPI.Models;
using System.Text.RegularExpressions; // ✅ มั่นใจว่ามีบรรทัดนี้ที่บนสุดของไฟล์ [cite: 2026-03-08]

namespace ShabuPOS
{
    public partial class FormManagePromotion : Form
    {
        public FormManagePromotion()
        {
            InitializeComponent();
        }

        private void FormManagePromotion_Load(object sender, EventArgs e)
        {
            LoadPackageComboBox();
            LoadPromotions();
            LoadPackages();

            // ✅ รันรหัสอัตโนมัติเมื่อเปิดหน้าจอ [cite: 2026-03-08]
            GenerateNextPromoId();
            GenerateNextPackageId();

            ThemeConfig.FormatMinimalistDataGridView(dgvPromo);
            ThemeConfig.FormatMinimalistDataGridView(dgvPackages);
        }

        // ==========================================
        // 📢 ส่วนจัดการโปรโมชั่น (Promotion)
        // ==========================================

        // --- สำหรับโปรโมชั่น (PR) --- [cite: 2026-03-08]
        private void GenerateNextPromoId()
        {
            using (var db = DbConfig.GetDbContext())
            {
                var lastId = db.Promotion
                    .OrderByDescending(p => p.Promo_ID)
                    .Select(p => p.Promo_ID)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(lastId))
                {
                    txtPromoId.Text = "PR001";
                }
                else
                {
                    // ✅ ใช้ Regex ดึงเฉพาะตัวเลขออกมาจากรหัสเดิม [cite: 2026-03-08]
                    string resultString = Regex.Match(lastId, @"\d+").Value;
                    int lastNum = int.TryParse(resultString, out int n) ? n : 0;

                    txtPromoId.Text = "PR" + (lastNum + 1).ToString("D3");
                }
                txtPromoId.ReadOnly = true;
            }
        }

        private void LoadPromotions()
        {
            using (var db = DbConfig.GetDbContext())
            {
                dgvPromo.DataSource = db.Promotion.Select(p => new
                {
                    รหัสโปรโมชั่น = p.Promo_ID,
                    ชื่อโปรโมชั่น = p.Promo_Name,
                    ส่วนลดเปอเซ็นต์ = p.Promo_Discount,
                    รหัสแพ็กเกจ = p.Package_ID
                }).ToList();
            }
        }

        private void btnSavePromotion_Click(object sender, EventArgs e)
        {
            using (var db = DbConfig.GetDbContext())
            {
                try
                {
                    string id = txtPromoId.Text.Trim();
                    // ตรวจสอบว่าเลือกแพ็กเกจหรือยัง [cite: 2026-03-08]
                    if (cboPackageId.SelectedValue == null)
                    {
                        MessageBox.Show("กรุณาเลือกแพ็กเกจที่ต้องการใช้โปรโมชั่นนี้");
                        return;
                    }

                    if (db.Promotion.Any(p => p.Promo_ID == id))
                    {
                        MessageBox.Show("รหัสนี้มีในระบบแล้ว หากต้องการแก้ไขโปรดใช้ปุ่มแก้ไข");
                        return;
                    }

                    db.Promotion.Add(new Promotion
                    {
                        Promo_ID = id,
                        Promo_Name = txtPromoName.Text.Trim(),
                        Promo_Discount = int.Parse(txtPromoDiscount.Text),
                        Package_ID = cboPackageId.SelectedValue.ToString() // ใช้ค่าจาก ComboBox [cite: 2026-03-08]
                    });
                    db.SaveChanges();
                    MessageBox.Show("บันทึกโปรโมชั่นสำเร็จ!");

                    RefreshPromoData();
                }
                catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
            }
        }

        private void buttonEdit_Click(object sender, EventArgs e)
        {
            using (var db = DbConfig.GetDbContext())
            {
                var promo = db.Promotion.Find(txtPromoId.Text.Trim());
                if (promo != null)
                {
                    promo.Promo_Name = txtPromoName.Text.Trim();
                    promo.Promo_Discount = int.Parse(txtPromoDiscount.Text);
                    promo.Package_ID = cboPackageId.SelectedValue?.ToString();
                    db.SaveChanges();
                    MessageBox.Show("แก้ไขข้อมูลสำเร็จ!");
                    RefreshPromoData();
                }
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("ยืนยันการลบ?", "แจ้งเตือน", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (var db = DbConfig.GetDbContext())
                {
                    var item = db.Promotion.Find(txtPromoId.Text.Trim());
                    if (item != null)
                    {
                        db.Promotion.Remove(item);
                        db.SaveChanges();
                        RefreshPromoData();
                        MessageBox.Show("ลบสำเร็จ!");
                    }
                }
            }
        }

        private void RefreshPromoData()
        {
            LoadPromotions();
            ClearPromoFields();
            GenerateNextPromoId(); // ✅ รันรหัสใหม่หลังจบการทำงาน [cite: 2026-03-08]
        }

        private void ClearPromoFields()
        {
            txtPromoName.Clear();
            txtPromoDiscount.Clear();
            cboPackageId.SelectedIndex = -1;
        }

        private void dgvPromo_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPromo.Rows[e.RowIndex];
                txtPromoId.Text = row.Cells[0].Value?.ToString();
                txtPromoName.Text = row.Cells[1].Value?.ToString();
                txtPromoDiscount.Text = row.Cells[2].Value?.ToString();
                cboPackageId.SelectedValue = row.Cells[3].Value?.ToString();
            }
        }

        private void LoadPackageComboBox()
        {
            using (var db = DbConfig.GetDbContext())
            {
                var packages = db.Packages
                    .Select(p => new { p.Package_ID, DisplayText = p.Package_ID + " - " + p.Package_Name })
                    .ToList();

                cboPackageId.DataSource = packages;
                cboPackageId.DisplayMember = "DisplayText";
                cboPackageId.ValueMember = "Package_ID";
                cboPackageId.SelectedIndex = -1;
            }
        }

        // ==========================================
        // 📦 ส่วนจัดการแพ็กเกจ (Package)
        // ==========================================

        // --- สำหรับแพ็กเกจ (PK) --- [cite: 2026-03-08]
        private void GenerateNextPackageId()
        {
            using (var db = DbConfig.GetDbContext())
            {
                // 1. ดึงรหัสล่าสุดจากฐานข้อมูล [cite: 2026-03-08]
                var lastId = db.Packages
                    .OrderByDescending(p => p.Package_ID)
                    .Select(p => p.Package_ID)
                    .FirstOrDefault();

                if (string.IsNullOrEmpty(lastId))
                {
                    // ถ้ายังไม่มีข้อมูลเลย ให้เริ่มที่ PACK001 [cite: 2026-03-08]
                    txtPkgId.Text = "PACK001";
                }
                else
                {
                    // 2. ใช้ Regex ดึงเฉพาะตัวเลขออกมา (จะรหัส PK001 หรือ P01 ก็จะดึงเลขได้) [cite: 2026-03-08]
                    string numericPart = Regex.Match(lastId, @"\d+").Value;

                    // 3. แปลงเป็นตัวเลขแล้วบวก 1 [cite: 2026-03-08]
                    int nextNum = int.TryParse(numericPart, out int n) ? n + 1 : 1;

                    // 4. ใส่ Prefix "PACK" และจัดรูปแบบตัวเลข 3 หลัก [cite: 2026-03-08]
                    txtPkgId.Text = "PACK" + nextNum.ToString("D3");
                }

                txtPkgId.ReadOnly = true; // ล็อคไว้ไม่ให้พิมพ์แก้เอง [cite: 2026-03-08]
            }
        }

        private void LoadPackages()
        {
            using (var db = DbConfig.GetDbContext())
            {
                dgvPackages.DataSource = db.Packages.Select(pkg => new
                {
                    รหัสแพ็กเกจ = pkg.Package_ID,
                    ชื่อแพ็กเกจ = pkg.Package_Name,
                    ราคา = pkg.Package_Price
                }).ToList();
            }
        }

        private void btnAddPkg_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPkgPrice.Text, out decimal price)) return;

            using (var db = DbConfig.GetDbContext())
            {
                db.Packages.Add(new Package
                {
                    Package_ID = txtPkgId.Text.Trim(),
                    Package_Name = txtPkgName.Text.Trim(),
                    Package_Price = (int)price
                });
                db.SaveChanges();
                LoadPackages();
                LoadPackageComboBox(); // อัปเดต ComboBox ในแท็บโปรโมชั่นด้วย [cite: 2026-03-08]
                txtPkgName.Clear(); txtPkgPrice.Clear();
                GenerateNextPackageId(); // ✅ รันรหัสใหม่
                MessageBox.Show("เพิ่มแพ็กเกจสำเร็จ!");
            }
        }

        private void btnEditPkg_Click(object sender, EventArgs e)
        {
            if (!decimal.TryParse(txtPkgPrice.Text, out decimal price)) return;

            using (var db = DbConfig.GetDbContext())
            {
                var pkg = db.Packages.Find(txtPkgId.Text.Trim());
                if (pkg != null)
                {
                    pkg.Package_Name = txtPkgName.Text.Trim();
                    pkg.Package_Price = (int)price;
                    db.SaveChanges();
                    LoadPackages();
                    LoadPackageComboBox();
                    MessageBox.Show("แก้ไขสำเร็จ!");
                }
            }
        }

        private void btnDeletePkg_Click(object sender, EventArgs e)
        {
            string id = txtPkgId.Text.Trim();
            if (string.IsNullOrEmpty(id)) return;

            if (MessageBox.Show($"คุณแน่ใจหรือไม่ว่าต้องการลบแพ็กเกจ {id}?", "ยืนยันการลบ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                using (var db = DbConfig.GetDbContext())
                {
                    try
                    {
                        var pkg = db.Packages.Find(id);
                        if (pkg != null)
                        {
                            db.Packages.Remove(pkg);
                            db.SaveChanges(); // 🚩 จุดที่เกิด Error

                            MessageBox.Show("ลบข้อมูลแพ็กเกจเรียบร้อยแล้ว", "สำเร็จ");
                            LoadPackages();
                            LoadPackageComboBox();
                            GenerateNextPackageId();
                        }
                    }
                    // ✅ ดักจับ Error กรณีมีการเชื่อมโยงข้อมูลอยู่
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException)
                    {
                        MessageBox.Show("ไม่สามารถลบแพ็กเกจนี้ได้ เนื่องจากมีการใช้งานอยู่ในข้อมูลการรับบริการ หรือโปรโมชั่นแล้วครับ\n\nคำแนะนำ: แนะนำให้เปลี่ยนสถานะเป็น 'ไม่ใช้งาน' แทนการลบครับ",
                                        "แจ้งเตือนความปลอดภัย", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("เกิดข้อผิดพลาดไม่คาดคิด: " + ex.Message);
                    }
                }
            }
        }

        private void dgvPackages_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var row = dgvPackages.Rows[e.RowIndex];
                txtPkgId.Text = row.Cells[0].Value?.ToString();
                txtPkgName.Text = row.Cells[1].Value?.ToString();
                txtPkgPrice.Text = row.Cells[2].Value?.ToString();
            }
        }

        private void btnNewPromo_Click(object sender, EventArgs e)
        {
            // 1. ล้างข้อมูลในช่องกรอกทั้งหมด [cite: 2026-03-08]
            txtPromoName.Clear();
            txtPromoDiscount.Clear();
            cboPackageId.SelectedIndex = -1; // ล้างการเลือกแพ็กเกจ

            // 2. รันรหัสโปรโมชั่นใหม่ (PR...) [cite: 2026-03-08]
            GenerateNextPromoId();

            // 3. ปลดล็อคหรือตั้งค่า Focus [cite: 2026-03-08]
            txtPromoName.Focus();
            MessageBox.Show("เตรียมพร้อมสำหรับเพิ่มโปรโมชั่นใหม่แล้วครับ", "แจ้งเตือน");
        }

        private void btnNewPkg_Click(object sender, EventArgs e)
        {
            // 1. ล้างข้อมูลในช่องกรอก [cite: 2026-03-08]
            txtPkgName.Clear();
            txtPkgPrice.Clear();

            // 2. รันรหัสแพ็กเกจใหม่ (PACK...) [cite: 2026-03-08]
            GenerateNextPackageId();

            // 3. ตั้งค่า Focus [cite: 2026-03-08]
            txtPkgName.Focus();
        }
    }
}