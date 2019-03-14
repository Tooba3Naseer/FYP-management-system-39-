namespace FYP_Management_System
{
    partial class Project
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
            this.ProjectData = new System.Windows.Forms.DataGridView();
            this.search = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SearchText = new System.Windows.Forms.TextBox();
            this.Create = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectData)).BeginInit();
            this.SuspendLayout();
            // 
            // ProjectData
            // 
            this.ProjectData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProjectData.Location = new System.Drawing.Point(65, 186);
            this.ProjectData.Name = "ProjectData";
            this.ProjectData.Size = new System.Drawing.Size(713, 252);
            this.ProjectData.TabIndex = 0;
            this.ProjectData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProjectData_CellContentClick);
            this.ProjectData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProjectData_CellContentClick);
            // 
            // search
            // 
            this.search.AutoSize = true;
            this.search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.Location = new System.Drawing.Point(70, 128);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(60, 20);
            this.search.TabIndex = 1;
            this.search.Text = "Search";
            this.search.Click += new System.EventHandler(this.label1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(280, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(233, 29);
            this.label1.TabIndex = 4;
            this.label1.Text = "Project Information";
            // 
            // SearchText
            // 
            this.SearchText.Location = new System.Drawing.Point(192, 127);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(179, 20);
            this.SearchText.TabIndex = 2;
            this.SearchText.TextChanged += new System.EventHandler(this.SearchText_TextChanged);
            // 
            // Create
            // 
            this.Create.Location = new System.Drawing.Point(673, 125);
            this.Create.Name = "Create";
            this.Create.Size = new System.Drawing.Size(105, 23);
            this.Create.TabIndex = 3;
            this.Create.Text = "Create";
            this.Create.UseVisualStyleBackColor = true;
            this.Create.Click += new System.EventHandler(this.Create_Click);
            // 
            // Project
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.ClientSize = new System.Drawing.Size(866, 506);
            this.Controls.Add(this.Create);
            this.Controls.Add(this.SearchText);
            this.Controls.Add(this.ProjectData);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.search);
            this.Name = "Project";
            this.Text = "project";
            this.Load += new System.EventHandler(this.Project_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ProjectData)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView ProjectData;
        private System.Windows.Forms.Label search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox SearchText;
        private System.Windows.Forms.Button Create;
    }
}

