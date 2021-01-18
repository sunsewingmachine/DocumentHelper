using Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DocumentHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Connection connection = new Connection();
            var table = connection.SelectQuery("select * from Document;");
            foreach (DataRow item in table.Rows)
            {
                Lv.Items.Add(new ListViewItem() { Text = item["Text"].ToString() });
            }
        }

        private void TbWeb_TextChanged(object sender, EventArgs e)
        {
            Connection connection = new Connection();
            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(TbWeb.Text);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlCode);

                var text = "";
                var list = doc.DocumentNode.InnerText.Replace("\n", "").Split(' ').Where(o => o.Length > 1);
                foreach (var item in list)
                {
                    // InnerText 
                    text += Environment.NewLine + item;
                    connection.CreateDocumnet(item);
                }
                TbAddToDB.Text = text;
            }

        }
    }
}
