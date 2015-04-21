using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Speech.Recognition;
using Aldebaran.Proxies;
using ChatterBotAPI;

namespace CleverNAO_Form
{
    public partial class Form1 : Form
    {
        SpeechRecognitionEngine sre;
        private bool recognitionRunning;
        private bool connectedToNAO;
        private bool connectedToCleverBot;

        ChatterBotFactory factory;
        ChatterBot robot;
        ChatterBotSession session;

        TextToSpeechProxy tts;

        BackgroundWorker bw;

        public Form1()
        {
            InitializeComponent();
            speechOnOff.Enabled = false;
            ErrorLabel.Text = "";
            clearConvoBtn.Click += new System.EventHandler(clearConvoBtn_click);
            initializeConvoBox();
            initializeBackgroundWorker();
            startSpeech();
            startCleverbot();
            startNAO();
        }

        #region Initialization Functions
        public void initializeConvoBox()
        {
            //string start = "Hold Shift to speak, then release.";
            convoBox.ReadOnly = true;
            convoBox.BackColor = System.Drawing.SystemColors.Window;
            //insertConvoLine(start, true);
        }

        public void initializeBackgroundWorker()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
        }
        public void startSpeech()
        {
            System.Speech.Recognition.DictationGrammar dg = new System.Speech.Recognition.DictationGrammar();
            dg.Enabled = true;
            
            sre = new SpeechRecognitionEngine();
            sre.LoadGrammar(dg);

            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(recognizer_SpeechRecognized);
            sre.SetInputToDefaultAudioDevice();
        }

        public void startCleverbot()
        {
            factory = new ChatterBotFactory();
            robot = factory.Create(ChatterBotType.CLEVERBOT);
            session = robot.CreateSession();

        }

        public void startNAO()
        {
            try
            {
                tts = new TextToSpeechProxy("192.168.0.102", 9559);
                connectedToNAO = true;
            }
            catch (Exception e)
            {
                connectedToNAO = false;
                ErrorLabel.Text = "No Connection to NAO";
            }
        }
        #endregion
        #region Helper Functions
        public void outputToConvoBox(string line, int type)
        {
            convoBox.AppendText("(" + DateTime.Now.ToLongTimeString() + ") ");
            int length = convoBox.TextLength;
            switch (type)
            {
                case 1: // From robot
                    convoBox.AppendText("Robot: " + line + "\n");
                    convoBox.SelectionStart = length;
                    convoBox.SelectionLength = line.Length + 8;
                    convoBox.SelectionColor = Color.Blue;
                    if (connectedToNAO)
                        sendToNAO(line);
                    break;
                case 2: // From human
                    convoBox.AppendText("Human: " + line + "\n");
                    convoBox.SelectionStart = length;
                    convoBox.SelectionLength = line.Length + 8;
                    convoBox.SelectionColor = Color.Black;
                    break;
                case 3: // System messages
                    convoBox.AppendText(line + "\n");
                    convoBox.SelectionStart = length;
                    convoBox.SelectionLength = line.Length + 1;
                    convoBox.SelectionColor = Color.Red;
                    break;
                default:
                    break;
            }
            inputBox.Enabled = true;
            inputBox.Focus();
            convoBox.SelectionStart = convoBox.Text.Length;
            convoBox.ScrollToCaret();
        }

        public string getResponse(string input)
        {
            try
            {
                connectedToCleverBot = true;
                return session.Think(input);
            }
            catch (Exception e)
            {
                connectedToCleverBot = false;
                return "Error: " + e.Message;
            }
        }

        public void sendToNAO(string input)
        {
            tts.say(input);
        }
#endregion
        #region EVENT HANDLERS
        void pushToTalk_keyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey && !recognitionRunning)
            {
                try
                {
                    sre.RecognizeAsync();
                    speechOnOff.BackColor = Color.LimeGreen;
                    speechOnOff.Text = "On";
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    recognitionRunning = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        void pushToTalk_keyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey)
            {
                sre.RecognizeAsyncCancel();
                speechOnOff.BackColor = System.Drawing.SystemColors.Control;
                speechOnOff.Text = "Off";
                recognitionRunning = false;
            }
        }

        void inputBox_enter(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13) 
            {
                if (!bw.IsBusy)
                {
                    outputToConvoBox(inputBox.Text, 2);
                    bw.RunWorkerAsync(inputBox.Text);
                    inputBox.Clear();
                    inputBox.Enabled = false;
                }
            }
                
        }
        void clearConvoBtn_click(object sender, EventArgs e)
        {
            convoBox.Clear();
        }

        // Speech Event Handlers
        void recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (!bw.IsBusy && e.Result.Confidence > 0.3)
            {
                outputToConvoBox(e.Result.Text, 2);
                bw.RunWorkerAsync(e.Result.Text);
            }
            else
            {
                outputToConvoBox("Try Again. Speak Clearly.", 3);
            }
        }

        void recognizer_UnrecognizedSpeech(object sender, SpeechRecognitionRejectedEventArgs e)
        {
            if (!bw.IsBusy)
            {
                outputToConvoBox("Speech not recognized. " + e.Result.Text, 3);
            }
        }

        //Background Worker Event Handlers
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            e.Result = getResponse((string)e.Argument);
        }
        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!connectedToCleverBot)
                outputToConvoBox((string)e.Result, 3);
            else
                outputToConvoBox((string)e.Result, 1);
        }
        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
        }

#endregion
    }
}
