using Libdmtx;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DMTXTestApp
{
    
    public partial class Form1 : Form
    {

        [DllImport("libdmtx.dll", EntryPoint = "dmtxEncodeCreate")]
        public static extern byte dmtxEncodeCreate();

        [DllImport("libdmtx.dll", EntryPoint = "dmtx_encode")]
        private static extern byte DmtxEncode(
            [In] byte[] plain_text,
            [In] UInt16 text_size,
            [Out] out IntPtr result,
            [In] EncodeOptions options);

        [DllImport("libdmtx.dll", EntryPoint = "dmtxVersion")]
        private static extern string dmtxVersion();
       

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        { //decoding
            // C# Example
            Libdmtx.DecodeOptions o = new Libdmtx.DecodeOptions();
            Bitmap b = new Bitmap(@"bitmap.png");
            Libdmtx.DmtxDecoded[] res = Dmtx.Decode(b, o);
            for (uint i = 0; i < res.Length; i++)
            {
                string str = Encoding.ASCII.GetString(res[i].Data).TrimEnd('\0');
                Console.WriteLine("Code " + i + ": " + str);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //// C# Example
            Libdmtx.EncodeOptions o = new Libdmtx.EncodeOptions();
            byte[] dataToEncode = Encoding.ASCII.GetBytes("Hello World!");
            Libdmtx.DmtxEncoded en = Dmtx.Encode(dataToEncode, o);
            pictureBox1.Image = en.Bitmap;

            //byte b  = dmtxEncodeCreate();
            //string s = dmtxVersion();

        }
    }
}
