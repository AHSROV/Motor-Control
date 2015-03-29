namespace motor_control
{
    partial class PortSelector
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
            this.buttonOK = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.refreshbutton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.arduinoPortLabel = new System.Windows.Forms.Label();
            this.comboBoxArduinoPort = new System.Windows.Forms.ComboBox();
            this.testingMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // buttonOK
            // 
            this.buttonOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonOK.Location = new System.Drawing.Point(122, 86);
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.Size = new System.Drawing.Size(75, 23);
            this.buttonOK.TabIndex = 5;
            this.buttonOK.Text = "Okay";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Please select the serial port to be used.";
            // 
            // refreshbutton
            // 
            this.refreshbutton.Location = new System.Drawing.Point(12, 86);
            this.refreshbutton.Name = "refreshbutton";
            this.refreshbutton.Size = new System.Drawing.Size(96, 23);
            this.refreshbutton.TabIndex = 4;
            this.refreshbutton.Text = "Refresh List";
            this.refreshbutton.UseVisualStyleBackColor = true;
            this.refreshbutton.Click += new System.EventHandler(this.refreshbutton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(59, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 8;
            // 
            // arduinoPortLabel
            // 
            this.arduinoPortLabel.AutoSize = true;
            this.arduinoPortLabel.Location = new System.Drawing.Point(10, 36);
            this.arduinoPortLabel.Name = "arduinoPortLabel";
            this.arduinoPortLabel.Size = new System.Drawing.Size(65, 13);
            this.arduinoPortLabel.TabIndex = 11;
            this.arduinoPortLabel.Text = "Arduino Port";
            // 
            // comboBoxArduinoPort
            // 
            this.comboBoxArduinoPort.FormattingEnabled = true;
            this.comboBoxArduinoPort.Location = new System.Drawing.Point(82, 34);
            this.comboBoxArduinoPort.Name = "comboBoxArduinoPort";
            this.comboBoxArduinoPort.Size = new System.Drawing.Size(121, 21);
            this.comboBoxArduinoPort.TabIndex = 3;
            this.comboBoxArduinoPort.SelectedIndexChanged += new System.EventHandler(this.comboBoxArduinoPort_SelectedIndexChanged);
            // 
            // testingMode
            // 
            this.testingMode.AutoSize = true;
            this.testingMode.Location = new System.Drawing.Point(12, 61);
            this.testingMode.Name = "testingMode";
            this.testingMode.Size = new System.Drawing.Size(91, 17);
            this.testingMode.TabIndex = 12;
            this.testingMode.Text = "Testing Mode";
            this.testingMode.UseVisualStyleBackColor = true;
            // 
            // PortSelector
            // 
            this.AcceptButton = this.buttonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(209, 121);
            this.Controls.Add(this.testingMode);
            this.Controls.Add(this.comboBoxArduinoPort);
            this.Controls.Add(this.arduinoPortLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.refreshbutton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonOK);
            this.Name = "PortSelector";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "PortSelector";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button refreshbutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label arduinoPortLabel;
        private System.Windows.Forms.ComboBox comboBoxArduinoPort;
        private System.Windows.Forms.CheckBox testingMode;
    }
}