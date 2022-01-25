using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;

namespace WindowsService1
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }
        string filePath = @"D:\MyServiceLog.txt";
        //定时器
        System.Timers.Timer tmBak = new System.Timers.Timer();
        //服务器启动时写日志、开启定时器
        protected override void OnStart(string[] args)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "服务启动！");
            }
            //到时间的时候执行事件 
            tmBak.Interval = 2000;//2S执行一次
            tmBak.AutoReset = true;//执行一次 false，一直执行true 
            //是否执行System.Timers.Timer.Elapsed事件 
            tmBak.Enabled = true;
            tmBak.Start();
            tmBak.Elapsed += new System.Timers.ElapsedEventHandler(WriteLog);

        }

        protected void WriteLog(object source, System.Timers.ElapsedEventArgs e)
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "执行定时任务写操作！");
            }
        }

        //服务停止时写日志
        protected override void OnStop()
        {
            using (FileStream stream = new FileStream(filePath, FileMode.Append))
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "服务停止！");
            }
        }
    }
}
