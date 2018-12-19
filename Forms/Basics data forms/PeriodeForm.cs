using OMEGA.Data_Classes;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class PeriodeForm : Form
    {
        private Periode periode;

        internal PeriodeForm()
        {
            InitializeComponent();
            dataGridViewDetailPeriode.Font = Commun.GetCurrentFont();
            dataGridViewPeriode.Font = Commun.GetCurrentFont();
            LoadPeriode();
        }

        private void LoadPeriode()
        {
            try
            {
                string query = PeriodeQuery.LoadPeriodeQuery();
                List<int> ListIdPeriode = SQlQueryExecuter.RunQueryReaderInt("IdPeriode", query);

                periode = new Periode(buttonOKPeriode, buttonCancelPeriode, buttonCancelPeriode, buttonAddPeriode,
                    dataGridViewPeriode,dataGridViewDetailPeriode, panelPeriode, ListIdPeriode, 0,
                    radioBttnMois, radioButtonWeek, radioButton2weeks, null, buttonAddPeriodeDetail,
                    buttonRemovePeriodeDetail, groupBoxTemplate,false,pictureBoxExport,this,buttonDuplicatePeriode);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void PeriodeForm_Load(object sender, EventArgs e)
        {
            dataGridViewDetailPeriode.CellClick += new DataGridViewCellEventHandler(periode.dataGridViewDetailPeriodeCellClick);
            dataGridViewDetailPeriode.CellValueChanged += new DataGridViewCellEventHandler(periode.dataGridViewDetailPeriodeCellValueChanged);
            dataGridViewDetailPeriode.KeyPress += new KeyPressEventHandler(periode.DatagridDetailPeriodKeyPress);
            dataGridViewPeriode.CellClick += new DataGridViewCellEventHandler(periode.dataGridViewPeriode_CellClick);
            dataGridViewPeriode.CellEndEdit += new DataGridViewCellEventHandler(periode.dataGridViewPeriodeEndEdit);
            buttonOKPeriode.Click += new EventHandler(periode.ButtonOkClick);
            buttonAddPeriode.Click += new EventHandler(periode.buttonAddPeriode);
            buttonDuplicatePeriode.Click += new EventHandler(periode.ButtonValidateName);
            radioBttnMois.CheckedChanged += new EventHandler(periode.radioBttnMoisCheckedChanged);
            radioButton2weeks.CheckedChanged += new EventHandler(periode.radioButton2weeksCheckChanged);
            radioButtonWeek.CheckedChanged += new EventHandler(periode.radioButtonWeekCheckChanged);
            buttonRemovePeriode.Click += new EventHandler(periode.buttonRemove);
            buttonCopy.Click += new EventHandler(periode.ButtonCopyClick2);
            dataGridViewDetailPeriode.CellEndEdit += new DataGridViewCellEventHandler(periode.dataGridViewDetailPeriode_CellEndEdit);
        }

        private void buttonCopy_Click(object sender, EventArgs e)
        {

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                LoadPeriode();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void pictureBoxExport_Click(object sender, EventArgs e)
        {
            Export.RunExportTable(dataGridViewPeriode, "Calendrier");
        }
    }
}
