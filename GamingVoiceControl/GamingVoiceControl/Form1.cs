using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
//Install-Package InputSimulator if not found
using WindowsInput;
using WindowsInput.Native;

namespace GamingVoiceControl
{
    public partial class GVC : Form
    {
        //key is the phrase spoken and value is the resulting output
        Dictionary<string, string> ControlDict = new Dictionary<string, string>();
        Dictionary<string, int> DurationDict = new Dictionary<string, int>();
        SpeechRecognitionEngine SRE = new SpeechRecognitionEngine();
        public string ProcessName = "";

        [DllImport("User32.dll")]
        static extern int SetForegroundWindow(IntPtr point);

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

        public enum Status { Standby, Compiling, Working, Stopped };

        public void SetStatus(Status status)
        {
            switch (status)
            {
                case (Status.Compiling):
                    StatusLabel.Text = "Status: Compiling";
                    StatusLabel.BackColor = System.Drawing.Color.Yellow;
                    break;

                case (Status.Standby):
                    StatusLabel.Text = "Status: Ready, Press start";
                    StatusLabel.BackColor = System.Drawing.Color.Orange;
                    break;

                case (Status.Working):
                    StatusLabel.Text = "Status: Working";
                    StatusLabel.BackColor = System.Drawing.Color.Green;
                    break;

                case (Status.Stopped):
                    StatusLabel.Text = "Status: Stoped";
                    StatusLabel.BackColor = System.Drawing.Color.Red;
                    break;

                default:
                    break;
            }
        }

        private void UpdatePhrases()
        {
            SetStatus(Status.Compiling);
            //make sre and enable
            ControlDict = new Dictionary<string, string>();
            DurationDict = new Dictionary<string, int>();
            foreach (DataGridViewRow DVR in ControlGrid.Rows)
            {
                if (!DVR.IsNewRow)
                {
                    if (DVR.Cells[0].Value != null && DVR.Cells[1].Value != null)
                    {
                        ControlDict.Add((string)DVR.Cells[1].Value, (string)DVR.Cells[0].Value);
                        if (int.TryParse(Convert.ToString(DVR.Cells[2].Value), out int dur))
                        {
                            DurationDict.Add((string)DVR.Cells[1].Value, dur);
                        }
                        else
                        {
                            DurationDict.Add((string)DVR.Cells[1].Value, 10);
                            DVR.Cells[2].Value = 10;
                        }
                    }
                }
            }

            UpdatePhrasesButton.Enabled = false;
            GenerateSRE();
        }

        private void UpdatePhrasesButton_Click(object sender, EventArgs e)
        {
            UpdatePhrases();
        }

        private void GenerateSRE()
        {
            SRE = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-EN"));
            //Use the microphone
            SRE.SetInputToDefaultAudioDevice();
            Choices Words = new Choices();
            //key is the phrase spoken, value is the output command
            foreach (string Phrase in ControlDict.Keys)
            {
                Words.Add(Phrase);
            }
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(Words);
            gb.Culture = Thread.CurrentThread.CurrentCulture;
            Grammar g = new Grammar(gb);
            SRE.LoadGrammar(g);
            SRE.SpeechRecognized += SRE_SpeechRecognized;
            if (!StartButton.Enabled)
            {
                //if the startbutton is pushed, we can start recognising immediately
                SRE.RecognizeAsync(RecognizeMode.Multiple);
                SetStatus(Status.Working);
            }
            else
            {
                SetStatus(Status.Standby);
            }
        }

        private void SRE_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //make sure we're executing here
            if (!StartButton.Enabled && ControlDict.ContainsKey(e.Result.Text))
            {
                //find phrase in dict, use value as input

                Process p = Process.GetProcessesByName(ProcessName).FirstOrDefault();
                if (p == null)
                {
                    foreach (Process PP in Process.GetProcesses())
                    {
                        //if no process found from user supplied name, try closest match
                        if (PP.ProcessName.Contains(ProcessName))
                        {
                            p = PP;
                            break;
                        }
                    }
                }
                if (p != null)
                {
                    IntPtr h = p.MainWindowHandle;
                    SetForegroundWindow(h);
                    string ThingToInput = ControlDict[e.Result.Text];
                    //Check for mouse controls
                    if (ThingToInput.Contains("mouse_"))
                    {

                        if (ThingToInput.Contains("mouse_leftclick"))
                        {
                            InputSimulator IS = new InputSimulator();
                            IS.Mouse.LeftButtonDown().XButtonClick(1);
                            Thread.Sleep(DurationDict[e.Result.Text]);
                            IS.Mouse.LeftButtonUp();

                        }
                        else if (ThingToInput.Contains("mouse_rightclick"))
                        {
                            InputSimulator IS = new InputSimulator();
                            IS.Mouse.RightButtonDown().XButtonClick(2);

                        }
                    }
                    else
                    {
                        InputSimulator IS = new InputSimulator();
                        foreach (char c in ThingToInput.ToUpper())
                        {
                            var x = GetKeyCode(Convert.ToChar(c));
                            IS.Keyboard.KeyDown(x);
                            Thread.Sleep(DurationDict[e.Result.Text]);
                            IS.Keyboard.KeyUp(x);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Process found under name " + ProcessName);
                }
            }
        }

        private VirtualKeyCode GetKeyCode(char input)
        {
            int code = (byte)input;
            return (VirtualKeyCode)code;
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            SetStatus(Status.Stopped);
            SRE.Dispose();
            SRE = null;
            StartButton.Enabled = true;
            StopButton.Enabled = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (SRE == null)
            {
                GenerateSRE();
            }
            SetStatus(Status.Working);
            StopButton.Enabled = true;
            StartButton.Enabled = false;
        }

        private void ProcessNameBox_TextChanged(object sender, EventArgs e)
        {
            UpdateProccessName.Enabled = true;
        }

        private void UpdateProccessName_Click(object sender, EventArgs e)
        {
            ProcessName = ProcessNameBox.Text;
            UpdateProccessName.Enabled = false;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = ("Text files | *.txt");
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
            {
                StreamWriter SW = new StreamWriter(saveFileDialog1.FileName);
                foreach (DataGridViewRow DVR in ControlGrid.Rows)
                {
                    if (!DVR.IsNewRow)
                    {
                        SW.WriteLine("COMMAND");
                        SW.WriteLine(DVR.Cells[0].Value);
                        SW.WriteLine(DVR.Cells[1].Value);
                        SW.WriteLine(DVR.Cells[2].Value);
                    }
                }
                SW.WriteLine("PROCESS");
                SW.WriteLine(ProcessName);
                SW.Close();
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = ("Text files | *.txt");
            openFileDialog1.FileName = "";
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "" && openFileDialog1.CheckFileExists)
            {
                foreach (DataGridViewRow DVR in ControlGrid.Rows)
                {
                    if (!DVR.IsNewRow)
                    {
                        ControlGrid.Rows.Remove(DVR);
                    }
                }
                    StreamReader SR = new StreamReader(openFileDialog1.FileName);
                    while (!SR.EndOfStream)
                    {
                        string type = SR.ReadLine();
                        if (type == "COMMAND")
                        {
                            string a = SR.ReadLine();
                            string b = SR.ReadLine();
                            string c = SR.ReadLine();
                            ControlGrid.Rows.Add(new object[] { a, b, c });
                            //ControlGrid.Rows.Add(SR.ReadLine(), SR.ReadLine(), Convert.ToString(SR.ReadLine()));
                        }
                        else
                        {
                            if (type == "PROCESS")
                            {
                                ProcessName = SR.ReadLine();
                                ProcessNameBox.Text = ProcessName;
                                UpdateProccessName.Enabled = false;
                            }
                        }
                    }

                    //now call update
                    //untoggle after test
                
                UpdatePhrases();
                UpdatePhrasesButton.Enabled = false;
            }
        }
    }
}
    


//TODO
//File saving/loading
//default duration if none listed (done)
//warning if same key used twice
//warning if null program (done)
//char iterator for keyboard input (done)
