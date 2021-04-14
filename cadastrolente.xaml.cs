using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;

namespace cardapiolentesUI
{
    /// <summary>
    /// Lógica interna para cadastrolente.xaml
    /// </summary>
    public partial class cadastrolente : Window
    {
        MySqlConnection con = null;
        public cadastrolente()
        {
            InitializeComponent();
            this.setconnection();
            this.populaativo();
            this.populpigmentacao();
            this.populaclasse();
            this.populafabricante();
            this.populamaterial();
            this.populatipovisao();
            this.populatipolente();
        }
        private void setconnection()
        {
            String connectionString = ConfigurationManager.ConnectionStrings["conexaomysql"].ConnectionString;
            con = new MySqlConnection(connectionString);
            try
            {
                con.Open();
            }
            catch (Exception exp) { }

        }

        private void populaativo()
        {
            MySqlDataAdapter sqlda = new MySqlDataAdapter("populaativo", con);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtativo = new DataTable();
            sqlda.Fill(dtativo);
            for (int i = 0; i < dtativo.Rows.Count; i++)
            {
                cbativo.Items.Add(dtativo.Rows[i]["ativonome"]);
            }
            con.Close();
        }

        private void populpigmentacao()
        {
            MySqlDataAdapter sqlda = new MySqlDataAdapter("populapigmentacao", con);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtpigmentacao = new DataTable();
            sqlda.Fill(dtpigmentacao);
            for (int i = 0; i < dtpigmentacao.Rows.Count; i++)
            {
                cbpigmentacao.Items.Add(dtpigmentacao.Rows[i]["pigmentacaonome"]);
            }
            con.Close();
        }

        private void populatipovisao()
        {
            MySqlDataAdapter sqlda = new MySqlDataAdapter("populatipovisao", con);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dttipovisao = new DataTable();
            sqlda.Fill(dttipovisao);
            for (int i = 0; i < dttipovisao.Rows.Count; i++)
            {
                cbtipovisao.Items.Add(dttipovisao.Rows[i]["tipovisaonome"]);
            }
            con.Close();
        }

        private void populatipolente()
        {
            MySqlDataAdapter sqlda = new MySqlDataAdapter("populatipolente", con);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dttipolente = new DataTable();
            sqlda.Fill(dttipolente);
            for (int i = 0; i < dttipolente.Rows.Count; i++)
            {
                cbtipolente.Items.Add(dttipolente.Rows[i]["tiponome"]);
            }
            con.Close();
        }

        private void populaclasse()
        {
            MySqlDataAdapter sqlda = new MySqlDataAdapter("populaclasse", con);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtclasse = new DataTable();
            sqlda.Fill(dtclasse);
            for (int i = 0; i < dtclasse.Rows.Count; i++)
            {
                cbclasse.Items.Add(dtclasse.Rows[i]["classenome"]);
            }
            con.Close();
        }

        private void populafabricante()
        {
            MySqlDataAdapter sqlda = new MySqlDataAdapter("populafabricante", con);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtfabricante = new DataTable();
            sqlda.Fill(dtfabricante);
            for (int i = 0; i < dtfabricante.Rows.Count; i++)
            {
                cbfabricante.Items.Add(dtfabricante.Rows[i]["fabricantenome"]);
            }
            con.Close();
        }

        private void populamaterial()
        {
            MySqlDataAdapter sqlda = new MySqlDataAdapter("populamaterial", con);
            sqlda.SelectCommand.CommandType = CommandType.StoredProcedure;
            DataTable dtmaterial = new DataTable();
            sqlda.Fill(dtmaterial);
            for (int i = 0; i < dtmaterial.Rows.Count; i++)
            {
                cbmaterial.Items.Add(dtmaterial.Rows[i]["materialnome"]);
            }
            con.Close();
        }

        private void btsalvar_Click(object sender, RoutedEventArgs e)
        {
            this.setconnection();
            try
            {
                MySqlCommand mysqlcmd = new MySqlCommand("adicionaeditalente", con);
                mysqlcmd.CommandType = CommandType.StoredProcedure;
                mysqlcmd.Parameters.AddWithValue("_lenteid", int.Parse(txtidlente.Text.ToUpper())) ;
                mysqlcmd.Parameters.AddWithValue("_lentecodigo", txtcodigo.Text.ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lentenome", txtlentenome.Text.ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lenteobs", txtobs.Text.ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lenteprazo", txtprazo.Text.ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lentevalorvenda", txtvalorvenda.Text = double.Parse(txtvalorvenda.Text).ToString().ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lentevalorsugerido", txtvalorsugerido.Text = double.Parse(txtvalorsugerido.Text).ToString().ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lentematerial", cbmaterial.Text.ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lentepigmentacao", cbpigmentacao.Text.ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lentefabricante", cbfabricante.Text.ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lenteclasse", cbclasse.Text.ToUpper());
                mysqlcmd.Parameters.AddWithValue("_lenteativo", cbativo.Text.ToUpper());
                mysqlcmd.ExecuteNonQuery();
                MessageBox.Show("CADASTRADO COM SUCESSO");
                MainWindow mw = new MainWindow();
                this.Close();
                mw.Show();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void btvoltar_Click(object sender, RoutedEventArgs e)
        {           
            MainWindow mw = new MainWindow();
            this.Close();
            mw.Show();    
        }

        private void txtvalorvenda_LostFocus(object sender, RoutedEventArgs e)
        {
            txtvalorvenda.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("id-ID"), "{0:#,##0.00}", double.Parse(txtvalorvenda.Text));
        }

        private void txtvalorsugerido_LostFocus(object sender, RoutedEventArgs e)
        {
            txtvalorsugerido.Text = string.Format(System.Globalization.CultureInfo.GetCultureInfo("id-ID"), "{0:#,##0.00}", double.Parse(txtvalorsugerido.Text));
        }
    }
}
