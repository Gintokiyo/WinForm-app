
namespace ProiectMediiVizuale
{
    partial class Entrance
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
            this.buttonMembers = new System.Windows.Forms.Button();
            this.buttonTeams = new System.Windows.Forms.Button();
            this.buttonRoles = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonMembers
            // 
            this.buttonMembers.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonMembers.Location = new System.Drawing.Point(67, 180);
            this.buttonMembers.Name = "buttonMembers";
            this.buttonMembers.Size = new System.Drawing.Size(116, 63);
            this.buttonMembers.TabIndex = 0;
            this.buttonMembers.Text = "MemberPlace";
            this.buttonMembers.UseVisualStyleBackColor = true;
            this.buttonMembers.Click += new System.EventHandler(this.buttonMembers_Click);
            // 
            // buttonTeams
            // 
            this.buttonTeams.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonTeams.Location = new System.Drawing.Point(200, 180);
            this.buttonTeams.Name = "buttonTeams";
            this.buttonTeams.Size = new System.Drawing.Size(116, 63);
            this.buttonTeams.TabIndex = 1;
            this.buttonTeams.Text = "TeamPlace";
            this.buttonTeams.UseVisualStyleBackColor = true;
            this.buttonTeams.Click += new System.EventHandler(this.buttonTeams_Click);
            // 
            // buttonRoles
            // 
            this.buttonRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonRoles.Location = new System.Drawing.Point(333, 180);
            this.buttonRoles.Name = "buttonRoles";
            this.buttonRoles.Size = new System.Drawing.Size(116, 63);
            this.buttonRoles.TabIndex = 2;
            this.buttonRoles.Text = "RolePlace";
            this.buttonRoles.UseVisualStyleBackColor = true;
            this.buttonRoles.Click += new System.EventHandler(this.buttonRoles_Click);
            // 
            // Entrance
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(533, 267);
            this.Controls.Add(this.buttonRoles);
            this.Controls.Add(this.buttonTeams);
            this.Controls.Add(this.buttonMembers);
            this.MaximumSize = new System.Drawing.Size(549, 306);
            this.MinimumSize = new System.Drawing.Size(549, 306);
            this.Name = "Entrance";
            this.Text = "Database";
            this.Load += new System.EventHandler(this.Entrance_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonMembers;
        private System.Windows.Forms.Button buttonTeams;
        private System.Windows.Forms.Button buttonRoles;
    }
}