using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using WMPLib;

namespace Youtube_Player
{
	public partial class Form1 : Form
	{
		int listbox_play_index = -1;
		int width = 0;
		int height = 0;
		string url_global = "";
		bool ende = false;

		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			string url = textBox1.Text;

			if (url.Length > 0)
			{
				url = url.Replace("watch?v=", "v/");
				if (url.Contains('&'))
				{
					url = url.Remove(url.IndexOf('&'));
				}
				listBox1.Items.Add(url);

				textBox1.Text = "";
			}
		}

		private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			int index = this.listBox1.IndexFromPoint(e.Location);
			if (index != System.Windows.Forms.ListBox.NoMatches)
			{
				if(listbox_play_index != index)
				{
					if (listbox_play_index >= 0)
					{
						if(listBox1.Items[listbox_play_index].ToString().StartsWith("»") || listBox1.Items[listbox_play_index].ToString().StartsWith("ǁ"))
						{
							listBox1.Items[listbox_play_index] = listBox1.Items[listbox_play_index].ToString().Substring(2);
						}
					}
					if (listBox1.Items[index].ToString().StartsWith("»") || listBox1.Items[index].ToString().StartsWith("ǁ"))
					{
						listBox1.Items[index] = listBox1.Items[index].ToString().Substring(2);
					}

					string item = listBox1.Items[index].ToString();
					item = item + "?autoplay=1&version=3&enablejsapi=1&hd=1";

					listbox_play_index = index;

					YTplayer.Movie = item;
					YTplayer.Play();
				}
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
					if (listBox1.Items[listbox_play_index].ToString().StartsWith("»") || listBox1.Items[listbox_play_index].ToString().StartsWith("ǁ"))
					{
						listBox1.Items[listbox_play_index] = listBox1.Items[listbox_play_index].ToString().Substring(2);
					}
					listBox1.Items[listbox_play_index] = "» " + listBox1.Items[listbox_play_index].ToString();
					break;
				case 2:		//paused
					if (listBox1.Items[listbox_play_index].ToString().StartsWith("»") || listBox1.Items[listbox_play_index].ToString().StartsWith("ǁ"))
					{
						listBox1.Items[listbox_play_index] = listBox1.Items[listbox_play_index].ToString().Substring(2);
					}
					listBox1.Items[listbox_play_index] = "ǁ " + listBox1.Items[listbox_play_index].ToString();
					break;
				case 0:		//ended
					if (listBox1.Items[listbox_play_index].ToString().StartsWith("»") || listBox1.Items[listbox_play_index].ToString().StartsWith("ǁ"))
					{
						listBox1.Items[listbox_play_index] = listBox1.Items[listbox_play_index].ToString().Substring(2);
					}

					if (listBox1.Items[listbox_play_index + 1] != null)
					{
						listbox_play_index++;
						string item = listBox1.Items[listbox_play_index].ToString();
						url_global = item + "?autoplay=1&version=3&enablejsapi=1&autohide=1";
						ende = true;
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

			listBox1.Items.Clear();
			listBox1.Items.AddRange(Properties.Settings.Default.liste.Split(';'));
		}

		private void play(object state)
		{
			while(true)
			{
				if (url_global.Length > 0 && ende == true)
				{
					ende = false;
					YTplayer.Movie = url_global;
				}
				Thread.Sleep(500);
			}
		}

		private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (listBox1.SelectedIndex >= 0)
			{
				button2.Enabled = true;
			}
			else
			{
				button2.Enabled = false;
			}
			//YTplayer.
		}

		private void button2_Click(object sender, EventArgs e)
		{
			int index = listBox1.SelectedIndex;
			if(listbox_play_index != index)
			{
				listBox1.Items.RemoveAt(index);
			}

			if (listbox_play_index > index)
			{
				listbox_play_index--;
			}
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			string temp = "";
			foreach(string t in listBox1.Items)
			{
				if(temp.Length != 0)
				{
					temp += ";";
				}

				if (t.StartsWith("»") || t.StartsWith("ǁ"))
				{
					temp += t.Substring(2);
				}
				else
				{
					temp += t;
				}
			}

			Properties.Settings.Default.liste = temp;
			Properties.Settings.Default.Save();
		}

		private void YTplayer_ControlAdded(object sender, ControlEventArgs e)
		{
			MessageBox.Show("I");
		}
	}
}
