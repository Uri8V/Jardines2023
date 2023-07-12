using Jardines2023.Entidades.Entidades;
using Jardines2023.Servicios.Interfaces;
using Jardines2023.Servicios.Servicios;
using Jardines2023.Windows.Helpers;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Jardines2023.Windows
{
    public partial class frmCompras : Form
    {
        public frmCompras()
        {
            InitializeComponent();
            servicioCompra = new ServicioCompra();
        }
        private readonly IServicioCompra servicioCompra;
        private int registros = 0;
        private int paginaActual = 1;
        private int paginas = 0;
        private int registrosPorPagina = 10;
        private List<Compra> lista; 
        private string textoFiltro=null;
        private void frmCompras_Load(object sender, EventArgs e)
        {
            try
            {
                RecargarGrilla();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void RecargarGrilla()
        {
            try
            {
                registros = servicioCompra.GetCantidad();
                paginas = FormHelper.CalcularPaginas(registros, registrosPorPagina);
                MostrarPaginado();
            }
            catch (Exception)
            {

                throw;
            }

        }

        private void MostrarPaginado()
        {
            lista = servicioCompra.GetProveedoresPorPagina(registrosPorPagina, paginaActual, textoFiltro);
            MostrarDatosEnGrilla();
        }

        private void MostrarDatosEnGrilla()
        {
            GridHelper.LimpiarGrilla(dgvDatos);
            foreach (var compra in lista)
            {
                DataGridViewRow r = GridHelper.ConstruirFila(dgvDatos);
                GridHelper.SetearFila(r, compra);
                GridHelper.AgregarFila(dgvDatos, r);
            }
            lblPaginaActual.Text = paginaActual.ToString();
            lblPaginas.Text = paginas.ToString();
            MostrarTotal();
        }

        private void MostrarTotal()
        {
            lblRegistros.Text = servicioCompra.GetCantidad().ToString();
        }

        private void btnPrimero_Click(object sender, EventArgs e)
        {
            paginaActual = 1;
            MostrarPaginado();
        }

        private void btnAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual == 1)
            {
                return;
            }
            paginaActual--;
            MostrarPaginado();
        }

        private void btnUltimo_Click(object sender, EventArgs e)
        {
            paginaActual = paginas;
            MostrarPaginado();
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual == paginas)
            {
                return;
            }
            paginaActual++;
            MostrarPaginado();
        }
    }
}
