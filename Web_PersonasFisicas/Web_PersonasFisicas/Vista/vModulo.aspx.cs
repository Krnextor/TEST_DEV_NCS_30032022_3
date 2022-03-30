using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Web_PersonasFisicas.Controlador;
using Web_PersonasFisicas.Modelo;

namespace Web_PersonasFisicas.Vista
{
    public partial class vModulo : System.Web.UI.Page
    {
        PersonasFisicas Persona = new PersonasFisicas();
        DB_Result resultadoBase = new DB_Result();
        PersonasFisicasData clase = new PersonasFisicasData();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session.IsNewSession)
                {
                    Response.Redirect("~/Login.aspx");
                }

                dvInsertar.Visible = false;
                dvConsultar.Visible = true;

                cargarGridPersonasFisicas();
            }
        }

        protected void btnInsertar_Click(object sender, EventArgs e)
        {
            dvInsertar.Visible = true;
            dvConsultar.Visible = false;
            hIdPersonaFisica.Value = "0";
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            dvInsertar.Visible = false;
            dvConsultar.Visible = true;
        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
            string token = PostItem("ucand0021", "yNDVARG80sr@dDPc2yCT!");
            string direccion = "https://api.toka.com.mx/candidato/api/customers";


        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (validaCampos())
            {
                Persona = new PersonasFisicas();
                resultadoBase = new DB_Result();

                Persona.IdPersonaFisica = Convert.ToInt32(hIdPersonaFisica.Value);
                Persona.Nombre = txtNombre.Text;
                Persona.ApellidoPaterno = txtApPaterno.Text;
                Persona.ApellidoMaterno = txtApMaterno.Text;
                Persona.RFC = txtRFC.Text;
                Persona.FechaNacimiento = Convert.ToDateTime(txtFechaNac.Text);
                Persona.UsuarioAgrega = Convert.ToInt32(Session["ID"]);

                if (hIdPersonaFisica.Value == "0")
                {
                    resultadoBase = clase.Registrar(Persona);
                    hIdPersonaFisica.Value = "0";
                }
                else
                {
                    resultadoBase = clase.Actualizar(Persona);
                    hIdPersonaFisica.Value = "0";
                }

                dvConsultar.Visible = true;
                dvInsertar.Visible = false;

                LimpiarCampos();
                cargarGridPersonasFisicas();

                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('" + resultadoBase.MENSAJEERROR +"');", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('Capture todos los campos');", true);
            }

        }


        protected void gvInforme_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (gvInforme.Rows.Count > 0)
            {
                gvInforme.PageIndex = e.NewPageIndex;
                cargarGridPersonasFisicas();
            }
        }

        protected void gvInforme_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Actualizar")
            {
                int idPersonaFisica = Convert.ToInt32(e.CommandArgument.ToString());

                resultadoBase = new DB_Result();
                Persona = new PersonasFisicas();

                Persona.IdPersonaFisica = idPersonaFisica;

                Persona = clase.ConsultarUnaPersona(Persona);

                hIdPersonaFisica.Value = Persona.IdPersonaFisica.ToString();
                txtNombre.Text = Persona.Nombre;
                txtApPaterno.Text = Persona.ApellidoPaterno;
                txtApMaterno.Text = Persona.ApellidoMaterno;
                txtRFC.Text = Persona.RFC;
                txtFechaNac.Text = Persona.FechaNacimiento.ToString("dd/MM/yyyy");

                dvConsultar.Visible = false;
                dvInsertar.Visible = true;

            }
            else if (e.CommandName == "Eliminar")
            {
                int idPersonaFisica = Convert.ToInt32(e.CommandArgument.ToString());

                resultadoBase = new DB_Result();
                Persona = new PersonasFisicas();

                Persona.IdPersonaFisica = idPersonaFisica;

                resultadoBase = clase.Eliminar(Persona);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "myscript", "alert('" + resultadoBase.MENSAJEERROR + "');", true);

                dvConsultar.Visible = true;
                dvInsertar.Visible = false;

                cargarGridPersonasFisicas();

            }
        }

        private bool validaCampos()
        {
            bool resultado = true;

            if (string.IsNullOrEmpty(txtNombre.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtApPaterno.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtApMaterno.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtRFC.Text.Trim()))
            {
                return false;
            }
            if (string.IsNullOrEmpty(txtFechaNac.Text.Trim()))
            {
                return false;
            }

            return resultado;
        }

        private void LimpiarCampos()
        {
            txtNombre.Text = "";
            txtApPaterno.Text = "";
            txtApMaterno.Text = "";
            txtRFC.Text = "";
            txtFechaNac.Text = "";
        }

        private void cargarGridPersonasFisicas()
        {
            List<PersonasFisicas> Lista = new List<PersonasFisicas>();
            Lista = clase.ConsultarPersonasFisicas();

            gvInforme.DataSource = Lista;
            gvInforme.DataBind();

        }

        private string PostItem(string user, string pss)
        {
            string resultado = string.Empty;
            var url = $"https://api.toka.com.mx/candidato/api/login/authenticate";
            var request = (HttpWebRequest)WebRequest.Create(url);
            string json = $"{{\"Username\":\"{user}\",\"Password\":\"{pss}\"}}";
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream strReader = response.GetResponseStream())
                    {
                        if (strReader == null) return string.Empty;
                        using (StreamReader objReader = new StreamReader(strReader))
                        {
                            string responseBody = objReader.ReadToEnd();
                            resultado = responseBody;
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle error
            }
            return resultado;
        }
    }
}