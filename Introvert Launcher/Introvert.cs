using System;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Diagnostics;
using System.Windows.Forms;
using DiscordRPC;

namespace Introvert_Launcher
{
    public partial class Form1 : Form
    {
        DiscordRpcClient client = null;


        Form CROSSHAIR = new NewCrosshair();
        Form SetAmount = new ChangeAmount();
        WebClient webClient = new WebClient();
        NotifyIcon IntrovertTray = new NotifyIcon();
        static bool isProgramInitialized = false;
        static bool isFiddlerCoreActive = false;

        public Form1()
        {
            InitializeComponent();
            InitializeSettings();

            // Auto Update App
            WebClient webClient = new WebClient();

            try
            {
                if (!webClient.DownloadString("https://rizkynugroho666.github.io/1ntrovert-cheat-checker/cheat-version.txt").Contains("1.6.30"))
                {
                    if (MessageBox.Show("Looks like there is an update! Do you want to download it? it may take a few minutes", "1ntrovert Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        using (var client = new WebClient())
                        {
                            Process.Start("1ntrovert_Updater.exe");
                            this.Close();
                        }
                }
            }
            catch
            {

            }
        }

        //// Discord RPC
        void Initialize()
        {
            client = new DiscordRpcClient("968545705306234900");
            client.Initialize();
            client.SetPresence(new RichPresence()
            {
                Timestamps = Timestamps.Now,

                Assets = new Assets()
                {
                    SmallImageKey = "dbd_small",
                }
            });
        }

        void Update_client()
        {
            client.Invoke();
        }

        void Deinitialize()
        {
            if (client != null)
                client.Dispose();
        }

        //////////////////// Program
        private async void Form1_Load(object sender, EventArgs e)
        {
            // Action to prevent any 3-rd party tool from 'easy scan' in process list
            this.Text = Req.EncryptProgramName();

            await Task.Run(() =>
            {
                Ver_Check();
                // Auto Update Market & FullProfile
                NetServices.REQUEST_DOWNLOAD("https://rizkynugroho666.github.io/1ntrovert-cheat-checker/Market.json", "Market\\Market.json");
                NetServices.REQUEST_DOWNLOAD("https://rizkynugroho666.github.io/1ntrovert-cheat-checker/FullProfile.txt", "FullProfile\\FullProfile.txt");
                Thread.Sleep(2000); // Wait 2 secs, make sure the Market & FullProfile Completely Downloaded
            });
        }
        protected private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            FiddlerCore.Stop();
            Req.DisableProxy();
        }
        private void Form1_Handle_Tray(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Show();
                IntrovertTray.Visible = false;
            }
        }
        private void InitializeSettings()
        {
            try
            {
                switch (Req.REGISTRY_VALUE_THEME)
                {
                    case "Default":
                        Req.INITIALIZEDTHEME = 0;
                        this.BackColor = Color.White;
                        panel1.BackColor = Color.Azure;
                        label4.ForeColor = Color.Black;
                        label5.ForeColor = Color.Black;
                        checkBox_InjectFullProfile.ForeColor = Color.Black;
                        checkBox_SpoofCurrencies.ForeColor = Color.Black;
                        checkBox5.ForeColor = Color.Black;
                        checkBox_Crosshair.ForeColor = Color.Black;
                        button7.BackColor = Color.Azure;
                        button8.BackColor = Color.LightGray;
                        break;
                }
            }
            catch { }
        }

        // Main Functions
        private async void Ver_Check() // DBD Version
        {
            try
            {
                NetServices.REQUEST_DOWNLOAD("https://rizkynugroho666.github.io/1ntrovert-cheat-checker/dbd-version.txt", "version.txt");
                if (File.Exists("version.txt"))
                {
                    GameVersion = File.ReadAllText("version.txt");
                    label2.Invoke(new Action(() => { label2.Text = $"{GameVersion}"; }));
                }
            }
            catch { }
        }

        private async void SHOW_QUEUE()
        {
            try
            {
                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (Req.QueuePosition == null || (label5.Text.Length == 23 && Req.QueuePosition.Length == 7))
                            label5.Invoke(new Action(() => { label5.Text = "Matchmaking Queue: -"; }));

                        else
                        {
                            if (label5.Text.Length == 20 && Req.QueuePosition.Length == 7)
                                label5.Invoke(new Action(() => { label5.Text = "Matchmaking Queue: -"; }));
                            else
                                label5.Invoke(new Action(() => { label5.Text = $"Matchmaking Queue: {Req.QueuePosition}"; }));
                        }
                        Thread.Sleep(2500);
                    }
                });
            }
            catch { }
        }
        private async void SHOW_BHVRSESSION() // Cookie Grabber
        {
            try
            {
                await Task.Run(() =>
                {
                    while (true)
                    {
                        if (Req.BhvrSession != null && Req.BhvrSession != "")
                        {
                            if (textBox5.Text.Length == 33)
                                button8.Invoke(new Action(() => { button8.Visible = true; }));
                            textBox5.Invoke(new Action(() => { textBox5.Text = Req.BhvrSession; }));
                        }

                        Thread.Sleep(8000);
                    }
                });
            }
            catch { }
        }
        private void readMarketJsonApi() // Get & Read Market.json
        {
            try
            {
                if (File.Exists("Market\\Market.json"))
                {
                    Req.MarketJson = File.ReadAllText("Market\\Market.json");
                }
                else
                {
                    MessageBox.Show("An error occurred while trying to connect to the 1ntrovertskrrt's Market server", Req.PROGRAM_EXECUTABLE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch
            {
                MessageBox.Show("Please wait a moment, still updating the Market!", Req.PROGRAM_EXECUTABLE, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                checkBox_Unlocker.Checked = false;
            }
        }
        private void readFullProfileApi() // Get & Read FullProfile.txt
        {
            try
            {
                if (File.Exists("FullProfile\\FullProfile.txt")) 
                {
                    Req.FullProfile = File.ReadAllText("FullProfile\\FullProfile.txt");
                }
                else
                {
                    MessageBox.Show("An error occurred while trying to connect to the 1ntrovertskrrt's FullProfile server", Req.PROGRAM_EXECUTABLE, MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            catch
            {
                MessageBox.Show("Please wait a moment, still updating the FullProfile!", Req.PROGRAM_EXECUTABLE, MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
                checkBox_InjectFullProfile.Checked = false;
            }
        }

        // Buttons
        private void button1_Click(object sender, EventArgs e) => this.Close();
        private void button2_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;
        private async void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Capture = false; await Task.Run(() =>
            {
                Message mouse = Message.Create(Handle, 0xa1, new IntPtr(2), IntPtr.Zero);
                WndProc(ref mouse);
            });
        }
        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            IntrovertTray.Icon = Properties.Resources.icon_tray;
            IntrovertTray.MouseClick += new MouseEventHandler(Form1_Handle_Tray);
            IntrovertTray.Visible = true;

            using (NotifyIcon Notify = new NotifyIcon())
            {
                Notify.BalloonTipTitle = Req.PROGRAM_EXECUTABLE;
                Notify.BalloonTipText = "Launcher is hidden to tray.";
                Notify.BalloonTipIcon = ToolTipIcon.Info;
                Notify.Icon = this.Icon;
                Notify.Visible = true;
                Notify.ShowBalloonTip(1000);
                Notify.Dispose();
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            FiddlerCore.Start();

            SHOW_BHVRSESSION();
            Button_StartCheat.Visible = false;
            label9.Text = "Running";
            label9.ForeColor = Color.Lime;
            isFiddlerCoreActive = true;
        }
        private void button4_Click(object sender, EventArgs e)
        {
            FiddlerCore.Stop();
            Button_StartCheat.Visible = true;
            FiddlerCore.RemoveRootCertificate();
            Req.DisableProxy();
            isFiddlerCoreActive = false;

            checkBox_Unlocker.Checked = false;
            checkBox_MatchmakingQueue.Checked = false;
            checkBox_InjectFullProfile.Checked = false;
            checkBox_SpoofCurrencies.Checked = false;
            checkBox_Crosshair.Checked = false;
            checkBox_DiscordRPC.Checked = false;
            label9.Text = "Stopped";
            label9.ForeColor = Color.Red;
        }
        private void button6_Click(object sender, EventArgs e)
        {
            string data = "What do you mean enter license bro? this is a free cheat, if you bought it from somewhere, you got scammed xD";

            MessageBox.Show(data, "Bruh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void button8_Click(object sender, EventArgs e) => Clipboard.SetText(textBox5.Text);

        // CheckBoxes

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            isProgramInitialized = true;
            if (checkBox_InjectFullProfile.Checked == true)
            {
                readFullProfileApi();
                Req.bool_FullProfileInject = true;
            }

            else
                Req.bool_FullProfileInject = false;

            if (isFiddlerCoreActive == true)
                MessageBox.Show("Changes was made when dbd is already running! Restart your game to see the changes, and STOP the cheat and press START again!", Req.PROGRAM_EXECUTABLE, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_SpoofCurrencies.Checked == true)
            {
                SetAmount.Show();
                Req.bool_SpoofCurrency = true;
            }
            else
            {
                SetAmount.Hide();
                Req.bool_SpoofCurrency = false;
            }
        }
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Crosshair.Checked == true)
            {
                CROSSHAIR.Show();
            }
            else
            {
                CROSSHAIR.Hide();
            }
        }
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_MatchmakingQueue.Checked == true)
            {
                label5.Visible = true;
                SHOW_QUEUE();
            }
            else
                label5.Visible = false;
        }
        private void checkBox10_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_DiscordRPC.Checked == true)
                Initialize();

            if (checkBox_DiscordRPC.Checked == false)
                Deinitialize();
        }

        private void checkBox9_CheckedChanged(object sender, EventArgs e)
        {

            isProgramInitialized = true;

            if (isProgramInitialized == false)
                return;

            if (isFiddlerCoreActive == true)
                MessageBox.Show("Changes was made when dbd is already running! Restart your game to see the changes, and STOP the cheat and press START again!", Req.PROGRAM_EXECUTABLE, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);

            if (checkBox_Unlocker.Checked == true)
            {
                Req.bool_MarketInject = true;
                readMarketJsonApi();
            }
            else
                Req.bool_MarketInject = false;
        }

        // Labels and TextBox
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {

        }  

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }     

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        // null var
        public static string GameVersion = null;

    }
}
