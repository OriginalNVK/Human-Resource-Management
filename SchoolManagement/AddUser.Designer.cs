
using System.Windows.Forms;

namespace SchoolManagement
{
    partial class AddUser
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddUser));
			this.label1 = new System.Windows.Forms.Label();
			this.txtName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtID = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
			this.label4 = new System.Windows.Forms.Label();
			this.Dashboard = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lbHello = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.pbLogout = new System.Windows.Forms.PictureBox();
			this.label8 = new System.Windows.Forms.Label();
			this.pbSection = new System.Windows.Forms.PictureBox();
			this.lbProfile = new System.Windows.Forms.Label();
			this.pbProfile = new System.Windows.Forms.PictureBox();
			this.lbClasses = new System.Windows.Forms.Label();
			this.pbClasses = new System.Windows.Forms.PictureBox();
			this.lbTeachers = new System.Windows.Forms.Label();
			this.pbTeachers = new System.Windows.Forms.PictureBox();
			this.label9 = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.txtHoTen = new System.Windows.Forms.TextBox();
			this.cbPhai = new System.Windows.Forms.ComboBox();
			this.dtpNgaySinh = new System.Windows.Forms.DateTimePicker();
			this.txtDiaChi = new System.Windows.Forms.TextBox();
			this.txtDT = new System.Windows.Forms.TextBox();
			this.cbTinhTrang = new System.Windows.Forms.ComboBox();
			this.txtLuong = new System.Windows.Forms.TextBox();
			this.txtPhuCap = new System.Windows.Forms.TextBox();
			this.cbVaiTro = new System.Windows.Forms.ComboBox();
			this.cbKhoa = new System.Windows.Forms.ComboBox();
			this.btnLogin = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			((System.ComponentModel.ISupportInitialize)(this.pbLogout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSection)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbClasses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTeachers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(673, 133);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(311, 22);
			this.label1.TabIndex = 180;
			this.label1.Text = "----------USER INFORMATION----------";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtName
			// 
			this.txtName.Enabled = false;
			this.txtName.Location = new System.Drawing.Point(786, 219);
			this.txtName.Margin = new System.Windows.Forms.Padding(4);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(285, 37);
			this.txtName.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtName.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtName.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtName.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtName.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtName.StateCommon.Border.Rounding = 20;
			this.txtName.StateCommon.Border.Width = 1;
			this.txtName.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtName.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtName.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtName.TabIndex = 177;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(595, 229);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(119, 22);
			this.label2.TabIndex = 176;
			this.label2.Text = "PASSWORD:";
			// 
			// txtID
			// 
			this.txtID.Enabled = false;
			this.txtID.Location = new System.Drawing.Point(786, 171);
			this.txtID.Margin = new System.Windows.Forms.Padding(4);
			this.txtID.Name = "txtID";
			this.txtID.Size = new System.Drawing.Size(285, 37);
			this.txtID.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtID.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtID.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtID.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtID.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtID.StateCommon.Border.Rounding = 20;
			this.txtID.StateCommon.Border.Width = 1;
			this.txtID.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtID.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtID.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtID.TabIndex = 175;
			this.txtID.TextChanged += new System.EventHandler(this.txtID_TextChanged);
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(595, 181);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(114, 22);
			this.label10.TabIndex = 174;
			this.label10.Text = "USERNAME:";
			// 
			// kryptonPalette1
			// 
			this.kryptonPalette1.ButtonSpecs.FormClose.Image = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormClose.Image")));
			this.kryptonPalette1.ButtonSpecs.FormClose.ImageStates.ImagePressed = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormClose.ImageStates.ImagePressed")));
			this.kryptonPalette1.ButtonSpecs.FormClose.ImageStates.ImageTracking = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormClose.ImageStates.ImageTracking")));
			this.kryptonPalette1.ButtonSpecs.FormMax.Image = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormMax.Image")));
			this.kryptonPalette1.ButtonSpecs.FormMax.ImageStates.ImagePressed = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormMax.ImageStates.ImagePressed")));
			this.kryptonPalette1.ButtonSpecs.FormMax.ImageStates.ImageTracking = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormMax.ImageStates.ImageTracking")));
			this.kryptonPalette1.ButtonSpecs.FormMin.Image = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormMin.Image")));
			this.kryptonPalette1.ButtonSpecs.FormMin.ImageStates.ImagePressed = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormMin.ImageStates.ImagePressed")));
			this.kryptonPalette1.ButtonSpecs.FormMin.ImageStates.ImageTracking = ((System.Drawing.Image)(resources.GetObject("kryptonPalette1.ButtonSpecs.FormMin.ImageStates.ImageTracking")));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateNormal.Border.Width = 0;
			this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StatePressed.Border.Width = 0;
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.kryptonPalette1.ButtonStyles.ButtonForm.StateTracking.Border.Width = 0;
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StatePressed.Border.Width = 0;
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.kryptonPalette1.ButtonStyles.ButtonFormClose.StateTracking.Border.Width = 0;
			this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.FormStyles.FormMain.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
			this.kryptonPalette1.FormStyles.FormMain.StateCommon.Border.Rounding = 12;
			this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.ButtonEdgeInset = 10;
			this.kryptonPalette1.HeaderStyles.HeaderForm.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, -1, -1, -1);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.BackColor = System.Drawing.Color.WhiteSmoke;
			this.label4.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Location = new System.Drawing.Point(703, 807);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(184, 22);
			this.label4.TabIndex = 206;
			this.label4.Text = "Nocopy right: 2025";
			// 
			// Dashboard
			// 
			this.Dashboard.AutoSize = true;
			this.Dashboard.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold);
			this.Dashboard.ForeColor = System.Drawing.Color.Yellow;
			this.Dashboard.Location = new System.Drawing.Point(379, 35);
			this.Dashboard.Name = "Dashboard";
			this.Dashboard.Size = new System.Drawing.Size(142, 32);
			this.Dashboard.TabIndex = 186;
			this.Dashboard.Text = "ADD USER";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.White;
			this.label5.Location = new System.Drawing.Point(3, 24);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(290, 80);
			this.label5.TabIndex = 184;
			this.label5.Text = "ADMIN \r\nSCHOOL PORTAL";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label5.UseWaitCursor = true;
			// 
			// lbHello
			// 
			this.lbHello.AutoSize = true;
			this.lbHello.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbHello.ForeColor = System.Drawing.SystemColors.Control;
			this.lbHello.Location = new System.Drawing.Point(1151, 35);
			this.lbHello.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbHello.Name = "lbHello";
			this.lbHello.Size = new System.Drawing.Size(123, 22);
			this.lbHello.TabIndex = 185;
			this.lbHello.Text = "Hello, Admin";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label6.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.label6.Location = new System.Drawing.Point(81, 476);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(90, 22);
			this.label6.TabIndex = 203;
			this.label6.Text = "LOGOUT";
			// 
			// pbLogout
			// 
			this.pbLogout.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbLogout.Image = ((System.Drawing.Image)(resources.GetObject("pbLogout.Image")));
			this.pbLogout.Location = new System.Drawing.Point(23, 463);
			this.pbLogout.Margin = new System.Windows.Forms.Padding(4);
			this.pbLogout.Name = "pbLogout";
			this.pbLogout.Size = new System.Drawing.Size(39, 54);
			this.pbLogout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbLogout.TabIndex = 202;
			this.pbLogout.TabStop = false;
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.label8.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label8.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.label8.Location = new System.Drawing.Point(81, 213);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(172, 22);
			this.label8.TabIndex = 201;
			this.label8.Text = "ROLES MANAGER";
			// 
			// pbSection
			// 
			this.pbSection.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbSection.Image = ((System.Drawing.Image)(resources.GetObject("pbSection.Image")));
			this.pbSection.Location = new System.Drawing.Point(23, 196);
			this.pbSection.Margin = new System.Windows.Forms.Padding(4);
			this.pbSection.Name = "pbSection";
			this.pbSection.Size = new System.Drawing.Size(39, 51);
			this.pbSection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbSection.TabIndex = 200;
			this.pbSection.TabStop = false;
			// 
			// lbProfile
			// 
			this.lbProfile.AutoSize = true;
			this.lbProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbProfile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbProfile.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbProfile.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbProfile.Location = new System.Drawing.Point(81, 411);
			this.lbProfile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbProfile.Name = "lbProfile";
			this.lbProfile.Size = new System.Drawing.Size(116, 22);
			this.lbProfile.TabIndex = 199;
			this.lbProfile.Text = "MY PROFILE";
			// 
			// pbProfile
			// 
			this.pbProfile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbProfile.Image = ((System.Drawing.Image)(resources.GetObject("pbProfile.Image")));
			this.pbProfile.Location = new System.Drawing.Point(23, 391);
			this.pbProfile.Margin = new System.Windows.Forms.Padding(4);
			this.pbProfile.Name = "pbProfile";
			this.pbProfile.Size = new System.Drawing.Size(39, 54);
			this.pbProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbProfile.TabIndex = 198;
			this.pbProfile.TabStop = false;
			// 
			// lbClasses
			// 
			this.lbClasses.AutoSize = true;
			this.lbClasses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbClasses.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbClasses.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbClasses.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbClasses.Location = new System.Drawing.Point(81, 145);
			this.lbClasses.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbClasses.Name = "lbClasses";
			this.lbClasses.Size = new System.Drawing.Size(167, 22);
			this.lbClasses.TabIndex = 197;
			this.lbClasses.Text = "USERS MANAGER";
			// 
			// pbClasses
			// 
			this.pbClasses.BackColor = System.Drawing.Color.White;
			this.pbClasses.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbClasses.Image = ((System.Drawing.Image)(resources.GetObject("pbClasses.Image")));
			this.pbClasses.Location = new System.Drawing.Point(23, 133);
			this.pbClasses.Margin = new System.Windows.Forms.Padding(4);
			this.pbClasses.Name = "pbClasses";
			this.pbClasses.Size = new System.Drawing.Size(39, 44);
			this.pbClasses.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbClasses.TabIndex = 196;
			this.pbClasses.TabStop = false;
			// 
			// lbTeachers
			// 
			this.lbTeachers.AutoSize = true;
			this.lbTeachers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbTeachers.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbTeachers.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbTeachers.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbTeachers.Location = new System.Drawing.Point(81, 340);
			this.lbTeachers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbTeachers.Name = "lbTeachers";
			this.lbTeachers.Size = new System.Drawing.Size(221, 22);
			this.lbTeachers.TabIndex = 195;
			this.lbTeachers.Text = "PERSONNEL MANAGER";
			// 
			// pbTeachers
			// 
			this.pbTeachers.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbTeachers.Image = ((System.Drawing.Image)(resources.GetObject("pbTeachers.Image")));
			this.pbTeachers.Location = new System.Drawing.Point(23, 320);
			this.pbTeachers.Margin = new System.Windows.Forms.Padding(4);
			this.pbTeachers.Name = "pbTeachers";
			this.pbTeachers.Size = new System.Drawing.Size(39, 54);
			this.pbTeachers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbTeachers.TabIndex = 194;
			this.pbTeachers.TabStop = false;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label9.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.label9.Location = new System.Drawing.Point(81, 273);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(199, 22);
			this.label9.TabIndex = 193;
			this.label9.Text = "STUDENTS MANAGER";
			// 
			// pictureBox2
			// 
			this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
			this.pictureBox2.Location = new System.Drawing.Point(23, 255);
			this.pictureBox2.Margin = new System.Windows.Forms.Padding(4);
			this.pictureBox2.Name = "pictureBox2";
			this.pictureBox2.Size = new System.Drawing.Size(39, 50);
			this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBox2.TabIndex = 192;
			this.pictureBox2.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.panel2.Controls.Add(this.label5);
			this.panel2.Location = new System.Drawing.Point(0, -12);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(316, 867);
			this.panel2.TabIndex = 205;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
			this.panel1.Controls.Add(this.lbHello);
			this.panel1.Controls.Add(this.Dashboard);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1323, 100);
			this.panel1.TabIndex = 204;
			// 
			// comboBox1
			// 
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Items.AddRange(new object[] {
            "Admin",
            "SINH VIEN",
            "GIAO VIEN"});
			this.comboBox1.Location = new System.Drawing.Point(787, 269);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(285, 24);
			this.comboBox1.TabIndex = 207;
			this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(595, 271);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(63, 22);
			this.label3.TabIndex = 208;
			this.label3.Text = "ROLE:";
			// 
			// groupBox2
			// 
			this.groupBox2.Location = new System.Drawing.Point(599, 328);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(473, 221);
			this.groupBox2.TabIndex = 210;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "groupBox2";
			// 
			// txtHoTen
			// 
			this.txtHoTen.Location = new System.Drawing.Point(0, 0);
			this.txtHoTen.Name = "txtHoTen";
			this.txtHoTen.Size = new System.Drawing.Size(100, 22);
			this.txtHoTen.TabIndex = 0;
			// 
			// cbPhai
			// 
			this.cbPhai.Location = new System.Drawing.Point(0, 0);
			this.cbPhai.Name = "cbPhai";
			this.cbPhai.Size = new System.Drawing.Size(121, 24);
			this.cbPhai.TabIndex = 0;
			// 
			// dtpNgaySinh
			// 
			this.dtpNgaySinh.Location = new System.Drawing.Point(0, 0);
			this.dtpNgaySinh.Name = "dtpNgaySinh";
			this.dtpNgaySinh.Size = new System.Drawing.Size(200, 22);
			this.dtpNgaySinh.TabIndex = 0;
			// 
			// txtDiaChi
			// 
			this.txtDiaChi.Location = new System.Drawing.Point(0, 0);
			this.txtDiaChi.Name = "txtDiaChi";
			this.txtDiaChi.Size = new System.Drawing.Size(100, 22);
			this.txtDiaChi.TabIndex = 0;
			// 
			// txtDT
			// 
			this.txtDT.Location = new System.Drawing.Point(0, 0);
			this.txtDT.Name = "txtDT";
			this.txtDT.Size = new System.Drawing.Size(100, 22);
			this.txtDT.TabIndex = 0;
			// 
			// cbTinhTrang
			// 
			this.cbTinhTrang.Location = new System.Drawing.Point(0, 0);
			this.cbTinhTrang.Name = "cbTinhTrang";
			this.cbTinhTrang.Size = new System.Drawing.Size(121, 24);
			this.cbTinhTrang.TabIndex = 0;
			// 
			// txtLuong
			// 
			this.txtLuong.Location = new System.Drawing.Point(0, 0);
			this.txtLuong.Name = "txtLuong";
			this.txtLuong.Size = new System.Drawing.Size(100, 22);
			this.txtLuong.TabIndex = 0;
			// 
			// txtPhuCap
			// 
			this.txtPhuCap.Location = new System.Drawing.Point(0, 0);
			this.txtPhuCap.Name = "txtPhuCap";
			this.txtPhuCap.Size = new System.Drawing.Size(100, 22);
			this.txtPhuCap.TabIndex = 0;
			// 
			// cbVaiTro
			// 
			this.cbVaiTro.Location = new System.Drawing.Point(0, 0);
			this.cbVaiTro.Name = "cbVaiTro";
			this.cbVaiTro.Size = new System.Drawing.Size(121, 24);
			this.cbVaiTro.TabIndex = 0;
			// 
			// cbKhoa
			// 
			this.cbKhoa.Location = new System.Drawing.Point(0, 0);
			this.cbKhoa.Name = "cbKhoa";
			this.cbKhoa.Size = new System.Drawing.Size(121, 24);
			this.cbKhoa.TabIndex = 0;
			// 
			// btnLogin
			// 
			this.btnLogin.Location = new System.Drawing.Point(752, 587);
			this.btnLogin.Margin = new System.Windows.Forms.Padding(4);
			this.btnLogin.Name = "btnLogin";
			this.btnLogin.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnLogin.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnLogin.OverrideDefault.Back.ColorAngle = 62F;
			this.btnLogin.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnLogin.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnLogin.OverrideDefault.Border.ColorAngle = 62F;
			this.btnLogin.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnLogin.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnLogin.OverrideDefault.Border.Rounding = 20;
			this.btnLogin.OverrideDefault.Border.Width = 1;
			this.btnLogin.Size = new System.Drawing.Size(180, 53);
			this.btnLogin.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnLogin.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnLogin.StateCommon.Back.ColorAngle = 62F;
			this.btnLogin.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnLogin.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnLogin.StateCommon.Border.ColorAngle = 62F;
			this.btnLogin.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnLogin.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnLogin.StateCommon.Border.Rounding = 20;
			this.btnLogin.StateCommon.Border.Width = 1;
			this.btnLogin.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
			this.btnLogin.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
			this.btnLogin.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnLogin.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnLogin.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnLogin.StatePressed.Back.ColorAngle = 62F;
			this.btnLogin.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnLogin.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnLogin.StatePressed.Border.ColorAngle = 62F;
			this.btnLogin.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnLogin.StatePressed.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnLogin.StatePressed.Border.Rounding = 20;
			this.btnLogin.StatePressed.Border.Width = 1;
			this.btnLogin.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnLogin.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnLogin.StateTracking.Back.ColorAngle = 62F;
			this.btnLogin.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnLogin.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnLogin.StateTracking.Border.ColorAngle = 62F;
			this.btnLogin.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnLogin.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnLogin.StateTracking.Border.Rounding = 20;
			this.btnLogin.StateTracking.Border.Width = 1;
			this.btnLogin.TabIndex = 211;
			this.btnLogin.Values.Text = "CREATE USER";
			// 
			// AddUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1323, 853);
			this.Controls.Add(this.btnLogin);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.comboBox1);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.pbLogout);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.pbSection);
			this.Controls.Add(this.lbProfile);
			this.Controls.Add(this.pbProfile);
			this.Controls.Add(this.lbClasses);
			this.Controls.Add(this.pbClasses);
			this.Controls.Add(this.lbTeachers);
			this.Controls.Add(this.pbTeachers);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtName);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtID);
			this.Controls.Add(this.label10);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "AddUser";
			this.Palette = this.kryptonPalette1;
			this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ADD USER";
			this.Load += new System.EventHandler(this.DepartmentManager_Load);
			((System.ComponentModel.ISupportInitialize)(this.pbLogout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSection)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbProfile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbClasses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTeachers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtName;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtID;
        private System.Windows.Forms.Label label10;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label Dashboard;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lbHello;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.PictureBox pbLogout;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.PictureBox pbSection;
		private System.Windows.Forms.Label lbProfile;
		private System.Windows.Forms.PictureBox pbProfile;
		private System.Windows.Forms.Label lbClasses;
		private System.Windows.Forms.PictureBox pbClasses;
		private System.Windows.Forms.Label lbTeachers;
		private System.Windows.Forms.PictureBox pbTeachers;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.GroupBox groupBox2;
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnLogin;
	}
}