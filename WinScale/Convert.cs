using System;
using System.Drawing;
using System.Linq;
using System.Text;

namespace WinScaleMoneyCounter
{
    public partial class Form1
    {

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //// Convert Hex String to Byte array
        /// <summary> Convert a string of hex digits (ex: E4 CA B2) to a byte array. </summary>
        /// <param name="s"> The string containing the hex digits (with or without spaces). </param>
        /// <returns> Returns an array of bytes. </returns>
        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static byte[] HexStringToByteArray(string s)
        {

            s = s.Replace(" ", "");
            byte[] buffer = new byte[s.Length / 2];
            for (int i = 0; i < s.Length; i += 2)
                buffer[i / 2] = (byte)Convert.ToByte(s.Substring(i, 2), 16);
            return buffer;

        }

        /////////////////////////////////////////////////////////////////////////////////////////////
        ///   Convert Byte Array to Hex String 
        /// <summary> Converts an array of bytes into a formatted string of hex digits (ex: E4 CA B2)</summary>
        /// <param name="data"> The array of bytes to be translated into a string of hex digits. </param>
        /// <returns> Returns a well formatted string of hex digits with spacing. </returns>
        /////////////////////////////////////////////////////////////////////////////////////////////

        public static string ByteArrayToHexString(byte[] data)
        {
            StringBuilder sb = new StringBuilder(data.Length * 3);
            foreach (byte b in data)
                sb.Append(Convert.ToString(b, 16).PadLeft(2, '0').PadRight(3, ' '));
            return sb.ToString().ToUpper();
        }


        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ////                        Convert ASCII string to Hex String
        /// </summary>
        //////////////////////////////////////////////////////////////////////////////////////////

        public static string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }


        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ////                        Convert Hex String to ASCII string 
        /// </summary>
        //////////////////////////////////////////////////////////////////////////////////////////

        public static string ConvertHexToString(string HexValue)
        {
            //string StrValue = "";
            //while (HexValue.Length > 0)
            //{
            //    StrValue += System.Convert.ToChar(System.Convert.ToUInt32(HexValue.Substring(0, 2), 16)).ToString();
            //    HexValue = HexValue.Substring(2, HexValue.Length - 2);
            //}
            //return StrValue;

            if (HexValue == null)
                throw new ArgumentNullException("hexString");
            if (HexValue.Length % 2 != 0)
                throw new ArgumentException("hexString must have an even length", "hexString");
            var bytes = new byte[HexValue.Length / 2];
            for (int i = 0; i < bytes.Length; i++)
            {
                string currentHex = HexValue.Substring(i * 2, 2);
                bytes[i] = Convert.ToByte(currentHex, 16);
            }

            return System.Text.Encoding.ASCII.GetString(bytes);

        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ////                        Convert Unicode to ASCII string 
        ////                convert from two byte unicode [page number, char on page].
        /// </summary>
        //////////////////////////////////////////////////////////////////////////////////////////


        public static string ConvertFromUnicode(string str)

        {
            char c = ' ';
            string rtstr = "";

            for (int i = 0; i < str.Length; i += 4)
            {
                string str1 = str.Substring(i, 4);
                c = (char)Int16.Parse(str1, System.Globalization.NumberStyles.HexNumber);
                rtstr += c;
            }
            return rtstr;

        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ////                        Convert ASCII String to Unicode 
        ////                convert from two byte unicode [page number, char on page].
        /// </summary>
        //////////////////////////////////////////////////////////////////////////////////////////

        public static string ConverttoUnicode(string str)
        {

            char c = ' ';

            return ((int)c).ToString("X4");
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ////                        Convert binary string to byte array 
        ////                convert from two byte unicode [page number, char on page].
        /// </summary>
        //////////////////////////////////////////////////////////////////////////////////////////
        public static byte[] BinaryStringToByteArray(string binaryStr)
        {
            /* int numOfBytes = input.Length/6;
             byte[] bytes = new byte[numOfBytes];
             for (int i = 0; i < numOfBytes; ++i)
             {
                 bytes[i] = Convert.ToByte(input.Substring(6*i, 6), 2);
             }*/

            var byteArray = Enumerable.Range(0, int.MaxValue / 6)
                .Select(i => i * 6)
                .TakeWhile(i => i < binaryStr.Length)
                .Select(i => binaryStr.Substring(i, 6))
                .Select(s => Convert.ToByte(s, 2))
                .ToArray();

            return byteArray;
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        ////                        Convert ASCII String to Unicode 
        ////                convert from two byte unicode [page number, char on page].
        /// </summary>
        //////////////////////////////////////////////////////////////////////////////////////////

        public static string ConvertStringToUnicode(char[] characters, int end_null)

        {
            string rtstr = "";

            for (int i = 0; i < characters.Length; i += 1)
            {
                rtstr += ((int)characters[i]).ToString("X4");
            }

            if (end_null == 1)
            {
                return rtstr + "0000"; //NULL terminated strings
            }
            else
            {
                return rtstr;
            }


        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Receives string and returns the string with its letters reversed.
        /// </summary>
        /// //////////////////////////////////////////////////////////////////////////////////////

        public static string ReverseString(string s)
        {
            char[] arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Receives Binary string and returns the string with its letters reversed.
        /// </summary>
        /// //////////////////////////////////////////////////////////////////////////////////////

        public static string ReverseBinaryString(string bin_data)
        {
            string[] bytes = new string[bin_data.Length];

            for (int i = 0; i < bin_data.Length; i = i + 2)
            {
                bytes[i] = bin_data.Substring(i, 2);
            }
            Array.Reverse(bytes, 0, bytes.Length);
            return string.Join("", bytes);
        }

        //////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Convert byte array to ASCII string.
        /// </summary>
        /// //////////////////////////////////////////////////////////////////////////////////////

        public static string ConvertByteArraytoString(byte[] data)
        {
            char[] characters = data.Select(b => (char)b).ToArray();
            return new string(characters);
        }

        /// <summary> Log data to the terminal window. </summary>
        /// <param name="msgtype"> The type of message to be written. </param>
        /// <param name="msg"> The string containing the message to be shown. </param>
        private void Log(LogMsgType msgtype, string msg)
        {
            rchtxbx_output.Invoke(new EventHandler(delegate
            {
                if (rchtxbx_output.Lines.Length > 1000
                ) //remove 500 lines when over 1000 to make sure we do not slow down.
                {
                    rchtxbx_output.Select(0,
                        rchtxbx_output.GetFirstCharIndexFromLine(rchtxbx_output.Lines.Length - 500));
                    rchtxbx_output.SelectedText = "";
                }

                rchtxbx_output.SelectedText = string.Empty;
                rchtxbx_output.SelectionFont = new Font(rchtxbx_output.SelectionFont, FontStyle.Bold);
                rchtxbx_output.SelectionColor = LogMsgTypeColor[(int)msgtype];

                rchtxbx_output.AppendText(msg);
                rchtxbx_output.ScrollToCaret();
            }));

        }
    }
}
