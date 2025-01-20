namespace SesliAsistan
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            label1 = new Label();
            cmbInputDevices = new ComboBox();
            cmbOutputDevices = new ComboBox();
            label2 = new Label();
            btnSaveDevices = new Button();
            lblStatus = new Label();
            label4 = new Label();
            txtLastCommand = new TextBox();
            progressMicLevel = new ProgressBar();
            btnStartListening = new Button();
            btnStopListening = new Button();
            chkRunAtStartup = new CheckBox();
            listBoxLogs = new ListBox();
            btnClearLogs = new Button();
            button1 = new Button();
            label5 = new Label();
            trkVoiceRate = new TrackBar();
            label6 = new Label();
            trkVoiceVolume = new TrackBar();
            label7 = new Label();
            lblMicLevel = new Label();
            lblListen = new Label();
            ((System.ComponentModel.ISupportInitialize)trkVoiceRate).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trkVoiceVolume).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(45, 48);
            label1.Name = "label1";
            label1.Size = new Size(149, 32);
            label1.TabIndex = 0;
            label1.Text = "Input Device";
            // 
            // cmbInputDevices
            // 
            cmbInputDevices.FormattingEnabled = true;
            cmbInputDevices.Location = new Point(45, 106);
            cmbInputDevices.Name = "cmbInputDevices";
            cmbInputDevices.Size = new Size(394, 40);
            cmbInputDevices.TabIndex = 1;
            cmbInputDevices.SelectedIndexChanged += cmbInputDevices_SelectedIndexChanged;
            // 
            // cmbOutputDevices
            // 
            cmbOutputDevices.FormattingEnabled = true;
            cmbOutputDevices.Location = new Point(485, 106);
            cmbOutputDevices.Name = "cmbOutputDevices";
            cmbOutputDevices.Size = new Size(318, 40);
            cmbOutputDevices.TabIndex = 3;
            cmbOutputDevices.SelectedIndexChanged += cmbOutputDevices_SelectedIndexChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(485, 48);
            label2.Name = "label2";
            label2.Size = new Size(169, 32);
            label2.TabIndex = 2;
            label2.Text = "Output Device";
            // 
            // btnSaveDevices
            // 
            btnSaveDevices.Location = new Point(825, 99);
            btnSaveDevices.Name = "btnSaveDevices";
            btnSaveDevices.Size = new Size(205, 47);
            btnSaveDevices.TabIndex = 4;
            btnSaveDevices.Text = "Kaydet";
            btnSaveDevices.UseVisualStyleBackColor = true;
            btnSaveDevices.Click += btnSaveDevices_Click;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(973, 650);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(78, 32);
            lblStatus.TabIndex = 6;
            lblStatus.Text = "Status";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(45, 209);
            label4.Name = "label4";
            label4.Size = new Size(173, 32);
            label4.TabIndex = 5;
            label4.Text = "Last Command";
            // 
            // txtLastCommand
            // 
            txtLastCommand.Location = new Point(45, 259);
            txtLastCommand.Name = "txtLastCommand";
            txtLastCommand.ReadOnly = true;
            txtLastCommand.Size = new Size(268, 39);
            txtLastCommand.TabIndex = 7;
            // 
            // progressMicLevel
            // 
            progressMicLevel.Location = new Point(485, 259);
            progressMicLevel.Name = "progressMicLevel";
            progressMicLevel.Size = new Size(526, 39);
            progressMicLevel.TabIndex = 8;
            // 
            // btnStartListening
            // 
            btnStartListening.Location = new Point(1067, 86);
            btnStartListening.Name = "btnStartListening";
            btnStartListening.Size = new Size(302, 57);
            btnStartListening.TabIndex = 9;
            btnStartListening.Text = "Start Listening";
            btnStartListening.UseVisualStyleBackColor = true;
            btnStartListening.Click += btnStartListening_Click;
            // 
            // btnStopListening
            // 
            btnStopListening.Location = new Point(1064, 192);
            btnStopListening.Name = "btnStopListening";
            btnStopListening.Size = new Size(302, 57);
            btnStopListening.TabIndex = 10;
            btnStopListening.Text = "Stop Listening";
            btnStopListening.UseVisualStyleBackColor = true;
            btnStopListening.Click += btnStopListening_Click;
            // 
            // chkRunAtStartup
            // 
            chkRunAtStartup.AutoSize = true;
            chkRunAtStartup.Location = new Point(1138, 313);
            chkRunAtStartup.Name = "chkRunAtStartup";
            chkRunAtStartup.Size = new Size(198, 36);
            chkRunAtStartup.TabIndex = 11;
            chkRunAtStartup.Text = "Run at Startup";
            chkRunAtStartup.UseVisualStyleBackColor = true;
            chkRunAtStartup.CheckedChanged += chkRunAtStartup_CheckedChanged;
            // 
            // listBoxLogs
            // 
            listBoxLogs.FormattingEnabled = true;
            listBoxLogs.Location = new Point(45, 409);
            listBoxLogs.Name = "listBoxLogs";
            listBoxLogs.Size = new Size(654, 388);
            listBoxLogs.TabIndex = 12;
            // 
            // btnClearLogs
            // 
            btnClearLogs.Location = new Point(705, 409);
            btnClearLogs.Name = "btnClearLogs";
            btnClearLogs.Size = new Size(205, 47);
            btnClearLogs.TabIndex = 13;
            btnClearLogs.Text = "Clear Logs";
            btnClearLogs.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new Point(1235, 812);
            button1.Name = "button1";
            button1.Size = new Size(8, 8);
            button1.TabIndex = 14;
            button1.Text = "button1";
            button1.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(973, 356);
            label5.Name = "label5";
            label5.Size = new Size(125, 32);
            label5.TabIndex = 15;
            label5.Text = "Voice Rate";
            // 
            // trkVoiceRate
            // 
            trkVoiceRate.Location = new Point(970, 409);
            trkVoiceRate.Maximum = 100;
            trkVoiceRate.Name = "trkVoiceRate";
            trkVoiceRate.Size = new Size(335, 90);
            trkVoiceRate.TabIndex = 16;
            trkVoiceRate.Scroll += trkVoiceRate_Scroll;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(970, 499);
            label6.Name = "label6";
            label6.Size = new Size(159, 32);
            label6.TabIndex = 15;
            label6.Text = "Voice Volume";
            // 
            // trkVoiceVolume
            // 
            trkVoiceVolume.Location = new Point(970, 557);
            trkVoiceVolume.Maximum = 100;
            trkVoiceVolume.Name = "trkVoiceVolume";
            trkVoiceVolume.Size = new Size(335, 90);
            trkVoiceVolume.TabIndex = 16;
            trkVoiceVolume.Scroll += trkVoiceVolume_Scroll;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(45, 351);
            label7.Name = "label7";
            label7.Size = new Size(63, 32);
            label7.TabIndex = 17;
            label7.Text = "Logs";
            // 
            // lblMicLevel
            // 
            lblMicLevel.AutoSize = true;
            lblMicLevel.Location = new Point(485, 217);
            lblMicLevel.Name = "lblMicLevel";
            lblMicLevel.Size = new Size(78, 32);
            lblMicLevel.TabIndex = 6;
            lblMicLevel.Text = "Status";
            // 
            // lblListen
            // 
            lblListen.AutoSize = true;
            lblListen.Location = new Point(45, 313);
            lblListen.Name = "lblListen";
            lblListen.Size = new Size(78, 32);
            lblListen.TabIndex = 6;
            lblListen.Text = "Status";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(13F, 32F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1454, 854);
            Controls.Add(label7);
            Controls.Add(trkVoiceVolume);
            Controls.Add(trkVoiceRate);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(btnClearLogs);
            Controls.Add(listBoxLogs);
            Controls.Add(chkRunAtStartup);
            Controls.Add(btnStopListening);
            Controls.Add(btnStartListening);
            Controls.Add(progressMicLevel);
            Controls.Add(txtLastCommand);
            Controls.Add(lblMicLevel);
            Controls.Add(lblListen);
            Controls.Add(lblStatus);
            Controls.Add(label4);
            Controls.Add(btnSaveDevices);
            Controls.Add(cmbOutputDevices);
            Controls.Add(label2);
            Controls.Add(cmbInputDevices);
            Controls.Add(label1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Sesli Asistan";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)trkVoiceRate).EndInit();
            ((System.ComponentModel.ISupportInitialize)trkVoiceVolume).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private ComboBox cmbInputDevices;
        private ComboBox cmbOutputDevices;
        private Label label2;
        private Button btnSaveDevices;
        private Label lblStatus;
        private Label label4;
        private TextBox txtLastCommand;
        private ProgressBar progressMicLevel;
        private Button btnStartListening;
        private Button btnStopListening;
        private CheckBox chkRunAtStartup;
        private ListBox listBoxLogs;
        private Button btnClearLogs;
        private Button button1;
        private Label label5;
        private TrackBar trkVoiceRate;
        private Label label6;
        private TrackBar trkVoiceVolume;
        private Label label7;
        private Label lblMicLevel;
        private Label lblListen;
    }
}
