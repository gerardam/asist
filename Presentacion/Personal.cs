using SisAsis.Datos;
using SisAsis.Logica;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

//| || //  (( |+|========================================================
//| ||// ((   |+| AsisT   | 05-07-2021                                   
//| ||\\ ((   |+| Kyocode | www.kyocode.com | Gerardo Alvarez Mendoza    
//| || \\  (( |+|========================================================
namespace SisAsis.Presentacion
{
    public partial class Personal : UserControl
    {
        #region VARIABLES
        int IdCargo = 0;
        int Desde = 1;
        int Hasta = 10;
        int Contador;
        int IdPersona;
        private int ItemPorPag = 10;
        string Estatus;
        int TotalPag;
        #endregion

        #region EVENTOS
        public Personal()
        {
            InitializeComponent();
        }

        private void Personal_Load(object sender, EventArgs e)
        {
            MostrarPersonal();
            ReiniciarPaginado();
        }

        private void txtBuscador_TextChanged(object sender, EventArgs e)
        {
            DataTable dtDatos = new DPersonal().BuscarPersonal(Desde, Hasta, txtBuscador.Text);
            dgvPersonal.DataSource = dtDatos;
            Bases.DisenhoDgv(ref dgvPersonal);
            Bases.DiseñoDtvEliminar(ref dgvPersonal);
            dgvPersonal.Columns[2].Visible = false;
            dgvPersonal.Columns[7].Visible = false;
            pnlPaginado.Visible = true;
        }

        private void btnMostrarTodo_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            LocalizarDgvCargo();
            pnlCargo.Visible = false;
            pnlPaginado.Visible = false;
            pnlRegistro.Visible = true;
            pnlRegistro.Dock = DockStyle.Fill;
            btnGuardarP.Visible = true;
            btnGuardarCP.Visible = false;
            Limpiar();
        }

        private void txtCargo_TextChanged(object sender, EventArgs e)
        {
            BuscarCargos();
        }

        private void txtSueldo_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldo, e);
        }

        private void btnGuardarP_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    if (!string.IsNullOrEmpty(cbxPais.Text))
                    {
                        if (IdCargo > 0)
                        {
                            if (!string.IsNullOrEmpty(txtSueldo.Text))
                            {
                                InsertarPersonal();
                            }
                        }
                    }
                }
            }
        }

        private void btnGuardarCP_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNombres.Text))
            {
                if (!string.IsNullOrEmpty(txtIdentificacion.Text))
                {
                    if (!string.IsNullOrEmpty(cbxPais.Text))
                    {
                        if (IdCargo > 0)
                        {
                            if (!string.IsNullOrEmpty(txtSueldo.Text))
                            {
                                EditarPersonal();
                            }
                        }
                    }
                }
            }
        }

        private void btnVolverP_Click(object sender, EventArgs e)
        {
            pnlRegistro.Visible = false;
            pnlPaginado.Visible = true;
        }

        private void btnAgregarCargo_Click(object sender, EventArgs e)
        {
            pnlCargo.Visible = true;
            pnlCargo.Dock = DockStyle.Fill;
            pnlCargo.BringToFront();
            btnGuardarC.Visible = true;
            btnGuardarCC.Visible = false;
            txtCargoG.Clear();
            txtSueldoG.Clear();
        }

        private void txtSueldoG_KeyPress(object sender, KeyPressEventArgs e)
        {
            Bases.Decimales(txtSueldoG, e);
        }

        private void btnGuardarC_Click(object sender, EventArgs e)
        {
            InsertarCargos();
        }

        private void btnGuardarCC_Click(object sender, EventArgs e)
        {
            EditarCargos();
        }

        private void btnVolverC_Click(object sender, EventArgs e)
        {
            pnlCargo.Visible = false;
        }

        private void dgvPersonal_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvPersonal.Columns["Eliminar"].Index)
            {
                EliminarPersonal();
            }
            else if (e.ColumnIndex == dgvPersonal.Columns["Editar"].Index)
            {
                ObtenerDatosP();
            }
        }

        private void dgvListadoCargos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvListadoCargos.Columns["EditarC"].Index)
            {
                ObtenerCargosEditar();
            }
            else if (e.ColumnIndex == dgvListadoCargos.Columns["Cargo"].Index)
            {
                ObtenerDatosCargo();
            }
        }

        private void btnPagPri_Click(object sender, EventArgs e)
        {
            ReiniciarPaginado();
            MostrarPersonal();
        }

        private void btnPagAnt_Click(object sender, EventArgs e)
        {
            Desde -= 10;
            Hasta -= 10;
            MostrarPersonal();
            ContarPersonal();
            if (Contador > Hasta)
            {
                btnPagSig.Enabled = true;
                btnPagAnt.Enabled = true;
            }
            else
            {
                btnPagSig.Enabled = false;
                btnPagAnt.Enabled = true;
            }
            if (Desde == 1)
            {
                ReiniciarPaginado();
            }
        }

        private void btnPagSig_Click(object sender, EventArgs e)
        {
            Desde += 10;
            Hasta += 10;
            MostrarPersonal();
            ContarPersonal();
            if (Contador > Hasta)
            {
                btnPagSig.Enabled = true;
                btnPagAnt.Enabled = true;
            }
            else
            {
                btnPagSig.Enabled = false;
                btnPagAnt.Enabled = true;
            }
            Paginar();
        }

        private void btnPagUlt_Click(object sender, EventArgs e)
        {
            Hasta = TotalPag * ItemPorPag;
            Desde = Hasta - 9;
            MostrarPersonal();
            ContarPersonal();
            if (Contador > Hasta)
            {
                btnPagSig.Enabled = true;
                btnPagAnt.Enabled = true;
            }
            else
            {
                btnPagSig.Enabled = false;
                btnPagAnt.Enabled = true;
            }
            Paginar();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Bases.DisenhoDgv(ref dgvPersonal);
            Bases.DiseñoDtvEliminar(ref dgvPersonal);
            dgvPersonal.Columns[2].Visible = false;
            dgvPersonal.Columns[7].Visible = false;
            pnlPaginado.Visible = true;

            timer1.Stop();
        }

        #endregion

        #region FUNCIONES

        private void InsertarPersonal()
        {
            LPersonal pm = new LPersonal();
            pm.Nombres = txtNombres.Text;
            pm.Identificacion = txtIdentificacion.Text;
            pm.Pais = cbxPais.Text;
            pm.Id_cargo = IdCargo;
            pm.SueldoPorHora = Convert.ToDouble(txtSueldo.Text);
            if (new DPersonal().InsertarPersonal(pm))
            {
                MostrarPersonal();
                pnlRegistro.Visible = false;
            }
        }

        private void EditarPersonal()
        {
            LPersonal pm = new LPersonal();
            pm.Id_personal = IdPersona;
            pm.Nombres = txtNombres.Text;
            pm.Identificacion = txtIdentificacion.Text;
            pm.Pais = cbxPais.Text;
            pm.Id_cargo = IdCargo;
            pm.SueldoPorHora = Convert.ToDouble(txtSueldo.Text);
            if (new DPersonal().EditarPersonal(pm))
            {
                MostrarPersonal();
                pnlRegistro.Visible = false;
            }
        }

        private void MostrarPersonal()
        {
            DataTable dtDatos = new DPersonal().MostrarPersonal(Desde, Hasta);
            dgvPersonal.DataSource = dtDatos;
            Bases.DisenhoDgv(ref dgvPersonal);
            Bases.DiseñoDtvEliminar(ref dgvPersonal);
            dgvPersonal.Columns[2].Visible = false;
            dgvPersonal.Columns[7].Visible = false;
            pnlPaginado.Visible = true;
        }

        private void EliminarPersonal()
        {
            DialogResult result = MessageBox.Show("El registro se actualizara de estado a Eliminado, desea continuar?", "Eliminar registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                IdPersona = Convert.ToInt32(dgvPersonal.SelectedCells[2].Value);
                LPersonal pm = new LPersonal();
                pm.Id_personal = IdPersona;
                if (new DPersonal().EliminarPersonal(pm))
                {
                    MostrarPersonal();
                }
            }
        }

        private void RestaurarPersonal()
        {
            DialogResult result = MessageBox.Show("Este personal se elimino, desea habilitarlo?", "Restaurar registro", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                LPersonal pm = new LPersonal();
                pm.Id_personal = IdPersona;
                if (new DPersonal().RestaurarPersonal(pm))
                {
                    MostrarPersonal();
                }
            }
        }

        private void ObtenerDatosP()
        {
            IdPersona = Convert.ToInt32(dgvPersonal.SelectedCells[2].Value);
            Estatus = dgvPersonal.SelectedCells[8].Value.ToString();
            if (Estatus == "ELIMINADO")
            {
                RestaurarPersonal();
            }
            else
            {
                LocalizarDgvCargo();
                txtNombres.Text = dgvPersonal.SelectedCells[3].Value.ToString();
                txtIdentificacion.Text = dgvPersonal.SelectedCells[4].Value.ToString();
                cbxPais.Text = dgvPersonal.SelectedCells[10].Value.ToString();
                txtCargo.Text = dgvPersonal.SelectedCells[6].Value.ToString();
                IdCargo = Convert.ToInt32(dgvPersonal.SelectedCells[7].Value.ToString());
                txtSueldo.Text = dgvPersonal.SelectedCells[5].Value.ToString();
                pnlPaginado.Visible = false;
                pnlRegistro.Visible = true;
                pnlRegistro.Dock = DockStyle.Fill;
                dgvListadoCargos.Visible = false;
                lblSueldoP.Visible = true;
                pnlBtnsGuardarP.Visible = true;
                btnGuardarP.Visible = false;
                btnGuardarCP.Visible = true;
                pnlCargo.Visible = false;
            }
        }

        private void ContarPersonal()
        {
            Contador = new DPersonal().ObtenerTotalPersonal();
        }

        private void InsertarCargos()
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoG.Text))
                {
                    LCargo pm = new LCargo();
                    pm.Cargo = txtCargoG.Text;
                    pm.SueldoPorHora = Convert.ToDouble(txtSueldoG.Text);
                    if (new DCargo().InsertarCargo(pm))
                    {
                        txtCargo.Clear();
                        BuscarCargos();
                        pnlCargo.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregar el sueldo", "Falta el sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregar el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void EditarCargos()
        {
            if (!string.IsNullOrEmpty(txtCargoG.Text))
            {
                if (!string.IsNullOrEmpty(txtSueldoG.Text))
                {
                    LCargo pm = new LCargo();
                    pm.Id_cargo = IdCargo;
                    pm.Cargo = txtCargoG.Text;
                    pm.SueldoPorHora = Convert.ToDouble(txtSueldoG.Text);
                    if (new DCargo().EditarCargo(pm))
                    {
                        txtCargo.Clear();
                        BuscarCargos();
                        pnlCargo.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Agregar el sueldo", "Falta el sueldo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("Agregar el cargo", "Falta el cargo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BuscarCargos()
        {
            DataTable dtDatos = new DCargo().BuscarCargo(txtCargo.Text);
            dgvListadoCargos.DataSource = dtDatos;
            Bases.DisenhoDgv(ref dgvListadoCargos);
            dgvListadoCargos.Columns[1].Visible = false;
            dgvListadoCargos.Columns[3].Visible = false;
            dgvListadoCargos.Visible = true;
        }

        private void LocalizarDgvCargo()
        {
            dgvListadoCargos.Location = new Point(panel8.Location.X, panel8.Location.Y + 1);
            dgvListadoCargos.Size = new Size(300, 90);
            dgvListadoCargos.Visible = true;
            lblSueldoP.Visible = false;
            pnlBtnsGuardarP.Visible = false;
        }

        private void ObtenerDatosCargo()
        {
            IdCargo = Convert.ToInt32(dgvListadoCargos.SelectedCells[1].Value);
            txtCargo.Text = dgvListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldo.Text = dgvListadoCargos.SelectedCells[3].Value.ToString();
            dgvListadoCargos.Visible = false;
            pnlBtnsGuardarP.Visible = true;
            lblSueldoP.Visible = true;
        }

        private void ObtenerCargosEditar()
        {
            IdCargo = Convert.ToInt32(dgvListadoCargos.SelectedCells[1].Value);
            txtCargoG.Text = dgvListadoCargos.SelectedCells[2].Value.ToString();
            txtSueldoG.Text = dgvListadoCargos.SelectedCells[3].Value.ToString();
            btnGuardarC.Visible = false;
            btnGuardarCC.Visible = true;
            txtCargoG.Focus();
            txtCargoG.SelectAll();
            pnlCargo.Visible = true;
            pnlCargo.Dock = DockStyle.Fill;
            pnlCargo.BringToFront();
        }

        private void ReiniciarPaginado()
        {
            Desde = 1;
            Hasta = 10;
            ContarPersonal();
            if (Contador > Hasta)
            {
                btnPagSig.Enabled = true;
                btnPagAnt.Enabled = false;
                btnPagUlt.Enabled = true;
                btnPagPri.Enabled = true;
            }
            else
            {
                btnPagSig.Enabled = false;
                btnPagAnt.Enabled = false;
                btnPagUlt.Enabled = false;
                btnPagPri.Enabled = false;
            }
            Paginar();
        }

        private void Paginar()
        {
            try
            {
                lblPagina.Text = (Hasta / ItemPorPag).ToString();
                lblTotalPagina.Text = Math.Ceiling(Convert.ToSingle(Contador) / ItemPorPag).ToString();
                TotalPag = Convert.ToInt32(lblTotalPagina.Text);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void Limpiar()
        {
            txtNombres.Clear();
            txtIdentificacion.Clear();
            txtCargo.Clear();
            txtSueldo.Clear();
            BuscarCargos();
        }

        #endregion
    }
}
