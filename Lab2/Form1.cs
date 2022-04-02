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
using System.Configuration;


namespace Lab2
{
    public partial class Form1 : Form
    {

        SqlConnection myConnection;
        SqlDataAdapter parentDA, childDA;
        DataSet myDataSet;
        SqlCommandBuilder myCommandBuilder;
        BindingSource childBindingSource, parentBindingSource;
        
        string parentName = ConfigurationManager.AppSettings["parentTable"];
        string childName = ConfigurationManager.AppSettings["childTable"];
        string childFK = ConfigurationManager.AppSettings["childFK"];
        string parentFK = ConfigurationManager.AppSettings["parentFKCol"];

        public Form1()
        {
            InitializeComponent();
        }

         private string getConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parentDA.Update(myDataSet, $"{parentName}");
            childDA.Update(myDataSet, $"{childName}");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            myConnection = new SqlConnection(getConnectionString());
            myDataSet = new DataSet();


            childDA = new SqlDataAdapter($"select * from {childName}", myConnection);
            parentDA = new SqlDataAdapter($"select * from {parentName}", myConnection);
            myCommandBuilder = new SqlCommandBuilder(childDA);

            childDA.Fill(myDataSet, $"{childName}");
            parentDA.Fill(myDataSet, $"{parentName}");

            DataRelation myDataRelation = new DataRelation($"FK_{childName}_{parentName}",
                myDataSet.Tables[$"{parentName}"].Columns[$"{parentFK}"], myDataSet.Tables[$"{childName}"].Columns[$"{childFK}"]);
            myDataSet.Relations.Add(myDataRelation);

            childBindingSource = new BindingSource();
            parentBindingSource = new BindingSource();

            parentBindingSource.DataSource = myDataSet;
            parentBindingSource.DataMember = $"{parentName}";

            childBindingSource.DataSource = parentBindingSource;
            childBindingSource.DataMember = $"FK_{childName}_{parentName}";

            TypeGrid.DataSource = parentBindingSource;
            AntiquityGrid.DataSource = childBindingSource;
        }
    }
}
