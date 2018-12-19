using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class ColorForm : Form
    {
        private int langue = Properties.Settings.Default.Langue;
        private Color color1;
        private Color color2;
        private Color color3;

        internal ColorForm()
        {
            InitializeComponent();
        }


        private void ColorForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
            LoadColorFromDataBase();
            SetPreview();
            buttonColor1.Text = Translation.Translate("Color row 1", langue);
            buttoncolor2.Text = Translation.Translate("Color row 2", langue);
            buttonHeader.Text = Translation.Translate("Color Header", langue);
           // dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = color3;
           //  dataGridView1.RowHeadersDefaultCellStyle.BackColor = color3;

        }

        private void LoadColorFromDataBase()
        {

            string query = "Select * From Couleur;";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("ARVB1", query);
            if (Commun.ListHasValue(list))
            {
                color1 = Color.FromArgb(list[0]);
                buttonColor1.BackColor = color1;
            }
            List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("ARVB2", query);
            if (Commun.ListHasValue(list2))
            {
                color2 = Color.FromArgb(list2[0]);
                buttoncolor2.BackColor = color2;

            }
            List<int> list3 = SQlQueryExecuter.RunQueryReaderInt("ARVB3", query);
            if (Commun.ListHasValue(list3))
            {
                color3 = Color.FromArgb(list3[0]);
                buttonHeader.BackColor = color3;
            }

        }

        private void buttonColor1_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = buttonColor1.BackColor;
            colorDialog1.ShowDialog();
            buttonColor1.BackColor = colorDialog1.Color;
            color1 = colorDialog1.Color;
            SetPreview();
            SaveColor();
        }

        private void buttoncolor2_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = buttoncolor2.BackColor;
            colorDialog1.ShowDialog();
            buttoncolor2.BackColor = colorDialog1.Color;
            color2 = colorDialog1.Color;
            SetPreview();
            SaveColor();
        }

        private void SetPreview()
        {
            try
            {
                int i = 0;
                while (i <= dataGridView1.RowCount - 1)
                {
                    if (i % 2 == 0) dataGridView1.Rows[i].DefaultCellStyle.BackColor = color1;
                    else dataGridView1.Rows[i].DefaultCellStyle.BackColor = color2;
                    i++;
                    // if its the last, exit the loop
                    if (i == dataGridView1.RowCount)
                    {
                        break;
                    }
                }
               // dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = color3;
               // dataGridView1.RowHeadersDefaultCellStyle.BackColor = color3;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void SaveColor()
        {
            if (color1 != null && color2 != null)
            {
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2, color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        private void LoadGrid()
        {
            DataTable table = new DataTable();
            for (int i = 1; i < 4; i++)
            {
                table.Columns.Add(Translation.Translate("Column", langue) + i);
            }
            for (int i = 0; i < 11; i++)
            {
                table.Rows.Add();
            }
            dataGridView1.DataSource = table;
            //dataGridView1.RowHeadersVisible = false;
            //dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            labelPreview.Text = Translation.Translate("Preview", langue);
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.Columns[2].SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeColumns = false;
        }

        private void buttonHeader_Click(object sender, EventArgs e)
        {
            colorDialog1.Color = buttoncolor2.BackColor;
            colorDialog1.ShowDialog();
            buttonHeader.BackColor = colorDialog1.Color;
            color3 = colorDialog1.Color;
            SetPreview();
            SaveColor();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
