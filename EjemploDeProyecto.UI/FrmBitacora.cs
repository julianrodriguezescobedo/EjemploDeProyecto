using EjemploDeProyecto.BE;
using EjemploDeProyecto.BLL;
using EjemploDeProyecto.Interfaces;
using System;
using System.Windows.Forms;

namespace EjemploDeProyecto.UI
{
    public partial class FrmBitacora : Form
    {
        BitacoraBLL bitacoraBLL = new BitacoraBLL();
        int page = 1;
        int perPage = 4;
        int totalPages = 0;

        public FrmBitacora()
        {
            InitializeComponent();
        }

        private void Bitacora_Load(object sender, EventArgs e)
        {
            cbBitacoraTipo.DataSource = Enum.GetValues(typeof(BitacoraTipo));         
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                ActualizarGrilla();
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (page > 0)
            {
                page--;
                ActualizarGrilla();
            }
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (page <= totalPages)
            {
                page++;
                ActualizarGrilla();
            }
        }

        void ActualizarGrilla()
        {
            try
            {
                IBitacorasFiltered bitacora;
                bitacora = bitacoraBLL.GetAllFiltered(page, perPage, new BitacoraFilters() { Desde = dtpDesde.Value, Hasta = dtpHasta.Value, Tipo = (BitacoraTipo)cbBitacoraTipo.SelectedItem, Like = txtTexto.Text });
                totalPages = bitacora.TotalPages;
                dgvBitacora.DataSource = null;
                dgvBitacora.DataSource = bitacora.Bitacoras;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
