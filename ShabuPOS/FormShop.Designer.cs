namespace ShabuPOS
{
    partial class FormShop
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
            label5 = new Label();
            labelMode = new Label();
            buttonDelete = new Button();
            buttonEdit = new Button();
            buttonAdd = new Button();
            textShopAddress = new TextBox();
            textShopName = new TextBox();
            textShopId = new TextBox();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            dgvShops = new DataGridView();
            textShopPhone = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvShops).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(373, 554);
            label5.Name = "label5";
            label5.Size = new Size(93, 31);
            label5.TabIndex = 54;
            label5.Text = "เบอร์โทร:";
            // 
            // labelMode
            // 
            labelMode.BackColor = Color.Green;
            labelMode.BorderStyle = BorderStyle.Fixed3D;
            labelMode.ForeColor = Color.White;
            labelMode.Location = new Point(92, 400);
            labelMode.Name = "labelMode";
            labelMode.Size = new Size(231, 56);
            labelMode.TabIndex = 52;
            // 
            // buttonDelete
            // 
            buttonDelete.Image = Properties.Resources.bin__1_;
            buttonDelete.Location = new Point(754, 638);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(225, 55);
            buttonDelete.TabIndex = 51;
            buttonDelete.Text = "ลบข้อมูล";
            buttonDelete.TextAlign = ContentAlignment.MiddleRight;
            buttonDelete.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonEdit
            // 
            buttonEdit.Image = Properties.Resources.editอันนี้แหละ;
            buttonEdit.Location = new Point(523, 638);
            buttonEdit.Name = "buttonEdit";
            buttonEdit.Size = new Size(224, 55);
            buttonEdit.TabIndex = 50;
            buttonEdit.Text = "แก้ไขข้อมูล";
            buttonEdit.TextAlign = ContentAlignment.MiddleRight;
            buttonEdit.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonEdit.UseVisualStyleBackColor = true;
            buttonEdit.Click += buttonEdit_Click;
            // 
            // buttonAdd
            // 
            buttonAdd.Image = Properties.Resources.plus__1_;
            buttonAdd.Location = new Point(292, 638);
            buttonAdd.Name = "buttonAdd";
            buttonAdd.Size = new Size(225, 55);
            buttonAdd.TabIndex = 49;
            buttonAdd.Text = "เพิ่มข้อมูลร้านค้า";
            buttonAdd.TextAlign = ContentAlignment.MiddleRight;
            buttonAdd.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonAdd.UseVisualStyleBackColor = true;
            buttonAdd.Click += buttonAdd_Click;
            // 
            // textShopAddress
            // 
            textShopAddress.Location = new Point(523, 497);
            textShopAddress.Name = "textShopAddress";
            textShopAddress.Size = new Size(239, 38);
            textShopAddress.TabIndex = 48;
            // 
            // textShopName
            // 
            textShopName.Location = new Point(523, 446);
            textShopName.Name = "textShopName";
            textShopName.Size = new Size(238, 38);
            textShopName.TabIndex = 47;
            // 
            // textShopId
            // 
            textShopId.Location = new Point(523, 398);
            textShopId.Name = "textShopId";
            textShopId.Size = new Size(238, 38);
            textShopId.TabIndex = 46;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(409, 496);
            label4.Name = "label4";
            label4.Size = new Size(57, 31);
            label4.TabIndex = 44;
            label4.Text = "ที่อยู่:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(362, 445);
            label3.Name = "label3";
            label3.Size = new Size(102, 31);
            label3.TabIndex = 43;
            label3.Text = "ชื่อร้านค้า:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(354, 400);
            label2.Name = "label2";
            label2.Size = new Size(112, 31);
            label2.TabIndex = 42;
            label2.Text = "รหัสร้านค้า:";
            // 
            // dgvShops
            // 
            dgvShops.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvShops.Location = new Point(14, 14);
            dgvShops.Margin = new Padding(5);
            dgvShops.Name = "dgvShops";
            dgvShops.RowHeadersWidth = 51;
            dgvShops.Size = new Size(1253, 344);
            dgvShops.TabIndex = 40;
            dgvShops.CellClick += dgvShops_CellClick;
            // 
            // textShopPhone
            // 
            textShopPhone.Location = new Point(523, 555);
            textShopPhone.Name = "textShopPhone";
            textShopPhone.Size = new Size(239, 38);
            textShopPhone.TabIndex = 55;
            // 
            // FormShop
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(textShopPhone);
            Controls.Add(label5);
            Controls.Add(labelMode);
            Controls.Add(buttonDelete);
            Controls.Add(buttonEdit);
            Controls.Add(buttonAdd);
            Controls.Add(textShopAddress);
            Controls.Add(textShopName);
            Controls.Add(textShopId);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dgvShops);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormShop";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormShop";
            Load += FormShop_Load;
            ((System.ComponentModel.ISupportInitialize)dgvShops).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label5;
        private Label labelMode;
        private Button buttonDelete;
        private Button buttonEdit;
        private Button buttonAdd;
        private TextBox textShopAddress;
        private TextBox textShopName;
        private TextBox textShopId;
        private Label label4;
        private Label label3;
        private Label label2;
        private DataGridView dgvShops;
        private TextBox textShopPhone;
    }
}