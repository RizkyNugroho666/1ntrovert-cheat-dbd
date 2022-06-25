using System;
using System.Drawing;
using System.Windows.Forms;

namespace Introvert_Launcher {
    public partial class NewCrosshair : Form {
        public NewCrosshair() {
            InitializeComponent();
            Color transparentColor = Color.Black;
            this.TransparencyKey = transparentColor;
            this.BackColor = transparentColor;
        }

        private void Crosshair_Load( object sender, EventArgs e ) {
            this.Text = Req.EncryptProgramName();
            this.Top -= -20; //this.Top - Convert.ToInt32(Properties.Settings.Default.zIndex);
            this.Left -= 51; //this.Left - Convert.ToInt32(Properties.Settings.Default.xIndex);
            this.crosshairBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(Properties.Settings.Default.crosshair);
        }

        public void Crosshair_Refresh () {
            this.Top = this.Top - Convert.ToInt32(Properties.Settings.Default.zIndex);
            this.Left = this.Left - Convert.ToInt32(Properties.Settings.Default.xIndex);
            this.crosshairBox.Image = (Image)Properties.Resources.ResourceManager.GetObject(Properties.Settings.Default.crosshair);
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                /* WS_EX_LAYERED |  WS_EX_TRANSPARENT */ 
                cp.ExStyle |= 0x80000  | 0x20 ;
                return cp;
            }
        }

        private void NewCrosshair_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
