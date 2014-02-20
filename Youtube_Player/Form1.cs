using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace Youtube_Player
{
	public partial class Form1 : Form
	{
		int play_index = -1;
		int width = 0;
		int height = 0;
		string url_global = "";
		bool ende = false;
		bool autoplay = true;

		public Form1()
		{
			InitializeComponent();
			menuStrip1.Renderer = new MyRenderer();
		}

		private class MyRenderer : ToolStripProfessionalRenderer
		{
			public MyRenderer() : base(new MyColors()) { }
		}

		private class MyColors : ProfessionalColorTable
		{
			public override Color MenuItemBorder
			{
				get { return Color.Black; }
			}
			public override Color MenuItemSelected
			{
				get { return Color.Yellow; }
			}
			public override Color MenuItemSelectedGradientBegin
			{
				get { return Color.Orange; }
			}
			public override Color MenuItemSelectedGradientEnd
			{
				get { return Color.Yellow; }
			}
		}

		private void YTplayer_FlashCall(object sender, AxShockwaveFlashObjects._IShockwaveFlashEvents_FlashCallEvent e)
		{
			try
			{
				if (e.request.Contains("onYouTubePlayerReady") ||
					e.request.Contains("YTStateChange") ||
					e.request.Contains("YTError"))
				{
					Console.Write("YTplayer_FlashCall: raw: " + e.request.ToString() + "\r\n");
					// message is in xml format so we need to parse it
					XmlDocument document = new XmlDocument();
					document.LoadXml(e.request);
					// get attributes to see which command flash is trying to call
					XmlAttributeCollection attributes = document.FirstChild.Attributes;
					String command = attributes.Item(0).InnerText;
					// get parameters
					XmlNodeList list = document.GetElementsByTagName("arguments");
					List<string> listS = new List<string>();
					foreach (XmlNode l in list)
					{
						listS.Add(l.InnerText);
					}
					Console.Write("YTplayer_FlashCall: \"" + command.ToString() + "(" + string.Join(",", listS) + ")\r\n");
					// Interpret command
					switch (command)
					{
						case "onYouTubePlayerReady": YTready(listS[0]); break;
						case "YTStateChange": YTStateChange(listS[0]); break;
						case "YTError": YTStateError(listS[0]); break;
						default: Console.Write("YTplayer_FlashCall: (unknownCommand: " + command + ")\r\n"); break;
					}
				}
				else
				{
					e.request = "";
				}
				/*if (e.request.Contains("ads"))
				{
					Console.Write("YTplayer_FlashCall: raw: " + e.request.ToString() + "\r\n");
				}*/
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private string YTplayer_CallFlash(string ytFunction)
		{
			string flashXMLrequest = "";
			string response = "";
			string flashFunction = "";
			List<string> flashFunctionArgs = new List<string>();

			Regex func2xml = new Regex(@"([a-z][a-z0-9]*)(\(([^)]*)\))?", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			Match fmatch = func2xml.Match(ytFunction);

			if (fmatch.Captures.Count != 1)
			{
				Console.Write("bad function request string");
				return "";
			}

			flashFunction = fmatch.Groups[1].Value.ToString();
			flashXMLrequest = "<invoke name=\"" + flashFunction + "\" returntype=\"xml\">";
			if (fmatch.Groups[3].Value.Length > 0)
			{
				flashFunctionArgs = parseDelimitedString(fmatch.Groups[3].Value);
				if (flashFunctionArgs.Count > 0)
				{
					flashXMLrequest += "<arguments><string>";
					flashXMLrequest += string.Join("</string><string>", flashFunctionArgs);
					flashXMLrequest += "</string></arguments>";
				}
			}
			flashXMLrequest += "</invoke>";

			try
			{
				Console.Write("YTplayer_CallFlash: \"" + flashXMLrequest + "\"\r\n");
				response = YTplayer.CallFunction(flashXMLrequest);
				Console.Write("YTplayer_CallFlash_response: \"" + response + "\"\r\n");
			}
			catch
			{
				Console.Write("YTplayer_CallFlash: error \"" + flashXMLrequest + "\"\r\n");
			}

			return response;
		}

		private static List<string> parseDelimitedString(string arguments, char delim = ',')
		{
			bool inQuotes = false;
			bool inNonQuotes = false;
			int whiteSpaceCount = 0;

			List<string> strings = new List<string>();

			StringBuilder sb = new StringBuilder();
			foreach (char c in arguments)
			{
				if (c == '\'' || c == '"')
				{
					if (!inQuotes)
						inQuotes = true;
					else
						inQuotes = false;

					whiteSpaceCount = 0;
				}
				else if (c == delim)
				{
					if (!inQuotes)
					{
						if (whiteSpaceCount > 0 && inQuotes)
						{
							sb.Remove(sb.Length - whiteSpaceCount, whiteSpaceCount);
							inNonQuotes = false;
						}
						strings.Add(sb.Replace("'", string.Empty).Replace("\"", string.Empty).ToString());
						sb.Remove(0, sb.Length);
					}
					else
					{
						sb.Append(c);
					}
					whiteSpaceCount = 0;
				}
				else if (char.IsWhiteSpace(c))
				{
					if (inNonQuotes || inQuotes)
					{
						sb.Append(c);
						whiteSpaceCount++;
					}
				}
				else
				{
					if (!inQuotes) inNonQuotes = true;
					sb.Append(c);
					whiteSpaceCount = 0;
				}
			}
			strings.Add(sb.Replace("'", string.Empty).Replace("\"", string.Empty).ToString());


			return strings;
		}

		private void YTready(string playerID)
		{
			//YTState = true;
			//start eventHandlers
			YTplayer_CallFlash("addEventListener(\"onStateChange\",\"YTStateChange\")");
			YTplayer_CallFlash("addEventListener(\"onError\",\"YTError\")");
			YTplayer_CallFlash("setPlaybackQuality(\"hd720\")");
		}

		private void YTStateChange(string YTplayState)
		{
			switch (int.Parse(YTplayState))
			{
				case -1:	//not started yet
					break;
				case 1:		//playing
					if (listView1.Items[play_index].Text.StartsWith("»") || listView1.Items[play_index].Text.StartsWith("ǁ"))
					{
						listView1.Items[play_index].Text = listView1.Items[play_index].Text.Substring(2);
					}
					listView1.Items[play_index].Text = "» " + listView1.Items[play_index].Text;
					break;
				case 2:		//paused
					if (listView1.Items[play_index].Text.StartsWith("»") || listView1.Items[play_index].Text.StartsWith("ǁ"))
					{
						listView1.Items[play_index].Text = listView1.Items[play_index].Text.Substring(2);
					}
					listView1.Items[play_index].Text = "ǁ " + listView1.Items[play_index].Text;
					break;
				case 0:		//ended
					if (listView1.Items[play_index].Text.StartsWith("»") || listView1.Items[play_index].Text.StartsWith("ǁ"))
					{
						listView1.Items[play_index].Text = listView1.Items[play_index].Text.Substring(2);
					}

					if (listView1.Items[play_index + 1] != null)
					{
						play_index++;
						string item = listView1.Items[play_index].Text;
						url_global = item + "?autoplay=1&version=3&enablejsapi=1&autohide=1";
						ende = true;
					}
					else
					{
						button5.Enabled = false;
					}
					break; 
				//case 3: ; break; //buffering
			}
		}
		
		private void YTStateError(string error)
		{
			Console.Write("YTplayer_error: " + error + "\r\n");
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			ThreadPool.QueueUserWorkItem(play);
			width = this.Width;
			height = this.Height;

			listView1.Items.Clear();
			foreach (string temp in Properties.Settings.Default.liste.Split('\x00'))
			{
				if (temp.Length > 0)
				{

					string[] temp2 = temp.Split(new Char[] { '\x01' }, 2);
					ListViewItem item = new ListViewItem(temp2[0]);
					if (temp2.Length == 2)
					{

						item.SubItems.Add(WebUtility.HtmlDecode(temp2[1]));
					}
					listView1.Items.Add(item);
				}
			}
			columnHeader2.Width = listView1.Width - columnHeader1.Width;

			button4.Width = this.Width / 2;
			button5.Width = this.Width / 2;
		}

		private void play(object state)
		{
			while(true)
			{
				if(autoplay)
				{
					if (url_global.Length > 0 && ende == true)
					{
						ende = false;
						YTplayer.Movie = url_global;
					}
				}
				Thread.Sleep(500);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (!Clipboard.ContainsText())
			{
				button1.Enabled = false;
				return;
			}
			add_url(Clipboard.GetText());
		}

		private void button2_Click(object sender, EventArgs e)
		{
			if(listView1.SelectedIndices.Count == 1)
			{ 
				int index = listView1.SelectedIndices[0];
				if(play_index != index)
				{
					listView1.Items.RemoveAt(index);

					if (play_index > index)
					{
						play_index--;
					}

					if (listView1.Items.Count > 0)
					{
						if (listView1.Items.Count < index)
						{
							listView1.Items[index].Selected = true;
						}
						else
						{
							listView1.Items[index - 1].Selected = true;
						}
					}
				}
			}
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (add_url(textBox1.Text))
			{
				textBox1.Text = "";
			}
		}

		public bool add_url(string url)
		{
			Uri uriResult;
			if (url.Length > 0 && url.Contains("youtube"))
			{

				url = url.Replace("https://", "http://");
				if (Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
				{
					WebClient x = new WebClient();
					string source = x.DownloadString(url);
					string title = Regex.Match(source, @"\<title\b[^>]*\>\s*(?<Title>[\s\S]*?)\</title\>", RegexOptions.IgnoreCase).Groups["Title"].Value;
					title = title.Replace(" - YouTube", "");

					url = url.Replace("watch?v=", "v/");
					if (url.Contains('&'))
					{
						url = url.Remove(url.IndexOf('&'));
					}

					ListViewItem item = new ListViewItem(url);
					item.SubItems.Add(WebUtility.HtmlDecode(Encoding.UTF8.GetString(Encoding.Default.GetBytes(title))));
					listView1.Items.Add(item);
					return true;
				}
			}

			return false;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			string temp = "";
			foreach(ListViewItem item in listView1.Items)
			{
				string t = item.Text;
				if(temp.Length != 0)
				{
					temp += "\x00";
				}

				if (t.StartsWith("»") || t.StartsWith("ǁ"))
				{
					temp += t.Substring(2);
				}
				else
				{
					temp += t;
				}

				if(item.SubItems[0].Text.Length > 0)
				{
					temp += "\x01" + item.SubItems[1].Text;
				}
			}

			Properties.Settings.Default.liste = temp;
			Properties.Settings.Default.Save();
		}

		private void menue_ontop_Click(object sender, EventArgs e)
		{
			if (this.TopMost == true)
			{
				menue_ontop.Image = Properties.Resources._unchecked;
				this.TopMost = false;
			}
			else
			{
				menue_ontop.Image = Properties.Resources._checked;
				this.TopMost = true;
			}
		}

		private void menue_close_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void Form1_Activated(object sender, EventArgs e)
		{
			if (Clipboard.ContainsText())
			{
				button1.Enabled = true;
			}
			else
			{
				button1.Enabled = false;
			}
		}

		private void listView1_Resize(object sender, EventArgs e)
		{
			columnHeader2.Width = listView1.Width - columnHeader1.Width;
		}

		private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if(listView1.SelectedIndices.Count == 1)
			{
				int index = listView1.SelectedIndices[0];
				if (play_index != index)
				{
					if (play_index >= 0)
					{
						if (listView1.Items[play_index].Text.StartsWith("»") || listView1.Items[play_index].Text.StartsWith("ǁ"))
						{
							listView1.Items[play_index].Text = listView1.Items[play_index].Text.Substring(2);
						}
					}
					if (listView1.Items[index].Text.StartsWith("»") || listView1.Items[index].Text.StartsWith("ǁ"))
					{
						listView1.Items[index].Text = listView1.Items[index].Text.Substring(2);
					}

					string item = listView1.Items[index].Text;
					item = item + "?autoplay=1&version=3&enablejsapi=1&hd=1";

					play_index = index;

					YTplayer.Movie = item;
					YTplayer.Play();
				}

				if (play_index - 1 >= 0)
				{
					button4.Enabled = true;
				}
				else
				{
					button4.Enabled = false;
				}

				if (play_index + 1 < listView1.Items.Count)
				{
					button5.Enabled = true;
				}
				else
				{
					button5.Enabled = false;
				}
			}
		}

		private void listView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listView1.SelectedIndices.Count == 1)
			{
				if (listView1.SelectedIndices[0] >= 0)
				{
					button2.Enabled = true;
				}
				else
				{
					button2.Enabled = false;
				}

				button6.Enabled = true;
				button7.Enabled = true;
				if (listView1.SelectedIndices[0] <= 0)
				{
					button6.Enabled = false;
				}

				if (listView1.SelectedIndices[0] >= listView1.Items.Count - 1)
				{
					button7.Enabled = false;
				}
			}
			else
			{
				button6.Enabled = false;
				button7.Enabled = false;
				button2.Enabled = false;
			}
		}

		private void menue_autoplay_Click(object sender, EventArgs e)
		{
			if (autoplay == true)
			{
				menue_autoplay.Image = Properties.Resources._unchecked;
				autoplay = false;
				url_global = "";
			}
			else
			{
				menue_autoplay.Image = Properties.Resources._checked;
				autoplay = true;
			}
		}

		private void Form1_Resize(object sender, EventArgs e)
		{

			button4.Width = this.Width / 2;
			button5.Width = this.Width / 2;
			Point loc = new Point(this.Width / 2, button5.Location.Y);
			button5.Location = loc;
		}

		private void button4_Click(object sender, EventArgs e)
		{
			button5.Enabled = true;
			if (play_index - 1 >= 0)
			{
				if (listView1.Items[play_index].Text.StartsWith("»") || listView1.Items[play_index].Text.StartsWith("ǁ"))
				{
					listView1.Items[play_index].Text = listView1.Items[play_index].Text.Substring(2);
				}
				if (listView1.Items[play_index - 1] != null)
				{
					play_index--;
					string item = listView1.Items[play_index].Text;
					url_global = item + "?autoplay=1&version=3&enablejsapi=1&autohide=1";
					ende = true;
				}
			}
			if (play_index - 1 < 0)
			{
				button4.Enabled = false;
			}
		}

		private void button5_Click(object sender, EventArgs e)
		{
			button4.Enabled = true;
			if (play_index + 1 < listView1.Items.Count)
			{
				if (listView1.Items[play_index].Text.StartsWith("»") || listView1.Items[play_index].Text.StartsWith("ǁ"))
				{
					listView1.Items[play_index].Text = listView1.Items[play_index].Text.Substring(2);
				}
				if (listView1.Items[play_index + 1] != null)
				{
					play_index++;
					string item = listView1.Items[play_index].Text;
					url_global = item + "?autoplay=1&version=3&enablejsapi=1&autohide=1";
					ende = true;
				}
			}

			if (play_index + 1 >= listView1.Items.Count)
			{
				button5.Enabled = false;
			}
		}

		private void sucheToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Form2 form2 = new Form2(this);
			form2.Show();
		}

		private void button6_Click(object sender, EventArgs e)
		{
			if(listView1.SelectedIndices.Count == 1)
			{
				if(listView1.SelectedIndices[0] > 0)
				{
					int i = listView1.SelectedIndices[0];
					ListViewItem temp = (ListViewItem)listView1.Items[i].Clone();
					ListViewItem temp2 = (ListViewItem)listView1.Items[i - 1].Clone();
					listView1.Items[i] = temp2;
					listView1.Items[i - 1] = temp;
					listView1.Items[i - 1].Selected = true;
				}
			}
		}

		private void button7_Click(object sender, EventArgs e)
		{
			if (listView1.SelectedIndices.Count == 1)
			{
				if (listView1.SelectedIndices[0] < listView1.Items.Count - 1)
				{
					int i = listView1.SelectedIndices[0];
					ListViewItem temp = (ListViewItem)listView1.Items[i].Clone();
					ListViewItem temp2 = (ListViewItem)listView1.Items[i + 1].Clone();
					listView1.Items[i] = temp2;
					listView1.Items[i + 1] = temp;
					listView1.Items[i + 1].Selected = true;
				}
			}
		}
	}
}
