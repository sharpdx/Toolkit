using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Direct3D11;
using SharpDX;

namespace SharpDXTutorial2
{
	static class Program
	{

		[STAThread]
		static void Main()
		{
			using (var game = new MyGame())
				game.Run();
		}
	}
}
