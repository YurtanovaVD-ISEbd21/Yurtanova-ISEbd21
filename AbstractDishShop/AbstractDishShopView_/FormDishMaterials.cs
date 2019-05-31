using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AbstractDishShopView
{
    public partial class FormDishMaterials : Form
    {
        public DishMaterialsViewModel Model
        {
            set { model = value; }
            get { return model; }
        }
        private DishMaterialsViewModel model;
        public FormDishMaterials()
        {
            InitializeComponent();
        }
        private void FormDishMaterials_Load(object sender, EventArgs e)
        {
            try
            {
                List<MaterialsViewModel> list = APIClient.GetRequest<List<MaterialsViewModel>>("api/Materials/GetList");
                if (list != null)
                {
                    comboBoxDishMaterials.DisplayMember = "MaterialsName";
                    comboBoxDishMaterials.ValueMember = "Id";
                    comboBoxDishMaterials.DataSource = list;
                    comboBoxDishMaterials.SelectedItem = null;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (model != null)
            {
                comboBoxDishMaterials.Enabled = false;
                comboBoxDishMaterials.SelectedValue = model.MaterialsId;
                textBoxCount.Text = model.Count.ToString();
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxCount.Text))
            {
                MessageBox.Show("Заполните поле Количество", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxDishMaterials.SelectedValue == null)
            {
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (model == null)
                {
                    model = new DishMaterialsViewModel
                    {
                        MaterialsId = Convert.ToInt32(comboBoxDishMaterials.SelectedValue),
                        MaterialsName = comboBoxDishMaterials.Text,
                        Count = Convert.ToInt32(textBoxCount.Text)
                    };
                }
                else
                {
                    model.Count = Convert.ToInt32(textBoxCount.Text);
                }
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