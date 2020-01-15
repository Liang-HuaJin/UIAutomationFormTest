using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Windows.Automation;
using System.Windows.Forms;
using static UIAutomationFormTest.WinAPI;

namespace UIAutomationFormTest
{
    public partial class Form1 : Form
    {
        public AutomationElement Desktop;
        public Int32 ElementCount = 0;
        public List<AutomationElement> ElementList;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;//干掉检测 不再检测跨线程
        }

        private void StartFindElement_Click(object sender, EventArgs e)
        {
            ThreadStart thStart = new ThreadStart(ShowElement);//threadStart委托 
            Thread thread = new Thread(thStart)
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            thread.Start();
        }

        public void ShowOneElement()
        {
            Thread.Sleep(3000);
            POINTAPI point = new POINTAPI();
            WinAPI.GetCursorPos(ref point);
            this.ShowElementText.Text = string.Format("X:{0},Y:{1}\n", point.X.ToString(), point.Y.ToString());
            IntPtr intPtr = WinAPI.WindowFromPoint(point.X, point.Y);
            AutomationElement startElement = AutomationElement.FromHandle(intPtr);
            List<UIAutomationElement> list = FindElement.GetElementList(startElement, point.X, point.Y);
            for (int i = 0; i < list.Count; i++)
            {
                AppendText(list[i], i);
            }
            
        }

        public void ShowElement()
        {
            while (true)
            {
                Thread.Sleep(3000);
                POINTAPI point = new POINTAPI();
                WinAPI.GetCursorPos(ref point);
                this.ShowElementText.Text = string.Format("X:{0},Y:{1}\n", point.X.ToString(), point.Y.ToString());
                IntPtr intPtr = WinAPI.WindowFromPoint(point.X, point.Y);
                AutomationElement startElement = AutomationElement.FromHandle(intPtr);
                List<UIAutomationElement> list = FindElement.GetElementList(startElement, point.X, point.Y);
                for (int i = 0; i < list.Count; i++)
                {
                    AppendText(list[i], i);
                }
                
            }
        }

        public void AppendText(AutomationElement element, string str)
        {
            string elementAttribute = string.Format("\n{0}\nName:{1}\nAutomationId:{2}\nFrameworkId:{3}\nClassNmae:{4}\nRuntimeId:{5}\nLabeledBy:{6}\n",
                    str, element.Current.Name, element.Current.AutomationId, element.Current.FrameworkId, element.Current.ClassName, 
                    string.Join(",", element.GetRuntimeId()), element.Current.LabeledBy);
            this.ShowElementText.AppendText(elementAttribute);            
        }
        public void AppendText(UIAutomationElement element, int num)
        {
            string elementAttribute = string.Format("{0}\nName:{1}\nAutomationId:{2}\nControlType:{3}\nClassNmae:{4}\n",
                    num.ToString(), element.ElementName, element.ElementAutomationId, element.ElementControlType, element.ElementClassName);
            this.ShowElementText.AppendText(elementAttribute);
        }

        private void ShowElementText_TextChanged(object sender, EventArgs e)
        {

        }
        public void WatchTime()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            sw.Stop();
            FileStream fs = new FileStream("", FileMode.Append, FileAccess.Write);
            StreamWriter streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine(string.Format("（2）DateTime.now:{0}, RunTime:{1}", DateTime.Now, sw.Elapsed));
            streamWriter.Close();
            streamWriter.Dispose();
            fs.Close();
            fs.Dispose();
        }

    }
}
