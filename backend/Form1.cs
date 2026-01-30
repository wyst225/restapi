using backend.Models;
using backend.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace backend
{
    public partial class Form1 : Form
    {
        private ProductService _service = new ProductService();
        private int selectedId = -1;

        public Form1()
        {
            InitializeComponent();
        }

        // LOAD (READ)
        private async void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = await _service.GetAll();
            selectedId = -1;
        }

        // CREATE
        private async void button2_Click(object sender, EventArgs e)
        {
            var product = new Product
            {
                name = textBox1.Text,
                price = decimal.Parse(textBox2.Text),
                category = textBox3.Text,
                inStock = checkBox1.Checked
            };

            await _service.Create(product);
            button1.PerformClick();
        }

        // UPDATE
        private async void button3_Click(object sender, EventArgs e)
        {
            if (selectedId == -1) return;

            var product = new Product
            {
                name = textBox1.Text,
                price = decimal.Parse(textBox2.Text),
                category = textBox3.Text,
                inStock = checkBox1.Checked
            };

            await _service.Update(selectedId, product);
            button1.PerformClick();
        }

        // DELETE
        private async void button4_Click(object sender, EventArgs e)
        {
            if (selectedId == -1) return;

            await _service.Delete(selectedId);
            button1.PerformClick();
        }

        // ROW SELECT
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex];

            selectedId = Convert.ToInt32(row.Cells["id"].Value);
            textBox1.Text = row.Cells["name"].Value.ToString();
            textBox2.Text = row.Cells["price"].Value.ToString();
            textBox3.Text = row.Cells["category"].Value.ToString();
            checkBox1.Checked = Convert.ToBoolean(row.Cells["inStock"].Value);
        }
    }
}
