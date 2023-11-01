using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace WinScaleMoneyCounter
{
    public partial class Form1
    {

        private void tmrCheckComPorts_Tick(object sender, EventArgs e)
        {
            // checks to see if COM ports have been added or removed
            // since it is quite common now with USB-to-Serial adapters
            RefreshComPortList();
        }

        private void cmbBaudRate_Validating(object sender, CancelEventArgs e)
        { int x; e.Cancel = !int.TryParse(cmbBaudRate.Text, out x); }

        private void cmbDataBits_Validating(object sender, CancelEventArgs e)
        { int x; e.Cancel = !int.TryParse(cmbDataBits.Text, out x); }

        private string[] OrderedPortNames()
        {
            // Just a placeholder for a successful parsing of a string to an integer
            int num;

            // Order the serial port names in numberic order (if possible)
            return SerialPort.GetPortNames().OrderBy(a => a.Length > 3 && int.TryParse(a.Substring(3), out num) ? num : 0).ToArray();
        }

        private string RefreshComPortList(IEnumerable<string> PreviousPortNames, string CurrentSelection, bool PortOpen)
        {
            // Create a new return report to populate
            string selected = null;

            // Retrieve the list of ports currently mounted by the operating system (sorted by name)
            string[] ports = SerialPort.GetPortNames();

            // First determain if there was a change (any additions or removals)
            bool updated = PreviousPortNames.Except(ports).Count() > 0 || ports.Except(PreviousPortNames).Count() > 0;

            // If there was a change, then select an appropriate default port
            if (updated)
            {
                // Use the correctly ordered set of port names
                ports = OrderedPortNames();

                // Find newest port if one or more were added
                string newest = SerialPort.GetPortNames().Except(PreviousPortNames).OrderBy(a => a).LastOrDefault();

                // If the port was already open... (see logic notes and reasoning in Notes.txt)
                if (PortOpen)
                {
                    if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else selected = ports.LastOrDefault();
                }
                else
                {
                    if (!String.IsNullOrEmpty(newest)) selected = newest;
                    else if (ports.Contains(CurrentSelection)) selected = CurrentSelection;
                    else selected = ports.LastOrDefault();
                }
            }

            // If there was a change to the port list, return the recommended default selection
            return selected;
        }
        
        private void RefreshComPortList()
        {
            // Determain if the list of com port names has changed since last checked
            string selected = RefreshComPortList(cmbPortName.Items.Cast<string>(), cmbPortName.SelectedItem as string, serialPort1.IsOpen);

            // If there was an update, then update the control showing the user the list of port names
            if (!String.IsNullOrEmpty(selected))
            {
                cmbPortName.Items.Clear();
                cmbPortName.Items.AddRange(OrderedPortNames());
                cmbPortName.SelectedItem = selected;
            }
        }

        private void chkDTR_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.DtrEnable = chkDTR.Checked;
        }

        private void chkRTS_CheckedChanged(object sender, EventArgs e)
        {
            serialPort1.RtsEnable = chkRTS.Checked;
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            // If the com port has been closed, do nothing
            if (!serialPort1.IsOpen) return;

            // This method will be called when there is data waiting in the port's buffer
            
            // Determain which mode (string or binary) the user is in
            
                // Obtain the number of bytes waiting in the port's buffer
                int bytes = serialPort1.BytesToRead;

                // Create a byte array buffer to hold the incoming data
                byte[] buffer = new byte[bytes];

                // Read the data from the port and store it in our buffer
                serialPort1.Read(buffer, 0, bytes);


                Log(LogMsgType.Incoming, ConvertByteArraytoString(buffer));

        }

        private void serialPort1_PinChanged(object sender, SerialPinChangedEventArgs e)
        {
            // Show the state of the pins
            UpdatePinState();
        }

        private void UpdatePinState()
        {
            this.Invoke(new ThreadStart(() => {
                // Show the state of the pins
                chkCD.Checked = serialPort1.CDHolding;
                chkCTS.Checked = serialPort1.CtsHolding;
                chkDSR.Checked = serialPort1.DsrHolding;
            }));
        }

        /// <summary> Send the user's data currently entered in the 'send' box.</summary>
        private void SendData(string DataToSend)
        {
           try
                {
                   // Send the binary data out the port
                    serialPort1.Write(DataToSend);
                    Log(LogMsgType.Outgoing, DataToSend + "\n"); //display in debug window

                
            }
                catch (FormatException)
                {
                    // Inform the user if the hex string was not properly formatted
                    Log(LogMsgType.Error, "Not properly formatted hex string: " + DataToSend + "\n");
                }
           
        }

        
        private void btnOpenPort_Click(object sender, EventArgs e)
        {
            bool error = false;

            // If the port is open, close it.
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                btn_send.Enabled = false;
            }
            else
            {
                // Set the port's settings
                serialPort1.BaudRate = int.Parse(cmbBaudRate.Text);
                serialPort1.DataBits = int.Parse(cmbDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cmbStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cmbParity.Text);
                serialPort1.PortName = cmbPortName.Text;

                try
                {
                    // Open the port
                    serialPort1.Open();
                    btn_send.Enabled = true;
                }
                catch (UnauthorizedAccessException) { error = true; }
                catch (IOException) { error = true; }
                catch (ArgumentException) { error = true; }

                if (error) MessageBox.Show(this, "Could not open the COM port.  Most likely it is already in use, has been removed, or is unavailable.", "COM Port Unavalible", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                else
                {
                    // Show the initial pin states
                    UpdatePinState();
                    chkDTR.Checked = serialPort1.DtrEnable;
                    chkRTS.Checked = serialPort1.RtsEnable;
                }
            }

            // Change the state of the form's controls
            EnableControls();

            
        }


    }
}
