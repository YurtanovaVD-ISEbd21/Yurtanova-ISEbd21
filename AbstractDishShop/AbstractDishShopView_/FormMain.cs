using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using AbstractDishShopView_;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace AbstractDishShopView
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                List<SOrderViewModel> list = APIClient.GetRequest<List<SOrderViewModel>>("api/SMain/GetList");
                if (list != null)
                {
                    dataGridView.DataSource = list;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[3].Visible = false;
                    dataGridView.Columns[5].Visible = false;
                    dataGridView.Columns[1].AutoSizeMode =
                    DataGridViewAutoSizeColumnMode.Fill;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void клиентыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormClients();
            form.ShowDialog();
        }
        private void материалыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormMaterials();
            form.ShowDialog();
        }
        private void БлюдаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormDishs();
            form.ShowDialog();
        }
        private void складыToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormStocks();
            form.ShowDialog();
        }
        private void пополнитьскладToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormPutOnStock();
            form.ShowDialog();
        }

        private void buttonCreateOrder_Click(object sender, EventArgs e)
        {
            var form = new FormCreateOrder();
            form.ShowDialog();
            LoadData();
        }
        private void buttonTakeOrderInWork_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    APIClient.PostRequest<SOrderBindingModel, bool>("api/SMain/TakeSOrderInWork", new SOrderBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonOrderReady_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    APIClient.PostRequest<SOrderBindingModel, bool>("api/SMain/FinishSOrder", new SOrderBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonPayOrder_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                int id = Convert.ToInt32(dataGridView.SelectedRows[0].Cells[0].Value);
                try
                {
                    APIClient.PostRequest<SOrderBindingModel, bool>("api/SMain/PaySOrder", new SOrderBindingModel { Id = id });
                    LoadData();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
                   MessageBoxIcon.Error);
                }
            }
        }
        private void buttonRef_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void прайсподарковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "doc|*.doc|docx|*.docx"
            };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    APIClient.PostRequest<ReportBindingModel, bool>("api/SReport/SaveDishPrice", new ReportBindingModel
                    {
                        FileName = sfd.FileName
                    });
                    MessageBox.Show("Выполнено", "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void загруженностьскладовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormStocksLoad();
            form.ShowDialog();
        }
        private void заказыклиентовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormClientOrders();
            form.ShowDialog();
        }


    }
}