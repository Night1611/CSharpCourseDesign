﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CourseDesign {
	/// <summary>
	/// MainWindow.xaml 的交互逻辑
	/// </summary>
	public partial class Login : Window {
		public Login() {
			InitializeComponent();
		}

		public Login(string UserID) {
			InitializeComponent();
			textBox_id.Text = UserID;
		}

		private void button_register_Click(object sender, RoutedEventArgs e) {

		}

		private void button_login_Click(object sender, RoutedEventArgs e) {
			string[] ip = textBlock_ip.Text.Split(':');
			TcpClient tcpClient = new TcpClient();
			IPAddress ServerIP = IPAddress.Parse(ip[0]);
			tcpClient.Connect(ServerIP, int.Parse(ip[1])); //建立与服务器的连接

			NetworkStream networkStream = tcpClient.GetStream();
			if (networkStream.CanWrite) {
				IMClassLibrary.LoginDataPackage loginDataPackage = new IMClassLibrary.LoginDataPackage(textBox_id.Text, "Server", textBox_id.Text, passwordBox.Password); //初始化登录数据包
				Byte[] sendBytes = loginDataPackage.DataPackageToBytes(); //登录数据包转化为字节数组
				networkStream.Write(sendBytes, 0, sendBytes.Length);
			}

			ClientWindow clientWindow = new ClientWindow(textBox_id.Text); //传入用户名
			clientWindow.Show();
			Close();
		}

		private void button_about_Click(object sender, RoutedEventArgs e) {
			About about = new About();
			about.ShowDialog();
		}
	}
}
