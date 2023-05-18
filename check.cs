using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace order_getter_csharp {
    public partial class check : Form {
        string connect =
            "server=localhost;" +
            "Database=diplom2;" +
            "User=root;" +
            "Port=3306;" +
            "Password=SHreder9955";
        public check() {
            InitializeComponent();
        }
        public int orderId {
            get; set;
        }
        private void check_Load(object sender, EventArgs e) {
            loadOrder();
        }

        private void loadOrder() {
            label1.Text = "Вы уверены, что хотите изменить статус заказа: " + @orderId + " на статус: 'DELIVERED'?";
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) {
            MySqlConnection conn = new MySqlConnection(connect);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "UPDATE orders SET status='DELIVERED' WHERE id=" + @orderId;
            cmd.ExecuteReader();
            MessageBox.Show("Статус успешно изменён");
            cmd.Dispose();
            conn.Close();
            this.Close();
        }
    }
}
