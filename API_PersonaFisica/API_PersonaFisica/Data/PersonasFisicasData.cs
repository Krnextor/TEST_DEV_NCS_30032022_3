using API_PersonaFisica.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace API_PersonaFisica.Data
{
    public class PersonasFisicasData
    {
        public static DB_Result Registrar(PersonasFisicas Persona)
        {
            string sQueryFill = "dbo.sp_AgregarPersonaFisica";

            DB_Result resultado = new DB_Result();
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

                        SqlParameter NombreParameter = new SqlParameter();
                        NombreParameter.SqlDbType = SqlDbType.VarChar;
                        NombreParameter.Direction = ParameterDirection.Input;
                        NombreParameter.Size = 50;
                        NombreParameter.ParameterName = "@Nombre";
                        NombreParameter.Value = Persona.Nombre;

                        SqlParameter ApPaternoParameter = new SqlParameter();
                        ApPaternoParameter.SqlDbType = SqlDbType.VarChar;
                        ApPaternoParameter.Direction = ParameterDirection.Input;
                        ApPaternoParameter.Size = 50;
                        ApPaternoParameter.ParameterName = "@ApellidoPaterno";
                        ApPaternoParameter.Value = Persona.ApellidoPaterno;

                        SqlParameter ApMaternoParameter = new SqlParameter();
                        ApMaternoParameter.SqlDbType = SqlDbType.VarChar;
                        ApMaternoParameter.Direction = ParameterDirection.Input;
                        ApMaternoParameter.Size = 50;
                        ApMaternoParameter.ParameterName = "@ApellidoMaterno";
                        ApMaternoParameter.Value = Persona.ApellidoMaterno;

                        SqlParameter RFCParameter = new SqlParameter();
                        RFCParameter.SqlDbType = SqlDbType.VarChar;
                        RFCParameter.Direction = ParameterDirection.Input;
                        RFCParameter.Size = 13;
                        RFCParameter.ParameterName = "@RFC";
                        RFCParameter.Value = Persona.RFC;

                        SqlParameter FechaNacParameter = new SqlParameter();
                        FechaNacParameter.SqlDbType = SqlDbType.Date;
                        FechaNacParameter.Direction = ParameterDirection.Input;
                        FechaNacParameter.ParameterName = "@FechaNacimiento";
                        FechaNacParameter.Value = Persona.FechaNacimiento;

                        SqlParameter UsuarioAParameter = new SqlParameter();
                        UsuarioAParameter.SqlDbType = SqlDbType.Int;
                        UsuarioAParameter.Direction = ParameterDirection.Input;
                        UsuarioAParameter.ParameterName = "@UsuarioAgrega";
                        UsuarioAParameter.Value = Persona.UsuarioAgrega;

                        cmd.Parameters.Add(NombreParameter);
                        cmd.Parameters.Add(ApPaternoParameter);
                        cmd.Parameters.Add(ApMaternoParameter);
                        cmd.Parameters.Add(RFCParameter);
                        cmd.Parameters.Add(FechaNacParameter);
                        cmd.Parameters.Add(UsuarioAParameter);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);

                    }
                    cnn.Close();
                }

                resultado.ERROR = Convert.ToInt32(datos.Rows[0]["ERROR"].ToString());
                resultado.MENSAJEERROR = datos.Rows[0]["MENSAJEERROR"].ToString();

            }
            catch (Exception ex)
            {
                resultado.ERROR = -1;
                resultado.MENSAJEERROR = "Hubo un error. Consulte con el Administrador";
            }

            return resultado;
        }

        public static DB_Result Actualizar(PersonasFisicas Persona)
        {
            string sQueryFill = "dbo.sp_ActualizarPersonaFisica";

            DB_Result resultado = new DB_Result();
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

                        SqlParameter idPersonaParameter = new SqlParameter();
                        idPersonaParameter.SqlDbType = SqlDbType.Int;
                        idPersonaParameter.Direction = ParameterDirection.Input;
                        idPersonaParameter.ParameterName = "@IdPersonaFisica";
                        idPersonaParameter.Value = Persona.IdPersonaFisica;

                        SqlParameter NombreParameter = new SqlParameter();
                        NombreParameter.SqlDbType = SqlDbType.VarChar;
                        NombreParameter.Direction = ParameterDirection.Input;
                        NombreParameter.Size = 50;
                        NombreParameter.ParameterName = "@Nombre";
                        NombreParameter.Value = Persona.Nombre;

                        SqlParameter ApPaternoParameter = new SqlParameter();
                        ApPaternoParameter.SqlDbType = SqlDbType.VarChar;
                        ApPaternoParameter.Direction = ParameterDirection.Input;
                        ApPaternoParameter.Size = 50;
                        ApPaternoParameter.ParameterName = "@ApellidoPaterno";
                        ApPaternoParameter.Value = Persona.ApellidoPaterno;

                        SqlParameter ApMaternoParameter = new SqlParameter();
                        ApMaternoParameter.SqlDbType = SqlDbType.VarChar;
                        ApMaternoParameter.Direction = ParameterDirection.Input;
                        ApMaternoParameter.Size = 50;
                        ApMaternoParameter.ParameterName = "@ApellidoMaterno";
                        ApMaternoParameter.Value = Persona.ApellidoMaterno;

                        SqlParameter RFCParameter = new SqlParameter();
                        RFCParameter.SqlDbType = SqlDbType.VarChar;
                        RFCParameter.Direction = ParameterDirection.Input;
                        RFCParameter.Size = 13;
                        RFCParameter.ParameterName = "@RFC";
                        RFCParameter.Value = Persona.RFC;

                        SqlParameter FechaNacParameter = new SqlParameter();
                        FechaNacParameter.SqlDbType = SqlDbType.Date;
                        FechaNacParameter.Direction = ParameterDirection.Input;
                        FechaNacParameter.ParameterName = "@FechaNacimiento";
                        FechaNacParameter.Value = Persona.FechaNacimiento;

                        SqlParameter UsuarioAParameter = new SqlParameter();
                        UsuarioAParameter.SqlDbType = SqlDbType.Int;
                        UsuarioAParameter.Direction = ParameterDirection.Input;
                        UsuarioAParameter.ParameterName = "@UsuarioAgrega";
                        UsuarioAParameter.Value = Persona.UsuarioAgrega;

                        cmd.Parameters.Add(idPersonaParameter);
                        cmd.Parameters.Add(NombreParameter);
                        cmd.Parameters.Add(ApPaternoParameter);
                        cmd.Parameters.Add(ApMaternoParameter);
                        cmd.Parameters.Add(RFCParameter);
                        cmd.Parameters.Add(FechaNacParameter);
                        cmd.Parameters.Add(UsuarioAParameter);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);

                    }
                    cnn.Close();
                }

                resultado.ERROR = Convert.ToInt32(datos.Rows[0]["ERROR"].ToString());
                resultado.MENSAJEERROR = datos.Rows[0]["MENSAJEERROR"].ToString();

            }
            catch (Exception ex)
            {
                resultado.ERROR = -1;
                resultado.MENSAJEERROR = "Hubo un error. Consulte con el Administrador";
            }

            return resultado;
        }

        public static DB_Result Eliminar(PersonasFisicas Persona)
        {
            string sQueryFill = "dbo.sp_EliminarPersonaFisica";

            DB_Result resultado = new DB_Result();
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

                        SqlParameter idPersonaParameter = new SqlParameter();
                        idPersonaParameter.SqlDbType = SqlDbType.Int;
                        idPersonaParameter.Direction = ParameterDirection.Input;
                        idPersonaParameter.ParameterName = "@IdPersonaFisica";
                        idPersonaParameter.Value = Persona.IdPersonaFisica;

                        cmd.Parameters.Add(idPersonaParameter);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);

                    }
                    cnn.Close();
                }

                resultado.ERROR = Convert.ToInt32(datos.Rows[0]["ERROR"].ToString());
                resultado.MENSAJEERROR = datos.Rows[0]["MENSAJEERROR"].ToString();

            }
            catch (Exception ex)
            {
                resultado.ERROR = -1;
                resultado.MENSAJEERROR = "Hubo un error. Consulte con el Administrador";
            }

            return resultado;
        }



        public static PersonasFisicas ConsultarUnaPersona(PersonasFisicas Persona)
        {
            string sQueryFill = "dbo.sp_ConsultarUnaPersonaFisica";

            PersonasFisicas resultado = new PersonasFisicas();
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

                        SqlParameter idPersonaParameter = new SqlParameter();
                        idPersonaParameter.SqlDbType = SqlDbType.Int;
                        idPersonaParameter.Direction = ParameterDirection.Input;
                        idPersonaParameter.ParameterName = "@IdPersonaFisica";
                        idPersonaParameter.Value = Persona.IdPersonaFisica;

                        cmd.Parameters.Add(idPersonaParameter);

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);

                    }
                    cnn.Close();
                }

                bool activo = false;

                resultado.IdPersonaFisica = Convert.ToInt32(datos.Rows[0]["IdPersonaFisica"].ToString());
                resultado.Nombre = datos.Rows[0]["Nombre"].ToString();
                resultado.ApellidoPaterno = datos.Rows[0]["ApellidoPaterno"].ToString();
                resultado.ApellidoMaterno = datos.Rows[0]["ApellidoMaterno"].ToString();
                resultado.RFC = datos.Rows[0]["RFC"].ToString();
                resultado.FechaRegistro = Convert.ToDateTime(datos.Rows[0]["FechaRegistro"]);
                resultado.FechaActualizacion = string.IsNullOrEmpty(datos.Rows[0]["FechaActualizacion"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(datos.Rows[0]["FechaActualizacion"]);
                resultado.FechaNacimiento = Convert.ToDateTime(datos.Rows[0]["FechaNacimiento"]);
                resultado.UsuarioAgrega = Convert.ToInt32(datos.Rows[0]["UsuarioAgrega"].ToString());

                activo = Convert.ToBoolean(datos.Rows[0]["Activo"].ToString());

                resultado.Activo = activo == true ? 1 : 0; 

            }
            catch (Exception ex)
            {
                resultado = new PersonasFisicas();
            }

            return resultado;
        }

        public static List<PersonasFisicas> ConsultarPersonasFisicas()
        {
            string sQueryFill = "dbo.sp_ConsultarPersonasFisicas";

            List<PersonasFisicas> resultado = new List<PersonasFisicas>();
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

                        SqlDataAdapter SqlData = new SqlDataAdapter(cmd);
                        SqlData.Fill(datos);

                    }
                    cnn.Close();
                }

                foreach (DataRow item in datos.Rows)
                {
                    PersonasFisicas nodo = new PersonasFisicas();

                    bool activo = false;

                    nodo.IdPersonaFisica = Convert.ToInt32(item["IdPersonaFisica"].ToString());
                    nodo.Nombre = item["Nombre"].ToString();
                    nodo.ApellidoPaterno = item["ApellidoPaterno"].ToString();
                    nodo.ApellidoMaterno = item["ApellidoMaterno"].ToString();
                    nodo.RFC = item["RFC"].ToString();
                    nodo.FechaRegistro = Convert.ToDateTime(item["FechaRegistro"]);
                    nodo.FechaActualizacion = string.IsNullOrEmpty(item["FechaActualizacion"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(item["FechaActualizacion"]);
                    nodo.FechaNacimiento = Convert.ToDateTime(item["FechaNacimiento"]);
                    nodo.UsuarioAgrega = Convert.ToInt32(item["UsuarioAgrega"].ToString());

                    activo = Convert.ToBoolean(item["Activo"].ToString());

                    nodo.Activo = activo == true ? 1 : 0;



                    resultado.Add(nodo);
                }


            }
            catch (Exception ex)
            {
                resultado = new List<PersonasFisicas>();
            }

            return resultado;
        }
    }
}