namespace ShabuPOS
{
    partial class FormReceiveStock
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
            label1 = new Label();
            txtSearchPO = new TextBox();
            btnSearch = new Button();
            dgvReceive = new DataGridView();
            btnSaveReceive = new Button();
            dgvPOList = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvReceive).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPOList).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1012, 161);
            label1.Name = "label1";
            label1.Size = new Size(134, 31);
            label1.TabIndex = 0;
            label1.Text = "เลขที่ใบสั่งซื้อ:";
            // 
            // txtSearchPO
            // 
            txtSearchPO.Location = new Point(1152, 161);
            txtSearchPO.Name = "txtSearchPO";
            txtSearchPO.Size = new Size(178, 38);
            txtSearchPO.TabIndex = 1;
            // 
            // btnSearch
            // 
            btnSearch.Image = Properties.Resources.search_10023603;
            btnSearch.Location = new Point(1355, 154);
            btnSearch.Name = "btnSearch";
            btnSearch.Size = new Size(163, 50);
            btnSearch.TabIndex = 2;
            btnSearch.Text = "ค้นหา";
            btnSearch.TextAlign = ContentAlignment.MiddleRight;
            btnSearch.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSearch.UseVisualStyleBackColor = true;
            btnSearch.Click += btnSearch_Click;
            // 
            // dgvReceive
            // 
            dgvReceive.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReceive.Location = new Point(46, 519);
            dgvReceive.Name = "dgvReceive";
            dgvReceive.RowHeadersWidth = 51;
            dgvReceive.Size = new Size(885, 335);
            dgvReceive.TabIndex = 3;
            // 
            // btnSaveReceive
            // 
            btnSaveReceive.Image = Properties.Resources.plus__1_;
            btnSaveReceive.Location = new Point(1108, 418);
            btnSaveReceive.Name = "btnSaveReceive";
            btnSaveReceive.Size = new Size(339, 71);
            btnSaveReceive.TabIndex = 4;
            btnSaveReceive.Text = "บันทึกรับสินค้า";
            btnSaveReceive.TextAlign = ContentAlignment.MiddleRight;
            btnSaveReceive.TextImageRelation = TextImageRelation.ImageBeforeText;
            btnSaveReceive.UseVisualStyleBackColor = true;
            btnSaveReceive.Click += btnSaveReceive_Click;
            // 
            // dgvPOList
            // 
            dgvPOList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPOList.Location = new Point(46, 154);
            dgvPOList.Name = "dgvPOList";
            dgvPOList.RowHeadersWidth = 51;
            dgvPOList.Size = new Size(885, 335);
            dgvPOList.TabIndex = 5;
            dgvPOList.CellClick += dgvPOList_CellClick;
            // 
            // FormReceiveStock
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(dgvPOList);
            Controls.Add(btnSaveReceive);
            Controls.Add(dgvReceive);
            Controls.Add(btnSearch);
            Controls.Add(txtSearchPO);
            Controls.Add(label1);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormReceiveStock";
            Text = "FormReceiveStock";
            Load += FormReceiveStock_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReceive).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPOList).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private TextBox txtSearchPO;
        private Button btnSearch;
        private DataGridView dgvReceive;
        private Button btnSaveReceive;
        private DataGridView dgvPOList;
    }
}