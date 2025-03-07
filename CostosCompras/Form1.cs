using System.Data;
using System.Drawing;
using FirebirdSql.Data.FirebirdClient;

namespace CostosCompras
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Config();
        }
        public void Config()
        {
            string filePath = "C:\\ConfigDB\\DB.txt"; // Ruta de tu archivo de texto
            List<string> lineas = new List<string>();

            // Verificar si el archivo existe
            if (File.Exists(filePath))
            {
                // Leer todas las líneas del archivo
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        GlobalSettings.Instance.Config.Add(linea);
                    }

                }
                GlobalSettings.Instance.Ip = GlobalSettings.Instance.Config[0];
                GlobalSettings.Instance.Puerto = GlobalSettings.Instance.Config[1];
                GlobalSettings.Instance.Direccion = GlobalSettings.Instance.Config[2];
                GlobalSettings.Instance.User = GlobalSettings.Instance.Config[3];
                GlobalSettings.Instance.Pw = GlobalSettings.Instance.Config[4];

                GlobalSettings.Instance.StringConnection =
                    "User=" + GlobalSettings.Instance.User + ";" +
                    "Password=" + GlobalSettings.Instance.Pw + ";" +
                    "Database=" + GlobalSettings.Instance.Direccion + ";" +
                    "DataSource=" + GlobalSettings.Instance.Ip + ";" +
                    "Port=" + GlobalSettings.Instance.Puerto + ";" +
                    "Dialect=3;" +
                    "Charset=UTF8;";
            }
            else
            {
                MessageBox.Show("Credenciales para base de datos no encontradas", "DB.txt", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        public decimal FindCosto()
        {
            FbConnection con = new FbConnection("User=" + GlobalSettings.Instance.User + ";" + "Password=" + GlobalSettings.Instance.Pw + ";" + "Database=" + GlobalSettings.Instance.Direccion + ";" + "DataSource=" + GlobalSettings.Instance.Ip + ";" + "Port=" + GlobalSettings.Instance.Puerto + ";" + "Dialect=3;" + "Charset=UTF8;");
            try
            {
                con.Open();
                FbCommand command = new FbCommand("GET_PRECIO_ULTCOM", con);
                command.CommandType = CommandType.StoredProcedure;

                // Parámetros de entrada
                command.Parameters.Add("V_PROVEEDOR_ID", FbDbType.Integer).Value = 974251;
                // Parámetro de salida


                // Parámetros de entrada
                command.Parameters.Add("V_ARTICULO_ID", FbDbType.Integer).Value = Folio.Text;
                // Parámetro de salida


                FbParameter paramCOSTO = new FbParameter("PRECIO_UVEN", FbDbType.Numeric);
                paramCOSTO.Direction = ParameterDirection.Output;
                command.Parameters.Add(paramCOSTO);

                // Ejecutar el procedimiento almacenado
                command.ExecuteNonQuery();
                decimal costo = paramCOSTO.Value != DBNull.Value ? Convert.ToDecimal(paramCOSTO.Value) : 0m;

                return costo;
                //MessageBox.Show("ALMACÉN: "+ Existencia.ToString() +"\n TIENDA: "+ ExistenciaTienda.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Se perdió la conexión :( , contacta a 06 o intenta de nuevo", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show(ex.ToString());
                return 0;
            }
            finally
            {
                con.Close();
            }
        }
        class FolioModificar
        {
            public string Folio = "";
            public string cadena = "";
            public FolioModificar(string folio)
            {
                Folio = folio;
                if (Folio != string.Empty)
                {
                    for (int i = 0; i < (9 - Folio.Length); i++)
                    {
                        cadena += "0";
                    }

                }
            }
            public string FolioFinal()
            {
                return cadena + Folio;
            }
        }
        public string[] PenultimoCosto(string articulo_id)
        {
            string query = @"
            SELECT 
                DOCTOS_CM.FOLIO,
                DOCTOS_CM_DET.CLAVE_ARTICULO, 
                DOCTOS_CM_DET.PRECIO_UNITARIO,
                DOCTOS_CM_DET.PCTJE_DSCTO,
                (DOCTOS_CM_DET.PRECIO_UNITARIO / DOCTOS_CM_DET.CONTENIDO_UMED) AS PRECIO_VENTA
            FROM 
                DOCTOS_CM_DET
            INNER JOIN 
                DOCTOS_CM ON DOCTOS_CM.DOCTO_CM_ID = DOCTOS_CM_DET.DOCTO_CM_ID
            WHERE
                DOCTOS_CM_DET.ARTICULO_ID = '" + articulo_id + "'" +
                "AND DOCTOS_CM.TIPO_DOCTO = 'C'" +
                "AND DOCTOS_CM.ESTATUS != 'C'" +
                "ORDER BY DOCTOS_CM_DET.DOCTO_CM_ID DESC;";
            FbConnection con = new FbConnection(GlobalSettings.Instance.StringConnection);
            try
            {
                con.Open();
                FbCommand command = new FbCommand(query, con);
                FbDataReader reader = command.ExecuteReader();
                int limite = 0;
                string[] arreglo = new string[2];
                while (reader.Read() && limite < 2)
                {
                    decimal Descuentodecimal = (100 - reader.GetDecimal(3))/100;
                    arreglo[limite] =  (reader.GetDecimal(4)*Descuentodecimal).ToString();
                    limite++;
                }
                return arreglo;
            }
            catch
            {
                MessageBox.Show("Se perdió la conexión :( , contacta a 06 o intenta de nuevo", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public string UnicoValorRetorno(string query)
        {

            FbConnection con = new FbConnection(GlobalSettings.Instance.StringConnection);
            try
            {
                con.Open();
                FbCommand command = new FbCommand(query, con);
                FbDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    return reader.GetString(0);
                }
                else
                {
                    MessageBox.Show("Error al conseguir el valor", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return null;
                }

            }
            catch
            {
                MessageBox.Show("Se perdió la conexión :( , contacta a 06 o intenta de nuevo", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            finally
            {
                con.Close();
            }
        }
        public void Colorear()
        {
            for (int i = 0; i < Tabla.Rows.Count; ++i)
            {
                if (decimal.Parse(Tabla.Rows[i].Cells[7].Value.ToString()) < decimal.Parse(Tabla.Rows[i].Cells[8].Value.ToString()))
                {
                    Tabla.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightGreen;
                }
                else if (decimal.Parse(Tabla.Rows[i].Cells[7].Value.ToString()) > decimal.Parse(Tabla.Rows[i].Cells[8].Value.ToString()))
                {
                    Tabla.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.LightSteelBlue;
                }
                else
                {
                    Tabla.Rows[i].DefaultCellStyle.BackColor = System.Drawing.Color.White;
                }
            }
            Tabla.Refresh();
        }
        public decimal CostoUVEN(string articulo_id, string ProveedorId)
        {
            try
            {
                using (FbConnection con = new FbConnection(GlobalSettings.Instance.StringConnection))
                {
                    con.Open();  // Abrir la conexión

                    // Definir el comando para ejecutar el procedimiento almacenado
                    using (FbCommand command = new FbCommand("GET_PRECIO_ULTCOM", con))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;  // Asegúrate de usar StoredProcedure

                        // Parámetros de entrada
                        command.Parameters.Add("V_ARTICULO_ID", FbDbType.Integer).Value = articulo_id;  // ID del artículo
                        command.Parameters.Add("PROVEEDOR_ID", FbDbType.Integer).Value = ProveedorId;  // ID del proveedor

                        // Parámetros de salida
                        FbParameter paramArt = new FbParameter("ARTICULO_ID", FbDbType.Numeric);
                        paramArt.Direction = System.Data.ParameterDirection.Output;
                        command.Parameters.Add(paramArt);
                        FbParameter paramProv = new FbParameter("V_PROVEEDOR_ID", FbDbType.Numeric);
                        paramProv.Direction = System.Data.ParameterDirection.Output;
                        command.Parameters.Add(paramProv);
                        FbParameter paramPrecioUven = new FbParameter("PRECIO_UVEN", FbDbType.Numeric);
                        paramPrecioUven.Direction = System.Data.ParameterDirection.Output;
                        command.Parameters.Add(paramPrecioUven);

                        // Ejecutar el procedimiento almacenado
                        command.ExecuteNonQuery();

                        // Recuperar el valor de salida
                        return Convert.ToDecimal(command.Parameters[4].Value);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones
                Console.WriteLine("Error al ejecutar el procedimiento: " + ex.Message);
                return 0;  // En caso de error, se retorna 0
            }
        }
        private void Obtener_Click(object sender, EventArgs e)
        {
            Tabla.Rows.Clear();
            string Docto_Cm_Id = string.Empty;
            string Proveedor = string.Empty;
            if (Folio.Text != string.Empty)
            {
                FbConnection con = new FbConnection(GlobalSettings.Instance.StringConnection);
                try
                {
                    FolioModificar folio = new FolioModificar(Folio.Text);
                    string query = "SELECT DOCTO_CM_ID FROM DOCTOS_CM WHERE FOLIO = '" + folio.FolioFinal() + "' AND TIPO_DOCTO = 'C';";
                    Docto_Cm_Id = UnicoValorRetorno(query);
                    if(Docto_Cm_Id == null)
                    {
                        MessageBox.Show("No se encontró el folio", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    query = "SELECT PROVEEDOR_ID FROM DOCTOS_CM WHERE DOCTO_CM_ID = '" + Docto_Cm_Id + "';";
                    Proveedor = UnicoValorRetorno(query);
                    con.Open();
                    query = "SELECT CLAVE_ARTICULO, ARTICULO_ID, CONTENIDO_UMED, PRECIO_UNITARIO, PCTJE_DSCTO, (PRECIO_UNITARIO / CONTENIDO_UMED) AS PRECIO_VENTA FROM DOCTOS_CM_DET WHERE DOCTO_CM_ID = '" + Docto_Cm_Id + "';";
                    FbCommand command = new FbCommand(query, con);
                    FbDataReader reader = command.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        ++i;
                        query = "SELECT NOMBRE FROM ARTICULOS WHERE ARTICULO_ID = '" + reader.GetString(1) + "';";
                        string Descripcion = UnicoValorRetorno(query);
                        decimal Descuentodecimal = (100 - reader.GetDecimal(4)) / 100;
                        decimal CostoUnitario = reader.GetDecimal(5)*Descuentodecimal;
                        query = "SELECT PRECIO FROM PRECIOS_ARTICULOS WHERE ARTICULO_ID = '" + reader.GetString(1) + "' AND PRECIO_EMPRESA_ID = '42';";
                        string Precio_ListaAnt = UnicoValorRetorno(query);
                        string[] PenulCosto = PenultimoCosto(reader.GetString(1));
                        if(PenulCosto[1] == null)
                        {
                            PenulCosto[1] = CostoUnitario.ToString();
                        }
                        decimal UtilidadBruta = decimal.Parse(Precio_ListaAnt) - decimal.Parse(PenulCosto[1]);
                        decimal Margen = (UtilidadBruta / decimal.Parse(Precio_ListaAnt)) * 100;
                        //Margen = Math.Round(Margen, 2);
                        //query = "SELECT MARGEN FROM LIBRES_ARTICULOS WHERE ARTICULO_ID = '" + reader.GetString(1) + "';";
                        //string Margen = UnicoValorRetorno(query);
                        decimal MargenDecimal = (100m - Margen) / 100;
                        //decimal CostoUnitario = reader.GetDecimal(3) / reader.GetInt32(2);
                        decimal PrecioListaAct = CostoUnitario / MargenDecimal;
                        Tabla.Rows.Add(true, i, reader.GetString(0), Descripcion, PenulCosto[1], CostoUnitario, Math.Round(Margen, 3), Precio_ListaAnt, Math.Round(PrecioListaAct, 2));
                    }
                    Colorear();
                    CbFiltro.Visible = true;
                    CbFiltro.SelectedIndex = 0;
                    Tabla.ClearSelection();

                }
                catch
                {
                    MessageBox.Show("Se perdió la conexión :( , contacta a 06 o intenta de nuevo", "¡Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                finally
                {
                    con.Close();
                }
            }
            else
            {
                MessageBox.Show("La clave principal no puede estar vacía");
            }
        }
        private void Clave_Principal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Obtener.Focus();
            }
        }

        private void Tabla_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Tabla.CurrentCell.ColumnIndex == 6)
            {
                decimal CostoSelected = decimal.Parse(Tabla.CurrentRow.Cells[5].Value.ToString());
                decimal Margen = decimal.Parse(Tabla.CurrentRow.Cells[6].Value.ToString());
                decimal MargenDecimal = (100m - Margen) / 100;
                decimal PrecioListaActSel = CostoSelected / MargenDecimal;
                Tabla.CurrentRow.Cells[8].Value = Math.Round(PrecioListaActSel, 2);
                Colorear();
            }
        }

        private void CbFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CbFiltro.SelectedIndex == 1)
            {
                if (Tabla.Rows.Count > 0)
                {
                    for (int i = 0; i < Tabla.Rows.Count; ++i)
                    {
                        if (decimal.Parse(Tabla.Rows[i].Cells[7].Value.ToString()) < decimal.Parse(Tabla.Rows[i].Cells[8].Value.ToString()))
                        {
                            Tabla.Rows[i].Cells[0].Value = true;
                        }
                        else
                        {
                            Tabla.Rows[i].Cells[0].Value = false;
                        }
                    }
                }
            }
            if (CbFiltro.SelectedIndex == 2)
            {
                if (Tabla.Rows.Count > 0)
                {
                    for (int i = 0; i < Tabla.Rows.Count; ++i)
                    {
                        if (decimal.Parse(Tabla.Rows[i].Cells[7].Value.ToString()) > decimal.Parse(Tabla.Rows[i].Cells[8].Value.ToString()))
                        {
                            Tabla.Rows[i].Cells[0].Value = true;
                        }
                        else
                        {
                            Tabla.Rows[i].Cells[0].Value = false;
                        }
                    }
                }
            }
            if (CbFiltro.SelectedIndex == 3)
            {
                if (Tabla.Rows.Count > 0)
                {
                    for (int i = 0; i < Tabla.Rows.Count; ++i)
                    {

                        Tabla.Rows[i].Cells[0].Value = true;

                    }
                }
            }
            if (CbFiltro.SelectedIndex == 4)
            {
                if (Tabla.Rows.Count > 0)
                {
                    for (int i = 0; i < Tabla.Rows.Count; ++i)
                    {

                        Tabla.Rows[i].Cells[0].Value = false;

                    }
                }
            }

        }

        private void Update_Click(object sender, EventArgs e)
        {
            if (Tabla.Rows.Count > 0)
            {
                DialogResult result = MessageBox.Show(
                "¿Estás seguro que deseas actualizar los precios?",
                "Confirmación",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

                // Verificar la respuesta del usuario
                if (result == DialogResult.Yes)
                {
                    // Código para actualizar los precios
                    MessageBox.Show("Precios actualizados correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Actualización de precios cancelada.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                MessageBox.Show("No hay nada en la tabla.", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        
    }
}
