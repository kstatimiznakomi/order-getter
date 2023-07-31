using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace order_getter_csharp
{
    public partial class Form1 : Form
    {
        string connect =
            "server=localhost;" +
            "Database=diplom2;" +
            "User=root;" +
            "Port=3306;" +
            "Password=1111";
        int deliveryPointId = 1;

        public Form1(){
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e){
          getOrders();
        }

        private void getOrders() {
            MySqlConnection conn = new MySqlConnection(connect);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select\r\n" +
                    "o1_1.id,\r\n" +
                    "o1_1.status,\r\n" +
                    "u1_0.person_name\r\n" +
                "from\r\n" +
                    "delivery_points_orders o1_0\r\n" +
                "join\r\n" +
                    "orders o1_1\r\n" +
                    "on o1_1.id=o1_0.orders_id\r\n" +
                    "left join\r\n" +
                    "users u1_0\r\n" +
                    "on u1_0.id=o1_1.user_id\r\n" +
                 "left join\r\n" +
                    "buckets b1_0\r\n" +
                 "on u1_0.id=b1_0.user_id\r\n" +
                 "where\r\n" +
                 "o1_0.delivery_points_id=" + @deliveryPointId + " AND o1_1.status = 'GOING'";
            MySqlDataReader dr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dataGridView1.DataSource = dt;
            dataGridView1.Columns[2].HeaderText = "Заказ";
            dataGridView1.Columns[3].HeaderText = "Статус заказа";
            dataGridView1.Columns[4].HeaderText = "ФИО";
            cmd.Dispose();
            conn.Close();
            timer1.Interval = 3000;
            timer1.Start();
        }

        private void ordersGot(int orderId) {
            check check = new check();
            check.orderId = orderId;
            check.Show();
            }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column1") {
                try {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                    ordersGot(id);
                }
                catch { }
            }
            if (dataGridView1.Columns[e.ColumnIndex].Name == "Column2") {
                try {
                    int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[2].Value);
                    details details = new details();
                    details.Value = id;
                    details.Show();
                }
                catch { }
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            getOrders();
        }
    }
}
