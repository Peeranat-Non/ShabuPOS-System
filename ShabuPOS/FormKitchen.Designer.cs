namespace ShabuPOS
{
    partial class FormKitchen
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
            btnRefresh = new Button();
            dgvOrderHeader = new DataGridView();
            dgvOrderDetails = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)dgvOrderHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).BeginInit();
            SuspendLayout();
            // 
            // btnRefresh
            // 
            btnRefresh.Font = new Font("Leelawadee UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnRefresh.Location = new Point(1363, 206);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(246, 141);
            btnRefresh.TabIndex = 14;
            btnRefresh.Text = "รีเฟรชข้อมูล";
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dgvOrderHeader
            // 
            dgvOrderHeader.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderHeader.Location = new Point(29, 25);
            dgvOrderHeader.Name = "dgvOrderHeader";
            dgvOrderHeader.RowHeadersWidth = 51;
            dgvOrderHeader.Size = new Size(1241, 322);
            dgvOrderHeader.TabIndex = 13;
            dgvOrderHeader.CellContentClick += dgvOrderHeader_CellContentClick;
            dgvOrderHeader.CellFormatting += dgvOrderHeader_CellFormatting;
            dgvOrderHeader.SelectionChanged += dgvOrderHeader_SelectionChanged;
            // 
            // dgvOrderDetails
            // 
            dgvOrderDetails.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrderDetails.Location = new Point(29, 375);
            dgvOrderDetails.Name = "dgvOrderDetails";
            dgvOrderDetails.RowHeadersWidth = 51;
            dgvOrderDetails.Size = new Size(1241, 504);
            dgvOrderDetails.TabIndex = 17;
            dgvOrderDetails.CellContentClick += dgvOrderDetails_CellContentClick;
            // 
            // FormKitchen
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1767, 958);
            Controls.Add(dgvOrderDetails);
            Controls.Add(btnRefresh);
            Controls.Add(dgvOrderHeader);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormKitchen";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormKitchen";
            Load += FormKitchen_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrderHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvOrderDetails).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnReject;
        private Button btnRefresh;
        private DataGridView dgvOrderHeader;
        private DataGridView dgvOrderDetails;
    }
}