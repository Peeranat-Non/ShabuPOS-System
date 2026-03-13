namespace ShabuPOS
{
    partial class FormMenu
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
            labelMode = new Label();
            buttonDelete = new Button();
            buttonEdit = new Button();
            buttonAdd = new Button();
            textMenuPrice = new TextBox();
            textMenuName = new TextBox();
            textMenuId = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            dgvMenus = new DataGridView();
            picMenuImage = new PictureBox();
            btnBrowseImage = new Button();
            comboProduct = new ComboBox();
            label6 = new Label();
            cbPackageFilter = new ComboBox();
            label1 = new Label();
            label5 = new Label();
            cbStatusFilter = new ComboBox();
            cbEditStatus = new ComboBox();
            label7 = new Label();
            clbPackages = new CheckedListBox();
            ((System.ComponentModel.ISupportInitialize)dgvMenus).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picMenuImage).BeginInit();
            SuspendLayout();
            // 
            // labelMode
            // 
            labelMode.BackColor = Color.Green;
            labelMode.BorderStyle = BorderStyle.Fixed3D;
            labelMode.ForeColor = Color.White;
            labelMode.Location = new Point(1085, 30);
            labelMode.Name = "labelMode";
            labelMode.Size = new Size(231, 56);
            labelMode.TabIndex = 66;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = Properties.Resources.bin__1_;
            buttonDelete.ImageAlign = ContentAlignment.MiddleLeft;
            buttonDelete.Location = new Point(1231, 878);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Padding = new Padding(30, 0, 0, 0);
            buttonDelete.Size = new Size(288, 58);
            buttonDelete.TabIndex = 65;
            buttonDelete.Text = "ลบข้อมูล";
            buttonDelete.TextAlign = ContentAlignment.MiddleRight;
            buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = Properties.Resources.editอันนี้แหละ;
            buttonEdit.ImageAlign = ContentAlignment.MiddleLeft;
            buttonEdit.Location = new Point(1231, 796);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Padding = new Padding(30, 0, 0, 0);
            buttonEdit.Size = new Size(288, 58);
            buttonEdit.TabIndex = 64;
            buttonEdit.Text = "แก้ไขข้อมูล";
            buttonEdit.TextAlign = ContentAlignment.MiddleRight;
            buttonEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Image = Properties.Resources.plus__1_;
            buttonAdd.ImageAlign = ContentAlignment.MiddleLeft;
            buttonAdd.Location = new Point(1231, 720);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Padding = new Padding(30, 0, 0, 0);
            buttonAdd.Size = new Size(288, 58);
            buttonAdd.TabIndex = 63;
            buttonAdd.Text = "เพิ่มรายการอาหาร";
            buttonAdd.TextAlign = ContentAlignment.MiddleRight;
            buttonAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textMenuPrice
            // 
            textMenuPrice.Location = new Point(1267, 204);
            textMenuPrice.Name = "textMenuPrice";
            textMenuPrice.Size = new Size(239, 38);
            textMenuPrice.TabIndex = 62;
            // 
            // textMenuName
            // 
            textMenuName.Location = new Point(1267, 153);
            textMenuName.Name = "textMenuName";
            textMenuName.Size = new Size(238, 38);
            textMenuName.TabIndex = 61;
            // 
            // textMenuId
            // 
            textMenuId.Location = new Point(1267, 105);
            textMenuId.Name = "textMenuId";
            textMenuId.Size = new Size(238, 38);
            textMenuId.TabIndex = 60;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1199, 204);
            label4.Name = "label4";
            label4.Size = new Size(62, 31);
            label4.TabIndex = 59;
            label4.Text = "ราคา:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1183, 156);
            label3.Name = "label3";
            label3.Size = new Size(78, 31);
            label3.TabIndex = 58;
            label3.Text = "ชื่อเมนู:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1085, 105);
            label2.Name = "label2";
            label2.Size = new Size(176, 31);
            label2.TabIndex = 57;
            label2.Text = "รหัสรายการอาหาร:";
            // 
            // dgvMenus
            // 
            dgvMenus.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvMenus.Location = new Point(37, 189);
            dgvMenus.Margin = new Padding(5);
            dgvMenus.Name = "dgvMenus";
            dgvMenus.RowHeadersWidth = 51;
            dgvMenus.Size = new Size(1021, 701);
            dgvMenus.TabIndex = 56;
            dgvMenus.CellClick += dgvMenus_CellClick;
            dgvMenus.CellContentClick += dgvMenus_CellContentClick;
            // 
            // picMenuImage
            // 
            picMenuImage.BorderStyle = BorderStyle.FixedSingle;
            picMenuImage.Location = new Point(1266, 269);
            picMenuImage.Name = "picMenuImage";
            picMenuImage.Size = new Size(184, 165);
            picMenuImage.SizeMode = PictureBoxSizeMode.Zoom;
            picMenuImage.TabIndex = 69;
            picMenuImage.TabStop = false;
            // 
            // btnBrowseImage
            // 
            btnBrowseImage.Location = new Point(1266, 451);
            btnBrowseImage.Name = "btnBrowseImage";
            btnBrowseImage.Size = new Size(184, 42);
            btnBrowseImage.TabIndex = 70;
            btnBrowseImage.Text = "เลือกรูปภาพ";
            btnBrowseImage.UseVisualStyleBackColor = true;
            btnBrowseImage.Click += btnBrowseImage_Click;
            // 
            // comboProduct
            // 
            comboProduct.FormattingEnabled = true;
            comboProduct.Location = new Point(1266, 518);
            comboProduct.Name = "comboProduct";
            comboProduct.Size = new Size(239, 39);
            comboProduct.TabIndex = 71;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1116, 518);
            label6.Name = "label6";
            label6.Size = new Size(144, 31);
            label6.TabIndex = 73;
            label6.Text = "ตัดสต๊อกสินค้า:";
            // 
            // cbPackageFilter
            // 
            cbPackageFilter.FormattingEnabled = true;
            cbPackageFilter.Location = new Point(304, 97);
            cbPackageFilter.Name = "cbPackageFilter";
            cbPackageFilter.Size = new Size(221, 39);
            cbPackageFilter.TabIndex = 76;
            cbPackageFilter.SelectedIndexChanged += cbPackageFilter_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(166, 100);
            label1.Name = "label1";
            label1.Size = new Size(132, 31);
            label1.TabIndex = 77;
            label1.Text = "เลือกแพ็กเกจ:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(570, 100);
            label5.Name = "label5";
            label5.Size = new Size(119, 31);
            label5.TabIndex = 79;
            label5.Text = "เลือกสถานะ:";
            // 
            // cbStatusFilter
            // 
            cbStatusFilter.FormattingEnabled = true;
            cbStatusFilter.Location = new Point(695, 97);
            cbStatusFilter.Name = "cbStatusFilter";
            cbStatusFilter.Size = new Size(221, 39);
            cbStatusFilter.TabIndex = 78;
            cbStatusFilter.SelectedIndexChanged += cbStatusFilter_SelectedIndexChanged;
            // 
            // cbEditStatus
            // 
            cbEditStatus.FormattingEnabled = true;
            cbEditStatus.Location = new Point(1266, 576);
            cbEditStatus.Name = "cbEditStatus";
            cbEditStatus.Size = new Size(239, 39);
            cbEditStatus.TabIndex = 80;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1151, 584);
            label7.Name = "label7";
            label7.Size = new Size(109, 31);
            label7.TabIndex = 81;
            label7.Text = "สถานะเมนู:";
            // 
            // clbPackages
            // 
            clbPackages.FormattingEnabled = true;
            clbPackages.Items.AddRange(new object[] { "Silver ", "Gold" });
            clbPackages.Location = new Point(1266, 639);
            clbPackages.Name = "clbPackages";
            clbPackages.Size = new Size(253, 70);
            clbPackages.TabIndex = 82;
            // 
            // FormMenu
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(clbPackages);
            Controls.Add(label7);
            Controls.Add(cbEditStatus);
            Controls.Add(label5);
            Controls.Add(cbStatusFilter);
            Controls.Add(label1);
            Controls.Add(cbPackageFilter);
            Controls.Add(label6);
            Controls.Add(comboProduct);
            Controls.Add(btnBrowseImage);
            Controls.Add(picMenuImage);
            Controls.Add(labelMode);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(textMenuPrice);
            Controls.Add(textMenuName);
            Controls.Add(textMenuId);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dgvMenus);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormMenu";
            Load += FormMenu_Load;
            ((System.ComponentModel.ISupportInitialize)dgvMenus).EndInit();
            ((System.ComponentModel.ISupportInitialize)picMenuImage).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label labelMode;
        private Button buttonDelete;
        private Button buttonEdit;
        private Button buttonAdd;
        private TextBox textMenuPrice;
        private TextBox textMenuName;
        private TextBox textMenuId;
        private Label label4;
        private Label label3;
        private Label label2;
        private DataGridView dgvMenus;
        private PictureBox picMenuImage;
        private Button btnBrowseImage;
        private ComboBox comboProduct;
        private Label label6;
        private ComboBox cbPackageFilter;
        private Label label1;
        private Label label5;
        private ComboBox cbStatusFilter;
        private ComboBox cbEditStatus;
        private Label label7;
        private CheckedListBox clbPackages;
    }
}