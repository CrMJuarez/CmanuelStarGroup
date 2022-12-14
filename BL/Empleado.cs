using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString("ConnectionStrings:DefaultConnection")))
                {
                    string query = "EmpleadoGetAll";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection.Open();
                    DataTable empleadoTable = new DataTable();
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(empleadoTable);

                    if (empleadoTable.Rows.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (DataRow row1 in empleadoTable.Rows)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = int.Parse(row1[0].ToString());
                            empleado.NombreEmpleado = row1[1].ToString();
                            empleado.SegundoNombreEmpleado = row1[2].ToString();
                            empleado.ApellidoPaternoEmpleado = row1[3].ToString();
                            empleado.ApellidoMaternoEmpleado = row1[4].ToString();
                            empleado.Edad = row1[5].ToString();
                            empleado.Email = row1[6].ToString();
                            empleado.Area = new ML.Area();
                            empleado.Area.IdArea = int.Parse(row1[7].ToString());
                            empleado.Area.NombreArea =row1[8].ToString();
                            empleado.Imagen = row1[9].ToString();
                            result.Objects.Add(empleado);
                        }
                        result.Correct = true;
                    }
                    else

                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al momento de mostrar el usuario";
                    }
                    cmd.Connection.Close();
                }

            }

            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;


        }
        public static ML.Result GetById(int IdEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {

                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString("ConnectionStrings:DefaultConnection")))
                {
                    string query = "EmpleadoGetById";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;


                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("@IdEmpleado", SqlDbType.Int);
                        collection[0].Value = IdEmpleado;

                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {

                            DataTable empleadoTable = new DataTable();
                            da.Fill(empleadoTable);
                            cmd.Connection.Open();

                            if (empleadoTable.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                DataRow row1 = empleadoTable.Rows[0];
                                ML.Empleado empleado = new ML.Empleado();
                                empleado.IdEmpleado = int.Parse(row1[0].ToString());
                                empleado.NombreEmpleado = row1[1].ToString();
                                empleado.SegundoNombreEmpleado = row1[2].ToString();
                                empleado.ApellidoPaternoEmpleado = row1[3].ToString();
                                empleado.ApellidoMaternoEmpleado = row1[4].ToString();
                                empleado.Edad = row1[5].ToString();
                                empleado.Email = row1[6].ToString();
                                empleado.Area = new ML.Area();
                                empleado.Area.IdArea = int.Parse(row1[7].ToString());
                                empleado.Area.NombreArea = row1[8].ToString();
                                empleado.Imagen = row1[9].ToString();
                                result.Object = empleado;
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;
                                result.ErrorMessage = "No se encontraron registros";
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;

        }

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString("ConnectionStrings:DefaultConnection")))
                {
                    string query = "EmpleadoAdd";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[8];

                    collection[0] = new SqlParameter("@NombreEmpleado", SqlDbType.VarChar);
                    collection[0].Value = empleado.NombreEmpleado;

                    collection[1] = new SqlParameter("@SegundoNombreEmpleado", SqlDbType.VarChar);
                    collection[1].Value = empleado.SegundoNombreEmpleado;
                    if (empleado.SegundoNombreEmpleado == "" || empleado.SegundoNombreEmpleado == null || empleado.SegundoNombreEmpleado.Equals(" "))
                    {
                        collection[1].Value = empleado.SegundoNombreEmpleado = " ";

                    }

                    collection[2] = new SqlParameter("@ApellidoPaternoEmpleado", SqlDbType.VarChar);
                    collection[2].Value = empleado.ApellidoPaternoEmpleado;

                    collection[3] = new SqlParameter("@ApellidoMaternoEmpleado", SqlDbType.VarChar);
                    collection[3].Value = empleado.ApellidoMaternoEmpleado;

                    collection[4] = new SqlParameter("@Edad", SqlDbType.VarChar);
                    collection[4].Value = empleado.Edad;

                    collection[5] = new SqlParameter("@Email", SqlDbType.VarChar);
                    collection[5].Value = empleado.Email;

                    collection[6] = new SqlParameter("@IdArea", SqlDbType.Int);
                    collection[6].Value = empleado.Area.IdArea;

                    collection[7] = new SqlParameter("@Imagen", SqlDbType.VarChar);
                    collection[7].Value = empleado.Imagen;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }
        public static ML.Result Update(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString("ConnectionStrings:DefaultConnection")))
                {
                    string query = "EmpleadoUpdate";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter[] collection = new SqlParameter[9];

                    collection[0] = new SqlParameter("@IdEmpleado", SqlDbType.Int);
                    collection[0].Value = empleado.IdEmpleado;

                    collection[1] = new SqlParameter("@NombreEmpleado", SqlDbType.VarChar);
                    collection[1].Value = empleado.NombreEmpleado;

                    collection[2] = new SqlParameter("@SegundoNombreEmpleado", SqlDbType.VarChar);
                    collection[2].Value = empleado.SegundoNombreEmpleado;

                    if (empleado.SegundoNombreEmpleado == "" || empleado.SegundoNombreEmpleado == null || empleado.SegundoNombreEmpleado.Equals(" "))
                    {
                        collection[2].Value = empleado.SegundoNombreEmpleado = " ";

                    }




                    collection[3] = new SqlParameter("@ApellidoPaternoEmpleado", SqlDbType.VarChar);
                    collection[3].Value = empleado.ApellidoPaternoEmpleado;

                    collection[4] = new SqlParameter("@ApellidoMaternoEmpleado", SqlDbType.VarChar);
                    collection[4].Value = empleado.ApellidoMaternoEmpleado;

                    collection[5] = new SqlParameter("@Edad", SqlDbType.VarChar);
                    collection[5].Value = empleado.Edad;

                    collection[6] = new SqlParameter("@Email", SqlDbType.VarChar);
                    collection[6].Value = empleado.Email;

                    collection[7] = new SqlParameter("@IdArea", SqlDbType.Int);
                    collection[7].Value = empleado.Area.IdArea;

                    collection[8] = new SqlParameter("@Imagen", SqlDbType.VarChar);
                    collection[8].Value = empleado.Imagen;


                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();
                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }

            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;

        }

        public static ML.Result Delete(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString("ConnectionStrings:DefaultConnection")))
                {
                    string query = "EmpleadoDelete";

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = query;
                    cmd.Connection = context;
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter[] collection = new SqlParameter[1];

                    collection[0] = new SqlParameter("@IdEmpleado", SqlDbType.Int);
                    collection[0].Value = empleado.IdEmpleado;

                    cmd.Parameters.AddRange(collection);
                    cmd.Connection.Open();

                    int RowsAffected = cmd.ExecuteNonQuery();
                    if (RowsAffected > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;

            }
            return result;
        }



    }
}
