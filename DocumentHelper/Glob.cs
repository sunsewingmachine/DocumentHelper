using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DocumentHelper
{
    public class Glob
    {
        public static bool IsTopMost = true;

        public static bool EnableNearWordsEntry = true;


        public static string GetAssemblyShortName()
        {
            return Assembly.GetCallingAssembly().GetName().Name;
        }
    }

    public static class Keys
    {
        public static string TbUrlNameText = "TbUrlNameText";


        public static string LstSelectedIndex = "LstSelectedIndex";
        public static string TbInText = "TbInText";

        public static string ChkGoogle = "ChkGoogle";
        public static string ChkOurs = "ChkOurs";

        public static string ThisLeft = "ThisLeft";
        public static string ThisTop = "ThisTop";
        public static string ThisWidth = "ThisWidth";
        public static string ThisHeight = "ThisHeight";

    }



    public class AutoClosingMessageBox
    {
        System.Threading.Timer _timeoutTimer;
        string _caption;
        AutoClosingMessageBox(string text, string caption, int timeout)
        {
            _caption = caption;
            _timeoutTimer = new System.Threading.Timer(OnTimerElapsed,
                null, timeout, System.Threading.Timeout.Infinite);
            using (_timeoutTimer)
                MessageBox.Show(text, caption);
        }
        public static void Show(string text, string caption, int timeout)
        {
            new AutoClosingMessageBox(text, caption, timeout);
        }
        void OnTimerElapsed(object state)
        {
            IntPtr mbWnd = FindWindow("#32770", _caption); // lpClassName is #32770 for MessageBox
            if (mbWnd != IntPtr.Zero)
                SendMessage(mbWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            _timeoutTimer.Dispose();
        }
        const int WM_CLOSE = 0x0010;
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
    }


    public static class StringsFunctions
    {
        /// <summary>
        /// Uses regex '\b' as suggested in https://stackoverflow.com/questions/6143642/way-to-have-string-replace-only-hit-whole-words
        /// </summary>
        /// <param name="original"></param>
        /// <param name="wordToFind"></param>
        /// <param name="replacement"></param>
        /// <param name="regexOptions"></param>
        /// <returns></returns>
        public static string ReplaceWholeWord(this string original, string wordToFind, string replacement, RegexOptions regexOptions = RegexOptions.None)
        {
            string pattern = String.Format(@"\b{0}\b", wordToFind);
            string ret = Regex.Replace(original, pattern, replacement, regexOptions);
            return ret;

        }

      //public static bool ContainsExactWholeWord(this string sentence, string wordToFind)
      //{
      //    // var input = "blue monkeys are not red";
      //    // var wordToFind = "b";

      //    var pattern = @"\b" + Regex.Escape(wordToFind) + @"\b";
      //    return Regex.IsMatch(sentence, pattern); // returns false

      //    // return (Regex.Match(sentence, @"\b" + wordToFind + @"\b", RegexOptions.None).Success);
      //}

      public static bool ContainsExactWholeWord(this string source, string wordToFind)
      {
          // var input = "blue monkeys are not red";
          // var wordToFind = "b";

          // var pattern = @"\b" + Regex.Escape(wordToFind) + @"\b";
         //  return Regex.IsMatch(sentence, pattern); // returns false

          //var punctuation = source.Where(Char.IsPunctuation).Distinct().ToArray();
          //var words = sentence.Split().Select(x => x.Trim(punctuation));
          //var containsHi = words.Contains("hi", StringComparer.OrdinalIgnoreCase);


          // var punctuation = source.Where(Char.IsPunctuation).Distinct().ToArray();

          // return false;
            // var words = sentence.Split().Select(x => x.Trim(punctuation));
            // var containsHi = words.Contains("hi", StringComparer.OrdinalIgnoreCase);

            var words = source.Split().Select(x => x.Trim());
            return words.Contains(wordToFind, StringComparer.CurrentCulture);
        }


      private static bool IsWordChar(char c)
      {
          return Char.IsLetterOrDigit(c) || c == '_';
      }

      public static string ReplaceFullWords(this string s, string oldWord, string newWord)
      {
          if (s == null)
          {
              return null;
          }
          int startIndex = 0; // Where we start to search in s.
          int copyPos = 0; // Where we start to copy from s to sb.
          var sb = new StringBuilder();
          while (true)
          {
              int position = s.IndexOf(oldWord, startIndex);
              if (position == -1)
              {
                  if (copyPos == 0)
                  {
                      return s;
                  }
                  if (s.Length > copyPos)
                  { // Copy last chunk.
                      sb.Append(s.Substring(copyPos, s.Length - copyPos));
                  }
                  return sb.ToString();
              }
              int indexAfter = position + oldWord.Length;
              if ((position == 0 || !IsWordChar(s[position - 1])) && (indexAfter == s.Length || !IsWordChar(s[indexAfter])))
              {
                  sb.Append(s.Substring(copyPos, position - copyPos)).Append(newWord);
                  copyPos = position + oldWord.Length;
              }
              startIndex = position + oldWord.Length;
          }
      }

      public static string RemoveSpaces(this string text)
      {
          text = string.Join(" ", text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
          return text;
      }

    }
}
