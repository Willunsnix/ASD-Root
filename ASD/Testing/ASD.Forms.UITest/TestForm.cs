using ASD.Forms.Controls;
using ASD.Global.Helpers;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace ASD.Forms.UITest
{
    public partial class TestForm : Form
    {
        public TestForm()
        {
            InitializeComponent();
        }

        private void ButtonControl1_Click(object sender, System.EventArgs e)
        {
            this.propertyGrid1.CollapseAllGridItems();
            this.propertyGrid1.Refresh();

            this.propertyGrid1.SelectedObject = ((ButtonControl)sender).Corners;

            PropertyDescriptorCollection pc =  PropertyHandler.GetProperties(sender);

            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

            dataGridView1.ColumnCount = 4;
            dataGridView1.Columns[0].Name = "Name";
            dataGridView1.Columns[1].Name = "Type";
            dataGridView1.Columns[2].Name = "Value";
            dataGridView1.Columns[3].Name = "DisplayName";

            foreach (PropertyDescriptor prop in pc)
            {
                System.TypeCode typeCode = Type.GetTypeCode(prop.PropertyType);
                switch (typeCode)
                {
                    case TypeCode.Boolean:
                        dataGridView1.Rows.Add(new string[] { prop.Name, prop.PropertyType.Name, (bool)prop.GetValue(sender) ? "True": "False", prop.DisplayName });
                        break;
                    case TypeCode.String:
                        dataGridView1.Rows.Add(new string[] { prop.Name, prop.PropertyType.Name, prop.GetValue(sender) == null ? String.Empty: prop.GetValue(sender).ToString(), prop.DisplayName });
                        break;
                    default:
                        dataGridView1.Rows.Add(new string[] { prop.Name, prop.PropertyType.Name, string.Empty, prop.DisplayName });
                        break;
                }
            }
        }
    }
}
