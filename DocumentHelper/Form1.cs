using Shared;
using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Shared.Tools;

namespace DocumentHelper
{
    public partial class Form1 : Form
    {

        public static bool UseListBox;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (UseListBox)
            {
                lstDBItems.Visible = true;
                Lvdb.Visible = false;
            }
            else
            {
                lstDBItems.Visible = false;
                Lvdb.Visible = true;

                Lvdb.Columns.Add("Name", 240, HorizontalAlignment.Left);
                Lvdb.Columns.Add("Freq", 60, HorizontalAlignment.Left);
                Lvdb.Columns.Add("Id", 60, HorizontalAlignment.Left);
                Lvdb.View = View.Details;
            }

            // LoadData(string.Empty);
        }

        private void LoadData(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            var connection = new Connection();
            connection.OpenConnection();
            var table = connection.SelectQuery($"select * from Document where Text like '{text}%';");

            if (UseListBox)
            {
                lstDBItems.Items.Clear();
            }
            else
            {
                Lvdb.Items.Clear();
            }

            foreach (DataRow item in table.Rows)
            {
                if (UseListBox)
                {
                    var b = item.ItemArray[1].ToString();
                    lstDBItems.Items.Add(b);
                }
                else
                {
                    var a = item.ItemArray[0].ToString();
                    var b = item.ItemArray[1].ToString();
                    var c = item.ItemArray[2].ToString();
                    var item2 = new ListViewItem(new[] { b, c, a });
                    Lvdb.Items.Add(item2);
                }
            }
            connection.CloseConnection();
        }


        private void BtnDownload_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TbWeb.Text)) return;
            BtnDownload.Enabled = false;

            using (WebClient client = new WebClient())
            {
                var htmlData = client.DownloadData(TbWeb.Text);
                var htmlCode = Encoding.UTF8.GetString(htmlData);

                var doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(htmlCode);

                var innerText = doc.DocumentNode.InnerText.Trim();
                innerText = innerText.Replace("\n", ".");
                innerText = innerText.Replace("..", ".");

                var reg = new Regex(@"[\|!#$%&/()=?»«@£§€{}\-'‘’“”<>_,]");   // . ;
                innerText = reg.Replace(innerText, string.Empty);
                var cleanedText = Regex.Replace(innerText, @"\s+", " ");

                // var list = cleanedText.Split(' ').Where(o => o.Length > 1);

                var list = cleanedText.Split('.').Where(o => o.Length > 1);

                foreach (var item in list)
                {
                    // if (!HasSpecialChar(item)) lstItems.Items.Add(item);
                    var line = item.Trim().Replace("..", string.Empty);
                    lstItems.Items.Add(line);
                }
            }

            BtnDownload.Enabled = true;
        }


        private void TbWeb_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Connection connection = new Connection();
            connection.OpenConnection();
            foreach (var item in lstItems.Items)
            {
                // Send each line, it will be split there
                connection.AddAllWordsToDb(item.ToString());
            }
            connection.CloseConnection();
            lstItems.Items.Clear();
        }


        // Previous code dropped
        //private void BtnAdd_Click(object sender, EventArgs e)
        //{
        //    Connection connection = new Connection();

        //    foreach (var item in lstItems.Items)
        //    {
        //        if (HasTextBefore(item.ToString())) continue;

        //        connection.CreateDocument(item.ToString());
        //    }

        //    LoadData(string.Empty);
        //}


        //public bool HasTextBefore(string text)
        //{
        //    Connection connection = new Connection();
        //    var table = connection.SelectQuery(string.Format("select * from Document where Text = '{0}';", text));
        //    return table.Rows.Count > 0;
        //}

        public bool HasSpecialChar(string input)
        {
            // if (!HasSpecialChar(item.ToString()) && !HasTextBefore(item.ToString()))

            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item) || input.Contains("\r")) return true;
            }

            return false;
        }
        private void GetNearWord(string beforeLastItem)
        {
            if (string.IsNullOrWhiteSpace(beforeLastItem)) return;

            var connection = new Connection();
            connection.OpenConnection();
            var table = connection.SelectQuery($"select * from Document where Text = '{beforeLastItem}';");

            Lvdb.Items.Clear();
            string nearWordIds = table.Rows[0]?.ItemArray[3]?.ToString();//near word ids
            if (!string.IsNullOrWhiteSpace(nearWordIds))
            {
                var arr = nearWordIds.Split(' ');
                string suggestedStr = string.Empty;
                foreach (var each in arr)
                {
                    if (!string.IsNullOrWhiteSpace(each))
                    {
                        var item = connection.SelectQuery($"select * from Document where ID = '{each}';");

                        suggestedStr += item?.Rows[0]?.ItemArray[1].ToString() + " ";
                    }
                }
                var item2 = new ListViewItem(new[] { suggestedStr, string.Empty, string.Empty });
                Lvdb.Items.Add(item2);
            }
            connection.CloseConnection();
        }
        private void Rt_TextChanged(object sender, EventArgs e)
        {
            string text = ((RichTextBox)sender).Text;
            var items = text.Split(' ');
            var lastItem = items.Last();
            if (text.Length > 0 && lastItem == "")
            {
                //near word mode
                var beforeLastItem = items.ElementAt(items.Length - 2);
                GetNearWord(beforeLastItem);
            }
            else
            {
                //auto complete for current word
                LoadData(lastItem);
            }
        }

        private void Rt_KeyDown(object sender, KeyEventArgs e)
        {
            if (!e.KeyCode.Equals(Keys.Enter)) return;
            e.SuppressKeyPress = true;


            if (UseListBox)
            {

                if (Rt.Text.LastIndexOf(' ') > 0)
                {
                    Rt.Text = Rt.Text.Substring(0, Rt.Text.LastIndexOf(' ')) + " " + (lstDBItems.SelectedItem ?? lstDBItems.Items[0].ToString());
                }
                else
                {
                    Rt.Text = (lstDBItems.SelectedItem ?? lstDBItems.Items[0].ToString()).ToString();
                }

                Rt.SelectionStart = Rt.Text.Length;

            }
            else
            {
                if (Rt.Text.LastIndexOf(' ') > 0)
                {
                    Rt.Text = Rt.Text.Substring(0, Rt.Text.LastIndexOf(' ')) + " " +
                              (Lvdb.Items[0].SubItems[0].Text);
                }
                else
                {
                    Rt.Text = Lvdb.Items[0].SubItems[0].Text.ToString();
                }

                Rt.SelectionStart = Rt.Text.Length;

            }




        }

    }
}
