namespace Youtube_Player
{
	partial class Form1
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.YTplayer = new AxShockwaveFlashObjects.AxShockwaveFlash();
			this.button2 = new System.Windows.Forms.Button();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.menue_close = new System.Windows.Forms.ToolStripMenuItem();
			this.menue_ontop = new System.Windows.Forms.ToolStripMenuItem();
			this.menue_removeplayed = new System.Windows.Forms.ToolStripMenuItem();
			this.sucheToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button3 = new System.Windows.Forms.Button();
			this.listView1 = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.button4 = new System.Windows.Forms.Button();
			this.button5 = new System.Windows.Forms.Button();
			this.button6 = new System.Windows.Forms.Button();
			this.button7 = new System.Windows.Forms.Button();
			this.menue_autoplay = new System.Windows.Forms.ToolStripMenuItem();
			((System.ComponentModel.ISupportInitialize)(this.YTplayer)).BeginInit();
			this.menuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BackColor = System.Drawing.Color.Black;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.textBox1.ForeColor = System.Drawing.Color.White;
			this.textBox1.Location = new System.Drawing.Point(0, 595);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(371, 26);
			this.textBox1.TabIndex = 2;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.BackColor = System.Drawing.Color.Black;
			this.button1.Enabled = false;
			this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button1.ForeColor = System.Drawing.Color.White;
			this.button1.Location = new System.Drawing.Point(483, 595);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(116, 26);
			this.button1.TabIndex = 3;
			this.button1.Text = "Add from Clipboard";
			this.button1.UseVisualStyleBackColor = false;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// YTplayer
			// 
			this.YTplayer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.YTplayer.Enabled = true;
			this.YTplayer.Location = new System.Drawing.Point(0, 25);
			this.YTplayer.Name = "YTplayer";
			this.YTplayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("YTplayer.OcxState")));
			this.YTplayer.Size = new System.Drawing.Size(715, 441);
			this.YTplayer.TabIndex = 4;
			this.YTplayer.FlashCall += new AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEventHandler(this.YTplayer_FlashCall);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.BackColor = System.Drawing.Color.Black;
			this.button2.Enabled = false;
			this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button2.ForeColor = System.Drawing.Color.White;
			this.button2.Location = new System.Drawing.Point(598, 595);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(116, 26);
			this.button2.TabIndex = 5;
			this.button2.Text = "Remove";
			this.button2.UseVisualStyleBackColor = false;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.BackColor = System.Drawing.Color.Black;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menue_close,
            this.menue_ontop,
            this.menue_autoplay,
            this.menue_removeplayed,
            this.sucheToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(714, 24);
			this.menuStrip1.TabIndex = 6;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// menue_close
			// 
			this.menue_close.ForeColor = System.Drawing.Color.White;
			this.menue_close.Name = "menue_close";
			this.menue_close.Size = new System.Drawing.Size(48, 20);
			this.menue_close.Text = "Close";
			this.menue_close.Click += new System.EventHandler(this.menue_close_Click);
			// 
			// menue_ontop
			// 
			this.menue_ontop.AutoSize = false;
			this.menue_ontop.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
			this.menue_ontop.CheckOnClick = true;
			this.menue_ontop.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.menue_ontop.ForeColor = System.Drawing.Color.White;
			this.menue_ontop.Image = global::Youtube_Player.Properties.Resources._unchecked;
			this.menue_ontop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.menue_ontop.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menue_ontop.Name = "menue_ontop";
			this.menue_ontop.Size = new System.Drawing.Size(110, 20);
			this.menue_ontop.Text = "Always on Top";
			this.menue_ontop.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.menue_ontop.Click += new System.EventHandler(this.menue_ontop_Click);
			// 
			// menue_removeplayed
			// 
			this.menue_removeplayed.ForeColor = System.Drawing.Color.White;
			this.menue_removeplayed.Image = global::Youtube_Player.Properties.Resources._checked;
			this.menue_removeplayed.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menue_removeplayed.Name = "menue_removeplayed";
			this.menue_removeplayed.Size = new System.Drawing.Size(113, 20);
			this.menue_removeplayed.Text = "Remove Played";
			this.menue_removeplayed.Click += new System.EventHandler(this.menue_removeplayed_Click);
			// 
			// sucheToolStripMenuItem
			// 
			this.sucheToolStripMenuItem.ForeColor = System.Drawing.Color.White;
			this.sucheToolStripMenuItem.Name = "sucheToolStripMenuItem";
			this.sucheToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.sucheToolStripMenuItem.Text = "Suche";
			this.sucheToolStripMenuItem.Click += new System.EventHandler(this.sucheToolStripMenuItem_Click);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.BackColor = System.Drawing.Color.Black;
			this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button3.ForeColor = System.Drawing.Color.White;
			this.button3.Location = new System.Drawing.Point(369, 595);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(116, 26);
			this.button3.TabIndex = 7;
			this.button3.Text = "Add";
			this.button3.UseVisualStyleBackColor = false;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.BackColor = System.Drawing.Color.Black;
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.listView1.ForeColor = System.Drawing.Color.White;
			this.listView1.FullRowSelect = true;
			this.listView1.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
			this.listView1.Location = new System.Drawing.Point(0, 492);
			this.listView1.MultiSelect = false;
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(685, 103);
			this.listView1.TabIndex = 8;
			this.listView1.UseCompatibleStateImageBehavior = false;
			this.listView1.View = System.Windows.Forms.View.Details;
			this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
			this.listView1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.listView1_MouseDoubleClick);
			this.listView1.Resize += new System.EventHandler(this.listView1_Resize);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Width = 250;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Width = 300;
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.button4.BackColor = System.Drawing.Color.Black;
			this.button4.Enabled = false;
			this.button4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button4.ForeColor = System.Drawing.Color.White;
			this.button4.Location = new System.Drawing.Point(0, 467);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(359, 25);
			this.button4.TabIndex = 9;
			this.button4.Text = "<<<";
			this.button4.UseVisualStyleBackColor = false;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button5
			// 
			this.button5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button5.BackColor = System.Drawing.Color.Black;
			this.button5.Enabled = false;
			this.button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button5.ForeColor = System.Drawing.Color.White;
			this.button5.Location = new System.Drawing.Point(355, 467);
			this.button5.Name = "button5";
			this.button5.Size = new System.Drawing.Size(359, 25);
			this.button5.TabIndex = 10;
			this.button5.Text = ">>>";
			this.button5.UseVisualStyleBackColor = false;
			this.button5.Click += new System.EventHandler(this.button5_Click);
			// 
			// button6
			// 
			this.button6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button6.BackColor = System.Drawing.Color.Black;
			this.button6.Enabled = false;
			this.button6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button6.ForeColor = System.Drawing.Color.White;
			this.button6.Location = new System.Drawing.Point(683, 491);
			this.button6.Name = "button6";
			this.button6.Size = new System.Drawing.Size(31, 53);
			this.button6.TabIndex = 11;
			this.button6.Text = "▲";
			this.button6.UseVisualStyleBackColor = false;
			this.button6.Click += new System.EventHandler(this.button6_Click);
			// 
			// button7
			// 
			this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button7.BackColor = System.Drawing.Color.Black;
			this.button7.Enabled = false;
			this.button7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.button7.ForeColor = System.Drawing.Color.White;
			this.button7.Location = new System.Drawing.Point(683, 543);
			this.button7.Name = "button7";
			this.button7.Size = new System.Drawing.Size(31, 53);
			this.button7.TabIndex = 12;
			this.button7.Text = "▼";
			this.button7.UseVisualStyleBackColor = false;
			this.button7.Click += new System.EventHandler(this.button7_Click);
			// 
			// menue_autoplay
			// 
			this.menue_autoplay.ForeColor = System.Drawing.Color.White;
			this.menue_autoplay.Image = global::Youtube_Player.Properties.Resources._checked;
			this.menue_autoplay.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
			this.menue_autoplay.Name = "menue_autoplay";
			this.menue_autoplay.Size = new System.Drawing.Size(80, 20);
			this.menue_autoplay.Text = "Autoplay";
			this.menue_autoplay.Click += new System.EventHandler(this.menue_autoplay_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.ControlDark;
			this.ClientSize = new System.Drawing.Size(714, 622);
			this.Controls.Add(this.button7);
			this.Controls.Add(this.button6);
			this.Controls.Add(this.button5);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.YTplayer);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new System.Drawing.Size(730, 660);
			this.Name = "Form1";
			this.Text = "Youtube Player";
			this.Activated += new System.EventHandler(this.Form1_Activated);
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.Form1_Load);
			this.Resize += new System.EventHandler(this.Form1_Resize);
			((System.ComponentModel.ISupportInitialize)(this.YTplayer)).EndInit();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menue_close;
        private System.Windows.Forms.ToolStripMenuItem menue_ontop;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ToolStripMenuItem menue_removeplayed;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button5;
		private System.Windows.Forms.ToolStripMenuItem sucheToolStripMenuItem;
		public System.Windows.Forms.ListView listView1;
		private AxShockwaveFlashObjects.AxShockwaveFlash YTplayer;
		private System.Windows.Forms.Button button6;
		private System.Windows.Forms.Button button7;
		private System.Windows.Forms.ToolStripMenuItem menue_autoplay;
	}
}

