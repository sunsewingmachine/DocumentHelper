using Shared;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

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
            LoadData(string.Empty);
        }
        private void LoadData(string text)
        {
            Connection connection = new Connection();
            var table = connection.SelectQuery(string.Format("select * from Document where Text like '{0}%';", text));
            lstDBItems.Items.Clear();
            foreach (DataRow item in table.Rows)
            {
                lstDBItems.Items.Add(item["Text"].ToString());
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

                var list = doc.DocumentNode.InnerText.Replace("\n", "").Split(' ').Where(o => o.Length > 1);
                foreach (var item in list)
                {
                    if (!HasSpecialChar(item))
                    {
                        lstItems.Items.Add(item);
                    }
                }
            }

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Connection connection = new Connection();
            foreach (var item in lstItems.Items)
            {
                if (!HasSpecialChar(item.ToString()) && !HasTextBefore(item.ToString()))
                {
                    connection.CreateDocumnet(item.ToString());
                }
            }
            LoadData(string.Empty);
        }
        public bool HasTextBefore(string text)
        {
            Connection connection = new Connection();
            var table = connection.SelectQuery(string.Format("select * from Document where Text = '{0}';", text));
            return table.Rows.Count > 0;
        }
        public bool HasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item) || input.Contains("\r")) return true;
            }

            return false;
        }

        private void Rt_TextChanged(object sender, EventArgs e)
        {
            string text = ((RichTextBox)sender).Text;
            var lastItem = text.Split(' ').Last();
            LoadData(lastItem);
        }

        private void Rt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Enter))
            {
                e.SuppressKeyPress = true;
                if (Rt.Text.LastIndexOf(' ') > 0)
                {
                    Rt.Text = Rt.Text.Substring(0, Rt.Text.LastIndexOf(' ') ) + " " + (lstDBItems.SelectedItem ?? lstDBItems.Items[0].ToString());
                }
                else
                {
                    Rt.Text = (lstDBItems.SelectedItem ?? lstDBItems.Items[0].ToString()).ToString();
                }
                Rt.SelectionStart = Rt.Text.Length;
            }
        }
    }
}
