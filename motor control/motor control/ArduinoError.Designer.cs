namespace motor_control
{
    partial class ArduinoError
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.yes = new System.Windows.Forms.Button();
            this.disable = new System.Windows.Forms.Button();
            this.ignore = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.versionLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(255, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "It looks like the Arduino is running the wrong version!";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(226, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "(or perhaps it\'s not running our software at all?)";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 207);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Would you like to run in compatibility mode?";
            // 
            // yes
            // 
            this.yes.Location = new System.Drawing.Point(6, 237);
            this.yes.Name = "yes";
            this.yes.Size = new System.Drawing.Size(109, 52);
            this.yes.TabIndex = 3;
            this.yes.Text = "Yes, use compatibility mode.";
            this.yes.UseVisualStyleBackColor = true;
            this.yes.Click += new System.EventHandler(this.yes_Click);
            // 
            // disable
            // 
            this.disable.Location = new System.Drawing.Point(121, 237);
            this.disable.Name = "disable";
            this.disable.Size = new System.Drawing.Size(215, 23);
            this.disable.TabIndex = 4;
            this.disable.Text = "Nay! Disable the Arduino!";
            this.disable.UseVisualStyleBackColor = true;
            this.disable.Click += new System.EventHandler(this.disable_Click);
            // 
            // ignore
            // 
            this.ignore.Location = new System.Drawing.Point(121, 266);
            this.ignore.Name = "ignore";
            this.ignore.Size = new System.Drawing.Size(215, 23);
            this.ignore.TabIndex = 5;
            this.ignore.Text = "Nay, but don\'t disable the Arduino.";
            this.ignore.UseVisualStyleBackColor = true;
            this.ignore.Click += new System.EventHandler(this.ignore_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 121);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(311, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Compatiblity mode assumes that every message from the Arduino";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(271, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "is just the temperature. Use it if the Arduino is only giving";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = " temperatures over serial.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(132, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Received Arduino version:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Warning;
            this.notifyIcon1.BalloonTipText = "Arduino compatibility error!";
            this.notifyIcon1.Text = "Arduino Error";
            this.notifyIcon1.Visible = true;
            // 
            // versionLabel
            // 
            this.versionLabel.AutoSize = true;
            this.versionLabel.Location = new System.Drawing.Point(152, 76);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(28, 13);
            this.versionLabel.TabIndex = 10;
            this.versionLabel.Text = "error";
            // 
            // ArduinoError
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(354, 301);
            this.Controls.Add(this.versionLabel);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.ignore);
            this.Controls.Add(this.disable);
            this.Controls.Add(this.yes);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "ArduinoError";
            this.Text = "Danger, Will Robinson";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button yes;
        private System.Windows.Forms.Button disable;
        private System.Windows.Forms.Button ignore;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Label versionLabel;
    }
}