using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace sqlPractice2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // 動物リスト
            List<string> animalList = new List<string>() { "犬", "猫", "鳥", "猿" };

            // MySQLへの接続情報
            string server = "localhost";
            string database = "mysql";
            string user = "root";
            string pass = "";
            string charset = "utf8";
            string connectionString = string.Format("Server={0};Database={1};Uid={2};Pwd={3};Charset={4}", server, database, user, pass, charset);

            MySqlConnection con = new MySqlConnection(connectionString);

            DataTable dt = new DataTable();

            try
            {
                // データベースと接続する
                con.Open();

                // SQLコマンドを宣言する。
                MySqlCommand cmd = con.CreateCommand();

                // テーブルを作成する。
                cmd.CommandText = " CREATE TABLE IF NOT EXISTS sample " +
                                  " (no INT NOT NULL, title VARCHAR(45),PRIMARY KEY(no))";
                cmd.ExecuteNonQuery();

                // データを全て削除する。
                cmd.CommandText = "DELETE FROM sample ";
                cmd.ExecuteNonQuery();

                // 動物リストを追加する。
                cmd.ExecuteNonQuery();
                for(int i = 0; i < animalList.Count; i++)
                {
                    // データを挿入する
                    cmd.CommandText = $" INSERT INTO sample VALUES ({i},'{animalList[i]}')";
                    cmd.ExecuteNonQuery();
                }

                // データを全件取得する。
                MySqlDataAdapter adapter = new MySqlDataAdapter("SELECT * FROM sample", con);

                // adapterからdtにデータを読み込む
                adapter.Fill(dt);

                // dtをdataGridViewのデータソースに設定する。
                dataGridView1.DataSource = dt;
            }
            finally
            { 
                con.Close();
            }
        }
    }
}
