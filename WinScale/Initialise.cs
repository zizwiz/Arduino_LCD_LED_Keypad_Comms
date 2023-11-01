using System;
using System.IO.Ports;
using System.Windows.Forms;

namespace WinScaleMoneyCounter
{
    public partial class Form1
    {
        

        private void InitializeControlValues()
        {
            cmbParity.Items.Clear(); cmbParity.Items.AddRange(Enum.GetNames(typeof(Parity)));
            cmbStopBits.Items.Clear(); cmbStopBits.Items.AddRange(Enum.GetNames(typeof(StopBits)));

            cmbParity.Text = settings.Parity.ToString();
            cmbStopBits.Text = settings.StopBits.ToString();
            cmbDataBits.Text = settings.DataBits.ToString();
            cmbParity.Text = settings.Parity.ToString();
            cmbBaudRate.Text = settings.BaudRate.ToString();
            

           RefreshComPortList();

            // If it is still avalible, select the last com port used
            if (cmbPortName.Items.Contains(settings.PortName)) cmbPortName.Text = settings.PortName;
            else if (cmbPortName.Items.Count > 0) cmbPortName.SelectedIndex = cmbPortName.Items.Count - 1;
            else
            {
                MessageBox.Show(this, "There are no COM Ports detected on this computer.\nPlease install a COM Port and restart this app.", "No COM Ports Installed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        /// <summary> Enable/disable controls based on the app's current state. </summary>
        private void EnableControls()
        {
            // Enable/disable controls based on whether the port is open or not
            cmbPortName.Enabled = cmbBaudRate.Enabled = cmbParity.Enabled = cmbDataBits.Enabled = cmbStopBits.Enabled = !serialPort1.IsOpen;
            

            if (serialPort1.IsOpen)
            {
                btnOpenPort.Text = "&Close Port";
                cmbPortName.Enabled = cmbBaudRate.Enabled = cmbParity.Enabled = cmbDataBits.Enabled = cmbStopBits.Enabled = false;

            }
            else
            {
                btnOpenPort.Text = "&Open Port";
                cmbPortName.Enabled = cmbBaudRate.Enabled = cmbParity.Enabled = cmbDataBits.Enabled = cmbStopBits.Enabled = true;
            }
        }

        /// <summary> Save the user's settings. </summary>
        private void SaveSettings()
        {
            settings.BaudRate = int.Parse(cmbBaudRate.Text);
            settings.DataBits = int.Parse(cmbDataBits.Text);
            settings.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
            settings.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
            settings.PortName = cmbPortName.Text;
            
            settings.Save();
        }
    }
}
