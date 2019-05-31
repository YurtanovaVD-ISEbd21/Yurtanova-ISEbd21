using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace AbstractDishShopView
{
    public partial class FormClients : Form
    {
        public FormClients()
        {
            InitializeComponent();
        }
        private void FormClients_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void LoadData()
        {
            try
            {
                List<SClientViewModel> list = APIClient.GetRequest<List<SClientViewModel>>("api/SClient/GetList");
                if (list != null)
                {
                    dataGridViewClients.DataSource = list;
                    dataGridViewClients.Columns[0].Visible = false;
                    dataGridViewClients.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void buttonAdd_Click(object sender, EventArgs e)
        {
            var form = new FormClient();
            if (form.ShowDialog() == DialogResult.OK)
            {
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 1)
            {
                var form = new FormClient();
                form.Id = Convert.ToInt32(dataGridViewClients.SelectedRows[0].Cells[0].Value);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridViewClients.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(dataGridViewClients.SelectedRows[0].Cells[0].Value);
                    try
                    {
                        APIClient.PostRequest<SClientBindingModel, bool>("api/SClient/DelElement", new SClientBindingModel { Id = id });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    LoadData();
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}