using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Kbg.NppPluginNET.PluginInfrastructure;

namespace Kbg.NppPluginNET
{
    class Main
    {
        internal const string PluginName = "Defanger";
        static string iniFilePath = null;
        static bool someSetting = false;

        public static void OnNotification(ScNotification notification)
        {

        }

        internal static void CommandMenuInit()
        {
            StringBuilder sbIniFilePath = new StringBuilder(Win32.MAX_PATH);
            Win32.SendMessage(PluginBase.nppData._nppHandle, (uint)NppMsg.NPPM_GETPLUGINSCONFIGDIR, Win32.MAX_PATH, sbIniFilePath);
            iniFilePath = sbIniFilePath.ToString();
            if (!Directory.Exists(iniFilePath)) Directory.CreateDirectory(iniFilePath);
            iniFilePath = Path.Combine(iniFilePath, PluginName + ".ini");
            someSetting = (Win32.GetPrivateProfileInt("SomeSection", "SomeKey", 0, iniFilePath) != 0);
            PluginBase.SetCommand(0, "Defang", Defanger, new ShortcutKey(false, false, false, Keys.None));
            PluginBase.SetCommand(1, "Refang", Refanger, new ShortcutKey(false, false, false, Keys.None));
            PluginBase.SetCommand(2, "Auto Defang All", DefangA, new ShortcutKey(false, false, false, Keys.None));
            PluginBase.SetCommand(3, "Auto Refang All", RefangA, new ShortcutKey(false, false, false, Keys.None));
        }

        internal static void PluginCleanUp()
        {
            Win32.WritePrivateProfileString("SomeSection", "SomeKey", someSetting ? "1" : "0", iniFilePath);
        }

        internal static string GetAllText()
        {
            int length = (int)Win32.SendMessage(PluginBase.GetCurrentScintilla(), SciMsg.SCI_GETLENGTH, 0, 0);
            IntPtr ptrToText = Marshal.AllocHGlobal(length + 1);
            Win32.SendMessage(PluginBase.GetCurrentScintilla(), SciMsg.SCI_GETTEXT, length + 1, ptrToText);
            string textAnsi = Marshal.PtrToStringAnsi(ptrToText);
            Marshal.FreeHGlobal(ptrToText);
            return textAnsi;
        }

        internal static string GetSelText()
        {
            int length = (int)Win32.SendMessage(PluginBase.GetCurrentScintilla(), SciMsg.SCI_GETLENGTH, 0, 0);
            IntPtr ptrToText = Marshal.AllocHGlobal(length + 1);
            Win32.SendMessage(PluginBase.GetCurrentScintilla(), SciMsg.SCI_GETSELTEXT, length + 1, ptrToText);
            string textAnsi = Marshal.PtrToStringAnsi(ptrToText);
            Marshal.FreeHGlobal(ptrToText);
            return textAnsi;
        }

        internal static void Defanger()
        {
            IntPtr currentScint = PluginBase.GetCurrentScintilla();
            ScintillaGateway scintillaGateway = new ScintillaGateway(currentScint);

            string selectedText = GetSelText();

            string urlPattern = @"(((ht|f)tp(s?))\:\/\/)?((([a-zA-Z0-9_\-]{2,}\.)+[a-zA-Z]{2,})|((?:(?:25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)(\?(\.?\d)\.)){4}))(:[a-zA-Z0-9]+)?(\/[a-zA-Z0-9\-\._\?\,\'\/\\\+&amp;%\$#\=~]*)?";

            string ipv4Pattern = @"(|https?:\/\/)(?:25[0-5]|2[0-4]\d|[01]?\d{1,2})(?:\.(?:25[0-5]|2[0-4]\d|[01]?\d{1,2})){3}(?::\d+)?((6553[0-5])|(655[0-2][0-9])|(65[0-4][0-9]{2})|(6[0-4][0-9]{3})|([1-5][0-9]{4})|([0-5]{0,5})|([0-9]{1,4}))?(?:\/[\w\-\\._~:\/?#%[\]@!\$&'()+,;=.]*)?";

            string ipv6Pattern = @"(^|\s|(\[))(::)?([a-fA-F\d]{1,4}::?){0,7}((?=(?(2)\]|($|\s|(?(3)($|\s)|(?(4)($|\s)|:\d)))))|((?(3)[a-fA-F\d]{1,4})|(?(4)[a-f\d]{1,4}))(?=(?(2)\]|($|\s))))(?(2)\])(:\d{1,5})?";

            Regex urlregex = new Regex(urlPattern);

            Regex ipv4regex = new Regex(ipv4Pattern);

            Regex ipv6regex = new Regex(ipv6Pattern);

            string replacedText = selectedText;

            if (urlregex.IsMatch(selectedText) || ipv4regex.IsMatch(selectedText) || ipv6regex.IsMatch(selectedText))
            {
                replacedText = urlregex.Replace(replacedText, AllMatchEvaluator);
                replacedText = ipv4regex.Replace(replacedText, AllMatchEvaluator);
                replacedText = ipv6regex.Replace(replacedText, AllMatchEvaluator);
            }

            string AllMatchEvaluator(Match match)
            {
                string urlip = match.Value;
                string replacedAll = urlip.Replace(".", "[.]").Replace(":", "[:]").Replace("https", "hxxps").Replace("http", "hxxp").Replace("[[.]]","[.]").Replace("[[:]]","[:]");
                return replacedAll;
            }

            scintillaGateway.ReplaceSel(replacedText);
        }

        internal static void Refanger()
        {
            IntPtr currentScint = PluginBase.GetCurrentScintilla();
            ScintillaGateway scintillaGateway = new ScintillaGateway(currentScint);

            string selectedText = GetSelText();

            string regexPattern = @"(?:hxxps?|hXXps?|fxps?)|\[\.\]|\\\.|\[\:\]|\(\.\)|{\.}|\[:\/\/\]|\[\/\]|\[dot\]|\(dot\)|{dot}";

            if (Regex.IsMatch(selectedText, regexPattern))
            {
                selectedText = Regex.Replace(selectedText, @"hxxps|hXXps", "https");
                selectedText = Regex.Replace(selectedText, @"hxxp|hXXp", "http");
                selectedText = Regex.Replace(selectedText, @"fxps", "ftps");
                selectedText = Regex.Replace(selectedText, @"fxp", "ftp");
                selectedText = Regex.Replace(selectedText, @"\[\.\]|\\\.|\(\.\)|{\.\}|\[dot\]|\(dot\)|{dot}", ".");
                selectedText = Regex.Replace(selectedText, @"\[\:]", ":");
                selectedText = Regex.Replace(selectedText, @"\[://]", "://");
                selectedText = Regex.Replace(selectedText, @"\[/]", "/");

            }

            scintillaGateway.ReplaceSel(selectedText);
        }

        internal static void DefangA()
        {
            IntPtr currentScint = PluginBase.GetCurrentScintilla();
            ScintillaGateway scintillaGateway = new ScintillaGateway(currentScint);

            string allText = GetAllText();

            string urlPattern = @"(((ht|f)tp(s?))\:\/\/)?((([a-zA-Z0-9_\-]{2,}\.)+[a-zA-Z]{2,})|((?:(?:25[0-5]|2[0-4]\d|[01]\d\d|\d?\d)(\?(\.?\d)\.)){4}))(:[a-zA-Z0-9]+)?(\/[a-zA-Z0-9\-\._\?\,\'\/\\\+&amp;%\$#\=~]*)?";

            string ipv4Pattern = @"(|https?:\/\/)(?:25[0-5]|2[0-4]\d|[01]?\d{1,2})(?:\.(?:25[0-5]|2[0-4]\d|[01]?\d{1,2})){3}(?::\d+)?((6553[0-5])|(655[0-2][0-9])|(65[0-4][0-9]{2})|(6[0-4][0-9]{3})|([1-5][0-9]{4})|([0-5]{0,5})|([0-9]{1,4}))?(?:\/[\w\-\\._~:\/?#%[\]@!\$&'()+,;=.]*)?";

            string ipv6Pattern = @"(^|\s|(\[))(::)?([a-fA-F\d]{1,4}::?){0,7}((?=(?(2)\]|($|\s|(?(3)($|\s)|(?(4)($|\s)|:\d)))))|((?(3)[a-fA-F\d]{1,4})|(?(4)[a-f\d]{1,4}))(?=(?(2)\]|($|\s))))(?(2)\])(:\d{1,5})?";

            Regex urlregex = new Regex(urlPattern);

            Regex ipv4regex = new Regex(ipv4Pattern);

            Regex ipv6regex = new Regex(ipv6Pattern);

            string replacedText = allText;

            if (urlregex.IsMatch(allText) || ipv4regex.IsMatch(allText) || ipv6regex.IsMatch(allText))
            {
                replacedText = urlregex.Replace(replacedText, AllMatchEvaluator);
                replacedText = ipv4regex.Replace(replacedText, AllMatchEvaluator);
                replacedText = ipv6regex.Replace(replacedText, AllMatchEvaluator);
            }

            string AllMatchEvaluator(Match match)
            {
                string urlip = match.Value;
                string replacedAll = urlip.Replace(".", "[.]").Replace(":", "[:]").Replace("https", "hxxps").Replace("http", "hxxp").Replace("[[.]]","[.]").Replace("[[:]]","[:]");
                return replacedAll;
            }

            scintillaGateway.SelectAll();
            scintillaGateway.ReplaceSel(replacedText);
        }
        
        internal static void RefangA()
        {
            IntPtr currentScint = PluginBase.GetCurrentScintilla();
            ScintillaGateway scintillaGateway = new ScintillaGateway(currentScint);

            string allText = GetAllText();

            string regexPattern = @"(?:hxxps?|hXXps?|fxps?)|\[\.\]|\\\.|\[\:\]|\(\.\)|{\.}|\[:\/\/\]|\[\/\]|\[dot\]|\(dot\)|{dot}";

            if (Regex.IsMatch(allText, regexPattern))
            {
                allText = Regex.Replace(allText, @"hxxps|hXXps", "https");
                allText = Regex.Replace(allText, @"hxxp|hXXp", "http");
                allText = Regex.Replace(allText, @"fxps", "ftps");
                allText = Regex.Replace(allText, @"fxp", "ftp");
                allText = Regex.Replace(allText, @"\[\.\]|\\\.|\(\.\)|{\.\}|\[dot\]|\(dot\)|{dot}", ".");
                allText = Regex.Replace(allText, @"\[\:]", ":");
                allText = Regex.Replace(allText, @"\[://]", "://");
                allText = Regex.Replace(allText, @"\[/]", "/");

            }

            scintillaGateway.SelectAll();
            scintillaGateway.ReplaceSel(allText);
        } 
    }
}
