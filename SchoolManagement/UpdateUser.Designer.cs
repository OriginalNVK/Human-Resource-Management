
using System.Windows.Forms;

namespace SchoolManagement
{
	partial class UpdateUser
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateUser));
			this.label1 = new System.Windows.Forms.Label();
			this.txtPassword = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtUsername = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
			this.Dashboard = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.lbHello = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.pbLogout = new System.Windows.Forms.PictureBox();
			this.lbRoles = new System.Windows.Forms.Label();
			this.pbSection = new System.Windows.Forms.PictureBox();
			this.lbProfile = new System.Windows.Forms.Label();
			this.pbProfile = new System.Windows.Forms.PictureBox();
			this.lbUsers = new System.Windows.Forms.Label();
			this.pbClasses = new System.Windows.Forms.PictureBox();
			this.lbPersonnel = new System.Windows.Forms.Label();
			this.pbTeachers = new System.Windows.Forms.PictureBox();
			this.lbStudents = new System.Windows.Forms.Label();
			this.pictureBox2 = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.panel1 = new System.Windows.Forms.Panel();
			this.RoleDropdown = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.btnCreateUser = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			this.txtFullname = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtPhoneNum = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtAddress = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.GenderDropdown = new System.Windows.Forms.ComboBox();
			this.dtpDOB = new System.Windows.Forms.DateTimePicker();
			this.dgvUser = new ComponentFactory.Krypton.Toolkit.KryptonDataGridView();
			this.label4 = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbLogout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSection)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbClasses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTeachers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgvUser)).BeginInit();
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
			// txtPassword
			// 
			this.txtPassword.Location = new System.Drawing.Point(786, 219);
			this.txtPassword.Margin = new System.Windows.Forms.Padding(4);
			this.txtPassword.Name = "txtPassword";
			this.txtPassword.Size = new System.Drawing.Size(285, 37);
			this.txtPassword.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtPassword.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtPassword.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtPassword.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtPassword.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtPassword.StateCommon.Border.Rounding = 20;
			this.txtPassword.StateCommon.Border.Width = 1;
			this.txtPassword.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtPassword.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPassword.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtPassword.TabIndex = 177;
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
			// txtUsername
			// 
			this.txtUsername.Location = new System.Drawing.Point(786, 171);
			this.txtUsername.Margin = new System.Windows.Forms.Padding(4);
			this.txtUsername.Name = "txtUsername";
			this.txtUsername.Size = new System.Drawing.Size(285, 37);
			this.txtUsername.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtUsername.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtUsername.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtUsername.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtUsername.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtUsername.StateCommon.Border.Rounding = 20;
			this.txtUsername.StateCommon.Border.Width = 1;
			this.txtUsername.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtUsername.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtUsername.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtUsername.TabIndex = 175;
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
			// Dashboard
			// 
			this.Dashboard.AutoSize = true;
			this.Dashboard.Font = new System.Drawing.Font("Century Gothic", 16F, System.Drawing.FontStyle.Bold);
			this.Dashboard.ForeColor = System.Drawing.Color.Yellow;
			this.Dashboard.Location = new System.Drawing.Point(379, 35);
			this.Dashboard.Name = "Dashboard";
			this.Dashboard.Size = new System.Drawing.Size(181, 32);
			this.Dashboard.TabIndex = 186;
			this.Dashboard.Text = "UPDATE USER";
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
			this.label6.Click += new System.EventHandler(this.label6_Click);
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
			// lbRoles
			// 
			this.lbRoles.AutoSize = true;
			this.lbRoles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbRoles.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbRoles.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbRoles.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbRoles.Location = new System.Drawing.Point(81, 213);
			this.lbRoles.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbRoles.Name = "lbRoles";
			this.lbRoles.Size = new System.Drawing.Size(172, 22);
			this.lbRoles.TabIndex = 201;
			this.lbRoles.Text = "ROLES MANAGER";
			this.lbRoles.Click += new System.EventHandler(this.lbRoles_Click);
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
			this.lbProfile.Click += new System.EventHandler(this.lbProfile_Click);
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
			// lbUsers
			// 
			this.lbUsers.AutoSize = true;
			this.lbUsers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbUsers.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbUsers.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbUsers.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbUsers.Location = new System.Drawing.Point(81, 145);
			this.lbUsers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbUsers.Name = "lbUsers";
			this.lbUsers.Size = new System.Drawing.Size(167, 22);
			this.lbUsers.TabIndex = 197;
			this.lbUsers.Text = "USERS MANAGER";
			this.lbUsers.Click += new System.EventHandler(this.lbUsers_Click);
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
			// lbPersonnel
			// 
			this.lbPersonnel.AutoSize = true;
			this.lbPersonnel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbPersonnel.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbPersonnel.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbPersonnel.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbPersonnel.Location = new System.Drawing.Point(81, 340);
			this.lbPersonnel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbPersonnel.Name = "lbPersonnel";
			this.lbPersonnel.Size = new System.Drawing.Size(221, 22);
			this.lbPersonnel.TabIndex = 195;
			this.lbPersonnel.Text = "PERSONNEL MANAGER";
			this.lbPersonnel.Click += new System.EventHandler(this.lbPersonnel_Click);
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
			// lbStudents
			// 
			this.lbStudents.AutoSize = true;
			this.lbStudents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbStudents.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbStudents.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbStudents.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbStudents.Location = new System.Drawing.Point(81, 273);
			this.lbStudents.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbStudents.Name = "lbStudents";
			this.lbStudents.Size = new System.Drawing.Size(199, 22);
			this.lbStudents.TabIndex = 193;
			this.lbStudents.Text = "STUDENTS MANAGER";
			this.lbStudents.Click += new System.EventHandler(this.lbStudents_Click);
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
			// RoleDropdown
			// 
			this.RoleDropdown.FormattingEnabled = true;
			this.RoleDropdown.Items.AddRange(new object[] {
            "ADMIN",
            "SINH VIEN",
            "NHAN VIEN"});
			this.RoleDropdown.Location = new System.Drawing.Point(787, 269);
			this.RoleDropdown.Name = "RoleDropdown";
			this.RoleDropdown.Size = new System.Drawing.Size(285, 24);
			this.RoleDropdown.TabIndex = 207;
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
			// btnCreateUser
			// 
			this.btnCreateUser.Location = new System.Drawing.Point(730, 752);
			this.btnCreateUser.Margin = new System.Windows.Forms.Padding(4);
			this.btnCreateUser.Name = "btnCreateUser";
			this.btnCreateUser.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnCreateUser.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnCreateUser.OverrideDefault.Back.ColorAngle = 62F;
			this.btnCreateUser.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnCreateUser.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnCreateUser.OverrideDefault.Border.ColorAngle = 62F;
			this.btnCreateUser.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnCreateUser.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnCreateUser.OverrideDefault.Border.Rounding = 20;
			this.btnCreateUser.OverrideDefault.Border.Width = 1;
			this.btnCreateUser.Size = new System.Drawing.Size(180, 53);
			this.btnCreateUser.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StateCommon.Back.ColorAngle = 62F;
			this.btnCreateUser.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StateCommon.Border.ColorAngle = 62F;
			this.btnCreateUser.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnCreateUser.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnCreateUser.StateCommon.Border.Rounding = 20;
			this.btnCreateUser.StateCommon.Border.Width = 1;
			this.btnCreateUser.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
			this.btnCreateUser.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
			this.btnCreateUser.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnCreateUser.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StatePressed.Back.ColorAngle = 62F;
			this.btnCreateUser.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StatePressed.Border.ColorAngle = 62F;
			this.btnCreateUser.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnCreateUser.StatePressed.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnCreateUser.StatePressed.Border.Rounding = 20;
			this.btnCreateUser.StatePressed.Border.Width = 1;
			this.btnCreateUser.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StateTracking.Back.ColorAngle = 62F;
			this.btnCreateUser.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnCreateUser.StateTracking.Border.ColorAngle = 62F;
			this.btnCreateUser.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnCreateUser.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnCreateUser.StateTracking.Border.Rounding = 20;
			this.btnCreateUser.StateTracking.Border.Width = 1;
			this.btnCreateUser.TabIndex = 211;
			this.btnCreateUser.Values.Text = "UPDATE USER";
			this.btnCreateUser.Click += new System.EventHandler(this.btnUpdateUser_Click);
			// 
			// txtFullname
			// 
			this.txtFullname.Location = new System.Drawing.Point(786, 310);
			this.txtFullname.Margin = new System.Windows.Forms.Padding(4);
			this.txtFullname.Name = "txtFullname";
			this.txtFullname.Size = new System.Drawing.Size(285, 37);
			this.txtFullname.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtFullname.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtFullname.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtFullname.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtFullname.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtFullname.StateCommon.Border.Rounding = 20;
			this.txtFullname.StateCommon.Border.Width = 1;
			this.txtFullname.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtFullname.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFullname.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtFullname.TabIndex = 213;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(595, 320);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(110, 22);
			this.label7.TabIndex = 212;
			this.label7.Text = "FULLNAME:";
			// 
			// txtPhoneNum
			// 
			this.txtPhoneNum.Location = new System.Drawing.Point(788, 408);
			this.txtPhoneNum.Margin = new System.Windows.Forms.Padding(4);
			this.txtPhoneNum.Name = "txtPhoneNum";
			this.txtPhoneNum.Size = new System.Drawing.Size(285, 37);
			this.txtPhoneNum.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtPhoneNum.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtPhoneNum.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtPhoneNum.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtPhoneNum.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtPhoneNum.StateCommon.Border.Rounding = 20;
			this.txtPhoneNum.StateCommon.Border.Width = 1;
			this.txtPhoneNum.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtPhoneNum.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPhoneNum.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtPhoneNum.TabIndex = 215;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(597, 418);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(161, 22);
			this.label11.TabIndex = 214;
			this.label11.Text = "PHONE NUMBER:";
			// 
			// txtAddress
			// 
			this.txtAddress.Location = new System.Drawing.Point(787, 453);
			this.txtAddress.Margin = new System.Windows.Forms.Padding(4);
			this.txtAddress.Name = "txtAddress";
			this.txtAddress.Size = new System.Drawing.Size(285, 37);
			this.txtAddress.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtAddress.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtAddress.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtAddress.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtAddress.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtAddress.StateCommon.Border.Rounding = 20;
			this.txtAddress.StateCommon.Border.Width = 1;
			this.txtAddress.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtAddress.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtAddress.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtAddress.TabIndex = 217;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(596, 463);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(98, 22);
			this.label12.TabIndex = 216;
			this.label12.Text = "ADDRESS:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(596, 508);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(147, 22);
			this.label13.TabIndex = 218;
			this.label13.Text = "DATE OF BIRTH:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(594, 369);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(92, 22);
			this.label14.TabIndex = 221;
			this.label14.Text = "GENDER:";
			// 
			// GenderDropdown
			// 
			this.GenderDropdown.FormattingEnabled = true;
			this.GenderDropdown.Items.AddRange(new object[] {
            "MALE",
            "FEMALE"});
			this.GenderDropdown.Location = new System.Drawing.Point(786, 367);
			this.GenderDropdown.Name = "GenderDropdown";
			this.GenderDropdown.Size = new System.Drawing.Size(285, 24);
			this.GenderDropdown.TabIndex = 220;
			// 
			// dtpDOB
			// 
			this.dtpDOB.Location = new System.Drawing.Point(788, 505);
			this.dtpDOB.Name = "dtpDOB";
			this.dtpDOB.Size = new System.Drawing.Size(283, 22);
			this.dtpDOB.TabIndex = 222;
			// 
			// dgvUser
			// 
			this.dgvUser.AllowUserToAddRows = false;
			this.dgvUser.AllowUserToDeleteRows = false;
			this.dgvUser.AllowUserToOrderColumns = true;
			this.dgvUser.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
			this.dgvUser.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllHeaders;
			this.dgvUser.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgvUser.ImeMode = System.Windows.Forms.ImeMode.On;
			this.dgvUser.Location = new System.Drawing.Point(601, 557);
			this.dgvUser.Margin = new System.Windows.Forms.Padding(4);
			this.dgvUser.Name = "dgvUser";
			this.dgvUser.ReadOnly = true;
			this.dgvUser.RowHeadersWidth = 51;
			this.dgvUser.Size = new System.Drawing.Size(472, 169);
			this.dgvUser.StateCommon.Background.Color1 = System.Drawing.Color.White;
			this.dgvUser.StateCommon.Background.Color2 = System.Drawing.Color.White;
			this.dgvUser.StateCommon.Background.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.dgvUser.StateCommon.BackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
			this.dgvUser.StateCommon.DataCell.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.dgvUser.StateCommon.DataCell.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(252)))), ((int)(((byte)(252)))));
			this.dgvUser.StateCommon.DataCell.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.dgvUser.StateCommon.DataCell.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.dgvUser.StateCommon.DataCell.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.dgvUser.StateCommon.DataCell.Border.Width = 1;
			this.dgvUser.StateCommon.DataCell.Content.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvUser.StateCommon.HeaderColumn.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.dgvUser.StateCommon.HeaderColumn.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.dgvUser.StateCommon.HeaderColumn.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.dgvUser.StateCommon.HeaderColumn.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.dgvUser.StateCommon.HeaderColumn.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.dgvUser.StateCommon.HeaderColumn.Border.Width = 1;
			this.dgvUser.StateCommon.HeaderColumn.Content.Color1 = System.Drawing.Color.White;
			this.dgvUser.StateCommon.HeaderColumn.Content.Color2 = System.Drawing.Color.White;
			this.dgvUser.StateCommon.HeaderColumn.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.dgvUser.StateCommon.HeaderRow.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.dgvUser.StateCommon.HeaderRow.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.dgvUser.StateCommon.HeaderRow.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.dgvUser.StateCommon.HeaderRow.Border.Width = 1;
			this.dgvUser.StateSelected.DataCell.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
			this.dgvUser.StateSelected.DataCell.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
			this.dgvUser.TabIndex = 223;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(782, 634);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(94, 22);
			this.label4.TabIndex = 224;
			this.label4.Text = "LIST ROLE";
			// 
			// UpdateUser
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1323, 853);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.dgvUser);
			this.Controls.Add(this.dtpDOB);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.GenderDropdown);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.txtAddress);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.txtPhoneNum);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.txtFullname);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnCreateUser);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.RoleDropdown);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.pbLogout);
			this.Controls.Add(this.lbRoles);
			this.Controls.Add(this.pbSection);
			this.Controls.Add(this.lbProfile);
			this.Controls.Add(this.pbProfile);
			this.Controls.Add(this.lbUsers);
			this.Controls.Add(this.pbClasses);
			this.Controls.Add(this.lbPersonnel);
			this.Controls.Add(this.pbTeachers);
			this.Controls.Add(this.lbStudents);
			this.Controls.Add(this.pictureBox2);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtPassword);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtUsername);
			this.Controls.Add(this.label10);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "UpdateUser";
			this.Palette = this.kryptonPalette1;
			this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ADD USER";
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
			((System.ComponentModel.ISupportInitialize)(this.dgvUser)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Label label1;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPassword;
		private System.Windows.Forms.Label label2;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtUsername;
		private System.Windows.Forms.Label label10;
		private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
		private System.Windows.Forms.Label Dashboard;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label lbHello;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.PictureBox pbLogout;
		private System.Windows.Forms.Label lbRoles;
		private System.Windows.Forms.PictureBox pbSection;
		private System.Windows.Forms.Label lbProfile;
		private System.Windows.Forms.PictureBox pbProfile;
		private System.Windows.Forms.Label lbUsers;
		private System.Windows.Forms.PictureBox pbClasses;
		private System.Windows.Forms.Label lbPersonnel;
		private System.Windows.Forms.PictureBox pbTeachers;
		private System.Windows.Forms.Label lbStudents;
		private System.Windows.Forms.PictureBox pictureBox2;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.ComboBox RoleDropdown;
		private System.Windows.Forms.Label label3;
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnCreateUser;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtFullname;
		private Label label7;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPhoneNum;
		private Label label11;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtAddress;
		private Label label12;
		private Label label13;
		private Label label14;
		private ComboBox GenderDropdown;
		private DateTimePicker dtpDOB;
		private ComponentFactory.Krypton.Toolkit.KryptonDataGridView dgvUser;
		private Label label4;
	}
}