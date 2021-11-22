using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using DepartamentoCoreCRUD.Models;

namespace DepartamentoCoreCRUD.Data
{
    public class DepartamentosContext
    {
        SqlConnection cn;
        SqlCommand com;
        SqlDataReader reader;

        public DepartamentosContext(string cadenaconexion)
        {
            this.cn = new SqlConnection(cadenaconexion);
            this.com = new SqlCommand();
            this.com.Connection = this.cn;
            this.com.CommandType = System.Data.CommandType.Text;

        }

        public List<Departamento> GetDepartamentos()
        {
            String sql = "select * from dept";
            this.com.CommandText = sql;
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            List<Departamento> departamentos = new List<Departamento>();

            while (this.reader.Read())
            {
                Departamento dept = new Departamento();
                dept.Numero = int.Parse(this.reader["DEPT_NO"].ToString());
                dept.Nombre = this.reader["DNOMBRE"].ToString();
                dept.Localidad = this.reader["LOC"].ToString();

                departamentos.Add(dept);
            }

            this.reader.Close();
            this.cn.Close();

            return departamentos;
        }

        public Departamento FindDepartamento(int iddepartamento)
        {
            String sql = "select * from dept where dept_no = @iddepartamento";
            this.com.CommandText = sql;
            SqlParameter pamid = new SqlParameter("@iddepartamento",iddepartamento);
            this.com.Parameters.Add(pamid);
            this.cn.Open();
            this.reader = this.com.ExecuteReader();

            Departamento dept = new Departamento();
            this.reader.Read();
            dept.Numero = int.Parse(this.reader["DEPT_NO"].ToString());
            dept.Nombre = this.reader["DNOMBRE"].ToString();
            dept.Localidad = this.reader["LOC"].ToString();

            this.reader.Close();
            this.cn.Close();
            this.com.Parameters.Clear();

            return dept;
        }

        public int InsertDepartamento(int iddepartamento, String nombre, String localidad)
        {
            String sql = "insert into dept values (@id, @nombre, @localidad)";
            this.com.CommandText = sql;
            SqlParameter pamid = new SqlParameter("@id", iddepartamento);
            SqlParameter pamnombre = new SqlParameter("@nombre", nombre);
            SqlParameter pamlocalidad = new SqlParameter("@localidad", localidad);

            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamlocalidad);

            this.cn.Open();
            int resultado = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

            return resultado;
        }

        public int UpdateDepartamento(int iddepartamento, String nombre, String localidad)
        {
            String sql = "update dept set dnombre=@nombre, loc=@localidad "
                + " where dept_no=@id";
            this.com.CommandText = sql;
            SqlParameter pamid = new SqlParameter("@id", iddepartamento);
            SqlParameter pamnombre = new SqlParameter("@nombre", nombre);
            SqlParameter pamlocalidad = new SqlParameter("@localidad", localidad);

            this.com.Parameters.Add(pamid);
            this.com.Parameters.Add(pamnombre);
            this.com.Parameters.Add(pamlocalidad);

            this.cn.Open();
            int resultado = this.com.ExecuteNonQuery();
            this.cn.Close();
            this.com.Parameters.Clear();

            return resultado;
        }

        public int DeleteDepartamento(int iddepartamento)
        {
            String sql = "delete from dept where dept_no = @iddepartamento";
            this.com.CommandText = sql;
            SqlParameter pamid = new SqlParameter("@iddepartamento", iddepartamento);
            this.com.Parameters.Add(pamid);
            this.cn.Open();

            int resultado = this.com.ExecuteNonQuery();


            this.cn.Close();
            this.com.Parameters.Clear();

            return resultado;
        }
    }
}
