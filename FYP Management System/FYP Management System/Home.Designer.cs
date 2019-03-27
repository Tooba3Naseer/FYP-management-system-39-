namespace FYP_Management_System
{
    partial class Home
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.buttonSg = new System.Windows.Forms.Button();
            this.MevalButton = new System.Windows.Forms.Button();
            this.Mprojects = new System.Windows.Forms.Button();
            this.Madvisors = new System.Windows.Forms.Button();
            this.Mstudents = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(798, 53);
            this.panel1.TabIndex = 22;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(220, 29);
            this.label1.TabIndex = 5;
            this.label1.Text = "FYP Management";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.SystemColors.Info;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.Mprojects, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.Madvisors, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.Mstudents, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.button1, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.MevalButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.buttonSg, 0, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 94);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(766, 316);
            this.tableLayoutPanel1.TabIndex = 23;
            // 
            // buttonSg
            // 
            this.buttonSg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.buttonSg.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.buttonSg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonSg.Location = new System.Drawing.Point(85, 231);
            this.buttonSg.Name = "buttonSg";
            this.buttonSg.Size = new System.Drawing.Size(213, 69);
            this.buttonSg.TabIndex = 4;
            this.buttonSg.Text = "Formation of Student Group and its management";
            this.buttonSg.UseVisualStyleBackColor = false;
            this.buttonSg.Click += new System.EventHandler(this.buttonSg_Click);
            // 
            // MevalButton
            // 
            this.MevalButton.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.MevalButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.MevalButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MevalButton.Location = new System.Drawing.Point(468, 130);
            this.MevalButton.Name = "MevalButton";
            this.MevalButton.Size = new System.Drawing.Size(213, 64);
            this.MevalButton.TabIndex = 3;
            this.MevalButton.Text = "Manage Evaluations";
            this.MevalButton.UseVisualStyleBackColor = false;
            this.MevalButton.Click += new System.EventHandler(this.MevalButton_Click);
            // 
            // Mprojects
            // 
            this.Mprojects.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Mprojects.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Mprojects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mprojects.Location = new System.Drawing.Point(85, 130);
            this.Mprojects.Name = "Mprojects";
            this.Mprojects.Size = new System.Drawing.Size(213, 64);
            this.Mprojects.TabIndex = 2;
            this.Mprojects.Text = "Manage Projects";
            this.Mprojects.UseVisualStyleBackColor = false;
            this.Mprojects.Click += new System.EventHandler(this.Mprojects_Click);
            // 
            // Madvisors
            // 
            this.Madvisors.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Madvisors.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Madvisors.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Madvisors.Location = new System.Drawing.Point(468, 22);
            this.Madvisors.Name = "Madvisors";
            this.Madvisors.Size = new System.Drawing.Size(213, 64);
            this.Madvisors.TabIndex = 1;
            this.Madvisors.Text = "Manage Advisors";
            this.Madvisors.UseVisualStyleBackColor = false;
            this.Madvisors.Click += new System.EventHandler(this.Madvisors_Click);
            // 
            // Mstudents
            // 
            this.Mstudents.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.Mstudents.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.Mstudents.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Mstudents.Location = new System.Drawing.Point(85, 22);
            this.Mstudents.Name = "Mstudents";
            this.Mstudents.Size = new System.Drawing.Size(213, 64);
            this.Mstudents.TabIndex = 0;
            this.Mstudents.Text = "Manage Students";
            this.Mstudents.UseVisualStyleBackColor = false;
            this.Mstudents.Click += new System.EventHandler(this.Mstudents_Click);
            // 
            // button1
            // 
            this.button1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.button1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(468, 234);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(213, 64);
            this.button1.TabIndex = 5;
            this.button1.Text = "Assignment of multiple advisors to the project \r\n";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Home
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel1);
            this.Name = "Home";
            this.Text = "Home";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button Mstudents;
        private System.Windows.Forms.Button MevalButton;
        private System.Windows.Forms.Button Mprojects;
        private System.Windows.Forms.Button Madvisors;
        private System.Windows.Forms.Button buttonSg;
        private System.Windows.Forms.Button button1;
    }
}