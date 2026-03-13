namespace ShabuPOS
{
    partial class FormMain
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
            buttonLogout = new Button();
            buttonManageEmployee = new Button();
            buttonManageMenu = new Button();
            buttonManageShop = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            lblWelcome = new Label();
            panelMediaSubMenu = new Panel();
            panelMenuScroll = new Panel();
            btnKitchen = new Button();
            btnSavePro = new Button();
            buttonCashier = new Button();
            panelLogo = new Panel();
            buttonAccountFree = new Button();
            btnManager = new Button();
            panelChildForm = new Panel();
            panelMediaSubMenu.SuspendLayout();
            panelMenuScroll.SuspendLayout();
            panelLogo.SuspendLayout();
            SuspendLayout();
            // 
            // buttonLogout
            // 
            buttonLogout.Font = new Font("Leelawadee UI", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonLogout.Image = Properties.Resources.logout_16697253;
            buttonLogout.ImageAlign = ContentAlignment.MiddleRight;
            buttonLogout.Location = new Point(22, 875);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(302, 62);
            buttonLogout.TabIndex = 7;
            buttonLogout.Text = "ออกจากระบบ";
            buttonLogout.TextImageRelation = TextImageRelation.ImageBeforeText;
            buttonLogout.UseVisualStyleBackColor = true;
            buttonLogout.Click += buttonLogout_Click;
            // 
            // buttonManageEmployee
            // 
            buttonManageEmployee.Location = new Point(12, 275);
            buttonManageEmployee.Name = "buttonManageEmployee";
            buttonManageEmployee.Padding = new Padding(60, 0, 0, 0);
            buttonManageEmployee.Size = new Size(328, 62);
            buttonManageEmployee.TabIndex = 6;
            buttonManageEmployee.Text = "จัดการข้อมูลพนักงาน";
            buttonManageEmployee.TextAlign = ContentAlignment.MiddleLeft;
            buttonManageEmployee.UseVisualStyleBackColor = true;
            buttonManageEmployee.Click += buttonManageEmployee_Click;
            // 
            // buttonManageMenu
            // 
            buttonManageMenu.Location = new Point(12, 343);
            buttonManageMenu.Name = "buttonManageMenu";
            buttonManageMenu.Padding = new Padding(60, 0, 0, 0);
            buttonManageMenu.Size = new Size(328, 62);
            buttonManageMenu.TabIndex = 5;
            buttonManageMenu.Text = "จัดการข้อมูลรายการอาหาร";
            buttonManageMenu.TextAlign = ContentAlignment.MiddleLeft;
            buttonManageMenu.UseVisualStyleBackColor = true;
            buttonManageMenu.Click += buttonManageMenu_Click;
            // 
            // buttonManageShop
            // 
            buttonManageShop.Location = new Point(12, 207);
            buttonManageShop.Name = "buttonManageShop";
            buttonManageShop.Padding = new Padding(60, 0, 0, 0);
            buttonManageShop.Size = new Size(328, 62);
            buttonManageShop.TabIndex = 4;
            buttonManageShop.Text = "จัดการข้อมูลร้านค้า";
            buttonManageShop.TextAlign = ContentAlignment.MiddleLeft;
            buttonManageShop.UseVisualStyleBackColor = true;
            buttonManageShop.Click += buttonManageShop_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 3);
            button1.Name = "button1";
            button1.Padding = new Padding(60, 0, 0, 0);
            button1.Size = new Size(328, 62);
            button1.TabIndex = 8;
            button1.Text = "พนักงานต้อนรับ";
            button1.TextAlign = ContentAlignment.MiddleLeft;
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 479);
            button2.Name = "button2";
            button2.Padding = new Padding(60, 0, 0, 0);
            button2.Size = new Size(328, 62);
            button2.TabIndex = 9;
            button2.Text = "จัดการสินค้า";
            button2.TextAlign = ContentAlignment.MiddleLeft;
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(12, 547);
            button3.Name = "button3";
            button3.Padding = new Padding(60, 0, 0, 0);
            button3.Size = new Size(328, 62);
            button3.TabIndex = 10;
            button3.Text = "ใบขอซื้อ";
            button3.TextAlign = ContentAlignment.MiddleLeft;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(12, 615);
            button4.Name = "button4";
            button4.Padding = new Padding(60, 0, 0, 0);
            button4.Size = new Size(328, 62);
            button4.TabIndex = 11;
            button4.Text = "ใบสั่งซื้อ";
            button4.TextAlign = ContentAlignment.MiddleLeft;
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(12, 683);
            button5.Name = "button5";
            button5.Padding = new Padding(60, 0, 0, 0);
            button5.Size = new Size(328, 62);
            button5.TabIndex = 12;
            button5.Text = "Stock";
            button5.TextAlign = ContentAlignment.MiddleLeft;
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(12, 751);
            button6.Name = "button6";
            button6.Padding = new Padding(60, 0, 0, 0);
            button6.Size = new Size(328, 62);
            button6.TabIndex = 13;
            button6.Text = "พนักงานการเงิน";
            button6.TextAlign = ContentAlignment.MiddleLeft;
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // lblWelcome
            // 
            lblWelcome.BorderStyle = BorderStyle.Fixed3D;
            lblWelcome.Location = new Point(847, 24);
            lblWelcome.Name = "lblWelcome";
            lblWelcome.Size = new Size(315, 31);
            lblWelcome.TabIndex = 19;
            // 
            // panelMediaSubMenu
            // 
            panelMediaSubMenu.BackColor = Color.FromArgb(15, 23, 42);
            panelMediaSubMenu.Controls.Add(panelMenuScroll);
            panelMediaSubMenu.Controls.Add(panelLogo);
            panelMediaSubMenu.Controls.Add(buttonLogout);
            panelMediaSubMenu.Dock = DockStyle.Left;
            panelMediaSubMenu.Location = new Point(0, 0);
            panelMediaSubMenu.Name = "panelMediaSubMenu";
            panelMediaSubMenu.Size = new Size(356, 958);
            panelMediaSubMenu.TabIndex = 21;
            // 
            // panelMenuScroll
            // 
            panelMenuScroll.AutoScroll = true;
            panelMenuScroll.Controls.Add(button6);
            panelMenuScroll.Controls.Add(button1);
            panelMenuScroll.Controls.Add(button5);
            panelMenuScroll.Controls.Add(buttonManageMenu);
            panelMenuScroll.Controls.Add(button2);
            panelMenuScroll.Controls.Add(buttonManageEmployee);
            panelMenuScroll.Controls.Add(button4);
            panelMenuScroll.Controls.Add(buttonManageShop);
            panelMenuScroll.Controls.Add(btnKitchen);
            panelMenuScroll.Controls.Add(btnSavePro);
            panelMenuScroll.Controls.Add(button3);
            panelMenuScroll.Controls.Add(buttonCashier);
            panelMenuScroll.Location = new Point(0, 154);
            panelMenuScroll.Name = "panelMenuScroll";
            panelMenuScroll.Padding = new Padding(20);
            panelMenuScroll.Size = new Size(340, 568);
            panelMenuScroll.TabIndex = 9;
            // 
            // btnKitchen
            // 
            btnKitchen.Location = new Point(12, 139);
            btnKitchen.Name = "btnKitchen";
            btnKitchen.Padding = new Padding(60, 0, 0, 0);
            btnKitchen.Size = new Size(328, 62);
            btnKitchen.TabIndex = 14;
            btnKitchen.Text = "พนักงานครัว";
            btnKitchen.TextAlign = ContentAlignment.MiddleLeft;
            btnKitchen.UseVisualStyleBackColor = true;
            btnKitchen.Click += btnKitchen_Click;
            // 
            // btnSavePro
            // 
            btnSavePro.Location = new Point(12, 411);
            btnSavePro.Name = "btnSavePro";
            btnSavePro.Padding = new Padding(60, 0, 0, 0);
            btnSavePro.Size = new Size(328, 62);
            btnSavePro.TabIndex = 15;
            btnSavePro.Text = "บันทึกโปรโมชั่น";
            btnSavePro.TextAlign = ContentAlignment.MiddleLeft;
            btnSavePro.UseVisualStyleBackColor = true;
            btnSavePro.Click += btnSavePro_Click;
            // 
            // buttonCashier
            // 
            buttonCashier.Location = new Point(12, 71);
            buttonCashier.Name = "buttonCashier";
            buttonCashier.Padding = new Padding(60, 0, 0, 0);
            buttonCashier.Size = new Size(328, 62);
            buttonCashier.TabIndex = 17;
            buttonCashier.Text = "แคชเชีย";
            buttonCashier.TextAlign = ContentAlignment.MiddleLeft;
            buttonCashier.UseVisualStyleBackColor = true;
            buttonCashier.Click += buttonCashier_Click;
            // 
            // panelLogo
            // 
            panelLogo.Controls.Add(buttonAccountFree);
            panelLogo.Controls.Add(btnManager);
            panelLogo.Dock = DockStyle.Top;
            panelLogo.Location = new Point(0, 0);
            panelLogo.Name = "panelLogo";
            panelLogo.Size = new Size(356, 148);
            panelLogo.TabIndex = 0;
            // 
            // buttonAccountFree
            // 
            buttonAccountFree.Font = new Font("Leelawadee UI", 15F);
            buttonAccountFree.Location = new Point(12, 83);
            buttonAccountFree.Name = "buttonAccountFree";
            buttonAccountFree.Padding = new Padding(60, 0, 0, 0);
            buttonAccountFree.Size = new Size(328, 62);
            buttonAccountFree.TabIndex = 17;
            buttonAccountFree.Text = "เจ้าหน้าที่บัญชี";
            buttonAccountFree.TextAlign = ContentAlignment.MiddleLeft;
            buttonAccountFree.UseVisualStyleBackColor = true;
            buttonAccountFree.Click += buttonAccountFree_Click;
            // 
            // btnManager
            // 
            btnManager.Font = new Font("Leelawadee UI", 15F);
            btnManager.Location = new Point(12, 15);
            btnManager.Name = "btnManager";
            btnManager.Padding = new Padding(60, 0, 0, 0);
            btnManager.Size = new Size(328, 62);
            btnManager.TabIndex = 16;
            btnManager.Text = "ผู้จัดการ";
            btnManager.TextAlign = ContentAlignment.MiddleLeft;
            btnManager.UseVisualStyleBackColor = true;
            btnManager.Click += btnManager_Click;
            // 
            // panelChildForm
            // 
            panelChildForm.BackColor = Color.White;
            panelChildForm.Dock = DockStyle.Fill;
            panelChildForm.Location = new Point(356, 0);
            panelChildForm.Name = "panelChildForm";
            panelChildForm.Size = new Size(1411, 958);
            panelChildForm.TabIndex = 22;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(13F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoScroll = true;
            ClientSize = new Size(1767, 958);
            Controls.Add(panelChildForm);
            Controls.Add(panelMediaSubMenu);
            Controls.Add(lblWelcome);
            Font = new Font("Leelawadee UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            Margin = new Padding(5);
            Name = "FormMain";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FormMain";
            WindowState = FormWindowState.Maximized;
            Load += FormMain_Load;
            panelMediaSubMenu.ResumeLayout(false);
            panelMenuScroll.ResumeLayout(false);
            panelLogo.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Button buttonLogout;
        private Button buttonManageEmployee;
        private Button buttonManageMenu;
        private Button buttonManageShop;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Label lblWelcome;
        private Panel panelMediaSubMenu;
        private Panel panelChildForm;
        private Button btnKitchen;
        private Button btnSavePro;
        private Button btnManager;
        private Button buttonCashier;
        private Button buttonAccountFree;
        private Panel panelLogo;
        private Panel panelMenuScroll;
    }
}