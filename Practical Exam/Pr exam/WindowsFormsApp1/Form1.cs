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


namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        SqlConnection myConnection;
        SqlDataAdapter CarshopDA, EmployeeDA;
        DataSet myDataSet;
        SqlCommandBuilder myCommandBuilder;
        BindingSource CarShopBindingSource, EmployeeBindingSource;
        public Form1()
        {
            InitializeComponent();
        }

        private void Select_Click(object sender, EventArgs e)
        {
            myConnection = new SqlConnection(@"Data Source = DESKTOP-MP1GSHH; Initial Catalog = practicalexam; Integrated Security = True");
            myDataSet = new DataSet();
            CarshopDA = new SqlDataAdapter("select * from CarShops", myConnection);
            EmployeeDA = new SqlDataAdapter("select * from Employees", myConnection);
            myCommandBuilder = new SqlCommandBuilder(EmployeeDA); // the table used in UPDATE above

            CarshopDA.Fill(myDataSet, "CarShops"); //parent table
            EmployeeDA.Fill(myDataSet, "Employees"); //child table

            DataRelation myDataRelation = new DataRelation("FK_CarShops_Employees",
                 myDataSet.Tables["CarShops"].Columns["Id"],
                 myDataSet.Tables["Employees"].Columns["CarShopId"]);
            myDataSet.Relations.Add(myDataRelation);

            CarShopBindingSource = new BindingSource(); // PARENT
            EmployeeBindingSource = new BindingSource(); // CHILD

            CarShopBindingSource.DataSource = myDataSet;
            CarShopBindingSource.DataMember = "CarShops";

            EmployeeBindingSource.DataSource = CarShopBindingSource;
            EmployeeBindingSource.DataMember = "FK_CarShops_Employees";


            dgvCarShop.DataSource = CarShopBindingSource;
            dgvEmployees.DataSource = EmployeeBindingSource;
        }

        private void Update_Click(object sender, EventArgs e)
        {
            EmployeeDA.Update(myDataSet, "employees");

        }
    }
}
