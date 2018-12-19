using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA
{
    /// <summary>
    /// Export permet de créer des fichiers .csv des différents tableaux
    /// afin de récupérer les données sur Excel
    /// </summary>
   static class Export
    {

        private static int langue = Properties.Settings.Default.Langue;

        internal static void RunExportTable(DataTable dataTable)
        {
           

            try
            {
                // we asked the user where she/he wants to save the export file
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowDialog();
                string path = folderBrowserDialog.SelectedPath;
                StreamWriter Exportfile;
                if (path == "") return;
                try
                {
                    Exportfile = new StreamWriter(File.Open(path + "//" + dataTable.TableName + ".csv", FileMode.CreateNew), Encoding.UTF8);
                }
                catch (IOException Ex)
                {// if the file already existe, then the user can delete it or leave the export function
                    DialogResult dialogresultfileExiste = MessageBox.Show(Translation.Translate("The file", langue) + " " + dataTable.TableName + Translation.Translate(".cvs exists in the directory", langue) + " : " + path + ". " + Translation.Translate("Do you want to replace the old file by a new one ", langue), "Warning", MessageBoxButtons.YesNo);
                    if (dialogresultfileExiste == DialogResult.Yes)
                    {
                        File.Delete(path + "//" + dataTable.TableName + ".csv");
                        Exportfile = new StreamWriter(File.Open(path + "//" + dataTable.TableName + ".csv", FileMode.CreateNew), Encoding.UTF8);
                    }
                    else return;
                }

                Boolean FirstColumn = true;
                string ligne = "";
                int NumberColumn=0;

                // we write the name of each field fisrt
                foreach (DataColumn column in dataTable.Columns)
                {
                    
                    NumberColumn += 1;
                    if (FirstColumn)
                    {
                        ligne = column.ColumnName;
                    }
                    else ligne = ligne + ";" + column.ColumnName;
                    FirstColumn = false;
                }
                Exportfile.Write(ligne);
                Exportfile.WriteLine();

                // we write each row in the file
                foreach (DataRow row in dataTable.Rows)
                {
                    for (int i = 0;i < NumberColumn; i++)
                    {
                        if (i ==0)
                        {
                            ligne = row.ItemArray[i].ToString();
                        }
                        else ligne = ligne + ";" + row.ItemArray[i].ToString();
                    }
                    Exportfile.Write(ligne);
                    Exportfile.WriteLine();
                }
                Exportfile.Close();
                Exportfile.Dispose();

                MessageBox.Show(Translation.Translate("Export successfully done", langue));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        internal static void RunExportTable(DataGridView gridView,string nom)
        {
            try
            {
                // we asked the user where she/he wants to save the export file
                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowDialog();
                string path = folderBrowserDialog.SelectedPath;
                StreamWriter Exportfile;
                if (path == "") return;
                try
                {
                    Exportfile = new StreamWriter(File.Open(path + "//" + nom + ".csv", FileMode.CreateNew), Encoding.UTF8);
                }
                catch (IOException Ex)
                {// if the file already existe, then the user can delete it or leave the export function
                    DialogResult dialogresultfileExiste = MessageBox.Show(Translation.Translate("The file",langue) + " " + nom + Translation.Translate(".cvs exists in the directory", langue) + " : " + path + ". "+ Translation.Translate("Do you want to replace the old file by a new one ", langue), "Warning", MessageBoxButtons.YesNo);
                    if (dialogresultfileExiste == DialogResult.Yes)
                    {
                        File.Delete(path + "//" + nom + ".csv");
                        Exportfile = new StreamWriter(File.Open(path + "//" + nom + ".csv", FileMode.CreateNew), Encoding.UTF8);
                    }
                    else return;
                }

                Boolean FirstColumn = true;
                string ligne = "";
                int NumberColumn = 0;

                // we write the name of each field fisrt
                foreach (DataGridViewColumn column in gridView.Columns)
                {

                    NumberColumn += 1;
                    if (FirstColumn)
                    {
                        ligne = column.Name;
                    }
                    else ligne = ligne + ";" + column.Name;
                    FirstColumn = false;
                }
                Exportfile.Write(ligne);
                Exportfile.WriteLine();

                // we write each row in the file
                foreach (DataGridViewRow row in gridView.Rows)
                {
                    for (int i = 0; i < NumberColumn; i++)
                    {
                        if (i == 0)
                        {
                            ligne = row.Cells[i].Value.ToString();
                        }
                        else ligne = ligne + ";" + row.Cells[i].Value.ToString();
                    }
                    Exportfile.Write(ligne);
                    Exportfile.WriteLine();
                }
                Exportfile.Close();
                Exportfile.Dispose();

                MessageBox.Show(Translation.Translate("Export successfully done",langue));
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
