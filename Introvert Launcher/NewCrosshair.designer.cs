namespace Introvert_Launcher {
    partial class NewCrosshair {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose ( bool disposing ) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent () {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewCrosshair));
            this.crosshairBox = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.crosshairBox)).BeginInit();
            this.SuspendLayout();
            // 
            // crosshairBox
            // 
            this.crosshairBox.BackColor = System.Drawing.Color.Transparent;
            this.crosshairBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.crosshairBox.Location = new System.Drawing.Point(0, 0);
            this.crosshairBox.Name = "crosshairBox";
            this.crosshairBox.Size = new System.Drawing.Size(32, 32);
            this.crosshairBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.crosshairBox.TabIndex = 0;
            this.crosshairBox.TabStop = false;
            // 
            // NewCrosshair
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(32, 32);
            this.ControlBox = false;
            this.Controls.Add(this.crosshairBox);
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewCrosshair";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NewCrosshair_FormClosing);
            this.Load += new System.EventHandler(this.Crosshair_Load);
            ((System.ComponentModel.ISupportInitialize)(this.crosshairBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox crosshairBox;

    }
}

