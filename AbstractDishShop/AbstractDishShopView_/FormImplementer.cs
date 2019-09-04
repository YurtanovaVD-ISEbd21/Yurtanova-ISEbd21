using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;
using AbstractDishShopView;
using System;
using System.Windows.Forms;

namespace AbstractDishShopView_
{
    public partial class FormImplementer : Form
    {
        public int Id { set { id = value; } }
        private int? id;
        public FormImplementer()
        {
            InitializeComponent();
        }
        private void FormImplementer_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    ImplementerViewModel view = APIClient.GetRequest<ImplementerViewModel>("api/Implementer/Get/" + id.Value);
                    textBoxFIO.Text = view.ImplementerName;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxFIO.Text))
            {
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<ImplementerBindingModel, bool>("api/Implementer/UpdElement", new ImplementerBindingModel
                    {
                        Id = id.Value,
                        ImplementerName = textBoxFIO.Text
                    });
                }
                else
                {
                    APIClient.PostRequest<ImplementerBindingModel, bool>("api/Implementer/AddElement", new ImplementerBindingModel
                    {
                        ImplementerName = textBoxFIO.Text
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