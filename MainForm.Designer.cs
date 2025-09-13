namespace SkyWatcherMotorControllerMonitor
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.textBoxLog = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.serialPortIn = new System.IO.Ports.SerialPort(this.components);
            this.serialPortOut = new System.IO.Ports.SerialPort(this.components);
            this.comboBoxPortIn = new System.Windows.Forms.ComboBox();
            this.comboBoxPortOut = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.buttonLogClear = new System.Windows.Forms.Button();
            this.textBoxLogMean = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBoxLog
            // 
            this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.textBoxLog.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxLog.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxLog.Location = new System.Drawing.Point(12, 42);
            this.textBoxLog.Multiline = true;
            this.textBoxLog.Name = "textBoxLog";
            this.textBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLog.Size = new System.Drawing.Size(311, 399);
            this.textBoxLog.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "Log";
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(358, 14);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(75, 23);
            this.buttonConnect.TabIndex = 2;
            this.buttonConnect.Text = "Connect";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // serialPortIn
            // 
            this.serialPortIn.BaudRate = 115200;
            this.serialPortIn.PortName = "COM6";
            this.serialPortIn.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortIn_DataReceived);
            // 
            // serialPortOut
            // 
            this.serialPortOut.BaudRate = 115200;
            this.serialPortOut.PortName = "COM4";
            this.serialPortOut.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPortOut_DataReceived);
            // 
            // comboBoxPortIn
            // 
            this.comboBoxPortIn.FormattingEnabled = true;
            this.comboBoxPortIn.Location = new System.Drawing.Point(184, 16);
            this.comboBoxPortIn.Name = "comboBoxPortIn";
            this.comboBoxPortIn.Size = new System.Drawing.Size(70, 20);
            this.comboBoxPortIn.TabIndex = 3;
            // 
            // comboBoxPortOut
            // 
            this.comboBoxPortOut.FormattingEnabled = true;
            this.comboBoxPortOut.Location = new System.Drawing.Point(281, 16);
            this.comboBoxPortOut.Name = "comboBoxPortOut";
            this.comboBoxPortOut.Size = new System.Drawing.Size(70, 20);
            this.comboBoxPortOut.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(260, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "→";
            // 
            // buttonLogClear
            // 
            this.buttonLogClear.Location = new System.Drawing.Point(41, 16);
            this.buttonLogClear.Name = "buttonLogClear";
            this.buttonLogClear.Size = new System.Drawing.Size(47, 20);
            this.buttonLogClear.TabIndex = 6;
            this.buttonLogClear.Text = "Clear";
            this.buttonLogClear.UseVisualStyleBackColor = true;
            this.buttonLogClear.Click += new System.EventHandler(this.buttonLogClear_Click);
            // 
            // textBoxLogMean
            // 
            this.textBoxLogMean.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxLogMean.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.textBoxLogMean.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.textBoxLogMean.Location = new System.Drawing.Point(329, 42);
            this.textBoxLogMean.Multiline = true;
            this.textBoxLogMean.Name = "textBoxLogMean";
            this.textBoxLogMean.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBoxLogMean.Size = new System.Drawing.Size(428, 399);
            this.textBoxLogMean.TabIndex = 7;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 454);
            this.Controls.Add(this.textBoxLogMean);
            this.Controls.Add(this.buttonLogClear);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxPortOut);
            this.Controls.Add(this.comboBoxPortIn);
            this.Controls.Add(this.buttonConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxLog);
            this.Name = "MainForm";
            this.Text = "Sky-Watcher Motor Controller Monitor";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxLog;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonConnect;
        private System.IO.Ports.SerialPort serialPortIn;
        private System.IO.Ports.SerialPort serialPortOut;
        private System.Windows.Forms.ComboBox comboBoxPortIn;
        private System.Windows.Forms.ComboBox comboBoxPortOut;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button buttonLogClear;
        private System.Windows.Forms.TextBox textBoxLogMean;
    }
}

