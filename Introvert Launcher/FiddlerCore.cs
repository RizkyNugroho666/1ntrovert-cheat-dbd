using Fiddler;
using System.Reflection;
using Newtonsoft.Json;

namespace Introvert_Launcher
{
    public partial class FiddlerCore
    {

        static FiddlerCore()
        {
            FiddlerApplication.BeforeRequest += FiddlerToCatchBeforeRequest;
            FiddlerApplication.AfterSessionComplete += FiddlerToCatchAfterSessionComplete;
            FiddlerApplication.ResetSessionCounter();
        }

        private static bool EnsureRootCertificate()
        {
            if (!CertMaker.rootCertExists())
            {
                if (!CertMaker.createRootCert())
                    return false;
                if (!CertMaker.trustRootCert())
                    return false;
                FiddlerApplication.Prefs.GetStringPref("fiddler.certmaker.bc.cert", null);
                FiddlerApplication.Prefs.GetStringPref("fiddler.certmaker.bc.key", null);
            }
            return true;
        }

        public static bool RemoveRootCertificate()
        {
            try
            {
                CertMaker.removeFiddlerGeneratedCerts(true);
                return true;
            }
            catch { return false; }
        }

        public static void Start()
        {
            EnsureRootCertificate();
            CONFIG.IgnoreServerCertErrors = true;
            FiddlerApplication.Startup(new FiddlerCoreStartupSettingsBuilder().ListenOnPort(8866).RegisterAsSystemProxy().ChainToUpstreamGateway().DecryptSSL().OptimizeThreadPool().Build());
        }
        public static void Stop()
        {
            FiddlerApplication.Shutdown();
            Req.DisableProxy();
        }

        public static void FiddlerToCatchBeforeRequest(Session oSession)
        {
            if (oSession.hostname == "steam.live.bhvrdbd.com" || oSession.hostname == "grdk.live.bhvrdbd.com" || oSession.hostname == "brill.live.bhvrdbd.com" || oSession.hostname == "cdn.live.dbd.bhvronline.com" || oSession.hostname == "latest.ptb.bhvrdbd.com" || oSession.hostname == "cdn.ptb.dbd.bhvronline.com")
            {
                if (oSession.uriContains("api/v1/config"))
                {
                    Req.BhvrSession = oSession.oRequest["Cookie"].Replace("bhvrSession=", "");
                    if (oSession.uriContains("steam.live"))
                        Req.Platfrom = "steam.live";
                    else if (oSession.uriContains("grdk.live"))
                        Req.Platfrom = "grdk.live";
                    else if (oSession.uriContains("brill.live"))
                        Req.Platfrom = "brill.live";
                    return;
                }

                if (oSession.uriContains("api/v1/friends/richPresence"))
                {
                    Req.PID = oSession.url.Remove(0, oSession.url.LastIndexOf("/") + 1);
                    return;
                }

                if (Req.bool_MarketInject == true)
                {
                    if (oSession.uriContains("api/v1/inventories"))
                    {
                        if (oSession.oRequest["x-kraken-client-os"] != "21.7.28700.1.768.64bit")
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody(Req.Fiddler_MarketJson());
                            NetServices.REQUEST_DBDEMULATION(oSession.url);
                        }

                        return;
                    }
                }

                if (Req.bool_FullProfileInject == true)
                {
                    if (oSession.uriContains("api/v1/players/me/states/FullProfile/binary"))
                    {
                        if (oSession.oRequest["x-kraken-client-os"] != "21.7.28700.1.768.64bit")
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.oResponse["Content-Type"] = "application/octet-stream";
                            oSession.oResponse["Kraken-State-Version"] = "1";
                            oSession.oResponse["Kraken-State-Schema-Version"] = "0";
                            oSession.utilSetResponseBody(Req.Fiddler_FullProfile());
                            NetServices.REQUEST_DBDEMULATION(oSession.url);
                        }

                        return;
                    }
                    if (oSession.uriContains("api/v1/players/me/states/binary?schemaVersion"))
                    {
                        oSession.utilCreateResponseAndBypassServer();
                        oSession.utilSetResponseBody("{\"version\":1,\"stateName\":\"FullProfile\",\"schemaVersion\":0,\"playerId\":\"" + Req.PID + "\"}");
                        return;
                    }
                }

                if (Req.bool_SpoofCurrency == true)
                {
                    if (oSession.uriContains("api/v1/wallet/currencies"))
                    {
                        if (oSession.oRequest["x-kraken-client-os"] != "21.7.28700.1.768.64bit")
                        {
                            oSession.utilCreateResponseAndBypassServer();
                            oSession.utilSetResponseBody("{\"list\":[{\"balance\":" + Req.value_Shards + ",\"currency\":\"Shards\"},{\"balance\":" + Req.value_AuricCells + ",\"currency\":\"Cells\"},{\"balance\":" + Req.value_BloodPoints + ",\"currency\":\"BonusBloodpoints\"},{\"balance\":0,\"currency\":\"Bloodpoints\"}]}");
                            NetServices.REQUEST_DBDEMULATION(oSession.url);
                        }

                        return;
                    }
                }
            }
        }

        public static void FiddlerToCatchAfterSessionComplete(Session oSession)
        {
            if (oSession.hostname == "steam.live.bhvrdbd.com" || oSession.hostname == "grdk.live.bhvrdbd.com" || oSession.hostname == "brill.live.bhvrdbd.com" || oSession.hostname == "latest.ptb.bhvrdbd.com")
            {
                if (oSession.uriContains("api/v1/queue"))
                {
                    try
                    {
                        if (oSession.uriContains("/cancel"))
                            Req.QueuePosition = "NONE";
                        else
                        {
                            oSession.utilDecodeResponse();
                            Req.QueuePosition = Req.Fiddler_QueuePos(System.Text.Encoding.UTF8.GetString(oSession.responseBodyBytes));
                        }
                        return;
                    }
                    catch { Req.QueuePosition = "NONE"; return; }
                }
                if (oSession.uriContains("api/v1/match"))
                {
                    Req.QueuePosition = "MATCHED";
                    oSession.utilDecodeResponse();
                    string responseAsString = System.Text.Encoding.UTF8.GetString(oSession.responseBodyBytes);
                    return;
                }
                if (oSession.uriContains("api/v1/auth/provider/steam/login?token="))
                {
                    Req.UID = Req.Fiddler_UID(oSession.url.ToString());
                    return;
                }
            }
            else return;
        }
    }
}
