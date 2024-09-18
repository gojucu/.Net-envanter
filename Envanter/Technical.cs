using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Envanter
{
    public partial class Technical : Form
    {
        public Technical()
        {
            InitializeComponent();
        }
        EnvanterDataContext db = new EnvanterDataContext();
        private void AddCategory_Click(object sender, EventArgs e)
        {
            string itemname = textBox1.Text.Trim();

            if (string.IsNullOrWhiteSpace(itemname))
            {
                MessageBox.Show("Category name cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // End if empty or null
            }

            var existingCategory = db.Categories.FirstOrDefault(c => c.Name == itemname);

            if (existingCategory != null)
            {
                 MessageBox.Show("This category already exists.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DateTime currentDate = DateTime.Now;

                var newCat = new Category
                {
                    Name = itemname,
                    CreateDate = currentDate,
                    isActive = true,
                };
                db.Categories.InsertOnSubmit(newCat);
                db.SubmitChanges();
                MessageBox.Show(itemname + " Added");
                textBox1.Clear();
                loadCategories();
            }
            
        }

        private void loadCategories()
        {
            //var categories = db.Categories.Where(x=>x.isActive).ToList();
            var categories = db.Categories.ToList();
            dataGridView1.DataSource = categories;
        }

        private void Technical_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'envanterDataSet.Category' table. You can move, or remove it, as needed.
            this.categoryTableAdapter.Fill(this.envanterDataSet.Category);
            loadCategories();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Satırın seçili olup olmadığını kontrol et
            if (e.RowIndex >= 0)
            {
                // Seçilen satırı al
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                int catID = (int)row.Cells[0].Value;

                string catName = row.Cells[1].Value.ToString();
                // Category ismini al
                bool activity = (bool)row.Cells[3].Value;

                var cat = db.Categories.FirstOrDefault(x => x.ID == catID);

                cat.isActive = !activity;
                db.SubmitChanges();
                // MessageBox ile göster
                string statusMessage = !activity ? "Activated" : "Deactivated";
                MessageBox.Show("Selected Category: " + catName + " is " + statusMessage);
                loadCategories();
            }
        }

    }
}
