using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paisios
{
    public partial class Form1
    {
        private async void btn_Send_1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Int32.Parse(txtbx_iterations1.Text); i++) // times to send file
            {
                SendData(txtSendData_1.Text);
                await Task.Delay(Int32.Parse(txtbx_delay1.Text)); //add any delay between sends
            }
        }

        private void txtSendData_1_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses [ENTER], send the data now
            if (KeyHandled = e.KeyCode == Keys.Enter)
            {
                e.Handled = true; SendData(txtSendData_1.Text);
            }
        }

        private void txtSendData_1_KeyPress(object sender, KeyPressEventArgs e)
        {
             e.Handled = KeyHandled; 
        }

        private void btn_clr1_Click(object sender, EventArgs e)
        {
            txtSendData_1.Text = "";
            txtbx_delay1.Text = "0";
            txtbx_iterations1.Text = "1";
        }

        private async void btn_Send_2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Int32.Parse(txtbx_iterations2.Text); i++) // times to send file
            {
                SendData(txtSendData_2.Text);
                await Task.Delay(Int32.Parse(txtbx_delay2.Text)); //add any delay between sends
            }
        }

        private void txtSendData_2_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses [ENTER], send the data now
            if (KeyHandled = e.KeyCode == Keys.Enter)
            {
                e.Handled = true; SendData(txtSendData_2.Text);
            }
        }

        private void txtSendData_2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = KeyHandled;
        }

        private void btn_clr2_Click(object sender, EventArgs e)
        {
            txtSendData_2.Text = "";
            txtbx_delay2.Text = "0";
            txtbx_iterations2.Text = "1";
        }

        private async void btn_Send_3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Int32.Parse(txtbx_iterations3.Text); i++) // times to send file
            {
                SendData(txtSendData_3.Text);
                await Task.Delay(Int32.Parse(txtbx_delay3.Text)); //add any delay between sends
            }
        }

        private void txtSendData_3_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses [ENTER], send the data now
            if (KeyHandled = e.KeyCode == Keys.Enter)
            {
                e.Handled = true; SendData(txtSendData_3.Text);
            }
        }

        private void txtSendData_3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = KeyHandled;
        }

        private void btn_clr3_Click(object sender, EventArgs e)
        {
            txtSendData_3.Text = "";
            txtbx_delay3.Text = "0";
            txtbx_iterations3.Text = "1";
        }

        private async void btn_Send_4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Int32.Parse(txtbx_iterations4.Text); i++) // times to send file
            {
                SendData(txtSendData_4.Text);
                await Task.Delay(Int32.Parse(txtbx_delay4.Text)); //add any delay between sends
            }
        }

        private void txtSendData_4_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses [ENTER], send the data now
            if (KeyHandled = e.KeyCode == Keys.Enter)
            {
                e.Handled = true; SendData(txtSendData_4.Text);
            }
        }

        private void txtSendData_4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = KeyHandled;
        }
        private void btn_clr4_Click(object sender, EventArgs e)
        {
            txtSendData_4.Text = "";
            txtbx_delay4.Text = "0";
            txtbx_iterations4.Text = "1";
        }

        private async void btn_Send_5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Int32.Parse(txtbx_iterations5.Text); i++) // times to send file
            {
                SendData(txtSendData_5.Text);
                await Task.Delay(Int32.Parse(txtbx_delay5.Text)); //add any delay between sends
            }
        }

        private void txtSendData_5_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses [ENTER], send the data now
            if (KeyHandled = e.KeyCode == Keys.Enter)
            {
                e.Handled = true; SendData(txtSendData_5.Text);
            }
        }

        private void txtSendData_5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = KeyHandled;
        }

        private void btn_clr5_Click(object sender, EventArgs e)
        {
            txtSendData_5.Text = "";
            txtbx_delay5.Text = "0";
            txtbx_iterations5.Text = "1";
        }
        private async void btn_Send_6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Int32.Parse(txtbx_iterations6.Text); i++) // times to send file
            {
                SendData(txtSendData_6.Text);
                await Task.Delay(Int32.Parse(txtbx_delay6.Text)); //add any delay between sends
            }
        }

        private void txtSendData_6_KeyDown(object sender, KeyEventArgs e)
        {
            // If the user presses [ENTER], send the data now
            if (KeyHandled = e.KeyCode == Keys.Enter)
            {
                e.Handled = true; SendData(txtSendData_6.Text);
            }
        }

        private void txtSendData_6_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = KeyHandled;
        }

        private void btn_clr6_Click(object sender, EventArgs e)
        {
            txtSendData_6.Text = "";
            txtbx_delay6.Text = "0";
            txtbx_iterations6.Text = "1";
        }

        

    }
}
