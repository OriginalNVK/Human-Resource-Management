
using System.Windows.Forms;

namespace SchoolManagement
{
    partial class UpdateSubject
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateSubject));
			this.label1 = new System.Windows.Forms.Label();
			this.txtCourseCode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txtSubjectCode = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.kryptonPalette1 = new ComponentFactory.Krypton.Toolkit.KryptonPalette(this.components);
			this.Dashboard = new System.Windows.Forms.Label();
			this.lbHello = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnUpdateSubject = new ComponentFactory.Krypton.Toolkit.KryptonButton();
			this.txtCourseName = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtCredits = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label11 = new System.Windows.Forms.Label();
			this.txtTheory = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.txtPractical = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
			this.endDay = new System.Windows.Forms.DateTimePicker();
			this.startDay = new System.Windows.Forms.DateTimePicker();
			this.label5 = new System.Windows.Forms.Label();
			this.pbSection = new System.Windows.Forms.PictureBox();
			this.lbProfile = new System.Windows.Forms.Label();
			this.pbProfile = new System.Windows.Forms.PictureBox();
			this.lbClasses = new System.Windows.Forms.Label();
			this.pbClasses = new System.Windows.Forms.PictureBox();
			this.lbTeachers = new System.Windows.Forms.Label();
			this.pbTeachers = new System.Windows.Forms.PictureBox();
			this.lbStudents = new System.Windows.Forms.Label();
			this.pbStudents = new System.Windows.Forms.PictureBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.label6 = new System.Windows.Forms.Label();
			this.pbGrade = new System.Windows.Forms.PictureBox();
			this.label9 = new System.Windows.Forms.Label();
			this.pbCalendar = new System.Windows.Forms.PictureBox();
			this.label15 = new System.Windows.Forms.Label();
			this.pbDetail = new System.Windows.Forms.PictureBox();
			this.label16 = new System.Windows.Forms.Label();
			this.logoutBtn = new System.Windows.Forms.Label();
			this.pbLogout = new System.Windows.Forms.PictureBox();
			this.facilityList = new System.Windows.Forms.ComboBox();
			this.teacherList = new System.Windows.Forms.ComboBox();
			this.notifications = new System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbSection)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbProfile)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbClasses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTeachers)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbStudents)).BeginInit();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbGrade)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCalendar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDetail)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogout)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.notifications)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(673, 133);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(342, 22);
			this.label1.TabIndex = 180;
			this.label1.Text = "----------SUBJECT INFORMATION----------";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtCourseCode
			// 
			this.txtCourseCode.Location = new System.Drawing.Point(787, 219);
			this.txtCourseCode.Margin = new System.Windows.Forms.Padding(4);
			this.txtCourseCode.Name = "txtCourseCode";
			this.txtCourseCode.Size = new System.Drawing.Size(285, 37);
			this.txtCourseCode.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtCourseCode.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtCourseCode.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtCourseCode.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtCourseCode.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtCourseCode.StateCommon.Border.Rounding = 20;
			this.txtCourseCode.StateCommon.Border.Width = 1;
			this.txtCourseCode.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtCourseCode.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCourseCode.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtCourseCode.TabIndex = 177;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label2.Location = new System.Drawing.Point(575, 229);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(151, 22);
			this.label2.TabIndex = 176;
			this.label2.Text = "COURSE CODE:";
			// 
			// txtSubjectCode
			// 
			this.txtSubjectCode.Location = new System.Drawing.Point(787, 171);
			this.txtSubjectCode.Margin = new System.Windows.Forms.Padding(4);
			this.txtSubjectCode.Name = "txtSubjectCode";
			this.txtSubjectCode.Size = new System.Drawing.Size(285, 37);
			this.txtSubjectCode.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtSubjectCode.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtSubjectCode.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtSubjectCode.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtSubjectCode.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtSubjectCode.StateCommon.Border.Rounding = 20;
			this.txtSubjectCode.StateCommon.Border.Width = 1;
			this.txtSubjectCode.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtSubjectCode.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSubjectCode.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtSubjectCode.TabIndex = 175;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label10.Location = new System.Drawing.Point(575, 181);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(150, 22);
			this.label10.TabIndex = 174;
			this.label10.Text = "SUBJECT CODE:";
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
			this.Dashboard.Location = new System.Drawing.Point(379, 34);
			this.Dashboard.Name = "Dashboard";
			this.Dashboard.Size = new System.Drawing.Size(226, 32);
			this.Dashboard.TabIndex = 186;
			this.Dashboard.Text = "UPDATE SUBJECT";
			// 
			// lbHello
			// 
			this.lbHello.AutoSize = true;
			this.lbHello.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbHello.ForeColor = System.Drawing.SystemColors.Control;
			this.lbHello.Location = new System.Drawing.Point(1151, 34);
			this.lbHello.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbHello.Name = "lbHello";
			this.lbHello.Size = new System.Drawing.Size(123, 22);
			this.lbHello.TabIndex = 185;
			this.lbHello.Text = "Hello, Admin";
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.SystemColors.Highlight;
			this.panel1.Controls.Add(this.notifications);
			this.panel1.Controls.Add(this.lbHello);
			this.panel1.Controls.Add(this.Dashboard);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1323, 100);
			this.panel1.TabIndex = 204;
			// 
			// btnUpdateSubject
			// 
			this.btnUpdateSubject.Location = new System.Drawing.Point(709, 693);
			this.btnUpdateSubject.Margin = new System.Windows.Forms.Padding(4);
			this.btnUpdateSubject.Name = "btnUpdateSubject";
			this.btnUpdateSubject.OverrideDefault.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.OverrideDefault.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.OverrideDefault.Back.ColorAngle = 62F;
			this.btnUpdateSubject.OverrideDefault.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.OverrideDefault.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.OverrideDefault.Border.ColorAngle = 62F;
			this.btnUpdateSubject.OverrideDefault.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnUpdateSubject.OverrideDefault.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnUpdateSubject.OverrideDefault.Border.Rounding = 20;
			this.btnUpdateSubject.OverrideDefault.Border.Width = 1;
			this.btnUpdateSubject.Size = new System.Drawing.Size(180, 53);
			this.btnUpdateSubject.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StateCommon.Back.ColorAngle = 62F;
			this.btnUpdateSubject.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StateCommon.Border.ColorAngle = 62F;
			this.btnUpdateSubject.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnUpdateSubject.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnUpdateSubject.StateCommon.Border.Rounding = 20;
			this.btnUpdateSubject.StateCommon.Border.Width = 1;
			this.btnUpdateSubject.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
			this.btnUpdateSubject.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
			this.btnUpdateSubject.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Century Gothic", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnUpdateSubject.StatePressed.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StatePressed.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StatePressed.Back.ColorAngle = 62F;
			this.btnUpdateSubject.StatePressed.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StatePressed.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StatePressed.Border.ColorAngle = 62F;
			this.btnUpdateSubject.StatePressed.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnUpdateSubject.StatePressed.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnUpdateSubject.StatePressed.Border.Rounding = 20;
			this.btnUpdateSubject.StatePressed.Border.Width = 1;
			this.btnUpdateSubject.StateTracking.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StateTracking.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StateTracking.Back.ColorAngle = 62F;
			this.btnUpdateSubject.StateTracking.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(142)))), ((int)(((byte)(197)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StateTracking.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(195)))), ((int)(((byte)(252)))));
			this.btnUpdateSubject.StateTracking.Border.ColorAngle = 62F;
			this.btnUpdateSubject.StateTracking.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.btnUpdateSubject.StateTracking.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.btnUpdateSubject.StateTracking.Border.Rounding = 20;
			this.btnUpdateSubject.StateTracking.Border.Width = 1;
			this.btnUpdateSubject.TabIndex = 211;
			this.btnUpdateSubject.Values.Text = "UPDATE";
			this.btnUpdateSubject.Click += new System.EventHandler(this.btnUpdateSubject_Click);
			// 
			// txtCourseName
			// 
			this.txtCourseName.Location = new System.Drawing.Point(787, 270);
			this.txtCourseName.Margin = new System.Windows.Forms.Padding(4);
			this.txtCourseName.Name = "txtCourseName";
			this.txtCourseName.Size = new System.Drawing.Size(285, 37);
			this.txtCourseName.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtCourseName.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtCourseName.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtCourseName.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtCourseName.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtCourseName.StateCommon.Border.Rounding = 20;
			this.txtCourseName.StateCommon.Border.Width = 1;
			this.txtCourseName.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtCourseName.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCourseName.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtCourseName.TabIndex = 213;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label7.Location = new System.Drawing.Point(575, 279);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(151, 22);
			this.label7.TabIndex = 212;
			this.label7.Text = "COURSE NAME:";
			// 
			// txtCredits
			// 
			this.txtCredits.Location = new System.Drawing.Point(788, 367);
			this.txtCredits.Margin = new System.Windows.Forms.Padding(4);
			this.txtCredits.Name = "txtCredits";
			this.txtCredits.Size = new System.Drawing.Size(285, 37);
			this.txtCredits.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtCredits.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtCredits.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtCredits.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtCredits.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtCredits.StateCommon.Border.Rounding = 20;
			this.txtCredits.StateCommon.Border.Width = 1;
			this.txtCredits.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtCredits.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCredits.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtCredits.TabIndex = 215;
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label11.Location = new System.Drawing.Point(575, 377);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(200, 22);
			this.label11.TabIndex = 214;
			this.label11.Text = "NUMBER OF CREDITS:";
			// 
			// txtTheory
			// 
			this.txtTheory.Location = new System.Drawing.Point(787, 412);
			this.txtTheory.Margin = new System.Windows.Forms.Padding(4);
			this.txtTheory.Name = "txtTheory";
			this.txtTheory.Size = new System.Drawing.Size(285, 37);
			this.txtTheory.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtTheory.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtTheory.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtTheory.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtTheory.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtTheory.StateCommon.Border.Rounding = 20;
			this.txtTheory.StateCommon.Border.Width = 1;
			this.txtTheory.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtTheory.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtTheory.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtTheory.TabIndex = 217;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label12.Location = new System.Drawing.Point(573, 422);
			this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(168, 22);
			this.label12.TabIndex = 216;
			this.label12.Text = "THEORY LESSONS:";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label13.Location = new System.Drawing.Point(577, 466);
			this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(202, 22);
			this.label13.TabIndex = 218;
			this.label13.Text = "PRACTICAL LESSONS:";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label14.Location = new System.Drawing.Point(573, 327);
			this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(92, 22);
			this.label14.TabIndex = 221;
			this.label14.Text = "FACILITY:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(576, 514);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(98, 22);
			this.label4.TabIndex = 225;
			this.label4.Text = "TEACHER:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label3.Location = new System.Drawing.Point(576, 562);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(112, 22);
			this.label3.TabIndex = 227;
			this.label3.Text = "DAY START:";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label8.Location = new System.Drawing.Point(579, 615);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(98, 22);
			this.label8.TabIndex = 229;
			this.label8.Text = "DAY END:";
			// 
			// txtPractical
			// 
			this.txtPractical.Location = new System.Drawing.Point(788, 459);
			this.txtPractical.Margin = new System.Windows.Forms.Padding(4);
			this.txtPractical.Name = "txtPractical";
			this.txtPractical.Size = new System.Drawing.Size(285, 37);
			this.txtPractical.StateCommon.Back.Color1 = System.Drawing.Color.White;
			this.txtPractical.StateCommon.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtPractical.StateCommon.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
			this.txtPractical.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
			this.txtPractical.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
			this.txtPractical.StateCommon.Border.Rounding = 20;
			this.txtPractical.StateCommon.Border.Width = 1;
			this.txtPractical.StateCommon.Content.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
			this.txtPractical.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtPractical.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
			this.txtPractical.TabIndex = 231;
			// 
			// endDay
			// 
			this.endDay.Location = new System.Drawing.Point(788, 615);
			this.endDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.endDay.Name = "endDay";
			this.endDay.Size = new System.Drawing.Size(285, 22);
			this.endDay.TabIndex = 234;
			// 
			// startDay
			// 
			this.startDay.Location = new System.Drawing.Point(788, 562);
			this.startDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.startDay.Name = "startDay";
			this.startDay.Size = new System.Drawing.Size(285, 22);
			this.startDay.TabIndex = 235;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.label5.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label5.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label5.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.label5.Location = new System.Drawing.Point(76, 229);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(103, 22);
			this.label5.TabIndex = 245;
			this.label5.Text = "CLASS LIST";
			// 
			// pbSection
			// 
			this.pbSection.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbSection.Image = ((System.Drawing.Image)(resources.GetObject("pbSection.Image")));
			this.pbSection.Location = new System.Drawing.Point(19, 212);
			this.pbSection.Margin = new System.Windows.Forms.Padding(4);
			this.pbSection.Name = "pbSection";
			this.pbSection.Size = new System.Drawing.Size(39, 50);
			this.pbSection.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbSection.TabIndex = 244;
			this.pbSection.TabStop = false;
			// 
			// lbProfile
			// 
			this.lbProfile.AutoSize = true;
			this.lbProfile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbProfile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbProfile.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbProfile.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbProfile.Location = new System.Drawing.Point(76, 427);
			this.lbProfile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbProfile.Name = "lbProfile";
			this.lbProfile.Size = new System.Drawing.Size(116, 22);
			this.lbProfile.TabIndex = 243;
			this.lbProfile.Text = "MY PROFILE";
			// 
			// pbProfile
			// 
			this.pbProfile.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbProfile.Image = ((System.Drawing.Image)(resources.GetObject("pbProfile.Image")));
			this.pbProfile.Location = new System.Drawing.Point(19, 406);
			this.pbProfile.Margin = new System.Windows.Forms.Padding(4);
			this.pbProfile.Name = "pbProfile";
			this.pbProfile.Size = new System.Drawing.Size(39, 54);
			this.pbProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbProfile.TabIndex = 242;
			this.pbProfile.TabStop = false;
			// 
			// lbClasses
			// 
			this.lbClasses.AutoSize = true;
			this.lbClasses.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbClasses.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbClasses.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbClasses.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbClasses.Location = new System.Drawing.Point(68, 160);
			this.lbClasses.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbClasses.Name = "lbClasses";
			this.lbClasses.Size = new System.Drawing.Size(226, 22);
			this.lbClasses.TabIndex = 241;
			this.lbClasses.Text = "SUBJECT MANAGEMENT";
			this.lbClasses.Click += new System.EventHandler(this.lbClasses_Click);
			// 
			// pbClasses
			// 
			this.pbClasses.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbClasses.Image = ((System.Drawing.Image)(resources.GetObject("pbClasses.Image")));
			this.pbClasses.Location = new System.Drawing.Point(19, 149);
			this.pbClasses.Margin = new System.Windows.Forms.Padding(4);
			this.pbClasses.Name = "pbClasses";
			this.pbClasses.Size = new System.Drawing.Size(39, 44);
			this.pbClasses.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbClasses.TabIndex = 240;
			this.pbClasses.TabStop = false;
			// 
			// lbTeachers
			// 
			this.lbTeachers.AutoSize = true;
			this.lbTeachers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbTeachers.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbTeachers.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbTeachers.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbTeachers.Location = new System.Drawing.Point(76, 356);
			this.lbTeachers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbTeachers.Name = "lbTeachers";
			this.lbTeachers.Size = new System.Drawing.Size(221, 22);
			this.lbTeachers.TabIndex = 239;
			this.lbTeachers.Text = "PERSONNEL MANAGER";
			// 
			// pbTeachers
			// 
			this.pbTeachers.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbTeachers.Image = ((System.Drawing.Image)(resources.GetObject("pbTeachers.Image")));
			this.pbTeachers.Location = new System.Drawing.Point(19, 336);
			this.pbTeachers.Margin = new System.Windows.Forms.Padding(4);
			this.pbTeachers.Name = "pbTeachers";
			this.pbTeachers.Size = new System.Drawing.Size(39, 54);
			this.pbTeachers.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbTeachers.TabIndex = 238;
			this.pbTeachers.TabStop = false;
			// 
			// lbStudents
			// 
			this.lbStudents.AutoSize = true;
			this.lbStudents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.lbStudents.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lbStudents.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lbStudents.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.lbStudents.Location = new System.Drawing.Point(76, 288);
			this.lbStudents.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbStudents.Name = "lbStudents";
			this.lbStudents.Size = new System.Drawing.Size(199, 22);
			this.lbStudents.TabIndex = 237;
			this.lbStudents.Text = "STUDENTS MANAGER";
			// 
			// pbStudents
			// 
			this.pbStudents.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbStudents.Image = ((System.Drawing.Image)(resources.GetObject("pbStudents.Image")));
			this.pbStudents.Location = new System.Drawing.Point(19, 271);
			this.pbStudents.Margin = new System.Windows.Forms.Padding(4);
			this.pbStudents.Name = "pbStudents";
			this.pbStudents.Size = new System.Drawing.Size(39, 50);
			this.pbStudents.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbStudents.TabIndex = 236;
			this.pbStudents.TabStop = false;
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.panel2.Controls.Add(this.label6);
			this.panel2.Controls.Add(this.pbGrade);
			this.panel2.Controls.Add(this.label9);
			this.panel2.Controls.Add(this.pbCalendar);
			this.panel2.Controls.Add(this.label15);
			this.panel2.Controls.Add(this.pbDetail);
			this.panel2.Controls.Add(this.label16);
			this.panel2.Controls.Add(this.logoutBtn);
			this.panel2.Controls.Add(this.pbLogout);
			this.panel2.Location = new System.Drawing.Point(0, -11);
			this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(339, 866);
			this.panel2.TabIndex = 247;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.label6.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label6.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label6.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.label6.Location = new System.Drawing.Point(76, 577);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(78, 22);
			this.label6.TabIndex = 195;
			this.label6.Text = "GRADE";
			// 
			// pbGrade
			// 
			this.pbGrade.BackColor = System.Drawing.Color.White;
			this.pbGrade.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbGrade.Image = ((System.Drawing.Image)(resources.GetObject("pbGrade.Image")));
			this.pbGrade.Location = new System.Drawing.Point(19, 565);
			this.pbGrade.Margin = new System.Windows.Forms.Padding(4);
			this.pbGrade.Name = "pbGrade";
			this.pbGrade.Size = new System.Drawing.Size(43, 46);
			this.pbGrade.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbGrade.TabIndex = 194;
			this.pbGrade.TabStop = false;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.label9.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label9.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label9.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.label9.Location = new System.Drawing.Point(76, 505);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(102, 22);
			this.label9.TabIndex = 193;
			this.label9.Text = "SCHEDULE";
			// 
			// pbCalendar
			// 
			this.pbCalendar.BackColor = System.Drawing.Color.White;
			this.pbCalendar.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbCalendar.Image = ((System.Drawing.Image)(resources.GetObject("pbCalendar.Image")));
			this.pbCalendar.Location = new System.Drawing.Point(19, 496);
			this.pbCalendar.Margin = new System.Windows.Forms.Padding(4);
			this.pbCalendar.Name = "pbCalendar";
			this.pbCalendar.Size = new System.Drawing.Size(43, 47);
			this.pbCalendar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbCalendar.TabIndex = 192;
			this.pbCalendar.TabStop = false;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.label15.Cursor = System.Windows.Forms.Cursors.Hand;
			this.label15.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label15.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.label15.Location = new System.Drawing.Point(76, 639);
			this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(235, 22);
			this.label15.TabIndex = 191;
			this.label15.Text = "REGISTER MANAGEMENT";
			// 
			// pbDetail
			// 
			this.pbDetail.BackColor = System.Drawing.Color.White;
			this.pbDetail.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbDetail.Image = ((System.Drawing.Image)(resources.GetObject("pbDetail.Image")));
			this.pbDetail.Location = new System.Drawing.Point(19, 625);
			this.pbDetail.Margin = new System.Windows.Forms.Padding(4);
			this.pbDetail.Name = "pbDetail";
			this.pbDetail.Size = new System.Drawing.Size(43, 49);
			this.pbDetail.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbDetail.TabIndex = 185;
			this.pbDetail.TabStop = false;
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Font = new System.Drawing.Font("Century Gothic", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label16.ForeColor = System.Drawing.Color.White;
			this.label16.Location = new System.Drawing.Point(3, 37);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(290, 80);
			this.label16.TabIndex = 184;
			this.label16.Text = "PERSONNEL \r\nSCHOOL PORTAL";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.label16.UseWaitCursor = true;
			// 
			// logoutBtn
			// 
			this.logoutBtn.AutoSize = true;
			this.logoutBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(73)))), ((int)(((byte)(94)))));
			this.logoutBtn.Cursor = System.Windows.Forms.Cursors.Hand;
			this.logoutBtn.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.logoutBtn.ForeColor = System.Drawing.SystemColors.ButtonFace;
			this.logoutBtn.Location = new System.Drawing.Point(76, 705);
			this.logoutBtn.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.logoutBtn.Name = "logoutBtn";
			this.logoutBtn.Size = new System.Drawing.Size(90, 22);
			this.logoutBtn.TabIndex = 20;
			this.logoutBtn.Text = "LOGOUT";
			this.logoutBtn.Click += new System.EventHandler(this.logoutBtn_Click);
			// 
			// pbLogout
			// 
			this.pbLogout.BackColor = System.Drawing.Color.White;
			this.pbLogout.Cursor = System.Windows.Forms.Cursors.Hand;
			this.pbLogout.Image = ((System.Drawing.Image)(resources.GetObject("pbLogout.Image")));
			this.pbLogout.Location = new System.Drawing.Point(19, 692);
			this.pbLogout.Margin = new System.Windows.Forms.Padding(4);
			this.pbLogout.Name = "pbLogout";
			this.pbLogout.Size = new System.Drawing.Size(39, 54);
			this.pbLogout.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbLogout.TabIndex = 19;
			this.pbLogout.TabStop = false;
			// 
			// facilityList
			// 
			this.facilityList.FormattingEnabled = true;
			this.facilityList.Location = new System.Drawing.Point(787, 326);
			this.facilityList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.facilityList.Name = "facilityList";
			this.facilityList.Size = new System.Drawing.Size(285, 24);
			this.facilityList.TabIndex = 248;
			// 
			// teacherList
			// 
			this.teacherList.FormattingEnabled = true;
			this.teacherList.Location = new System.Drawing.Point(787, 512);
			this.teacherList.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.teacherList.Name = "teacherList";
			this.teacherList.Size = new System.Drawing.Size(285, 24);
			this.teacherList.TabIndex = 249;
			// 
			// notifications
			// 
			this.notifications.Image = ((System.Drawing.Image)(resources.GetObject("notifications.Image")));
			this.notifications.Location = new System.Drawing.Point(1067, 16);
			this.notifications.Name = "notifications";
			this.notifications.Size = new System.Drawing.Size(63, 50);
			this.notifications.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.notifications.TabIndex = 212;
			this.notifications.TabStop = false;
			this.notifications.Click += new System.EventHandler(this.notifications_Click);
			// 
			// UpdateSubject
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1323, 846);
			this.Controls.Add(this.teacherList);
			this.Controls.Add(this.facilityList);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.pbSection);
			this.Controls.Add(this.lbProfile);
			this.Controls.Add(this.pbProfile);
			this.Controls.Add(this.lbClasses);
			this.Controls.Add(this.pbClasses);
			this.Controls.Add(this.lbTeachers);
			this.Controls.Add(this.pbTeachers);
			this.Controls.Add(this.lbStudents);
			this.Controls.Add(this.pbStudents);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.startDay);
			this.Controls.Add(this.endDay);
			this.Controls.Add(this.txtPractical);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.txtTheory);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.txtCredits);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.txtCourseName);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.btnUpdateSubject);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.txtCourseCode);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtSubjectCode);
			this.Controls.Add(this.label10);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "UpdateSubject";
			this.Palette = this.kryptonPalette1;
			this.PaletteMode = ComponentFactory.Krypton.Toolkit.PaletteMode.Custom;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ADD USER";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbSection)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbProfile)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbClasses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTeachers)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbStudents)).EndInit();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbGrade)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbCalendar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDetail)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogout)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.notifications)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCourseCode;
        private System.Windows.Forms.Label label2;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtSubjectCode;
        private System.Windows.Forms.Label label10;
        private ComponentFactory.Krypton.Toolkit.KryptonPalette kryptonPalette1;
		private System.Windows.Forms.Label Dashboard;
		private System.Windows.Forms.Label lbHello;
		private System.Windows.Forms.Panel panel1;
		private ComponentFactory.Krypton.Toolkit.KryptonButton btnUpdateSubject;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCourseName;
		private Label label7;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtCredits;
		private Label label11;
		private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtTheory;
		private Label label12;
		private Label label13;
		private Label label14;
		private Label label4;
        private Label label3;
        private Label label8;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPractical;
        private DateTimePicker endDay;
        private DateTimePicker startDay;
        private Label label5;
        private PictureBox pbSection;
        private Label lbProfile;
        private PictureBox pbProfile;
        private Label lbClasses;
        private PictureBox pbClasses;
        private Label lbTeachers;
        private PictureBox pbTeachers;
        private Label lbStudents;
        private PictureBox pbStudents;
        private Panel panel2;
        private Label label6;
        private PictureBox pbGrade;
        private Label label9;
        private PictureBox pbCalendar;
        private Label label15;
        private PictureBox pbDetail;
        private Label label16;
        private Label logoutBtn;
        private PictureBox pbLogout;
        private ComboBox facilityList;
        private ComboBox teacherList;
		private PictureBox notifications;
	}
}