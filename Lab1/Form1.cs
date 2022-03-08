using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Lab1take2
{
    public partial class Form1 : Form
    {

        SqlConnection myConnection;
        SqlDataAdapter AntiquityDA, AntiquityTypeDA;
        DataSet myDataSet;
        SqlCommandBuilder myCommandBuilder;
        BindingSource AntiquityBindingSource, AntiquityTypeBindingSource;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AntiquityDA.Update(myDataSet, "Antiquity");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myConnection = new SqlConnection(@"Data Source = DESKTOP-MP1GSHH; Initial Catalog = antiquitylab; Integrated Security = True");
            myDataSet = new DataSet();
            AntiquityDA = new SqlDataAdapter("select * from Antiquity", myConnection);
            AntiquityTypeDA = new SqlDataAdapter("select * from AntiquityType", myConnection);
            myCommandBuilder = new SqlCommandBuilder(AntiquityDA);

            AntiquityDA.Fill(myDataSet, "Antiquity");
            AntiquityTypeDA.Fill(myDataSet, "AntiquityType");

            DataRelation myDataRelation = new DataRelation("FK_Antiquity_AType",
                myDataSet.Tables["AntiquityType"].Columns["atype_id"], myDataSet.Tables["Antiquity"].Columns["anttype_id"]);
            myDataSet.Relations.Add(myDataRelation);

            AntiquityBindingSource = new BindingSource();
            AntiquityTypeBindingSource = new BindingSource();

            AntiquityTypeBindingSource.DataSource = myDataSet;
            AntiquityTypeBindingSource.DataMember = "AntiquityType";

            AntiquityBindingSource.DataSource = AntiquityTypeBindingSource;
            AntiquityBindingSource.DataMember = "FK_Antiquity_AType";

            TypeGrid.DataSource = AntiquityTypeBindingSource;
            AntiquityGrid.DataSource = AntiquityBindingSource;
        }
    }
}
