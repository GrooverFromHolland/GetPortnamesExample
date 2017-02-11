using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Management;
using System.Threading;


namespace GetPortnamesExample
{

   public partial class Form1 : Form
   {
      private Dictionary<string, string> ComPortInformation =
                                new Dictionary<string, string>(); //key = comport name, value = friendly name
      SerialPort SelectedPort;
      string SelectedPortPortname;
      public Form1()
      {
         InitializeComponent();
         button1.Enabled = false;
         listBoxComports.Enabled = false;
         SetupComPortInformation();
      }
      private void SetupComPortInformation()
      {
         string[] portNames = SerialPort.GetPortNames();// Get real serial ports
         string[] serialDevices;// Stores all other devices that have serial capablities
         foreach (string s in portNames)
         {
            if (!ComPortInformation.ContainsKey(s))
            {
               ComPortInformation.Add(s, s);// SerialPort.GetPortNames() gives no friendly names.
            }
         }

         List<string> otherSerial = new List<string>();
         List<ManagementObject> listObj = new List<ManagementObject>();//using System.Management;
         try
         {
            // get only devices that are working properly."
            string query = "SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0";// get only devices that are working properly."
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
            listObj = searcher.Get().Cast<ManagementObject>().ToList();
            searcher.Dispose();
         }
         catch (Exception ex)
         {
            MessageBox.Show(ex.ToString());
            otherSerial = new List<string>(); // clear the list because there are errors
                                              // addcode here to handle the error.....
         }
         foreach (ManagementObject obj in listObj)
         {
            object captionObj = obj["Caption"]; //This will get you the friendly name.
            if (captionObj != null)
            {
               string caption = captionObj.ToString();
               if (caption.Contains("(COM")) //This is where I filter on COM-ports.
               {
                  otherSerial.Add(caption);
               }
            }
         }
         serialDevices = otherSerial.Distinct().OrderBy(s => s).ToArray();

         foreach (string s in serialDevices)
         {
            // Name will be like "USB Bridge (COM14)"
            if (s == null) continue;
            int start = s.IndexOf("(COM", StringComparison.Ordinal) + 1;
            if (start >= 0)
            {
               string sub;
               string sub4;
               string sub5;
               string frnd;
               frnd = s.Remove(start).Trim('('); // remove the part with (comxx)
               sub = s.Replace(frnd, "").TrimStart('(').TrimEnd(')'); // get the part with "commxx", remove spaces and ()
               if (sub.Length > 4)//like  com14
               {
                  sub4 = sub.Substring(0, 4);
                  sub5 = sub4;
               }
               else sub5 = sub; //like com 4
               if (sub.Length > 4)// can be like com14 or com4 + something
               {
                  sub5 = sub.Substring(0, 5);// get first five like com14
                  char c = sub5.Last();
                  if (!char.IsDigit(c))
                  {
                     sub5.Remove(sub5.Length - 1); //it was like com4 + something so remove something
                  }
               }
               if (ComPortInformation.ContainsKey(sub5))//remove double entries
               {
                  ComPortInformation.Remove(sub5);
                  ComPortInformation.Add(frnd + ": " + sub5, sub5); //like key = "com4: , value = "USB Bridge: com4 "
               }
            }
         }
         //We have to remove listbox event handler, because binding triggers the event!
         listBoxComports.SelectedValueChanged -= listBoxComports_SelectedValueChanged;
         listBoxComports.DataSource = new BindingSource(ComPortInformation, null);
         listBoxComports.DisplayMember = "key";
         listBoxComports.ValueMember = "value";
         listBoxComports.Enabled = true;
         listBoxComports.SelectedIndex = -1; //Select nothing on startup
         listBoxComports.ClearSelected();// same as select nothing
         button1.Enabled = false;
         //Now it is save to add the event handler
         listBoxComports.SelectedValueChanged += listBoxComports_SelectedValueChanged;
      }

      // Do not leave ports open
      private void Form1_FormClosing(object sender, FormClosingEventArgs e)
      {
         if (SelectedPort != null && SelectedPort.IsOpen)
         {
            SelectedPort.Close();
         }
      }

      private void button1_Click(object sender, EventArgs e)
      {
         if (string.IsNullOrEmpty(SelectedPortPortname)) return;
         if (SelectedPort != null && SelectedPort.IsOpen)
         {
            SelectedPort.Close();
            MessageBox.Show("Closing port " + SelectedPort.PortName);
         }
         SelectedPort = new SerialPort()
         {
            PortName = SelectedPortPortname,
            BaudRate = 9600,
            DataBits = 8,
            StopBits = StopBits.One,
            Parity = Parity.Even
         };
         OpenPort(SelectedPort);
      }

      private void OpenPort(SerialPort port)
      {

         if (!port.IsOpen)
         {
            try
            {
               port.Open();
            }
            catch (Exception ex)
            {
               // Show the error, most common error is port is in use
               MessageBox.Show(ex.ToString());
            }
         }
         Thread.Sleep(500);// Give the system some time to open the port
         if (port.IsOpen)
         {
            MessageBox.Show(port.PortName + " succesfully openend");
            button1.Enabled = false;//Prevent opening of open port
         }
         else
         {
            MessageBox.Show("Could not open " + port.PortName);
         }
      }

      private void listBoxComports_SelectedValueChanged(object sender, EventArgs e)
      {
         if (listBoxComports.SelectedIndex == -1) return;
         var val = listBoxComports.SelectedValue.ToString();
         if (!string.IsNullOrEmpty(val))
            SelectedPortPortname = val;
         button1.Enabled = true;
      }
   }

}
