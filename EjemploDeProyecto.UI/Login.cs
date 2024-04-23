using EjemploDeProyecto.BE;
using EjemploDeProyecto.BLL;
using EjemploDeProyecto.Framework;
using EjemploDeProyecto.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EjemploDeProyecto.UI
{
    public partial class Login : Form
    {
        BitacoraBLL bitacoraBLL = new BitacoraBLL();

        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                //Validaciones...

                //Comprobar credenciales...

                //Bitácora en BLL al momento de validar las credenciales

                IUser user = new User() { Username = txtUsername.Text, Password = txtPassword.Text };

                Sesion.Login(user);

                bitacoraBLL.Add(new Bitacora() { Tipo = BitacoraTipo.INFO, Usuario = user.Username, Mensaje = "Inicio de sesión." });

                MessageBox.Show("Sesión iniciada por: " + Sesion.ObtenerUsername());
            }
            catch (Exception ex)
            {
                bitacoraBLL.Add(new Bitacora() { Tipo = BitacoraTipo.ERROR, Usuario = Sesion.ObtenerUsername(), Mensaje = ex.Message.ToString() });
                MessageBox.Show(ex.Message.ToString(), "ERROR",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}