using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace order_getter_csharp
{
    public partial class details : Form
    {
        string connect =
            "server=localhost;" +
            "Database=diplom2;" +
            "User=root;" +
            "Port=3306;" +
            "Password=1111";
        public int Value {
            get; set;
        }
        public details()
        {
            InitializeComponent();
        }

        private void details_Load(object sender, EventArgs e) {
            viewDetails();
        }
        private void viewDetails() {
            MySqlConnection conn = new MySqlConnection(connect);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = 
                "select\r\n" +
                    "d1_0.amount,\r\n" +
                    "d1_0.price,\r\n" +
                    "p1_0.name,\r\n" +
                    "p1_0.price,\r\n" +
                    "c1_0.name,\r\n" +
                    "b1_0.name\r\nfrom\r\n" +
                    "orders_products d1_0\r\n" +
                "left join\r\n" +
                    "(products p1_0\r\n" +
                "left join\r\n" +
                    "category_products p1_1\r\n" +
                    "on p1_0.id=p1_1.product_id\r\n" +
                "left join\r\n" +
                    "brand_products p1_2\r\n" +
                    "on p1_0.id=p1_2.product_id)\r\n" +
                    "on p1_0.id=d1_0.products_id\r\n" +
                "left join\r\n" +
                    "categories c1_0\r\n" +
                    "on c1_0.id=p1_1.category_id\r\n" +
                    "left join\r\n" +
                    "brands b1_0\r\n" +
                    "on b1_0.id=p1_2.brand_id\r\n" +
                "where\r\n" +
                    "d1_0.order_id=" + @Value;
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[0].HeaderText = "Количество";
            dataGridView1.Columns[1].HeaderText = "Цена";
            dataGridView1.Columns[2].HeaderText = "Товар";
            dataGridView1.Columns[3].HeaderText = "Сумма";
            dataGridView1.Columns[4].HeaderText = "Категория";
            dataGridView1.Columns[5].HeaderText = "Бренд";
            cmd.Dispose();
            conn.Close();
        }
    }
}
