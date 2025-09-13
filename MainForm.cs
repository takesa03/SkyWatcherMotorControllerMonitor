using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace SkyWatcherMotorControllerMonitor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private string lastCommand = "";
        private int lastChannel = 0;
        private string lastParam = "";

        private void LoadAvailablePorts()
        {
            comboBoxPortIn.Items.AddRange(SerialPort.GetPortNames());
            if (comboBoxPortIn.Items.Count > 0)
            {
                for (int i=0; i < comboBoxPortIn.Items.Count; i++)
                {
                    if (comboBoxPortIn.Items[i].ToString() == serialPortIn.PortName)
                    {
                        comboBoxPortIn.SelectedIndex = i;
                        break;
                    }
                }
            }

            comboBoxPortOut.Items.AddRange(SerialPort.GetPortNames());
            if (comboBoxPortOut.Items.Count > 0)
            {
                for (int i = 0; i < comboBoxPortOut.Items.Count; i++)
                {
                    if (comboBoxPortOut.Items[i].ToString() == serialPortOut.PortName)
                    {
                        comboBoxPortOut.SelectedIndex = i;
                        break;
                    }
                }
            }

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (serialPortIn.IsOpen)
                {
                    serialPortIn.Close();
                    serialPortOut.Close();
                    buttonConnect.ForeColor = Color.Black;
                    //Debug.WriteLine("Port Close");
                    ShowReceivedData("Port Close" + Environment.NewLine);

                }
                else
                {
                    serialPortIn.Open();
                    serialPortOut.Open();
                    buttonConnect.ForeColor = Color.Red;
                    //Debug.WriteLine("Port Open");
                    //Debug.WriteLine(serialPortIn.PortName);
                    //Debug.WriteLine(serialPortOut.PortName);
                    //textBoxLog.Clear();
                    ShowReceivedData("Port Open: "
                        + serialPortIn.PortName 
                        + " " 
                        + serialPortOut.PortName 
                        + Environment.NewLine);
                    ShowReceivedData("Wait for SynScanPro to Connect." +Environment.NewLine);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        /// <summary>
        /// SynScanProからのデータを受信したときのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPortIn_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                //string data = serialPortIn.ReadLine();
                string data = serialPortIn.ReadExisting();
                //Debug.WriteLine("Send: " + data);
                ShowReceivedData("--> " + data + Environment.NewLine);

                if (data.Substring(0, 1) == ":")
                {
                    lastCommand = data.Substring(1, 1);
                    lastChannel = int.Parse(data.Substring(2, 1));
                    lastParam = "";
                    string mean = "";
                    switch (lastCommand)
                    {
                        case "b":
                            mean = "Inquire Timer Interrupt Freq";
                            break;
                        case "e":
                            mean = "Inquire Motor Board Version";
                            break;
                        case "f":
                            mean = "Inquire Status";
                            break;
                        case "q":
                            mean = "Extended Inquire";
                            break;
                        case "s":
                            mean = "Inquire PEC period";
                            break;
                        case "P":
                            mean = "Set AutoGuide Speed";
                            lastParam = data.Substring(3);
                            break;
                        case "X":
                            mean = "X_" +data.Substring(3, 2);
                            lastCommand = data.Substring(3, 2);
                            if (data.Length > 5)
                                lastParam = data.Substring(5);
                            break;
                        case "V":
                            mean = "Set Polar Scope LED brightness";
                            lastParam = data.Substring(3);
                            break;
                        default:
                            mean = "unknown";
                            break;
                    }
                    mean += " Ch" + lastChannel.ToString();
                    if (!lastParam.Equals(""))
                        mean += " Param:" + InsertSpaceEvery8Chars(lastParam);
                    ShowReceivedMean("--> " + mean + Environment.NewLine);
                    
                }
                serialPortOut.Write(data);  //転送
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }


        }

        /// <summary>
        /// モーターコントローラーからのデータを受信したときのイベントハンドラ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void serialPortOut_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = serialPortOut.ReadExisting();
                //Debug.WriteLine("Receive: " + data);
                ShowReceivedData("<-- " + data + Environment.NewLine);

                if (data.Substring(0, 1) == "=")
                {
                    string mean = "";
                    if (data.Length > 1)
                        mean = InsertSpaceEvery8Chars(data.Substring(1));
                    ShowReceivedMean("<--  " + mean + Environment.NewLine);

                    //if (data.Substring(1).Length >= 32)
                    //{
                    //    long long1 = DecodeBigEndianHexToLong(data.Substring(1, 8));
                    //    long long2 = DecodeBigEndianHexToLong(data.Substring(9, 8));
                    //    long long3 = DecodeBigEndianHexToLong(data.Substring(17, 8));
                    //    long long4 = DecodeBigEndianHexToLong(data.Substring(25, 8));
                    //    mean = long1.ToString() + " " + long2.ToString() + " " + long3.ToString() + " " + long4.ToString();
                    //    ShowReceivedMean("     " + mean + Environment.NewLine);
                    //}

                } else if (data.Substring(0, 1) == "!")
                {
                    //エラーメッセージ
                    ShowReceivedMean("<--  Error" + Environment.NewLine);
                }

                serialPortIn.Write(data);  //転送
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return;
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadAvailablePorts();

        }

        private void ShowReceivedData(string text)
        {
            if (textBoxLog.InvokeRequired)
            {
                textBoxLog.Invoke(new Action<string>(ShowReceivedData), text);
            }
            else
            {
                //textBoxLog.AppendText(text + "\r\n");
                textBoxLog.AppendText(text);
            }
        }

        private void ShowReceivedMean(string text)
        {
            if (textBoxLogMean.InvokeRequired)
            {
                textBoxLogMean.Invoke(new Action<string>(ShowReceivedMean), text);
            }
            else
            {
                textBoxLogMean.AppendText(text);
            }
        }


        private void buttonLogClear_Click(object sender, EventArgs e)
        {
            textBoxLog.Clear();
            textBoxLogMean.Clear();
        }

        private string InsertSpaceEvery8Chars(string text)
        {
            int chunkSize = 8;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            for (int i = 0; i < text.Length; i += chunkSize)
            {
                int length = Math.Min(chunkSize, text.Length - i);
                sb.Append(text.Substring(i, length));

                // 最後のチャンクでなければスペースを追加
                if (i + chunkSize < text.Length)
                    sb.Append(" ");
            }

            return sb.ToString();
        }

        private long DecodeSkyWatcherHexToLong(string hex)
        {
            // 入力チェック：6文字または8文字の16進文字列
            if (hex.Length != 6 && hex.Length != 8)
                throw new ArgumentException("6桁または8桁の16進文字列が必要です");

            // 2文字ずつ分割してバイト順を逆にする（リトルエンディアン）
            var bytes = new List<string>();
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes.Add(hex.Substring(i, 2));
            }
            bytes.Reverse(); // バイト順を逆転

            string reordered = string.Join("", bytes);

            // 16進数として long に変換
            return Convert.ToInt64(reordered, 16);
        }

        private long DecodeBigEndianHexToLong(string hex)
        {
            // 入力チェック：6文字または8文字の16進文字列
            if (hex.Length != 6 && hex.Length != 8)
                throw new ArgumentException("6桁または8桁の16進文字列が必要です");

            // そのまま順番を保持して変換（ビッグエンディアン）
            return Convert.ToInt64(hex, 16);
        }

    }
}
