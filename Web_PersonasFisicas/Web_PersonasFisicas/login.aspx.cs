using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_PersonasFisicas.Controlador;
using Web_PersonasFisicas.Modelo;

namespace Web_PersonasFisicas
{
    public partial class login : System.Web.UI.Page
    {
        UsuariosData clase = new UsuariosData();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (User.Text == "" || Pass.Text == "")
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Ingrese Usuario/Contraseña');", true);
            else
            {
                UsuaroResponse Logueo = new UsuaroResponse();
                UsuarioRequest Usuario = new UsuarioRequest();
                Usuario.Usuario = User.Text;
                Usuario.Contrasenia = Pass.Text;

                Logueo = clase.ConsultarUnaPersona(Usuario);

                if (Logueo.ID > 0)
                {
                    Session.Add("ID", Logueo.ID);
                    Session.Add("Usuario", Logueo.Usuario);

                    Response.Redirect("~/Vista/vModulo.aspx");

                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Credenciales incorrectas');", true);
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            User.Text = "";
            Pass.Text = "";
        }
    }
}