namespace ShabuPOS
{
    partial class FormManagePromotion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnSavePromotion = new Button();
            buttonEdit = new Button();
            buttonDelete = new Button();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            btnNewPromo = new Button();
            cboPackageId = new ComboBox();
            label1 = new Label();
            txtPromoDiscount = new TextBox();
            txtPromoName = new TextBox();
            txtPromoId = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            dgvPromo = new DataGridView();
            tabPage2 = new TabPage();
            btnNewPkg = new Button();
            btnDeletePkg = new Button();
            btnEditPkg = new Button();
            btnAddPkg = new Button();
            txtPkgPrice = new TextBox();
            txtPkgName = new TextBox();
            txtPkgId = new TextBox();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            dgvPackages = new DataGridView();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPromo).BeginInit();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPackages).BeginInit();
            SuspendLayout();
            // 
            // btnSavePromotion
            // 
            btnSavePromotion.BackColor = SystemColors.Control;
            btnSavePromotion.Image = Properties.Resources.plus__1_;
            btnSavePromotion.Location = new Point(442, 750);
            btnSavePromotion.Name = "btnSavePromotion";
            btnSavePromotion.Size = new Size(225, 59);
            btnSavePromotion.TabIndex = 160;
            btnSavePromotion.Text = "บันทึกโปโมชั่น";
            btnSavePromotion.TextAlign = ContentAlignment.MiddleRight;
            btnSavePromotion.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSavePromotion.UseVisualStyleBackColor = false;
            btnSavePromotion.Click += btnSavePromotion_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = Properties.Resources.editอันนี้แหละ;
            buttonEdit.Location = new Point(673, 750);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(225, 59);
            buttonEdit.TabIndex = 161;
            buttonEdit.Text = "แก้ไขข้อมูล";
            buttonEdit.TextAlign = ContentAlignment.MiddleRight;
            buttonEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonEdit.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = Properties.Resources.bin__1_;
            buttonDelete.Location = new Point(904, 750);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(225, 59);
            buttonDelete.TabIndex = 162;
            buttonDelete.Text = "ลบข้อมูล";
            buttonDelete.TextAlign = ContentAlignment.MiddleRight;
            buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Location = new Point(28, 23);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(1764, 878);
            tabControl1.TabIndex = 165;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(btnNewPromo);
            tabPage1.Controls.Add(cboPackageId);
            tabPage1.Controls.Add(label1);
            tabPage1.Controls.Add(buttonDelete);
            tabPage1.Controls.Add(buttonEdit);
            tabPage1.Controls.Add(txtPromoDiscount);
            tabPage1.Controls.Add(btnSavePromotion);
            tabPage1.Controls.Add(txtPromoName);
            tabPage1.Controls.Add(txtPromoId);
            tabPage1.Controls.Add(label4);
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(label2);
            tabPage1.Controls.Add(dgvPromo);
            tabPage1.Location = new Point(4, 40);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1756, 834);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "จัดการโปรโมชั่น";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // btnNewPromo
            // 
            btnNewPromo.Image = Properties.Resources.save;
            btnNewPromo.Location = new Point(177, 750);
            btnNewPromo.Name = "btnNewPromo";
            btnNewPromo.Size = new Size(225, 59);
            btnNewPromo.TabIndex = 173;
            btnNewPromo.Text = "เพิ่มโปโมชั่น";
            btnNewPromo.TextAlign = ContentAlignment.MiddleRight;
            btnNewPromo.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNewPromo.UseVisualStyleBackColor = true;
            btnNewPromo.Click += btnNewPromo_Click;
            // 
            // cboPackageId
            // 
            cboPackageId.FormattingEnabled = true;
            cboPackageId.Location = new Point(671, 676);
            cboPackageId.Name = "cboPackageId";
            cboPackageId.Size = new Size(310, 39);
            cboPackageId.TabIndex = 172;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(459, 679);
            label1.Margin = new Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new Size(184, 31);
            label1.TabIndex = 171;
            label1.Text = "รหัสแพ็กเกจที่ใช้ได้:";
            // 
            // txtPromoDiscount
            // 
            txtPromoDiscount.Location = new Point(671, 604);
            txtPromoDiscount.Margin = new Padding(5);
            txtPromoDiscount.Name = "txtPromoDiscount";
            txtPromoDiscount.Size = new Size(310, 38);
            txtPromoDiscount.TabIndex = 169;
            // 
            // txtPromoName
            // 
            txtPromoName.Location = new Point(672, 543);
            txtPromoName.Margin = new Padding(5);
            txtPromoName.Name = "txtPromoName";
            txtPromoName.Size = new Size(309, 38);
            txtPromoName.TabIndex = 168;
            // 
            // txtPromoId
            // 
            txtPromoId.Location = new Point(672, 487);
            txtPromoId.Margin = new Padding(5);
            txtPromoId.Name = "txtPromoId";
            txtPromoId.Size = new Size(309, 38);
            txtPromoId.TabIndex = 167;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(523, 613);
            label4.Margin = new Padding(5, 0, 5, 0);
            label4.Name = "label4";
            label4.Size = new Size(105, 31);
            label4.TabIndex = 166;
            label4.Text = "ส่วนลด %:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(512, 552);
            label3.Margin = new Padding(5, 0, 5, 0);
            label3.Name = "label3";
            label3.Size = new Size(121, 31);
            label3.TabIndex = 165;
            label3.Text = "ชื่อโปรโมชั่น:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(504, 496);
            label2.Margin = new Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new Size(131, 31);
            label2.TabIndex = 164;
            label2.Text = "รหัสโปรโมชั่น:";
            // 
            // dgvPromo
            // 
            dgvPromo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPromo.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPromo.Location = new Point(11, 27);
            dgvPromo.Margin = new Padding(8);
            dgvPromo.Name = "dgvPromo";
            dgvPromo.RowHeadersWidth = 51;
            dgvPromo.Size = new Size(1269, 414);
            dgvPromo.TabIndex = 163;
            dgvPromo.CellClick += dgvPromo_CellClick;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(btnNewPkg);
            tabPage2.Controls.Add(btnDeletePkg);
            tabPage2.Controls.Add(btnEditPkg);
            tabPage2.Controls.Add(btnAddPkg);
            tabPage2.Controls.Add(txtPkgPrice);
            tabPage2.Controls.Add(txtPkgName);
            tabPage2.Controls.Add(txtPkgId);
            tabPage2.Controls.Add(label6);
            tabPage2.Controls.Add(label7);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(dgvPackages);
            tabPage2.Location = new Point(4, 40);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1756, 834);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "จัดการแพ็กเกจ";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnNewPkg
            // 
            btnNewPkg.Image = Properties.Resources.save;
            btnNewPkg.Location = new Point(207, 677);
            btnNewPkg.Name = "btnNewPkg";
            btnNewPkg.Size = new Size(225, 59);
            btnNewPkg.TabIndex = 187;
            btnNewPkg.Text = "เพิ่มโปโมชั่น";
            btnNewPkg.TextAlign = ContentAlignment.MiddleRight;
            btnNewPkg.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnNewPkg.UseVisualStyleBackColor = true;
            btnNewPkg.Click += btnNewPkg_Click;
            // 
            // btnDeletePkg
            // 
            btnDeletePkg.Location = new Point(948, 677);
            btnDeletePkg.Name = "btnDeletePkg";
            btnDeletePkg.Size = new Size(225, 59);
            btnDeletePkg.TabIndex = 186;
            btnDeletePkg.Text = "ลบข้อมูล";
            btnDeletePkg.TextAlign = ContentAlignment.MiddleRight;
            btnDeletePkg.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnDeletePkg.UseVisualStyleBackColor = true;
            btnDeletePkg.Click += btnDeletePkg_Click;
            // 
            // btnEditPkg
            // 
            btnEditPkg.Location = new Point(717, 677);
            btnEditPkg.Name = "btnEditPkg";
            btnEditPkg.Size = new Size(225, 59);
            btnEditPkg.TabIndex = 185;
            btnEditPkg.Text = "แก้ไขข้อมูล";
            btnEditPkg.TextAlign = ContentAlignment.MiddleRight;
            btnEditPkg.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnEditPkg.UseVisualStyleBackColor = true;
            btnEditPkg.Click += btnEditPkg_Click;
            // 
            // btnAddPkg
            // 
            btnAddPkg.BackColor = SystemColors.Control;
            btnAddPkg.Location = new Point(486, 677);
            btnAddPkg.Name = "btnAddPkg";
            btnAddPkg.Size = new Size(225, 59);
            btnAddPkg.TabIndex = 184;
            btnAddPkg.Text = "บันทึกแพ็กเกจ";
            btnAddPkg.TextAlign = ContentAlignment.MiddleRight;
            btnAddPkg.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnAddPkg.UseVisualStyleBackColor = false;
            btnAddPkg.Click += btnAddPkg_Click;
            // 
            // txtPkgPrice
            // 
            txtPkgPrice.Location = new Point(715, 602);
            txtPkgPrice.Margin = new Padding(5);
            txtPkgPrice.Name = "txtPkgPrice";
            txtPkgPrice.Size = new Size(310, 38);
            txtPkgPrice.TabIndex = 181;
            // 
            // txtPkgName
            // 
            txtPkgName.Location = new Point(716, 541);
            txtPkgName.Margin = new Padding(5);
            txtPkgName.Name = "txtPkgName";
            txtPkgName.Size = new Size(309, 38);
            txtPkgName.TabIndex = 180;
            // 
            // txtPkgId
            // 
            txtPkgId.Location = new Point(716, 485);
            txtPkgId.Margin = new Padding(5);
            txtPkgId.Name = "txtPkgId";
            txtPkgId.Size = new Size(309, 38);
            txtPkgId.TabIndex = 179;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(567, 611);
            label6.Margin = new Padding(5, 0, 5, 0);
            label6.Name = "label6";
            label6.Size = new Size(62, 31);
            label6.TabIndex = 178;
            label6.Text = "ราคา:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(556, 550);
            label7.Margin = new Padding(5, 0, 5, 0);
            label7.Name = "label7";
            label7.Size = new Size(115, 31);
            label7.TabIndex = 177;
            label7.Text = "ชื่อแพ็กเกจ:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(548, 494);
            label8.Margin = new Padding(5, 0, 5, 0);
            label8.Name = "label8";
            label8.Size = new Size(125, 31);
            label8.TabIndex = 176;
            label8.Text = "รหัสแพ็กเกจ:";
            // 
            // dgvPackages
            // 
            dgvPackages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPackages.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPackages.Location = new Point(51, 32);
            dgvPackages.Margin = new Padding(8);
            dgvPackages.Name = "dgvPackages";
            dgvPackages.RowHeadersWidth = 51;
            dgvPackages.Size = new Size(1384, 414);
            dgvPackages.TabIndex = 175;
            dgvPackages.CellClick += dgvPackages_CellClick;
            // 
            // FormManagePromotion
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(tabControl1);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormManagePromotion";
            Text = "FormManagePromotion";
            Load += FormManagePromotion_Load;
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPromo).EndInit();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPackages).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnSavePromotion;
        private Button buttonEdit;
        private Button buttonDelete;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Label label1;
        private TextBox txtPromoDiscount;
        private TextBox txtPromoName;
        private TextBox txtPromoId;
        private Label label4;
        private Label label3;
        private Label label2;
        private DataGridView dgvPromo;
        private TabPage tabPage2;
        private Button btnDeletePkg;
        private Button btnEditPkg;
        private Button btnAddPkg;
        private TextBox txtPkgPrice;
        private TextBox txtPkgName;
        private TextBox txtPkgId;
        private Label label6;
        private Label label7;
        private Label label8;
        private DataGridView dgvPackages;
        private ComboBox cboPackageId;
        private Button btnNewPromo;
        private Button btnNewPkg;
    }
}