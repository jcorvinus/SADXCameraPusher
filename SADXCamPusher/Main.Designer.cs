namespace SADXCamPusher
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.helpButton = new System.Windows.Forms.Button();
            this.processLabel = new System.Windows.Forms.Label();
            this.loadProcessButton = new System.Windows.Forms.Button();
            this.detatchButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.zRotText = new System.Windows.Forms.TextBox();
            this.yRotText = new System.Windows.Forms.TextBox();
            this.xRotText = new System.Windows.Forms.TextBox();
            this.gameStateLabel = new System.Windows.Forms.Label();
            this.gameStateBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.zCoordBox = new System.Windows.Forms.TextBox();
            this.yCoordBox = new System.Windows.Forms.TextBox();
            this.xCoordBox = new System.Windows.Forms.TextBox();
            this.pulseTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.recordingExistLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.playButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.lockSonicToCamCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // helpButton
            // 
            this.helpButton.Location = new System.Drawing.Point(272, 144);
            this.helpButton.Name = "helpButton";
            this.helpButton.Size = new System.Drawing.Size(23, 23);
            this.helpButton.TabIndex = 7;
            this.helpButton.Text = "?";
            this.helpButton.UseVisualStyleBackColor = true;
            this.helpButton.Click += new System.EventHandler(this.helpButton_Click);
            // 
            // processLabel
            // 
            this.processLabel.AutoSize = true;
            this.processLabel.Location = new System.Drawing.Point(189, 158);
            this.processLabel.Name = "processLabel";
            this.processLabel.Size = new System.Drawing.Size(69, 13);
            this.processLabel.TabIndex = 8;
            this.processLabel.Text = "process label";
            // 
            // loadProcessButton
            // 
            this.loadProcessButton.Location = new System.Drawing.Point(12, 153);
            this.loadProcessButton.Name = "loadProcessButton";
            this.loadProcessButton.Size = new System.Drawing.Size(75, 23);
            this.loadProcessButton.TabIndex = 9;
            this.loadProcessButton.Text = "Load Proc";
            this.loadProcessButton.UseVisualStyleBackColor = true;
            this.loadProcessButton.Click += new System.EventHandler(this.loadProcessButton_Click);
            // 
            // detatchButton
            // 
            this.detatchButton.Enabled = false;
            this.detatchButton.Location = new System.Drawing.Point(95, 153);
            this.detatchButton.Name = "detatchButton";
            this.detatchButton.Size = new System.Drawing.Size(75, 23);
            this.detatchButton.TabIndex = 10;
            this.detatchButton.Text = "Detatch";
            this.detatchButton.UseVisualStyleBackColor = true;
            this.detatchButton.Click += new System.EventHandler(this.detatchButton_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.zRotText);
            this.groupBox1.Controls.Add(this.yRotText);
            this.groupBox1.Controls.Add(this.xRotText);
            this.groupBox1.Controls.Add(this.gameStateLabel);
            this.groupBox1.Controls.Add(this.gameStateBox);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.zCoordBox);
            this.groupBox1.Controls.Add(this.yCoordBox);
            this.groupBox1.Controls.Add(this.xCoordBox);
            this.groupBox1.Location = new System.Drawing.Point(15, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(280, 126);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Variables";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 13;
            this.label1.Text = "Z Rotation:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(137, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Y Rotation:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(137, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "X Rotation:";
            // 
            // zRotText
            // 
            this.zRotText.Location = new System.Drawing.Point(197, 66);
            this.zRotText.Name = "zRotText";
            this.zRotText.ReadOnly = true;
            this.zRotText.Size = new System.Drawing.Size(67, 20);
            this.zRotText.TabIndex = 10;
            this.zRotText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // yRotText
            // 
            this.yRotText.Location = new System.Drawing.Point(197, 41);
            this.yRotText.Name = "yRotText";
            this.yRotText.ReadOnly = true;
            this.yRotText.Size = new System.Drawing.Size(67, 20);
            this.yRotText.TabIndex = 9;
            this.yRotText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // xRotText
            // 
            this.xRotText.Location = new System.Drawing.Point(197, 18);
            this.xRotText.Name = "xRotText";
            this.xRotText.ReadOnly = true;
            this.xRotText.Size = new System.Drawing.Size(67, 20);
            this.xRotText.TabIndex = 8;
            this.xRotText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gameStateLabel
            // 
            this.gameStateLabel.AutoSize = true;
            this.gameStateLabel.Location = new System.Drawing.Point(6, 96);
            this.gameStateLabel.Name = "gameStateLabel";
            this.gameStateLabel.Size = new System.Drawing.Size(63, 13);
            this.gameStateLabel.TabIndex = 7;
            this.gameStateLabel.Text = "GameState:";
            // 
            // gameStateBox
            // 
            this.gameStateBox.Location = new System.Drawing.Point(69, 93);
            this.gameStateBox.Name = "gameStateBox";
            this.gameStateBox.ReadOnly = true;
            this.gameStateBox.Size = new System.Drawing.Size(74, 20);
            this.gameStateBox.TabIndex = 6;
            this.gameStateBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 67);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(17, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Z:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Y:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(17, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "X:";
            // 
            // zCoordBox
            // 
            this.zCoordBox.Location = new System.Drawing.Point(24, 65);
            this.zCoordBox.Name = "zCoordBox";
            this.zCoordBox.ReadOnly = true;
            this.zCoordBox.Size = new System.Drawing.Size(91, 20);
            this.zCoordBox.TabIndex = 2;
            this.zCoordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // yCoordBox
            // 
            this.yCoordBox.Location = new System.Drawing.Point(24, 39);
            this.yCoordBox.Name = "yCoordBox";
            this.yCoordBox.ReadOnly = true;
            this.yCoordBox.Size = new System.Drawing.Size(91, 20);
            this.yCoordBox.TabIndex = 1;
            this.yCoordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // xCoordBox
            // 
            this.xCoordBox.Location = new System.Drawing.Point(24, 16);
            this.xCoordBox.Name = "xCoordBox";
            this.xCoordBox.ReadOnly = true;
            this.xCoordBox.Size = new System.Drawing.Size(91, 20);
            this.xCoordBox.TabIndex = 0;
            this.xCoordBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pulseTimer
            // 
            this.pulseTimer.Interval = 33;
            this.pulseTimer.Tick += new System.EventHandler(this.pulseTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.recordingExistLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 249);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(305, 22);
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLabel
            // 
            this.statusLabel.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right;
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(28, 17);
            this.statusLabel.Text = "Idle";
            // 
            // recordingExistLabel
            // 
            this.recordingExistLabel.Name = "recordingExistLabel";
            this.recordingExistLabel.Size = new System.Drawing.Size(59, 17);
            this.recordingExistLabel.Text = "No Record";
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new System.Drawing.Point(183, 186);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(75, 23);
            this.playButton.TabIndex = 17;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.playButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Enabled = false;
            this.stopButton.Location = new System.Drawing.Point(224, 215);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(75, 23);
            this.stopButton.TabIndex = 18;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.stopButton_Click_1);
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(131, 215);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(87, 23);
            this.loadFileButton.TabIndex = 19;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            this.loadFileButton.Click += new System.EventHandler(this.loadFileButton_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Camera Paths|*.campath";
            // 
            // lockSonicToCamCheckBox
            // 
            this.lockSonicToCamCheckBox.AutoSize = true;
            this.lockSonicToCamCheckBox.Enabled = false;
            this.lockSonicToCamCheckBox.Location = new System.Drawing.Point(12, 188);
            this.lockSonicToCamCheckBox.Name = "lockSonicToCamCheckBox";
            this.lockSonicToCamCheckBox.Size = new System.Drawing.Size(135, 17);
            this.lockSonicToCamCheckBox.TabIndex = 20;
            this.lockSonicToCamCheckBox.Text = "Lock Sonic To Camera";
            this.lockSonicToCamCheckBox.UseVisualStyleBackColor = true;
            this.lockSonicToCamCheckBox.CheckedChanged += new System.EventHandler(this.lockSonicToCamCheckBox_CheckedChanged);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 271);
            this.Controls.Add(this.lockSonicToCamCheckBox);
            this.Controls.Add(this.loadFileButton);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.detatchButton);
            this.Controls.Add(this.loadProcessButton);
            this.Controls.Add(this.processLabel);
            this.Controls.Add(this.helpButton);
            this.KeyPreview = true;
            this.MaximumSize = new System.Drawing.Size(480, 300);
            this.MinimumSize = new System.Drawing.Size(313, 300);
            this.Name = "Main";
            this.Text = "SADX Camera Playback - V1.0";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button helpButton;
        private System.Windows.Forms.Label processLabel;
        private System.Windows.Forms.Button loadProcessButton;
        private System.Windows.Forms.Button detatchButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox zCoordBox;
        private System.Windows.Forms.TextBox yCoordBox;
        private System.Windows.Forms.TextBox xCoordBox;
        private System.Windows.Forms.Timer pulseTimer;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLabel;
        private System.Windows.Forms.ToolStripStatusLabel recordingExistLabel;
        private System.Windows.Forms.TextBox gameStateBox;
        private System.Windows.Forms.Label gameStateLabel;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox zRotText;
        private System.Windows.Forms.TextBox yRotText;
        private System.Windows.Forms.TextBox xRotText;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.CheckBox lockSonicToCamCheckBox;
    }
}

