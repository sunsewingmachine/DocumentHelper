﻿
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.Lvdb = new System.Windows.Forms.ListView();
            this.lstDBItems = new System.Windows.Forms.ListBox();
            this.Rt = new System.Windows.Forms.RichTextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.BtnDownload = new System.Windows.Forms.Button();
            this.lstItems = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BtnAdd = new System.Windows.Forms.Button();
            this.TbWeb = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.TbHs = new System.Windows.Forms.TextBox();
            this.BtnAddTypingToDb = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Tahoma", 14F);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.Padding = new System.Drawing.Point(0, 0);
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1208, 654);
            this.tabControl1.TabIndex = 5;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Silver;
            this.tabPage1.Controls.Add(this.Lvdb);
            this.tabPage1.Controls.Add(this.lstDBItems);
            this.tabPage1.Controls.Add(this.Rt);
            this.tabPage1.Location = new System.Drawing.Point(4, 32);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1200, 618);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Typing   ";
            // 
            // Lvdb
            // 
            this.Lvdb.HideSelection = false;
            this.Lvdb.Location = new System.Drawing.Point(808, 11);
            this.Lvdb.Name = "Lvdb";
            this.Lvdb.Size = new System.Drawing.Size(386, 601);
            this.Lvdb.TabIndex = 10;
            this.Lvdb.UseCompatibleStateImageBehavior = false;
            // 
            // lstDBItems
            // 
            this.lstDBItems.FormattingEnabled = true;
            this.lstDBItems.ItemHeight = 23;
            this.lstDBItems.Location = new System.Drawing.Point(808, 11);
            this.lstDBItems.Name = "lstDBItems";
            this.lstDBItems.Size = new System.Drawing.Size(386, 602);
            this.lstDBItems.TabIndex = 9;
            // 
            // Rt
            // 
            this.Rt.Location = new System.Drawing.Point(8, 8);
            this.Rt.Name = "Rt";
            this.Rt.Size = new System.Drawing.Size(793, 603);
            this.Rt.TabIndex = 8;
            this.Rt.TabStop = false;
            this.Rt.Text = "";
            this.Rt.TextChanged += new System.EventHandler(this.Rt_TextChanged);
            this.Rt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Rt_KeyDown);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.BtnAddTypingToDb);
            this.tabPage2.Controls.Add(this.BtnDownload);
            this.tabPage2.Controls.Add(this.lstItems);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.BtnAdd);
            this.tabPage2.Controls.Add(this.TbWeb);
            this.tabPage2.Location = new System.Drawing.Point(4, 32);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1200, 618);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Database   ";
            // 
            // BtnDownload
            // 
            this.BtnDownload.Font = new System.Drawing.Font("Tahoma", 12F);
            this.BtnDownload.Location = new System.Drawing.Point(449, 119);
            this.BtnDownload.Name = "BtnDownload";
            this.BtnDownload.Size = new System.Drawing.Size(225, 40);
            this.BtnDownload.TabIndex = 10;
            this.BtnDownload.Text = "Download to Listbox";
            this.BtnDownload.UseVisualStyleBackColor = true;
            this.BtnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // lstItems
            // 
            this.lstItems.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lstItems.FormattingEnabled = true;
            this.lstItems.ItemHeight = 16;
            this.lstItems.Location = new System.Drawing.Point(30, 193);
            this.lstItems.Name = "lstItems";
            this.lstItems.Size = new System.Drawing.Size(1164, 340);
            this.lstItems.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 147);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(156, 23);
            this.label2.TabIndex = 8;
            this.label2.Text = "Text to add to db";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 23);
            this.label1.TabIndex = 6;
            this.label1.Text = "Website/Webpage link";
            // 
            // BtnAdd
            // 
            this.BtnAdd.Font = new System.Drawing.Font("Tahoma", 12F);
            this.BtnAdd.Location = new System.Drawing.Point(1023, 539);
            this.BtnAdd.Name = "BtnAdd";
            this.BtnAdd.Size = new System.Drawing.Size(171, 40);
            this.BtnAdd.TabIndex = 5;
            this.BtnAdd.Text = "Add List items To Db";
            this.BtnAdd.UseVisualStyleBackColor = true;
            this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
            // 
            // TbWeb
            // 
            this.TbWeb.Location = new System.Drawing.Point(280, 83);
            this.TbWeb.Name = "TbWeb";
            this.TbWeb.Size = new System.Drawing.Size(394, 30);
            this.TbWeb.TabIndex = 4;
            this.TbWeb.Text = "https://indiabeeps.com/allprojects/del/addtexttodb/test5.html";
            this.TbWeb.TextChanged += new System.EventHandler(this.TbWeb_TextChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.Silver;
            this.tabPage3.Controls.Add(this.label4);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Controls.Add(this.TbHs);
            this.tabPage3.Location = new System.Drawing.Point(4, 32);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1200, 618);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "HotStrings   ";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Tahoma", 9F);
            this.label4.Location = new System.Drawing.Point(180, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(398, 66);
            this.label4.TabIndex = 11;
            this.label4.Text = "Examples:\r\nggc - google cloud\r\nprt   - please read this\r\n(put each in a new line," +
    " with hypen - as delimiter)\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(134, 23);
            this.label3.TabIndex = 10;
            this.label3.Text = "Add hotStrings";
            // 
            // TbHs
            // 
            this.TbHs.AcceptsReturn = true;
            this.TbHs.AcceptsTab = true;
            this.TbHs.Font = new System.Drawing.Font("Tahoma", 12F);
            this.TbHs.Location = new System.Drawing.Point(28, 72);
            this.TbHs.Multiline = true;
            this.TbHs.Name = "TbHs";
            this.TbHs.Size = new System.Drawing.Size(1152, 540);
            this.TbHs.TabIndex = 9;
            // 
            // BtnAddTypingToDb
            // 
            this.BtnAddTypingToDb.Font = new System.Drawing.Font("Tahoma", 12F);
            this.BtnAddTypingToDb.Location = new System.Drawing.Point(719, 119);
            this.BtnAddTypingToDb.Name = "BtnAddTypingToDb";
            this.BtnAddTypingToDb.Size = new System.Drawing.Size(200, 40);
            this.BtnAddTypingToDb.TabIndex = 11;
            this.BtnAddTypingToDb.Text = "Add Typing To ListBox";
            this.BtnAddTypingToDb.UseVisualStyleBackColor = true;
            this.BtnAddTypingToDb.Click += new System.EventHandler(this.BtnAddTypingToDb_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(680, 127);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 23);
            this.label5.TabIndex = 12;
            this.label5.Text = "Or";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(1211, 660);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Tahoma", 10F);
            this.Name = "Form1";
            this.Text = "Document Typing Helper";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox Rt;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button BtnAdd;
        private System.Windows.Forms.TextBox TbWeb;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox TbHs;
        private System.Windows.Forms.ListBox lstItems;
        private System.Windows.Forms.ListBox lstDBItems;
        private System.Windows.Forms.ListView Lvdb;
        private System.Windows.Forms.Button BtnDownload;
        private System.Windows.Forms.Button BtnAddTypingToDb;
        private System.Windows.Forms.Label label5;
    }
}

