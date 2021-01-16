
namespace DocumentHelper
{
    partial class Form1
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
            this.Tb = new System.Windows.Forms.TextBox();
            this.Lv = new System.Windows.Forms.ListView();
            this.TbWeb = new System.Windows.Forms.TextBox();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tb
            // 
            this.Tb.AcceptsReturn = true;
            this.Tb.AcceptsTab = true;
            this.Tb.Font = new System.Drawing.Font("Tahoma", 22F);
            this.Tb.HideSelection = false;
            this.Tb.Location = new System.Drawing.Point(12, 12);
            this.Tb.Multiline = true;
            this.Tb.Name = "Tb";
            this.Tb.Size = new System.Drawing.Size(666, 528);
            this.Tb.TabIndex = 0;
            // 
            // Lv
            // 
            this.Lv.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Lv.HideSelection = false;
            this.Lv.Location = new System.Drawing.Point(684, 12);
            this.Lv.Name = "Lv";
            this.Lv.Size = new System.Drawing.Size(341, 387);
            this.Lv.TabIndex = 1;
            this.Lv.UseCompatibleStateImageBehavior = false;
            // 
            // TbWeb
            // 
            this.TbWeb.Location = new System.Drawing.Point(686, 481);
            this.TbWeb.Name = "TbWeb";
            this.TbWeb.Size = new System.Drawing.Size(339, 20);
            this.TbWeb.TabIndex = 2;
            // 
            // BtnAdd
            // 
            this.BtnAdd.Font = new System.Drawing.Font("Tahoma", 12F);
            this.BtnAdd.Location = new System.Drawing.Point(895, 507);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(128, 33);
            this.BtnAdd.TabIndex = 3;
            this.BtnAdd.Text = "Add To Db";
            this.BtnAdd.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1035, 551);
            this.Controls.Add(this.BtnAdd);
            this.Controls.Add(this.TbWeb);
            this.Controls.Add(this.Lv);
            this.Controls.Add(this.Tb);
            this.Name = "Form1";
            this.Text = "Document Typing Helper";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox Tb;
        private System.Windows.Forms.ListView Lv;
        private System.Windows.Forms.TextBox TbWeb;
        private System.Windows.Forms.Button BtnAdd;
    }
}

