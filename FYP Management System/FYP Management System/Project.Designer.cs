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
            this.SearchText = new System.Windows.Forms.TextBox();
            this.Create = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.ProjectData)).BeginInit();
            this.SuspendLayout();
            // 
            // ProjectData
            // 
            this.ProjectData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ProjectData.Location = new System.Drawing.Point(30, 134);
            this.ProjectData.Name = "ProjectData";
            this.ProjectData.Size = new System.Drawing.Size(545, 265);
            this.ProjectData.TabIndex = 0;
            this.ProjectData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProjectData_CellContentClick);
            this.ProjectData.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.ProjectData_CellContentClick);
            // 
            // search
            // 
            this.search.AutoSize = true;
            this.search.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.search.Location = new System.Drawing.Point(26, 76);
            this.search.Name = "search";
            this.search.Size = new System.Drawing.Size(60, 20);
            this.search.TabIndex = 1;
            this.search.Text = "Search";
            this.search.Click += new System.EventHandler(this.label1_Click);
            // 
            // SearchText
            // 
            this.SearchText.Location = new System.Drawing.Point(118, 76);
            this.SearchText.Name = "SearchText";
            this.SearchText.Size = new System.Drawing.Size(179, 20);
            this.SearchText.TabIndex = 2;
            this.SearchText.TextChanged += new System.EventHandler(this.SearchText_TextChanged);
            // 
            // Create
            // 
            this.Create.Location = new System.Drawing.Point(470, 73);
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
            this.ClientSize = new System.Drawing.Size(617, 440);
            this.Controls.Add(this.Create);
            this.Controls.Add(this.SearchText);
            this.Controls.Add(this.search);
            this.Controls.Add(this.ProjectData);
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
        private System.Windows.Forms.TextBox SearchText;
        private System.Windows.Forms.Button Create;
    }
}

