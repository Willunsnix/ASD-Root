
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
            ASD.Drawing.Properties.Corners corners1 = new ASD.Drawing.Properties.Corners();
            this.propertyGrid1 = new System.Windows.Forms.PropertyGrid();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ButtonControl1 = new ASD.Forms.Controls.ButtonControl();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // propertyGrid1
            // 
            this.propertyGrid1.Location = new System.Drawing.Point(13, 21);
            this.propertyGrid1.Name = "propertyGrid1";
            this.propertyGrid1.Size = new System.Drawing.Size(278, 378);
            this.propertyGrid1.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(313, 21);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(319, 251);
            this.dataGridView1.TabIndex = 2;
            // 
            // ButtonControl1
            // 
            this.ButtonControl1.BackColor = System.Drawing.Color.Transparent;
            this.ButtonControl1.ButtonColor = System.Drawing.Color.Red;
            this.ButtonControl1.Caption = "";
            this.ButtonControl1.Corners = corners1;
            this.ButtonControl1.Location = new System.Drawing.Point(503, 334);
            this.ButtonControl1.Name = "ButtonControl1";
            this.ButtonControl1.Orientation = ASD.Drawing.Enums.ShapeOrientation.Horizontal;
            this.ButtonControl1.Renderer = null;
            this.ButtonControl1.RepeatInterval = 100;
            this.ButtonControl1.RepeatState = false;
            this.ButtonControl1.Size = new System.Drawing.Size(128, 64);
            this.ButtonControl1.StartRepeatInterval = 500;
            this.ButtonControl1.State = ASD.Forms.Controls.ButtonControl.ButtonState.Normal;
            this.ButtonControl1.Style = ASD.Drawing.Enums.ShapenStyle.Rectangular;
            this.ButtonControl1.TabIndex = 3;
            this.ButtonControl1.Click += new System.EventHandler(this.ButtonControl1_Click);
            // 
            // TestForm
            // 
            this.ClientSize = new System.Drawing.Size(644, 421);
            this.Controls.Add(this.ButtonControl1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.propertyGrid1);
            this.Name = "TestForm";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }


        #endregion
        private System.Windows.Forms.PropertyGrid propertyGrid1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private Controls.ButtonControl ButtonControl1;
    }
}

