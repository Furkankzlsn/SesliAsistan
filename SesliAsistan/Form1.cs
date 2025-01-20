using NAudio.Wave;
using System;
using System.Diagnostics;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using Timer = System.Windows.Forms.Timer;

namespace SesliAsistan
{
    public partial class Form1 : Form
    {
        private SpeechSynthesizer synthesizer;
        private SpeechRecognitionEngine recognizer;
        private WaveInEvent waveIn;
        private Timer micLevelTimer;
        private int micLevel = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeMicLevel();
            InitializeSpeechRecognition();
            synthesizer = new SpeechSynthesizer();
        }

        private bool isWaveInRecording = false; // Kaydýn baþlatýlýp baþlatýlmadýðýný kontrol eden bayrak
        private bool isFormLoading = true;

        private void Form1_Load(object sender, EventArgs e)
        {
            isFormLoading = true; // Form yükleniyor

            LoadInputDevices();
            LoadOutputDevices();

            cmbInputDevices.SelectedItem = Settings.Default.InputDevice;
            cmbOutputDevices.SelectedItem = Settings.Default.OutputDevice;
            chkRunAtStartup.Checked = IsApplicationInStartup();

            trkVoiceRate.Minimum = -10;
            trkVoiceRate.Maximum = 10;

            trkVoiceVolume.Minimum = 0;
            trkVoiceVolume.Maximum = 100;

            if (Settings.Default.VoiceRate < trkVoiceRate.Minimum || Settings.Default.VoiceRate > trkVoiceRate.Maximum)
            {
                Settings.Default.VoiceRate = 0;
                Settings.Default.Save();
            }

            if (Settings.Default.VoiceVolume < trkVoiceVolume.Minimum || Settings.Default.VoiceVolume > trkVoiceVolume.Maximum)
            {
                Settings.Default.VoiceVolume = 50;
                Settings.Default.Save();
            }

            trkVoiceRate.Value = Settings.Default.VoiceRate;
            trkVoiceVolume.Value = Settings.Default.VoiceVolume;

            progressMicLevel.Minimum = 0;
            progressMicLevel.Maximum = 100;

            if (!isWaveInRecording)
            {
                waveIn.StartRecording();
                isWaveInRecording = true;
            }

            micLevelTimer.Start();
            UpdateVoiceSettings();

            isFormLoading = false; // Form yüklenmesi tamamlandý
        }


        private void InitializeMicLevel()
        {
            waveIn = new WaveInEvent();
            waveIn.WaveFormat = new WaveFormat(44100, 1);
            waveIn.DataAvailable += OnMicDataAvailable;

            micLevelTimer = new Timer();
            micLevelTimer.Interval = 100;
            micLevelTimer.Tick += UpdateMicLevel;
        }

        private void InitializeSpeechRecognition()
        {
            try
            {
                // SpeechRecognitionEngine için Ýngilizce dil ayarý
                recognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
                recognizer.SetInputToDefaultAudioDevice();

                // Komutlarý tanýmlayýn
                Choices commands = new Choices();
                commands.Add(new string[] { "Hey pc", "stop listening", "clear logs", "open meet", "what time is it", "search on YouTube" });

                // GrammarBuilder için ayný dili ayarlayýn
                GrammarBuilder gb = new GrammarBuilder();
                gb.Culture = new System.Globalization.CultureInfo("en-US"); // Ayný dil kullanýlmalý
                gb.Append(commands);

                Grammar g = new Grammar(gb);
                recognizer.LoadGrammar(g);

                recognizer.SpeechRecognized += Recognizer_SpeechRecognized;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing speech recognition: {ex.Message}");
            }
        }

        private void OnMicDataAvailable(object sender, WaveInEventArgs e)
        {
            var buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;
            double sum = 0;

            for (int i = 0; i < bytesRecorded; i += 2)
            {
                short sample = BitConverter.ToInt16(buffer, i);
                sum += sample * sample;
            }

            double rms = Math.Sqrt(sum / (bytesRecorded / 2));
            double decibel = 20 * Math.Log10(rms + 1);
            micLevel = (int)(decibel / 40.0 * 100);
            micLevel = Math.Clamp(micLevel, 0, 100);
        }

        private void UpdateMicLevel(object sender, EventArgs e)
        {
            progressMicLevel.Value = micLevel;
            lblMicLevel.Text = $"Mic Level: {micLevel}%";
        }

        private void btnSaveDevices_Click(object sender, EventArgs e)
        {
            var selectedInput = cmbInputDevices.SelectedItem?.ToString();
            var selectedOutput = cmbOutputDevices.SelectedItem?.ToString();

            if (!string.IsNullOrEmpty(selectedInput) && !string.IsNullOrEmpty(selectedOutput))
            {
                try
                {
                    Settings.Default.InputDevice = selectedInput;
                    Settings.Default.OutputDevice = selectedOutput;
                    Settings.Default.Save();
                    MessageBox.Show("Devices saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error saving devices: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select both input and output devices.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void Recognizer_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            string command = e.Result.Text;

            // Algýlanan komutu arka plan rengini deðiþtirerek görselleþtir
            txtLastCommand.Text = command;
            txtLastCommand.BackColor = Color.LightGreen;
            listBoxLogs.Items.Add($"{DateTime.Now}: Command recognized: {command}");

            if (command == "hey google")
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple);
                synthesizer.Speak($"I am listening");
                lblListen.Text = "Listening...";
                txtLastCommand.BackColor = Color.LightGreen;
            }
            else if (command == "stop listening")
            {
                synthesizer.Speak($"Goodbye");
                recognizer.RecognizeAsyncStop();
                lblListen.Text = "Stopped.";
                txtLastCommand.BackColor = Color.LightCoral;
            }
            else if (command == "clear logs")
            {
                listBoxLogs.Items.Clear();
                lblListen.Text = "Logs cleared.";
            }
            else if (command == "open meet")
            {
                OpenBrowser("https://meet.google.com/vfj-tazd-cxi");
                lblListen.Text = "Browser opened.";
                synthesizer.Speak("I have opened the meet.");
            }
            else if (command == "what time is it")
            {
                string time = DateTime.Now.ToShortTimeString();
                synthesizer.Speak($"The time is {time}");
                lblListen.Text = $"The time is {time}";
            }
            else if (command == "search on YouTube")
            {
                synthesizer.Speak("What do you want to search on YouTube?");
                lblListen.Text = "Waiting for YouTube search query...";
                GetYouTubeSearchQuery();
            }
        }

        private void GetYouTubeSearchQuery()
        {
            var tempRecognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
            bool isConfirmationRecognizerDisposed = false;

            try
            {
                tempRecognizer.SetInputToDefaultAudioDevice();
                tempRecognizer.LoadGrammar(new DictationGrammar());

                tempRecognizer.SpeechRecognized += (s, e) =>
                {
                    string query = e.Result.Text;
                    lblListen.Text = $"{query}";
                    synthesizer.Speak($"Did you mean {query}? Say yes or no.");

                    // Yeni bir tanýma motoru oluþtur
                    var confirmationRecognizer = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));
                    confirmationRecognizer.SetInputToDefaultAudioDevice();

                    // Yes/No cevaplarýný tanýmla
                    Choices confirmationChoices = new Choices();
                    confirmationChoices.Add("yes");
                    confirmationChoices.Add("no");

                    GrammarBuilder confirmationGb = new GrammarBuilder();
                    confirmationGb.Culture = new System.Globalization.CultureInfo("en-US"); // Ayný dil ayarýný yap
                    confirmationGb.Append(confirmationChoices);

                    Grammar confirmationGrammar = new Grammar(confirmationGb);
                    confirmationRecognizer.LoadGrammar(confirmationGrammar);

                    confirmationRecognizer.SpeechRecognized += (confSender, confE) =>
                    {
                        string response = confE.Result.Text;

                        if (response == "yes")
                        {
                            synthesizer.Speak($"Searching YouTube for {query}.");
                            lblListen.Text = $"Searching YouTube for: {query}";
                            OpenBrowser($"https://www.youtube.com/results?search_query={Uri.EscapeDataString(query)}");
                        }
                        else if (response == "no")
                        {
                            synthesizer.Speak("What do you want to search on YouTube?");
                            lblListen.Text = "Waiting for new YouTube search query...";
                            if (!isConfirmationRecognizerDisposed)
                            {
                                confirmationRecognizer.RecognizeAsyncStop();
                                confirmationRecognizer.Dispose();
                                isConfirmationRecognizerDisposed = true;
                            }
                            GetYouTubeSearchQuery(); // Tekrar sorgu al
                            return; // Erken dönüþ
                        }

                        if (!isConfirmationRecognizerDisposed)
                        {
                            confirmationRecognizer.RecognizeAsyncStop();
                            confirmationRecognizer.Dispose();
                            isConfirmationRecognizerDisposed = true;
                        }
                    };

                    confirmationRecognizer.RecognizeCompleted += (confSender, confE) =>
                    {
                        if (!isConfirmationRecognizerDisposed)
                        {
                            confirmationRecognizer.Dispose();
                            isConfirmationRecognizerDisposed = true;
                        }
                    };

                    confirmationRecognizer.RecognizeAsync(RecognizeMode.Single);
                };

                tempRecognizer.RecognizeAsync(RecognizeMode.Single);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error recognizing speech: {ex.Message}");
            }
        }

        private void OpenBrowser(string url)
        {
            try
            {
                var psi = new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true // URL'yi varsayýlan tarayýcýyla açmak için gerekli
                };
                Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening browser: {ex.Message}");
            }
        }

        private void LoadInputDevices()
        {
            cmbInputDevices.Items.Clear();
            for (int deviceId = 0; deviceId < WaveIn.DeviceCount; deviceId++)
            {
                var deviceInfo = WaveIn.GetCapabilities(deviceId);
                cmbInputDevices.Items.Add(deviceInfo.ProductName);
            }

            if (cmbInputDevices.Items.Count > 0)
            {
                cmbInputDevices.SelectedIndex = 0; // Varsayýlan mikrofonu seç
            }
        }

        private void LoadOutputDevices()
        {
            cmbOutputDevices.Items.Clear();
            for (int deviceId = 0; deviceId < WaveOut.DeviceCount; deviceId++)
            {
                var deviceInfo = WaveOut.GetCapabilities(deviceId);
                cmbOutputDevices.Items.Add(deviceInfo.ProductName);
            }

            if (cmbOutputDevices.Items.Count > 0)
                cmbOutputDevices.SelectedIndex = 0;
        }

        private bool IsApplicationInStartup()
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", false);
            return key?.GetValue("VoiceAssistantApp") != null;
        }

        private void chkRunAtStartup_CheckedChanged(object sender, EventArgs e)
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (chkRunAtStartup.Checked)
            {
                key.SetValue("VoiceAssistantApp", Application.ExecutablePath);
            }
            else
            {
                key.DeleteValue("VoiceAssistantApp", false);
            }
        }

        private void trkVoiceRate_Scroll(object sender, EventArgs e)
        {
            Settings.Default.VoiceRate = trkVoiceRate.Value;
            Settings.Default.Save();
            UpdateVoiceSettings();
        }

        private void trkVoiceVolume_Scroll(object sender, EventArgs e)
        {
            Settings.Default.VoiceVolume = trkVoiceVolume.Value;
            Settings.Default.Save();
            UpdateVoiceSettings();
        }

        private void UpdateVoiceSettings()
        {
            synthesizer.Rate = trkVoiceRate.Value;
            synthesizer.Volume = trkVoiceVolume.Value;
            lblStatus.Text = $"Voice Rate: {trkVoiceRate.Value}, Volume: {trkVoiceVolume.Value}%";
        }

        private void btnStartListening_Click(object sender, EventArgs e)
        {
            try
            {
                recognizer.RecognizeAsync(RecognizeMode.Multiple); // Tanýmayý baþlat
                lblListen.Text = "Listening...";
                txtLastCommand.BackColor = Color.LightGreen;
                synthesizer.Speak($"I am listening");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void btnStopListening_Click(object sender, EventArgs e)
        {
            try
            {
                recognizer.RecognizeAsyncStop(); // Tanýmayý durdur
                lblListen.Text = "Stopped.";
                txtLastCommand.BackColor = Color.LightCoral;
                synthesizer.Speak($"GoodBye!");

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void cmbInputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoading) return; // Form yükleniyorsa olayý atla

            try
            {
                if (cmbInputDevices.SelectedItem != null)
                {
                    string selectedInputDevice = cmbInputDevices.SelectedItem.ToString();
                    for (int device = 0; device < WaveIn.DeviceCount; device++)
                    {
                        var deviceInfo = WaveIn.GetCapabilities(device);
                        if (deviceInfo.ProductName == selectedInputDevice)
                        {
                            if (waveIn != null)
                            {
                                waveIn.StopRecording();
                                waveIn.Dispose();
                            }

                            waveIn = new WaveInEvent { DeviceNumber = device };
                            waveIn.WaveFormat = new WaveFormat(44100, 1); // 44.1kHz, Mono
                            waveIn.DataAvailable += OnMicDataAvailable;
                            waveIn.StartRecording();
                            isWaveInRecording = true;

                            MessageBox.Show($"Input device changed to: {selectedInputDevice}");
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing input device: {ex.Message}");
            }
        }

        private void cmbOutputDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isFormLoading) return; // Form yükleniyorsa olayý atla

            try
            {
                if (cmbOutputDevices.SelectedItem != null)
                {
                    string selectedOutputDevice = cmbOutputDevices.SelectedItem.ToString();
                    synthesizer.SetOutputToDefaultAudioDevice(); // Çýkýþ cihazýný varsayýlana ayarla
                    MessageBox.Show($"Output device changed to: {selectedOutputDevice}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing output device: {ex.Message}");
            }
        }

    }
}