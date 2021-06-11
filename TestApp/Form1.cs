using System.ComponentModel;
using System.Windows.Forms;

namespace TestApp
{
	public partial class Form1 : Form
	{
		private BackgroundWorker bgWorker;
		private Timer t;

		public Form1()
		{
			InitializeComponent();

			bgWorker = new BackgroundWorker();
			bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
			bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);

			t = new Timer { Interval = 10 };
			t.Tick += (s, e) => bgWorker.RunWorkerAsync(new float[] { 1f, 0.5f, 0.25f, 0.125f });
			t.Start();
		}

		private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			float prog1 = ((float[])e.Argument)[0];
			float prog2 = ((float[])e.Argument)[1];
			float prog3 = ((float[])e.Argument)[2];
			float prog4 = ((float[])e.Argument)[3];

			if (progressBar1.Progress < 100f) progressBar1.Progress += prog1;
			if (progressBar2.Progress < 100f) progressBar2.Progress += prog2;
			if (progressBar3.Progress < 100f) progressBar3.Progress += prog3;
			if (progressBar4.Progress < 100f) progressBar4.Progress += prog4;
		}
		private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			if (progressBar1.Progress == 100f &&
				progressBar2.Progress == 100f &&
				progressBar3.Progress == 100f &&
				progressBar4.Progress == 100f)
			{
				t.Enabled = false;
				MessageBox.Show("Progress finished.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
