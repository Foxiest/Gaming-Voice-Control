using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GamingVoiceControl
{
    public partial class GVC : Form
    {
        Dictionary<string, string> ControlDict = new Dictionary<string, string>();
        SpeechRecognitionEngine SRE = new SpeechRecognitionEngine();

        public GVC()
        {
            InitializeComponent();
            ControlGrid.RowStateChanged += ControlGrid_RowStateChanged;
            
        }

        private void ControlGrid_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
            UpdatePhrasesButton.Enabled = true;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            ControlGrid.Rows.Add();
        }

        private void RemoveInputButton_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in ControlGrid.SelectedRows)
            {
                    if (!row.IsNewRow)
                    {
                        ControlGrid.Rows.Remove(row);
                    }
            }
        }

        private void ControlGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void UpdatePhrasesButton_Click(object sender, EventArgs e)
        {
            //make sre and enable
            ControlDict = new Dictionary<string, string>();
            foreach(DataGridViewRow DVR in ControlGrid.Rows)
            {
                if (!DVR.IsNewRow)
                {
                    if (DVR.Cells[0].Value != null && DVR.Cells[1].Value != null)
                    {
                        ControlDict.Add((string)DVR.Cells[0].Value, (string)DVR.Cells[1].Value);
                        MessageBox.Show((string)DVR.Cells[0].Value + " " + (string)DVR.Cells[1].Value);
                    }
                }
            }

            UpdatePhrasesButton.Enabled = false;
            GenerateSRE();
        }

        private void GenerateSRE()
        {

        }
    }
}
