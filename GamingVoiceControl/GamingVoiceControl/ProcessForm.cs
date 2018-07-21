using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamingVoiceControl
{
    public partial class ProcessForm : Form
    {
        public string ProcessName = "";
        public ProcessForm()
        {
            InitializeComponent();
            //display process list
            GetProcesses();
        }

        private void  GetProcesses()
        {
            ProcessList.Items.Clear();
            var Processes = Process.GetProcesses();
            foreach(Process P in Processes)
            {
                ProcessList.Items.Add(P.ProcessName);
            }
        }

        public void SelectButton_Click(object sender, EventArgs e)
        {
            if (ProcessList.SelectedItem != null)
            {
                ProcessName = ProcessList.SelectedItem.ToString();
            }
            else
            {
                ProcessName = "";
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void UpdateListButton_Click(object sender, EventArgs e)
        {
            GetProcesses();
        }
    }
}
