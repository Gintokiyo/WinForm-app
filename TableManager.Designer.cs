
namespace ProiectMediiVizuale
{
    partial class TableManager
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
            this.groupBoxTeamManager = new System.Windows.Forms.GroupBox();
            this.buttonErrorWindow = new System.Windows.Forms.Button();
            this.buttonDeleteField = new System.Windows.Forms.Button();
            this.buttonAddField = new System.Windows.Forms.Button();
            this.dataGridViewTable = new System.Windows.Forms.DataGridView();
            this.groupBoxTeamManager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBoxTeamManager
            // 
            this.groupBoxTeamManager.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBoxTeamManager.Controls.Add(this.buttonErrorWindow);
            this.groupBoxTeamManager.Controls.Add(this.buttonDeleteField);
            this.groupBoxTeamManager.Controls.Add(this.buttonAddField);
            this.groupBoxTeamManager.Controls.Add(this.dataGridViewTable);
            this.groupBoxTeamManager.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBoxTeamManager.Location = new System.Drawing.Point(12, 12);
            this.groupBoxTeamManager.Name = "groupBoxTeamManager";
            this.groupBoxTeamManager.Size = new System.Drawing.Size(829, 585);
            this.groupBoxTeamManager.TabIndex = 1;
            this.groupBoxTeamManager.TabStop = false;
            this.groupBoxTeamManager.Text = "TableNameHolder";
            // 
            // buttonErrorWindow
            // 
            this.buttonErrorWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonErrorWindow.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonErrorWindow.Location = new System.Drawing.Point(753, 0);
            this.buttonErrorWindow.Name = "buttonErrorWindow";
            this.buttonErrorWindow.Size = new System.Drawing.Size(76, 20);
            this.buttonErrorWindow.TabIndex = 10;
            this.buttonErrorWindow.Text = "Error Viewer";
            this.buttonErrorWindow.UseVisualStyleBackColor = true;
            this.buttonErrorWindow.Click += new System.EventHandler(this.buttonErrorWindow_Click);
            // 
            // buttonDeleteField
            // 
            this.buttonDeleteField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDeleteField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonDeleteField.Location = new System.Drawing.Point(169, 529);
            this.buttonDeleteField.Name = "buttonDeleteField";
            this.buttonDeleteField.Size = new System.Drawing.Size(140, 37);
            this.buttonDeleteField.TabIndex = 3;
            this.buttonDeleteField.Text = "Delete";
            this.buttonDeleteField.UseVisualStyleBackColor = true;
            this.buttonDeleteField.Click += new System.EventHandler(this.buttonDeleteObject_Click);
            // 
            // buttonAddField
            // 
            this.buttonAddField.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAddField.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonAddField.Location = new System.Drawing.Point(10, 529);
            this.buttonAddField.Name = "buttonAddField";
            this.buttonAddField.Size = new System.Drawing.Size(140, 37);
            this.buttonAddField.TabIndex = 2;
            this.buttonAddField.Text = "Add";
            this.buttonAddField.UseVisualStyleBackColor = true;
            this.buttonAddField.Click += new System.EventHandler(this.buttonAddObject_Click);
            // 
            // dataGridViewTable
            // 
            this.dataGridViewTable.AllowUserToAddRows = false;
            this.dataGridViewTable.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridViewTable.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewTable.Location = new System.Drawing.Point(10, 28);
            this.dataGridViewTable.Name = "dataGridViewTable";
            this.dataGridViewTable.ReadOnly = true;
            this.dataGridViewTable.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridViewTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewTable.Size = new System.Drawing.Size(813, 479);
            this.dataGridViewTable.TabIndex = 1;
            this.dataGridViewTable.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewTable_CellDoubleClick);
            // 
            // MainManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(853, 609);
            this.Controls.Add(this.groupBoxTeamManager);
            this.MaximumSize = new System.Drawing.Size(869, 648);
            this.MinimumSize = new System.Drawing.Size(869, 648);
            this.Name = "MainManager";
            this.Text = "Table Edit";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBoxTeamManager.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewTable)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBoxTeamManager;
        private System.Windows.Forms.DataGridView dataGridViewTable;
        private System.Windows.Forms.Button buttonDeleteField;
        private System.Windows.Forms.Button buttonAddField;
        private System.Windows.Forms.Button buttonErrorWindow;
    }
}

