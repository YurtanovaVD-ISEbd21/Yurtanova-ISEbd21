using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;


namespace AbstractDishShopView
{
    public partial class FormDish : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        private List<DishMaterialsViewModel> DishMaterialss;
        public FormDish()
        {
            InitializeComponent();
        }
        private void FormDish_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    DishViewModel view = APIClient.GetRequest<DishViewModel>("api/Dish/Get/" + id.Value);
                    if (view != null)
                    {
                        textBoxName.Text = view.DishName;
                        textBoxPrice.Text = view.Price.ToString();
                        DishMaterialss = view.DishMaterials;
                        LoadData();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                DishMaterialss = new List<DishMaterialsViewModel>();
            }
        }
        private void LoadData()
        {
            try
            {
                if (DishMaterialss != null)
                {
                    dataGridView.DataSource = null;
                    dataGridView.DataSource = DishMaterialss;
                    dataGridView.Columns[0].Visible = false;
                    dataGridView.Columns[1].Visible = false;
                    dataGridView.Columns[2].Visible = false;
                    dataGridView.Columns[3].AutoSizeMode =
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
            var form = new FormDishMaterials();
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.Model != null)
                {
                    if (id.HasValue)
                    {
                        form.Model.DishId = id.Value;
                    }
                    DishMaterialss.Add(form.Model);
                }
                LoadData();
            }
        }
        private void buttonUpd_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                var form = new FormDishMaterials();
                form.Model = DishMaterialss[dataGridView.SelectedRows[0].Cells[0].RowIndex];
                if (form.ShowDialog() == DialogResult.OK)
                {
                    DishMaterialss[dataGridView.SelectedRows[0].Cells[0].RowIndex] = form.Model;
                    LoadData();
                }
            }
        }
        private void buttonDel_Click(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Удалить запись", "Вопрос", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DishMaterialss.RemoveAt(dataGridView.SelectedRows[0].Cells[0].RowIndex);
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
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxName.Text))
            {
                MessageBox.Show("Заполните название", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBoxPrice.Text))
            {
                MessageBox.Show("Заполните цену", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (DishMaterialss == null || DishMaterialss.Count == 0)
            {
                MessageBox.Show("Заполните материалы", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                List<DishMaterialsBindingModel> productComponentBM = new List<DishMaterialsBindingModel>();
                for (int i = 0; i < DishMaterialss.Count; ++i)
                {
                    productComponentBM.Add(new DishMaterialsBindingModel
                    {
                        Id = DishMaterialss[i].Id,
                        DishId = DishMaterialss[i].DishId,
                        MaterialsId = DishMaterialss[i].MaterialsId,
                        Count = DishMaterialss[i].Count
                    });
                }
                if (id.HasValue)
                {
                    APIClient.PostRequest<DishBindingModel, bool>("api/Dish/UpdElement", new DishBindingModel
                    {
                        Id = id.Value,
                        DishName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        DishMaterialss = productComponentBM
                    });
                }
                else
                {
                    APIClient.PostRequest<DishBindingModel, bool>("api/Dish/AddElement", new DishBindingModel
                    {
                        DishName = textBoxName.Text,
                        Price = Convert.ToInt32(textBoxPrice.Text),
                        DishMaterialss = productComponentBM
                    });
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