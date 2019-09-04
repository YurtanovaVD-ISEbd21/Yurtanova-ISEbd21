using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using AbstractDishShopView;

namespace AbstractDishShopView_
{
    public partial class FormPutOnStock : Form
    {
        public FormPutOnStock()
        {
            InitializeComponent();
        }

        private void FormPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                List<MaterialsViewModel> listC = APIClient.GetRequest<List<MaterialsViewModel>>("api/Materials/GetList");
                if (listC != null)
                {
                    comboBoxMaterials.DisplayMember = "MaterialsName";
                    comboBoxMaterials.ValueMember = "Id";
                    comboBoxMaterials.DataSource = listC;
                    comboBoxMaterials.SelectedItem = null;
                }
                List<StockViewModel> listS = APIClient.GetRequest<List<StockViewModel>>("api/Stock/GetList");
                if (listS != null)
                {
                    comboBoxStocks.DisplayMember = "SStockName";
                    comboBoxStocks.ValueMember = "Id";
                    comboBoxStocks.DataSource = listS;
                    comboBoxStocks.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxMaterials.SelectedValue == null)
            {
                MessageBox.Show("Выберите материалы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxMaterials.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<StockMaterialsBindingModel, bool>("api/SMain/PutMaterialsOnStock", new StockMaterialsBindingModel
                {
                    MaterialsId = Convert.ToInt32(comboBoxMaterials.SelectedValue),
                    StockId = Convert.ToInt32(comboBoxStocks.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text)
                });
                MessageBox.Show("Сохранение прошло успешно", "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

    }
}