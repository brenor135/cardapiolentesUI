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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace cardapiolentesUI
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        private string lenteidrepasse;
        private string codigorepasse;
        private string nomerepasse;
        private string obsrepasse;
        private string prazorepasse;
        private string vendarepasse;
        private string sugeridorepasse;
        private string materialrepasse;
        private string pigmentacaorepasse;
        private string fabricanterepasse;
        private string classerepasse;
        private string ativorepasse;
        private string tipovisaorepasse;
        private string tipolenterepasse;
        private string tipomenurepasse;

        public string Lenteidrepasse
        {
            get { return lenteidrepasse; }
            set { lenteidrepasse = value; }

        }
        public string Codigorepasse
        {
            get { return codigorepasse; }
            set { codigorepasse = value;}
            
        }
        public string Nomerepasse
        {
            get { return nomerepasse; }
            set { nomerepasse = value;}
        }
        public string Obsrepasse
        {
            get { return obsrepasse; }
            set { obsrepasse = value; }
        }
        public string Prazorepasse
        {
            get { return prazorepasse; }
            set { prazorepasse = value; }
        }
        public string Vendarepasse
        {
            get { return vendarepasse; }
            set { vendarepasse = value; }
        }
        public string Sugeridorepasse
        {
            get { return sugeridorepasse; }
            set { sugeridorepasse = value; }
        }
        public string Materialrepasse
        {
            get { return materialrepasse; }
            set { materialrepasse = value; }
        }
        public string Pigmentacaorepasse
        {
            get { return pigmentacaorepasse; }
            set { pigmentacaorepasse = value; }
        }
        public string Fabricanterepasse
        {
            get { return fabricanterepasse; }
            set { fabricanterepasse = value; }
        }
        public string Classerepasse
        {
            get { return classerepasse; }
            set { classerepasse = value; }
        }
        public string Ativorepasse
        {
            get { return ativorepasse; }
            set { ativorepasse = value; }
        }
        public string Tipovisaorepasse
        {
            get { return tipovisaorepasse; }
            set { tipovisaorepasse = value; }
        }
        public string Tipolenterepasse
        {
            get { return tipolenterepasse; }
            set { tipolenterepasse = value; }
        }
        public string Tipomenurepasse
        {
            get { return tipomenurepasse; }
            set { tipomenurepasse = value; }
        }



        MySqlConnection con = null;
        public static string idlenterepasse;
        public MainWindow()
        {
            this.setConnection();
            InitializeComponent();
        }
        private void setConnection()
        {
            String conexao = ConfigurationManager.ConnectionStrings["conexaomysql"].ConnectionString;
            con = new MySqlConnection(conexao);
            try
            {
                con.Open();
            }
            catch (Exception exp){ 

            }
        }

        private void populacbpesquisa()
        {
           
        }
        private void updateDataGrid()
        {
            MySqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select lenteid as ID, ativonome as ATIVO, lentecodigo as CODIGO,lentenome as NOME, lenteprazo as PRAZO, lentevalorvenda as 'VALOR VENDA', lentevalorsugerido as 'VALOR SUGERIDO', materialnome as MATERIAL," +
" lenteobs as OBSERVACAO, pigmentacaonome as PIGMENTACAO, fabricantenome as FABRICANTE, tipovisaonome AS VISAO, tipolentenome as LENTE, tipomenunome as TIPOMENU, classenome as CLASSE from lentes " +
"inner join ativo on ativoid = lenteativo " +
"inner join material on materialid = lentematerial " +
"inner join pigmentacao on pigmentacaoid = lentepigmentacao " +
"inner join fabricante on fabricanteid = lentefabricante " +
"inner join tipovisao on tipovisaoid = lentetipovisao " +
"inner join tipolente on tipolenteid = lentetipolente " +
"inner join tipomenu on tipomenuid = lentetipomenu " +
"inner join classe on classeid = lenteclasse";
            cmd.CommandType = CommandType.Text;
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            tabela.ItemsSource = dt.DefaultView;
            dr.Close();
            tabela.Columns[0].Visibility = Visibility.Hidden;
            tabela.Columns[13].Visibility = Visibility.Hidden;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.updateDataGrid();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            con.Close();
        }

        private void brcadastro_Click(object sender, RoutedEventArgs e)
        {
            new cadastrolente().Show();
            this.Hide();
        }

        private void tabela_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGrid gd = (DataGrid)sender;
            DataRowView row_selected = gd.SelectedItem as DataRowView;
            if (row_selected != null)
            {
                Lenteidrepasse = row_selected["ID"].ToString();
                Ativorepasse = row_selected["ATIVO"].ToString();
                Codigorepasse = row_selected["CODIGO"].ToString();
                Nomerepasse = row_selected["NOME"].ToString();
                Prazorepasse = row_selected["PRAZO"].ToString();
                Vendarepasse = row_selected["VALOR VENDA"].ToString();
                Sugeridorepasse = row_selected["VALOR SUGERIDO"].ToString();
                Materialrepasse = row_selected["MATERIAL"].ToString();
                Obsrepasse = row_selected["OBSERVACAO"].ToString();
                Pigmentacaorepasse = row_selected["PIGMENTACAO"].ToString();
                Fabricanterepasse = row_selected["FABRICANTE"].ToString();
                Classerepasse = row_selected["CLASSE"].ToString();
                cadastrolente cl = new cadastrolente();
                cl.Show();
                cl.txtidlente.Text = Lenteidrepasse;
                cl.cbativo.Text = Ativorepasse;
                cl.txtcodigo.Text = Codigorepasse;
                cl.txtlentenome.Text = Nomerepasse;
                cl.txtprazo.Text = Prazorepasse;
                cl.txtvalorvenda.Text = Vendarepasse;
                cl.txtvalorsugerido.Text = Sugeridorepasse;
                cl.cbmaterial.Text = Materialrepasse;
                cl.txtobs.Text = Obsrepasse;
                cl.cbpigmentacao.Text = Pigmentacaorepasse;
                cl.cbfabricante.Text = Fabricanterepasse;
                cl.cbclasse.Text = Classerepasse;
                this.Close();
                

        //        obsrepasse;
        //private string prazorepasse;
        //private string vendarepasse;
        //private string sugeridorepasse;
        //private string materialrepasse;
        //private string pigmentacaorepasse;
        //private string fabricanterepasse;
        //private string classerepasse;
        //private string ativorepasse;
    }
        }

        private void btpesquisa_Click(object sender, RoutedEventArgs e)
        { if (cbpesquisa.Text.Equals("NOME"))
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select lenteid as ID,ativonome as ATIVO, lentecodigo as CODIGO,lentenome as NOME, lenteprazo as PRAZO, lentevalorvenda as 'VALOR VENDA', lentevalorsugerido as 'VALOR SUGERIDO', materialnome as MATERIAL," +
    " lenteobs as OBSERVACAO, pigmentacaonome as PIGMENTACAO, fabricantenome as FABRICANTE, tipovisaonome AS VISAO, tipolentenome as LENTE, tipomenunome as TIPOMENU, classenome as CLASSE from lentes " +
    "inner join ativo on ativoid = lenteativo " +
    "inner join material on materialid = lentematerial " +
    "inner join pigmentacao on pigmentacaoid = lentepigmentacao " +
    "inner join fabricante on fabricanteid = lentefabricante " +
    "inner join tipovisao on tipovisaoid = lentetipovisao " +
    "inner join tipolente on tipolenteid = lentetipolente " +
    "inner join tipomenu on tipomenuid = lentetipomenu " +
    "inner join classe on classeid = lenteclasse where lentenome like '%" + txtpesquisa.Text +"%'";
                cmd.CommandType = CommandType.Text;
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                tabela.ItemsSource = dt.DefaultView;
                dr.Close();
                tabela.Columns[0].Visibility = Visibility.Hidden;
                tabela.Columns[13].Visibility = Visibility.Hidden;
            } else
            {
                MySqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select lenteid as ID,ativonome as ATIVO, lentecodigo as CODIGO,lentenome as NOME, lenteprazo as PRAZO, lentevalorvenda as 'VALOR VENDA', lentevalorsugerido as 'VALOR SUGERIDO', materialnome as MATERIAL," +
    " lenteobs as OBSERVACAO, pigmentacaonome as PIGMENTACAO, fabricantenome as FABRICANTE, tipovisaonome AS VISAO, tipolentenome as LENTE, tipomenunome as TIPOMENU, classenome as CLASSE from lentes " +
    "inner join ativo on ativoid = lenteativo " +
    "inner join material on materialid = lentematerial " +
    "inner join pigmentacao on pigmentacaoid = lentepigmentacao " +
    "inner join fabricante on fabricanteid = lentefabricante " +
    "inner join tipovisao on tipovisaoid = lentetipovisao " +
    "inner join tipolente on tipolenteid = lentetipolente " +
    "inner join tipomenu on tipomenuid = lentetipomenu " +
    "inner join classe on classeid = lenteclasse where lentecodigo like '%" + txtpesquisa.Text + "%'";
                cmd.CommandType = CommandType.Text;
                MySqlDataReader dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                tabela.ItemsSource = dt.DefaultView;
                dr.Close();
                tabela.Columns[0].Visibility = Visibility.Hidden;
                tabela.Columns[13].Visibility = Visibility.Hidden;
            }

        }

        private void tabela_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
