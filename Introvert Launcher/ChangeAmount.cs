using System;
using System.Windows.Forms;

namespace Introvert_Launcher
{
    public partial class ChangeAmount : Form
    {
        public ChangeAmount()
        {
            InitializeComponent();
            textBox1.Text = "0";
            textBox2.Text = "0";
            textBox3.Text = "0";
        }
        public static bool isKeypressDigit(KeyPressEventArgs e, string currentstring)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != 8 && e.KeyChar != 127)
                return false;
            else if (currentstring != "")
            {
                if (currentstring[0] == '9')
                {
                    if (e.KeyChar >= 49 && e.KeyChar <= 57 && e.KeyChar != 8)
                        return false;
                }
            }
            return true;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) => Req.value_BloodPoints = textBox1.Text;

        private void textBox2_TextChanged(object sender, EventArgs e) => Req.value_Shards = textBox2.Text;

        private void textBox3_TextChanged(object sender, EventArgs e) => Req.value_AuricCells = textBox3.Text;

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Req.value_BloodPoints))
                e.Handled = true;
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Req.value_Shards))
                e.Handled = true;
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!isKeypressDigit(e, Req.value_AuricCells))
                e.Handled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ChangeAmount_Load(object sender, EventArgs e)
        {
            this.Text = "Spoof BP/Shards/Auric Cells Visual";
            this.Top -= -20; //this.Top - Convert.ToInt32(Properties.Settings.Default.zIndex);
            this.Left -= 51; //this.Left - Convert.ToInt32(Properties.Settings.Default.xIndex);
        }

        private void ChangeAmount_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }
    }
}
