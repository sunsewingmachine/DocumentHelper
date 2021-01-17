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
            DocumentEntity documentEntity = new DocumentEntity();
            var table = documentEntity.SelectQuery("select * from Document;");
            foreach (DataRow item in table.Rows)
            {
                Lv.Items.Add(new ListViewItem() { Text = item["Text"].ToString() });
            }
        }

        private void TbWeb_TextChanged(object sender, EventArgs e)
        {
            //var webRequest = WebRequest.Create(TbWeb.Text);

            //using (var response = webRequest.GetResponse())
            //using (var content = response.GetResponseStream())
            //using (var reader = new StreamReader(content))
            //{
            //    var strContent = reader.ReadToEnd();
            //}

            using (WebClient client = new WebClient())
            {
                string htmlCode = client.DownloadString(TbWeb.Text);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlCode);

                // InnerText 
                TbAddToDB.Text = doc.DocumentNode.InnerText;
            }

        }
    }
}
