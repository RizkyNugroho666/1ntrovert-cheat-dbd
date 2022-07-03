using Introvert.Saves;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;

using Fiddler;

namespace Introvert_Launcher
{
    public static class Req
    {
        /////////////// High Priority Variables
        public static string PROGRAM_EXECUTABLE = System.AppDomain.CurrentDomain.FriendlyName;
        public static string EncryptProgramName()
        {
            Random rnd = new Random();
            byte[] array = new byte[rnd.Next(8, 16)];
            rnd.NextBytes(array);
            return System.Convert.ToBase64String(array);
        }

        public const string REGISTRY_MAIN = @"HKEY_CURRENT_USER\SOFTWARE\1ntrovert Cheat";
        public const string PROGRAM_OFFLINEVERSION = "3801";
        public static string PROGRAM_TEXT_OFFLINEVERSION = System.Text.RegularExpressions.Regex.Replace(PROGRAM_OFFLINEVERSION, "(.)", "$1.").Remove(PROGRAM_OFFLINEVERSION.Length * 2 - 1);
        public static string PROGRAM_NEWS_LASTSEENTHREAD = REGISTRY_GETVALUE("LSNT");

        public static string REGISTRY_VALUE_PAKFILEPATH = REGISTRY_GETVALUE("PakFilePath");
        public static string REGISTRY_VALUE_PAKFOLDERPATH = REGISTRY_VALUE_PAKFILEPATH.Replace("\\pakchunk1-WindowsNoEditor.pak", "");
        public static string REGISTRY_VALUE_THEME = REGISTRY_GETVALUE("SelectedTheme");
        public static int INITIALIZEDTHEME = 0;
        public static string REGISTRY_GETVALUE(string WINREGNAME)
        {
            try
            { return Registry.GetValue(REGISTRY_MAIN, WINREGNAME, "NONE").ToString(); }
            catch { return "NONE"; }
        }
        public static int REGISTRY_GETINTVALUE(string WINREGNAME)
        {
            try
            { return Convert.ToInt32(Registry.GetValue(REGISTRY_MAIN, WINREGNAME, -1)); }
            catch { return -1; }
        }

        public static string OVERRIDEN_VALUE_USERAGENT = null;

        public static string Fiddler_QueuePos(string input)
        {
            if (QueuePosition != null)
            {
                try
                {
                    var JsQueueResponse = JObject.Parse(input);
                    if ((string)JsQueueResponse["status"] == "QUEUED")
                        return (string)JsQueueResponse["queueData"]["position"];
                    else if ((string)JsQueueResponse["status"] == "MATCHED")
                    {
                        MatchId = (string)JsQueueResponse["matchData"]["matchId"];
                        return "MATCHED";
                    }
                    else
                        return "-";
                }
                catch { return "-"; }
            }
            else return "-";
        }

        public static string Fiddler_UID(string input)
        {
            if (input != null)
            {
                string output = input.Remove(0, input.IndexOf("14", StringComparison.InvariantCulture)).Remove(0, 24);
                output = output.Remove(16, output.Length - 16);
                return output;
            }
            else return null;
        }
        public static string Fiddler_FullProfile()
        {
            if (FullProfile != null)
            {
                dynamic JsSaveFile = JsonConvert.DeserializeObject(FullProfile);
                JsSaveFile["playerUId"] = PlayerUID;
                JsSaveFile["currentSeasonTicks"] = (long)((DateTime.Now.ToUniversalTime() - CurrentNETtimestampstart).TotalMilliseconds + 0.5);
                return SaveFile.EncryptSavefile(JsonConvert.SerializeObject(JsSaveFile, Formatting.None));
            }
            else return null;
        }

        public static string Fiddler_MarketJson()
        {
            if (MarketJson != null)
            {
                try
                {
                    JObject JsMarketFile = JObject.Parse(MarketJson);
                    JObject JsMarketFileData = (JObject)JsMarketFile.SelectToken("data");
                    JsMarketFileData.Property("playerId").Value = PlayerID;
                    return JsMarketFile.ToString();
                }
                catch { return MarketJson; }
            }
            else return null;
        }

        public static DateTime CurrentNETtimestampstart { get; private set; }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.ASCII.GetString(base64EncodedBytes);
        }

        public static void DisableProxy()
        {
            try
            {
                string regproxykeypath = @"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Internet Settings";
                Registry.SetValue(regproxykeypath, "ProxyEnable", 0);
                Registry.SetValue(regproxykeypath, "ProxyServer", "");
                Registry.SetValue(regproxykeypath, "ProxyOverride", "");
            }
            catch { }
        }

        // //FiddlerCore Var
        // String
        public static string QueuePosition = "-";
        public static string Platfrom = null;
        public static string FullProfile = null;
        public static string BhvrSession = null;
        public static string MarketJson = null;
        public static string MatchId = null;
        public static string PlayerID = null;
        public static string PlayerUID = null;
        public static string value_level = null;
        public static string value_devotion = null;
        public static string value_KillerPips = null;
        public static string value_SurvivorPips = null;
        public static string value_BloodPoints = null;
        public static string value_Shards = null;
        public static string value_AuricCells = null;
        public static string checking_banned = null;
        public static string isBanned = null;

        // Boolean
        public static bool bool_changeLevel = false;
        public static bool bool_FullProfileInject = false;
        public static bool bool_MarketInject = false;
        public static bool bool_SpoofCurrency = false;
        public static bool bool_ChangePips = false;
        public static bool bool_isBanned = false;

    }
}
