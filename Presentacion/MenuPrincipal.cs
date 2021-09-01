using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//| || //  (( |+|========================================================
//| ||// ((   |+| AsisT   | 05-07-2021                                   
//| ||\\ ((   |+| Kyocode | www.kyocode.com | Gerardo Alvarez Mendoza    
//| || \\  (( |+|========================================================
namespace SisAsis.Presentacion
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void MenuPrincipal_Load(object sender, EventArgs e)
        {
            pnlBienvenida.Dock = DockStyle.Fill;
        }

        private void btnPersonal_Click(object sender, EventArgs e)
        {
            pnlBody.Controls.Clear();
            Personal control = new Personal();
            control.Dock = DockStyle.Fill;
            pnlBody.Controls.Add(control);
        }

        private void btnUsuarios_Click(object sender, EventArgs e)
        {
            pnlBody.Controls.Clear();
            CtlUsuario control = new CtlUsuario();
            control.Dock = DockStyle.Fill;
            pnlBody.Controls.Add(control);
        }
    }
}
