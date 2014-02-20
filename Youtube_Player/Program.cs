using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Youtube_Player
{
	static class Program
	{
		/// <summary>
		/// Der Haupteinstiegspunkt für die Anwendung.
		/// </summary>
		[STAThread]
		static void Main()
		{
			string resource1 = "AxInterop.ShockwaveFlashObjects.dll";
			string resource2 = "Google.GData.Client.dll";
			string resource3 = "Google.GData.Extensions.dll";
			string resource4 = "Google.GData.YouTube.dll";
			string resource5 = "Interop.ShockwaveFlashObjects.dll";
			string resource6 = "Newtonsoft.Json.dll";
			EmbeddedAssembly.Load("Youtube_Player." + resource1, resource1);
			EmbeddedAssembly.Load("Youtube_Player." + resource2, resource2);
			EmbeddedAssembly.Load("Youtube_Player." + resource3, resource3);
			EmbeddedAssembly.Load("Youtube_Player." + resource4, resource4);
			EmbeddedAssembly.Load("Youtube_Player." + resource5, resource5);
			EmbeddedAssembly.Load("Youtube_Player." + resource6, resource6);

			AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Form1());
		}

		static System.Reflection.Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
		{
			return EmbeddedAssembly.Get(args.Name);
		}
	}
}
