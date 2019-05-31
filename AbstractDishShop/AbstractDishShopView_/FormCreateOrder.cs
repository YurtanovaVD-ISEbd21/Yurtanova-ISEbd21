using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace AbstractDishShopView
{
    public partial class FormCreateOrder : Form
    {
        public FormCreateOrder()
        {
            InitializeComponent();
        }
        private void FormCreateOrder_Load(object sender, EventArgs e)
        {
            try
            {
                List<SClientViewModel> listC = APIClient.GetRequest<List<SClientViewModel>>("api/SClient/GetList");
                if (listC != null)
                {
                    comboBoxClient.DisplayMember = "SClientFIO";
                    comboBoxClient.ValueMember = "Id";
                    comboBoxClient.DataSource = listC;
                    comboBoxClient.SelectedItem = null;
                }
                List<DishViewModel> listP = APIClient.GetRequest<List<DishViewModel>>("api/Dish/GetList");
                if (listP != null)
                {
                    comboBoxDish.DisplayMember = "DishName";
                    comboBoxDish.ValueMember = "Id";
                    comboBoxDish.DataSource = listP;
                    comboBoxDish.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void CalcSum()
        {
            if (comboBoxDish.SelectedValue != null && !string.IsNullOrEmpty(textBoxCount.Text))
            {
                try
                {
                    int id = Convert.ToInt32(comboBoxDish.SelectedValue);
                    DishViewModel item = APIClient.GetRequest<DishViewModel>("api/Dish/Get/" + id);
                    int count = Convert.ToInt32(textBoxCount.Text);
                    textBoxSum.Text = (count * item.Price).ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void textBoxCount_CountChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void comboBoxDish_SumChanged(object sender, EventArgs e)
        {
            CalcSum();
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxClient.SelectedValue == null)
            {
                MessageBox.Show("Выберите клиента", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxDish.SelectedValue == null)
            {
                MessageBox.Show("Выберите Блюдо", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                APIClient.PostRequest<SOrderBindingModel, bool>("api/SMain/CreateSOrder", new SOrderBindingModel
                {
                    SClientId = Convert.ToInt32(comboBoxClient.SelectedValue),
                    DishId = Convert.ToInt32(comboBoxDish.SelectedValue),
                    Count = Convert.ToInt32(textBoxCount.Text),
                    Sum = Convert.ToDecimal(textBoxSum.Text)
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

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}