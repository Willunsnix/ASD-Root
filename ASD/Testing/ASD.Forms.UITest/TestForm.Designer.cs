
namespace ASD.Forms.UITest
{
    partial class TestForm
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
            this.buttonControl1 = new ASD.Forms.Controls.ButtonControl();
            this.SuspendLayout();
            // 
            // buttonControl1
            // 
            this.buttonControl1.BackColor = System.Drawing.Color.Transparent;
            this.buttonControl1.ButtonColor = System.Drawing.Color.Red;
            this.buttonControl1.Caption = "";
            this.buttonControl1.Location = new System.Drawing.Point(571, 311);
            this.buttonControl1.Name = "buttonControl1";
            this.buttonControl1.Renderer = null;
            this.buttonControl1.RepeatInterval = 100;
            this.buttonControl1.RepeatState = false;
            this.buttonControl1.Size = new System.Drawing.Size(151, 61);
            this.buttonControl1.StartRepeatInterval = 500;
            this.buttonControl1.State = ASD.Forms.Controls.ButtonControl.ButtonState.Normal;
            this.buttonControl1.Style = ASD.Forms.Controls.ButtonControl.ButtonStyle.Rectangular;
            this.buttonControl1.TabIndex = 0;
            // 
            // TestForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonControl1);
            this.Name = "TestForm";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ButtonControl buttonControl1;
    }
}

