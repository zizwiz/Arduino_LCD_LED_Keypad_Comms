using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using WinScaleMoneyCounter.Properties;


namespace WinScaleMoneyCounter
{
    #region Public Enumerations
    public enum DataMode { Text, Hex }
    public enum LogMsgType { Incoming, Outgoing, Normal, Warning, Error };
    #endregion

    public partial class Form1 : Form
    {
        #region Private Variables

        // Various colors for logging info
        private Color[] LogMsgTypeColor = { Color.Blue, Color.Green, Color.Black, Color.Orange, Color.Red };
        private Settings settings = Settings.Default;

        #endregion

        public Form1()
        {
            InitializeComponent();
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Restore the users settings
            InitializeControlValues();

            // Enable/disable controls based on the current state
            EnableControls();
            btn_send.Enabled = false;

            Text += " : v" + Assembly.GetExecutingAssembly().GetName().Version; // put in the version number
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            rchtxbx_output.Clear();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveSettings();
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
        }
        
        private void btn_send_Click(object sender, EventArgs e)
        {
            SendData(DateTime.Now.ToString("T"));
        }
    }
}
