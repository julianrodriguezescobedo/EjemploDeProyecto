using EjemploDeProyecto.BE;
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
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //Validaciones...

            //Comprobar credenciales...

            IUser user = new User() { Username = txtUsername.Text, Password = txtPassword.Text };

            Sesion.Login(user);

            MessageBox.Show("Sesión iniciada por: " + Sesion.ObtenerUsername());
        }
    }
}
