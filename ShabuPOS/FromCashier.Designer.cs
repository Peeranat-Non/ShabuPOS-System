namespace ShabuPOS
{
    partial class FromCashier
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
            txtCash = new TextBox();
            txtPrice = new TextBox();
            label3 = new Label();
            label2 = new Label();
            dgvReceipt = new DataGridView();
            btnPrint = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvReceipt).BeginInit();
            SuspendLayout();
            // 
            // txtCash
            // 
            txtCash.Location = new Point(606, 666);
            txtCash.Name = "txtCash";
            txtCash.Size = new Size(238, 38);
            txtCash.TabIndex = 41;
            // 
            // txtPrice
            // 
            txtPrice.Location = new Point(606, 618);
            txtPrice.Name = "txtPrice";
            txtPrice.ReadOnly = true;
            txtPrice.Size = new Size(238, 38);
            txtPrice.TabIndex = 40;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(497, 666);
            label3.Name = "label3";
            label3.Size = new Size(85, 31);
            label3.TabIndex = 37;
            label3.Text = "เงินที่รับ:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(452, 621);
            label2.Name = "label2";
            label2.Size = new Size(132, 31);
            label2.TabIndex = 36;
            label2.Text = "ราคาแพ็คเกจ:";
            // 
            // dgvReceipt
            // 
            dgvReceipt.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvReceipt.Location = new Point(45, 45);
            dgvReceipt.Margin = new Padding(5);
            dgvReceipt.Name = "dgvReceipt";
            dgvReceipt.RowHeadersWidth = 51;
            dgvReceipt.Size = new Size(1400, 468);
            dgvReceipt.TabIndex = 34;
            // 
            // btnPrint
            // 
            btnPrint.Location = new Point(606, 727);
            btnPrint.Name = "btnPrint";
            btnPrint.Size = new Size(168, 57);
            btnPrint.TabIndex = 43;
            btnPrint.Text = "พิมพ์ใบเสร็จ";
            btnPrint.UseVisualStyleBackColor = true;
            btnPrint.Click += btnPrint_Click;
            // 
            // FromCashier
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1767, 958);
            Controls.Add(btnPrint);
            Controls.Add(txtCash);
            Controls.Add(txtPrice);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(dgvReceipt);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FromCashier";
            Text = "FromCashier";
            Load += FromCashier_Load;
            ((System.ComponentModel.ISupportInitialize)dgvReceipt).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox txtCash;
        private TextBox txtPrice;
        private Label label3;
        private Label label2;
        private DataGridView dgvReceipt;
        private Button btnPrint;
    }
}