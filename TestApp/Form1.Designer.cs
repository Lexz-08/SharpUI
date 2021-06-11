
namespace TestApp
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.progressBar1 = new SharpUI.Controls.ProgressBar();
			this.progressBar2 = new SharpUI.Controls.ProgressBar();
			this.progressBar3 = new SharpUI.Controls.ProgressBar();
			this.progressBar4 = new SharpUI.Controls.ProgressBar();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.progressBar1.Location = new System.Drawing.Point(213, 186);
			this.progressBar1.MaxProgress = 100F;
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Progress = 0F;
			this.progressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.progressBar1.Size = new System.Drawing.Size(400, 10);
			this.progressBar1.TabIndex = 0;
			// 
			// progressBar2
			// 
			this.progressBar2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.progressBar2.Location = new System.Drawing.Point(213, 202);
			this.progressBar2.MaxProgress = 100F;
			this.progressBar2.Name = "progressBar2";
			this.progressBar2.Progress = 0F;
			this.progressBar2.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.progressBar2.Size = new System.Drawing.Size(400, 10);
			this.progressBar2.TabIndex = 1;
			// 
			// progressBar3
			// 
			this.progressBar3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.progressBar3.Location = new System.Drawing.Point(213, 218);
			this.progressBar3.MaxProgress = 100F;
			this.progressBar3.Name = "progressBar3";
			this.progressBar3.Progress = 0F;
			this.progressBar3.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.progressBar3.Size = new System.Drawing.Size(400, 10);
			this.progressBar3.TabIndex = 2;
			// 
			// progressBar4
			// 
			this.progressBar4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.progressBar4.Location = new System.Drawing.Point(213, 234);
			this.progressBar4.MaxProgress = 100F;
			this.progressBar4.Name = "progressBar4";
			this.progressBar4.Progress = 0F;
			this.progressBar4.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
			this.progressBar4.Size = new System.Drawing.Size(400, 10);
			this.progressBar4.TabIndex = 3;
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(903, 564);
			this.Controls.Add(this.progressBar4);
			this.Controls.Add(this.progressBar3);
			this.Controls.Add(this.progressBar2);
			this.Controls.Add(this.progressBar1);
			this.Name = "Form1";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = " Form1";
			this.ResumeLayout(false);

		}

		#endregion

		private SharpUI.Controls.ProgressBar progressBar1;
		private SharpUI.Controls.ProgressBar progressBar2;
		private SharpUI.Controls.ProgressBar progressBar3;
		private SharpUI.Controls.ProgressBar progressBar4;
	}
}

