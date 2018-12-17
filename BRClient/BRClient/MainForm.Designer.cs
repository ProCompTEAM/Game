/*
 * Создано в SharpDevelop.
 * Пользователь: Kirill
 * Дата: 28.11.2018
 * Время: 16:19
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace BRClient
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.PictureBox pic_logo;
		private System.Windows.Forms.LinkLabel btn_exit;
		private System.Windows.Forms.TextBox textbox_ip;
		private System.Windows.Forms.Button btn_connect;
		private System.Windows.Forms.LinkLabel btn_hide;
		private System.Windows.Forms.ListBox list_ips;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.pic_logo = new System.Windows.Forms.PictureBox();
			this.btn_exit = new System.Windows.Forms.LinkLabel();
			this.textbox_ip = new System.Windows.Forms.TextBox();
			this.btn_connect = new System.Windows.Forms.Button();
			this.btn_hide = new System.Windows.Forms.LinkLabel();
			this.list_ips = new System.Windows.Forms.ListBox();
			((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
			this.SuspendLayout();
			// 
			// pic_logo
			// 
			this.pic_logo.Image = ((System.Drawing.Image)(resources.GetObject("pic_logo.Image")));
			this.pic_logo.Location = new System.Drawing.Point(12, 12);
			this.pic_logo.Name = "pic_logo";
			this.pic_logo.Size = new System.Drawing.Size(362, 165);
			this.pic_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pic_logo.TabIndex = 0;
			this.pic_logo.TabStop = false;
			// 
			// btn_exit
			// 
			this.btn_exit.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_exit.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.btn_exit.LinkColor = System.Drawing.Color.White;
			this.btn_exit.Location = new System.Drawing.Point(739, 9);
			this.btn_exit.Name = "btn_exit";
			this.btn_exit.Size = new System.Drawing.Size(50, 57);
			this.btn_exit.TabIndex = 1;
			this.btn_exit.TabStop = true;
			this.btn_exit.Text = "✕";
			this.btn_exit.VisitedLinkColor = System.Drawing.Color.White;
			this.btn_exit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Btn_exitLinkClicked);
			// 
			// textbox_ip
			// 
			this.textbox_ip.BackColor = System.Drawing.Color.DarkSlateGray;
			this.textbox_ip.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textbox_ip.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textbox_ip.ForeColor = System.Drawing.Color.White;
			this.textbox_ip.Location = new System.Drawing.Point(13, 547);
			this.textbox_ip.Name = "textbox_ip";
			this.textbox_ip.Size = new System.Drawing.Size(583, 41);
			this.textbox_ip.TabIndex = 2;
			// 
			// btn_connect
			// 
			this.btn_connect.FlatAppearance.BorderColor = System.Drawing.Color.White;
			this.btn_connect.FlatAppearance.BorderSize = 0;
			this.btn_connect.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.btn_connect.ForeColor = System.Drawing.Color.White;
			this.btn_connect.Location = new System.Drawing.Point(602, 548);
			this.btn_connect.Name = "btn_connect";
			this.btn_connect.Size = new System.Drawing.Size(183, 41);
			this.btn_connect.TabIndex = 3;
			this.btn_connect.Text = "Подключиться";
			this.btn_connect.UseVisualStyleBackColor = true;
			this.btn_connect.Click += new System.EventHandler(this.Btn_connectClick);
			// 
			// btn_hide
			// 
			this.btn_hide.Font = new System.Drawing.Font("Microsoft Sans Serif", 25.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btn_hide.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
			this.btn_hide.LinkColor = System.Drawing.Color.White;
			this.btn_hide.Location = new System.Drawing.Point(693, 0);
			this.btn_hide.Name = "btn_hide";
			this.btn_hide.Size = new System.Drawing.Size(50, 57);
			this.btn_hide.TabIndex = 4;
			this.btn_hide.TabStop = true;
			this.btn_hide.Text = "_";
			this.btn_hide.VisitedLinkColor = System.Drawing.Color.White;
			this.btn_hide.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Btn_hideLinkClicked);
			// 
			// list_ips
			// 
			this.list_ips.BackColor = System.Drawing.Color.DarkSlateGray;
			this.list_ips.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.list_ips.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.list_ips.ForeColor = System.Drawing.Color.Silver;
			this.list_ips.FormattingEnabled = true;
			this.list_ips.ItemHeight = 25;
			this.list_ips.Items.AddRange(new object[] {
			"127.0.0.1:48888",
			"193.124.178.77:48888"});
			this.list_ips.Location = new System.Drawing.Point(13, 184);
			this.list_ips.Name = "list_ips";
			this.list_ips.Size = new System.Drawing.Size(583, 352);
			this.list_ips.Sorted = true;
			this.list_ips.TabIndex = 5;
			this.list_ips.SelectedIndexChanged += new System.EventHandler(this.List_ipsSelectedIndexChanged);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkSlateGray;
			this.ClientSize = new System.Drawing.Size(800, 600);
			this.Controls.Add(this.list_ips);
			this.Controls.Add(this.btn_hide);
			this.Controls.Add(this.btn_connect);
			this.Controls.Add(this.textbox_ip);
			this.Controls.Add(this.btn_exit);
			this.Controls.Add(this.pic_logo);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "BRClient";
			((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
	}
}
