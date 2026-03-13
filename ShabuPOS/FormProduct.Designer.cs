namespace ShabuPOS
{
    partial class FormProduct
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
            textProStock = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            btnBrowseImage = new Button();
            picProImage = new PictureBox();
            labelMode = new Label();
            buttonDelete = new Button();
            buttonEdit = new Button();
            buttonAdd = new Button();
            textProQuan = new TextBox();
            textProName = new TextBox();
            textProId = new TextBox();
            dgvProducts = new DataGridView();
            textProUnit = new TextBox();
            label5 = new Label();
            comboPR = new ComboBox();
            comboPO = new ComboBox();
            label6 = new Label();
            label7 = new Label();
            cboStockCategoryFilter = new ComboBox();
            txtSearchProductName = new TextBox();
            btnExecuteSearch = new Button();
            label8 = new Label();
            label9 = new Label();
            ((System.ComponentModel.ISupportInitialize)picProImage).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).BeginInit();
            SuspendLayout();
            // 
            // textProStock
            // 
            textProStock.Location = new Point(1334, 331);
            textProStock.Name = "textProStock";
            textProStock.Size = new Size(238, 38);
            textProStock.TabIndex = 107;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(1148, 331);
            label4.Name = "label4";
            label4.Size = new Size(145, 31);
            label4.TabIndex = 106;
            label4.Text = "จำนวนคงเหลือ:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(1073, 276);
            label3.Name = "label3";
            label3.Size = new Size(220, 31);
            label3.TabIndex = 105;
            label3.Text = "จำนวนต่อแพ็ค/ปริมาณ:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(1200, 222);
            label2.Name = "label2";
            label2.Size = new Size(94, 31);
            label2.TabIndex = 104;
            label2.Text = "ชื่อสินค้า:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1176, 174);
            label1.Name = "label1";
            label1.Size = new Size(117, 31);
            label1.TabIndex = 103;
            label1.Text = "รหัสวัตถุดิบ:";
            // 
            // btnBrowseImage
            // 
            btnBrowseImage.Location = new Point(1287, 635);
            btnBrowseImage.Name = "btnBrowseImage";
            btnBrowseImage.Size = new Size(94, 42);
            btnBrowseImage.TabIndex = 102;
            btnBrowseImage.Text = "เลือกรูปภาพ";
            btnBrowseImage.UseVisualStyleBackColor = true;
            btnBrowseImage.Click += btnBrowseImage_Click;
            // 
            // picProImage
            // 
            picProImage.BorderStyle = BorderStyle.FixedSingle;
            picProImage.Location = new Point(1244, 446);
            picProImage.Name = "picProImage";
            picProImage.Size = new Size(184, 165);
            picProImage.SizeMode = PictureBoxSizeMode.Zoom;
            picProImage.TabIndex = 101;
            picProImage.TabStop = false;
            // 
            // labelMode
            // 
            labelMode.BackColor = Color.Green;
            labelMode.BorderStyle = BorderStyle.Fixed3D;
            labelMode.ForeColor = Color.White;
            labelMode.Location = new Point(1062, 91);
            labelMode.Name = "labelMode";
            labelMode.Size = new Size(231, 56);
            labelMode.TabIndex = 100;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = Properties.Resources.bin__1_;
            buttonDelete.Location = new Point(1505, 767);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(225, 57);
            buttonDelete.TabIndex = 99;
            buttonDelete.Text = "ลบข้อมูล";
            buttonDelete.TextAlign = ContentAlignment.MiddleRight;
            buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = Properties.Resources.editอันนี้แหละ;
            buttonEdit.Location = new Point(1274, 767);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(225, 57);
            buttonEdit.TabIndex = 98;
            buttonEdit.Text = "แก้ไขข้อมูล";
            buttonEdit.TextAlign = ContentAlignment.MiddleRight;
            buttonEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Image = Properties.Resources.plus__1_;
            buttonAdd.Location = new Point(1043, 767);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(225, 57);
            buttonAdd.TabIndex = 97;
            buttonAdd.Text = "เพิ่มข้อมูลสินค้า";
            buttonAdd.TextAlign = ContentAlignment.MiddleRight;
            buttonAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textProQuan
            // 
            textProQuan.Location = new Point(1333, 273);
            textProQuan.Name = "textProQuan";
            textProQuan.Size = new Size(239, 38);
            textProQuan.TabIndex = 96;
            // 
            // textProName
            // 
            textProName.Location = new Point(1333, 222);
            textProName.Name = "textProName";
            textProName.Size = new Size(238, 38);
            textProName.TabIndex = 95;
            // 
            // textProId
            // 
            textProId.Location = new Point(1333, 174);
            textProId.Name = "textProId";
            textProId.Size = new Size(238, 38);
            textProId.TabIndex = 94;
            // 
            // dgvProducts
            // 
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProducts.Location = new Point(34, 249);
            dgvProducts.Margin = new Padding(5);
            dgvProducts.Name = "dgvProducts";
            dgvProducts.RowHeadersWidth = 51;
            dgvProducts.Size = new Size(979, 587);
            dgvProducts.TabIndex = 93;
            dgvProducts.CellClick += dgvProducts_CellClick;
            // 
            // textProUnit
            // 
            textProUnit.Location = new Point(1333, 391);
            textProUnit.Name = "textProUnit";
            textProUnit.Size = new Size(238, 38);
            textProUnit.TabIndex = 108;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(1197, 391);
            label5.Name = "label5";
            label5.Size = new Size(96, 31);
            label5.TabIndex = 109;
            label5.Text = "หน่วยนับ:";
            // 
            // comboPR
            // 
            comboPR.FormattingEnabled = true;
            comboPR.Location = new Point(1145, 711);
            comboPR.Name = "comboPR";
            comboPR.Size = new Size(151, 39);
            comboPR.TabIndex = 110;
            // 
            // comboPO
            // 
            comboPO.FormattingEnabled = true;
            comboPO.Location = new Point(1432, 711);
            comboPO.Name = "comboPO";
            comboPO.Size = new Size(151, 39);
            comboPO.TabIndex = 111;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(1043, 711);
            label6.Name = "label6";
            label6.Size = new Size(93, 31);
            label6.TabIndex = 112;
            label6.Text = "ใบขอซื้อ:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(1333, 711);
            label7.Name = "label7";
            label7.Size = new Size(88, 31);
            label7.TabIndex = 113;
            label7.Text = "ใบสั่งซื้อ:";
            // 
            // cboStockCategoryFilter
            // 
            cboStockCategoryFilter.FormattingEnabled = true;
            cboStockCategoryFilter.Items.AddRange(new object[] { "ทั้งหมด", "เนื้อสัตว์", "ผัก", "ของทานเล่น", "เครื่องดื่ม" });
            cboStockCategoryFilter.Location = new Point(404, 117);
            cboStockCategoryFilter.Name = "cboStockCategoryFilter";
            cboStockCategoryFilter.Size = new Size(270, 39);
            cboStockCategoryFilter.TabIndex = 114;
            // 
            // txtSearchProductName
            // 
            txtSearchProductName.Location = new Point(404, 178);
            txtSearchProductName.Name = "txtSearchProductName";
            txtSearchProductName.Size = new Size(270, 38);
            txtSearchProductName.TabIndex = 115;
            // 
            // btnExecuteSearch
            // 
            btnExecuteSearch.Location = new Point(721, 117);
            btnExecuteSearch.Name = "btnExecuteSearch";
            btnExecuteSearch.Size = new Size(136, 54);
            btnExecuteSearch.TabIndex = 116;
            btnExecuteSearch.Text = "ค้นหาข้อมูล";
            btnExecuteSearch.UseVisualStyleBackColor = true;
            btnExecuteSearch.Click += btnExecuteSearch_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(238, 117);
            label8.Name = "label8";
            label8.Size = new Size(145, 31);
            label8.TabIndex = 117;
            label8.Text = "หมวดหมู่สินค้า:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(289, 181);
            label9.Name = "label9";
            label9.Size = new Size(94, 31);
            label9.TabIndex = 118;
            label9.Text = "ชื่อสินค้า:";
            // 
            // FormProduct
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(btnExecuteSearch);
            Controls.Add(txtSearchProductName);
            Controls.Add(cboStockCategoryFilter);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(comboPO);
            Controls.Add(comboPR);
            Controls.Add(label5);
            Controls.Add(textProUnit);
            Controls.Add(textProStock);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(btnBrowseImage);
            Controls.Add(picProImage);
            Controls.Add(labelMode);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(textProQuan);
            Controls.Add(textProName);
            Controls.Add(textProId);
            Controls.Add(dgvProducts);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormProduct";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormProduct";
            Load += FormProduct_Load;
            ((System.ComponentModel.ISupportInitialize)picProImage).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProducts).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textProStock;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Button btnBrowseImage;
        private PictureBox picProImage;
        private Label labelMode;
        private Button buttonDelete;
        private Button buttonEdit;
        private Button buttonAdd;
        private TextBox textProQuan;
        private TextBox textProName;
        private TextBox textProId;
        private DataGridView dgvProducts;
        private TextBox textProUnit;
        private Label label5;
        private ComboBox comboPR;
        private ComboBox comboPO;
        private Label label6;
        private Label label7;
        private ComboBox cboStockCategoryFilter;
        private TextBox txtSearchProductName;
        private Button btnExecuteSearch;
        private Label label8;
        private Label label9;
    }
}