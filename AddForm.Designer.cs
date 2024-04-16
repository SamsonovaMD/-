
namespace Planner_with_postgresql
{
    partial class AddForm
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
            this.DataName = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.CatigoryName = new System.Windows.Forms.TextBox();
            this.TaskName = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.Close_button = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PeachPuff;
            this.panel1.Controls.Add(this.DataName);
            this.panel1.Controls.Add(this.buttonAdd);
            this.panel1.Controls.Add(this.CatigoryName);
            this.panel1.Controls.Add(this.TaskName);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(445, 197);
            this.panel1.TabIndex = 1;
            this.panel1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseDown);
            this.panel1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseMove);
            // 
            // DataName
            // 
            this.DataName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.DataName.Location = new System.Drawing.Point(293, 91);
            this.DataName.Multiline = true;
            this.DataName.Name = "DataName";
            this.DataName.Size = new System.Drawing.Size(123, 26);
            this.DataName.TabIndex = 7;
            this.DataName.Enter += new System.EventHandler(this.DataName_Enter);
            this.DataName.Leave += new System.EventHandler(this.DataName_Leave);
            // 
            // buttonAdd
            // 
            this.buttonAdd.BackColor = System.Drawing.Color.SaddleBrown;
            this.buttonAdd.FlatAppearance.BorderSize = 0;
            this.buttonAdd.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Brown;
            this.buttonAdd.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Brown;
            this.buttonAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAdd.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonAdd.ForeColor = System.Drawing.Color.PeachPuff;
            this.buttonAdd.Location = new System.Drawing.Point(157, 140);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(122, 32);
            this.buttonAdd.TabIndex = 6;
            this.buttonAdd.Text = "Добавить";
            this.buttonAdd.UseVisualStyleBackColor = false;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // CatigoryName
            // 
            this.CatigoryName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.CatigoryName.Location = new System.Drawing.Point(157, 91);
            this.CatigoryName.Multiline = true;
            this.CatigoryName.Name = "CatigoryName";
            this.CatigoryName.Size = new System.Drawing.Size(130, 26);
            this.CatigoryName.TabIndex = 5;
            this.CatigoryName.UseSystemPasswordChar = true;
            this.CatigoryName.Enter += new System.EventHandler(this.CatigoryName_Enter);
            this.CatigoryName.Leave += new System.EventHandler(this.CatigoryName_Leave);
            // 
            // TaskName
            // 
            this.TaskName.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.TaskName.Location = new System.Drawing.Point(28, 91);
            this.TaskName.Multiline = true;
            this.TaskName.Name = "TaskName";
            this.TaskName.Size = new System.Drawing.Size(123, 26);
            this.TaskName.TabIndex = 3;
            this.TaskName.Enter += new System.EventHandler(this.TaskName_Enter);
            this.TaskName.Leave += new System.EventHandler(this.TaskName_Leave);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.SandyBrown;
            this.panel2.Controls.Add(this.Close_button);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Comic Sans MS", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(445, 59);
            this.panel2.TabIndex = 0;
            // 
            // Close_button
            // 
            this.Close_button.AutoSize = true;
            this.Close_button.BackColor = System.Drawing.Color.SaddleBrown;
            this.Close_button.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Close_button.Font = new System.Drawing.Font("Comic Sans MS", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Close_button.ForeColor = System.Drawing.Color.White;
            this.Close_button.Location = new System.Drawing.Point(413, -11);
            this.Close_button.Name = "Close_button";
            this.Close_button.Size = new System.Drawing.Size(29, 35);
            this.Close_button.TabIndex = 1;
            this.Close_button.Text = "x";
            this.Close_button.Click += new System.EventHandler(this.Close_button_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.SaddleBrown;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.ForeColor = System.Drawing.Color.PeachPuff;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(445, 59);
            this.label1.TabIndex = 0;
            this.label1.Text = "Добавить задачу";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // AddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(445, 197);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AddForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddForm";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox CatigoryName;
        private System.Windows.Forms.TextBox TaskName;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label Close_button;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox DataName;
    }
}