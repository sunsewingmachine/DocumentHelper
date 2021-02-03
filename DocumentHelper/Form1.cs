using Shared;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;
using Shared.Tools;

namespace DocumentHelper
{
    public partial class Form1 : Form
    {

        public static bool UseListBox;
        private string RegSubKey = @"SOFTWARE\E3TypingHelper";
        private string RegValue_RtContent = @"RtContent";
        private string RegValue_HotStringContent = @"HotStringContent";
        private string RegValue_Left = @"Left";
        private string RegValue_Top = @"Top";
        private List<string> UnAllowedWords = new List<string>();

        public Form1()
        {
            InitializeComponent();
            Closed += Form1_Closed;
        }

        private void Form1_Closed(object sender, EventArgs e)
        {
            SaveSettings();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GetSettings();
            PopulateUnAllowedWords();

            if (UseListBox)
            {
                lstDBItems.Visible = true;
                Lvdb.Visible = false;
            }
            else
            {
                lstDBItems.Visible = false;
                Lvdb.Visible = true;

                Lvdb.Columns.Add("SC", 60, HorizontalAlignment.Left);
                Lvdb.Columns.Add("Name", 240, HorizontalAlignment.Left);
                Lvdb.Columns.Add("Freq", 60, HorizontalAlignment.Left);
                Lvdb.Columns.Add("Id", 60, HorizontalAlignment.Left);
                Lvdb.View = View.Details;
            }

            LoadHotStrings();
            // LoadData(string.Empty);
        }

        private void SaveSettings()
        {
            RegistryKey key2 = Registry.CurrentUser.CreateSubKey(RegSubKey);
            key2.SetValue(RegValue_RtContent, Rt.Text);
            key2.SetValue(RegValue_HotStringContent, TbHs.Text);

            if (this.WindowState == FormWindowState.Minimized) return;
            if (this.WindowState == FormWindowState.Maximized) return;

            if (Left > this.Width * 0.9) return;
            if (Top > this.Height * 0.9) return;

            key2.SetValue(RegValue_Left, Left);
            key2.SetValue(RegValue_Top, Top);

        }

        private void GetSettings()
        {
            RegistryKey key2 = Registry.CurrentUser.OpenSubKey(RegSubKey);
            if (key2 == null)
            {
                key2 = Registry.CurrentUser.CreateSubKey(RegSubKey);
                key2.SetValue(RegValue_RtContent, "");
                key2.SetValue(RegValue_HotStringContent, "");
                key2.SetValue(RegValue_Left, 500);
                key2.SetValue(RegValue_Top, 500);
            }

            var RtText = key2.GetValue(RegValue_RtContent, "").ToString();
            Rt.Text = string.IsNullOrWhiteSpace(RtText) ? " " : RtText;

            var TbHsText = key2.GetValue(RegValue_HotStringContent, "").ToString();
            TbHs.Text = !string.IsNullOrWhiteSpace(TbHsText)? TbHsText : "அஅ. - அல்லாஹ் கூறுகிறான், " + Environment.NewLine + 
                                                                         "நந. - நபி (ஸல்) அவர்கள் கூறுகிறார்கள், ";
            
            Left = (int)key2.GetValue(RegValue_Left, 500);
            Top = (int)key2.GetValue(RegValue_Top, 500);
        }


        private void LoadHotStrings()
        {
            string[] lines = TbHs.Text.Split(new[] { Environment.NewLine },  StringSplitOptions.None);

            foreach (var line in lines)
            {
                if (!line.Contains("-")) continue;
                var k = line.Split('-');
                hotStrings.Add(k[0].Trim(), k[1]);
            }

        }

        private void LoadData(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return;

            var connection = new Connection();
            connection.OpenConnection();
            var table = connection.SelectQuery($"select * from Document where Text like '{text}%';");


            foreach (DataRow item in table.Rows)
            {
                if (UseListBox)
                {
                    var b = item.ItemArray[1].ToString();
                    lstDBItems.Items.Add(b);
                }
                else
                {
                    var b = item.ItemArray[1].ToString();

                    if (ContainsPr(b)) continue;

                    var a = item.ItemArray[0].ToString();
                    var c = item.ItemArray[2].ToString();
                    var id = (Lvdb.Items.Count + 1).ToString();

                    var item2 = new ListViewItem(new[] {id, b, c, a });
                    Lvdb.Items.Add(item2);
                }
            }
            connection.CloseConnection();
        }

        private bool ContainsPr(string str)
        {
            var query = Lvdb.Items
                .Cast<ListViewItem>().Any(item => item.SubItems[1].Text == str);

            return query;
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
                AddTextToListBox(innerText);
            }

            BtnDownload.Enabled = true;
        }

        private void AddTextToListBox(string innerText)
        {
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
                if (ContainsWrongWords(line)) continue;
                lstItems.Items.Add(line);
            }
        }

        
        private bool ContainsWrongWords(string line)
        {
            //CultureInfo culture = CultureInfo.CurrentCulture;
            //bool b = UnAllowedWords.Any(s => culture.CompareInfo.IndexOf(s, line, CompareOptions.IgnoreCase)>0);
            //// return b;

            //;
            //bool b = UnAllowedWords.Any(s => s.IndexOf(line, StringComparison.CurrentCultureIgnoreCase) >= 0 );
            //return b;

            return UnAllowedWords.Any(word => line.IndexOf(word, StringComparison.CurrentCultureIgnoreCase) >= 0);
        }


        private void TbWeb_TextChanged(object sender, EventArgs e)
        {

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Connection connection = new Connection();
            connection.OpenConnection();

            do
            { 
                // Send each line, it will be split there
                connection.AddAllWordsToDb(lstItems.Items[0].ToString());
                lstItems.Items.RemoveAt(0);
                lstItems.Refresh();
            }
            while (lstItems.Items.Count > 0);

            connection.CloseConnection();
            // lstItems.Items.Clear();
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
            if (table.Rows.Count < 1) goto CloseConnection;

            string nearWordIds = table.Rows[0]?.ItemArray[3]?.ToString();//near word ids
            if (!string.IsNullOrWhiteSpace(nearWordIds))
            {
                NearWordsList.Clear();
                var arr = nearWordIds.Split(' ');

                foreach (var each in arr)
                {
                    if (!string.IsNullOrWhiteSpace(each))
                    {
                        var item = connection.SelectQuery($"select * from Document where ID = '{each}';");

                        if (item == null) continue;
                        if (item.Rows.Count == 0) continue;

                        // suggestedStr += item?.Rows[0]?.ItemArray[1].ToString() + " ";
                        var suggestedStr = item?.Rows[0]?.ItemArray[1] + " ";

                        if (!string.IsNullOrWhiteSpace(suggestedStr))
                        {
                            var id = (Lvdb.Items.Count + 1).ToString();
                            var item2 = new ListViewItem(new[] {id, suggestedStr, string.Empty, string.Empty});
                            Lvdb.Items.Add(item2);
                            NearWordsList.Add(item2);
                        }
                    }
                }
            }

            CloseConnection:
            connection.CloseConnection();
        }

        List<ListViewItem> NearWordsList = new List<ListViewItem>();

        private void Rt_TextChanged(object sender, EventArgs e)
        {
            string text = ((RichTextBox)sender).Text;
            if (string.IsNullOrWhiteSpace(text)) return;

            if (UseListBox)
            {
                lstDBItems.Items.Clear();
            }
            else
            {
                Lvdb.Items.Clear();
            }

            var items = text.Split(' ');
            var lastItem = items.Last();

            // string[] items = text.Split(new string[] { " ", "\n" }, StringSplitOptions.None);
            // var lastItem = items.Last();

            if (hotStrings.ContainsKey(lastItem))
            {
                var aa = hotStrings[lastItem];

                Rt.Text = Rt.Text.Substring(0, Rt.Text.LastIndexOf(' ')) + " " + aa ;
                Rt.SelectionStart = Rt.Text.Length;
            }

            if (text.Length > 0 && lastItem == "")
            {
                //near word mode
                var beforeLastItem = items.ElementAt(items.Length - 2);
                GetNearWord(beforeLastItem);
            }
            else
            {
                //auto complete for current word
                AddNearWords(lastItem);
                LoadData(lastItem);
            }
        }

        private Dictionary<string, string> hotStrings = new Dictionary<string, string>();

        private void AddNearWords(string lastItem)
        {
            if (UseListBox)
            {
                lstDBItems.Items.Clear();
            }
            else
            {
                Lvdb.Items.Clear();
            }

            foreach (var item in NearWordsList)
            {
                if (!item.SubItems[1].Text.StartsWith(lastItem)) continue;
                Lvdb.Items.Add(item);
            }

        }

        private void Rt_KeyDown(object sender, KeyEventArgs e)
        {
            int row;

            if (e.KeyCode == Keys.Escape)
            {
                if(Lvdb.Items.Count > 0) Lvdb.Items.RemoveAt(0);
                return;

                //if (Lvdb.SelectedItems.Count < 0)
                //{
                //    Lvdb.Items[1].Selected = true;
                //    return;
                //}
                //int index = (Lvdb.SelectedItems == null) ? 0 : 2;
            }

            if (e.KeyCode.Equals(Keys.Enter))
            {
                if (e.Shift)
                {
                    return;
                    Rt.Text = Rt.Text + Environment.NewLine + " ";
                    Rt.SelectionStart = Rt.Text.Length;
                    e.SuppressKeyPress = true;
                    return;
                }
                row = 0;
            }
            else
            {
                return;
                //if ((e.KeyValue >= 48 && e.KeyValue <= 57))
                //{
                //    row = e.KeyValue - 48;
                //    row--;
                //}
                //else
                //{
                //    return;
                //}
            }

            e.SuppressKeyPress = true;

            if (Lvdb.Items.Count == 0) return;

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
                              Lvdb.Items[row].SubItems[1].Text;
                }
                else
                {
                    Rt.Text = Lvdb.Items[row].SubItems[1].Text;
                }

                Rt.SelectionStart = Rt.Text.Length;
            }
        }

        private void BtnAddTypingToDb_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            lstItems.Items.Clear();
            AddTextToListBox(Rt.Text);
        }

        private void PopulateUnAllowedWords()
        {

            UnAllowedWords = new List<string>
            {
                "a", "e", "i", "o", "u", "b", "c", "d", "k", "l", "r", "s",
                "ا", "ب", "و", "م", "ن", "ل", "س",  "ف", "ق", "ح",
            };

            return;
            UnAllowedWords = new List<string>
            {
                "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
                "January", "February", "March", "April", "May", "June", "July", "August" , "September" , "October" , "November" , "December", 
                "Tamil", "Bayan", "points", "Word", "website", "copy", "in", "the", "one","but", "naveeth", "rahma", "farook", "abdul", "abdur", "pj",
                "nav", "name", "top","back","next","page", "we", "are","were","was","he","you",
                "ا", "ب", "و", "م", "ن", "ل", "س",  "ف", "ق", "ح",
                "india", "usa"
            };

        }
    }

}
