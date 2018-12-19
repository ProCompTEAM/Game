using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace BRClient
{
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
		}
		
		void Btn_exitLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			MessageBox.Show("Спасибо за использование!", "Сярожа One Love", MessageBoxButtons.OK, MessageBoxIcon.Information);
			Environment.Exit(0);
		}
		
		void Btn_hideLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.WindowState = FormWindowState.Minimized;
		}
		
		void List_ipsSelectedIndexChanged(object sender, EventArgs e)
		{
			textbox_ip.Text = list_ips.SelectedItem.ToString();
		}
		
		void Btn_connectClick(object sender, EventArgs e)
		{
			Process p = new Process();
			p.StartInfo.FileName = "IExplore.exe";
			p.StartInfo.Arguments = "-nohome -k http://mcrpg.ru/game/";
			p.Start();
		}
	}
}
