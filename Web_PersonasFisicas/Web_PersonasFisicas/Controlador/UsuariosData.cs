using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Web_PersonasFisicas.Modelo;

namespace Web_PersonasFisicas.Controlador
{
    public class UsuariosData
    {
        public UsuaroResponse ConsultarUnaPersona(UsuarioRequest Usuario)
        {
            string sQueryFill = "dbo.sp_loginUsuario";

            UsuaroResponse resultado = new UsuaroResponse();
            DataTable datos = new DataTable();

            try
            {
                using (SqlConnection cnn = new SqlConnection(Conexion.cadenaConexion))
                {
                    cnn.Open();

                    using (SqlCommand cmd = new SqlCommand(sQueryFill, cnn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = sQueryFill;
                        cmd.Parameters.Clear();

                        SqlParameter UsuarioParameter = new SqlParameter();
                        UsuarioParameter.SqlDbType = SqlDbType.VarChar;
                        UsuarioParameter.Size = 30;
                        UsuarioParameter.Direction = ParameterDirection.Input;
                        UsuarioParameter.ParameterName = "@Usuario";
                        UsuarioParameter.Value = Usuario.Usuario;

                        SqlParameter ContraParameter = new SqlParameter();
                        ContraParameter.SqlDbType = SqlDbType.VarChar;
                        ContraParameter.Size = 30;
                        ContraParameter.Direction = ParameterDirection.Input;
                        ContraParameter.ParameterName = "@Contrasenia";
                        ContraParameter.Value = Usuario.Contrasenia;

                        cmd.Parameters.Add(UsuarioParameter);
                        cmd.Parameters.Add(ContraParameter);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);

                    }
                    cnn.Close();
                }

                if (datos.Rows.Count > 0)
                {
                    resultado.ID = Convert.ToInt32(datos.Rows[0]["ID"].ToString());
                    resultado.Usuario = datos.Rows[0]["Usuario"].ToString();
                }

            }
            catch (Exception ex)
            {
                resultado = new UsuaroResponse();
            }

            return resultado;
        }

    }
}