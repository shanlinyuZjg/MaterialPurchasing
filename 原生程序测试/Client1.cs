using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 原生程序测试
{
    public partial class Client1 : Form
    {
        //定义Socket对象
        Socket clientSocket;
        //创建接收消息的线程
        Thread threadReceive;
        //接收服务端发送的数据
        string str;

        public Client1()
        {
            InitializeComponent();
        }

        private void Client1_Load(object sender, EventArgs e)
        {
            DataTable dtTemp = new DataTable();
            dtTemp.Columns.Add("Id");
            dtTemp.Columns.Add("姓名");
            dtTemp.Columns.Add("年龄");
            dtTemp.Columns.Add("成绩");
            dtTemp.Columns.Add("排名");

            for(int i = 0;i <= 100;i++)
            {
                DataRow dr = dtTemp.NewRow();
                dr["Id"] = i;
                dr["姓名"] ="姓名"+i ;
                dr["年龄"] =i ;
                dr["成绩"] =i ;
                dr["排名"] = i;
                dtTemp.Rows.Add(dr.ItemArray);
            }
            dgv.DataSource = dtTemp;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            IPAddress ip = IPAddress.Parse(this.text_ip.Text.Trim());
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                //连接服务端
                clientSocket.Connect(ip, Convert.ToInt32(this.text_port.Text.Trim()));
                //开启线程不停的接收服务端发送的数据
                threadReceive = new Thread(new ThreadStart(Receive));
                threadReceive.IsBackground = true;
                threadReceive.Start();
                //设置连接按钮在连接服务端后状态为不可点
                this.btnConnect.Enabled = false;
            }
            catch
            {
                MessageBox.Show("连接服务端失败，请确认ip和端口是否填写正确", "连接服务端失败");
            }
        }
        //接收服务端消息的线程方法
        private void Receive()
        {
            try
            {
                while (true)
                {
                    byte[] buff = new byte[20000];
                    int r = clientSocket.Receive(buff);
                    str = Encoding.Default.GetString(buff, 0, r);
                    this.Invoke(new Action(() => { this.text_log1.Text = str; }));
                }
            }
            catch
            {
                MessageBox.Show("获取服务端参数失败", "获取服务端参数失败");
            }
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            //clientSocket关闭
            clientSocket.Close();
            //threadReceive关闭

        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg = this.text_log2.Text.Trim();
                byte[] buffer = new byte[1024000];
                buffer = Encoding.Default.GetBytes(strMsg);
                clientSocket.Send(buffer);
            }
            catch
            {
                MessageBox.Show("发送数据失败", "发送数据失败");
            }

        }

        private void btnSendDataTable_Click(object sender, EventArgs e)
        {           
            clientSocket.Send(GetBinaryFormatDataSet((DataTable)dgv.DataSource));
        }

        public static byte[] GetBinaryFormatDataSet(DataTable dt)
        {
            //创建内存流
            MemoryStream memStream = new MemoryStream();
            //产生二进制序列化格式
            IFormatter formatter = new BinaryFormatter();
            //指定DataSet串行化格式是二进制
            dt.RemotingFormat = SerializationFormat.Binary;
            //串行化到内存中
            formatter.Serialize(memStream, dt);
            //将DataSet转化成byte[]
            byte[] binaryResult = memStream.ToArray();
            //清空和释放内存流
            memStream.Close();
            memStream.Dispose();
            return binaryResult;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string aa = "shandong   ddd  aaa   12345";
            string bb = "山东药玻股份有限公司";
            // MessageBox.Show(aa.Replace(" ",""));
            MessageBox.Show("长度：" +bb.Length);
            MessageBox.Show(bb.Substring(0,2));
        }
    }
}
