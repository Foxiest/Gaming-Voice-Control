using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Speech.Recognition;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
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

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

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

        private void UpdatePhrasesButton_Click(object sender, EventArgs e)
        {
            //make sre and enable
            ControlDict = new Dictionary<string, string>();
            DurationDict = new Dictionary<string, int>();
            foreach(DataGridViewRow DVR in ControlGrid.Rows)
            {
                if (!DVR.IsNewRow)
                {
                    if (DVR.Cells[0].Value != null && DVR.Cells[1].Value != null)
                    {
                        ControlDict.Add((string)DVR.Cells[1].Value, (string)DVR.Cells[0].Value);
                        DurationDict.Add((string)DVR.Cells[1].Value, Convert.ToInt32(DVR.Cells[2].Value));
                        MessageBox.Show((string)DVR.Cells[0].Value + " " + (string)DVR.Cells[1].Value);
                    }
                }
            }

            UpdatePhrasesButton.Enabled = false;
            GenerateSRE();
        }

        private void GenerateSRE()
        {
            SRE = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-EN"));
            //Use the microphone
            SRE.SetInputToDefaultAudioDevice();
            Choices Words = new Choices();
            //key is the phrase spoken, value is the output command
            foreach(string Phrase in ControlDict.Keys)
            {
                Words.Add(Phrase);
            }
            GrammarBuilder gb = new GrammarBuilder();
            gb.Append(Words);
            gb.Culture = Thread.CurrentThread.CurrentCulture;
            Grammar g = new Grammar(gb);
            SRE.LoadGrammar(g);
            SRE.SpeechRecognized += SRE_SpeechRecognized;
            SRE.RecognizeAsync(RecognizeMode.Multiple);
        }

        private void SRE_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //make sure we're executing here
            if (!StartButton.Enabled)
            {
                //find phrase in dict, use value as input
                if (ControlDict.ContainsKey(e.Result.Text))
                {
                    Process p = Process.GetProcessesByName(ProcessName).FirstOrDefault();
                    if (p != null)
                    {

                        IntPtr h = p.MainWindowHandle;
                        SetForegroundWindow(h);
                       


                        string ThingToInput = ControlDict[e.Result.Text];
                        //Check for mouse controls
                        if (ThingToInput.Contains("mouse_"))
                        {
                            const int MOUSEEVENTF_LEFTDOWN = 0x02;
                            const int MOUSEEVENTF_LEFTUP = 0x04;
                            const int MOUSEEVENTF_RIGHTDOWN = 0x08;
                            const int MOUSEEVENTF_RIGHTUP = 0x10;

                            if (ThingToInput.Contains("mouse_leftclick"))
                            {
                                InputSimulator IS = new InputSimulator();
                                IS.Mouse.LeftButtonClick();
                                /*
                                uint X = (uint)Cursor.Position.X;
                                uint Y = (uint)Cursor.Position.Y;
                                mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
                                //MessageBox.Show("Moving left down");
                                */
                            }
                            else if (ThingToInput.Contains("mouse_rightclick"))
                            {
                                IS.Mouse.LeftButtonClick();
                                //MessageBox.Show("Moving right down");
                            }

                            //MessageBox.Show("mouse worked");
                        }
                        else
                        {
                            //MessageBox.Show("GOT COMMAND");

                           
                                InputSimulator IS = new InputSimulator();
                                var x = GetKeyCode(Convert.ToChar(ThingToInput.ToUpper()));
                                IS.Keyboard.KeyDown(x);
                                Thread.Sleep(DurationDict[e.Result.Text]);
                                IS.Keyboard.KeyUp(x);
                            //MessageBox.Show("moving " + ThingToInput);
                        }
                    }
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
            SRE.Dispose();
            StartButton.Enabled = true;
            StopButton.Enabled = false;
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
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
    }
}
