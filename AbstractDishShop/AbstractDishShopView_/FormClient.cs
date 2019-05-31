using AbstractDishShopServiceDAL.ViewModel;
using AbstractDishShopServiceDAL.BindingModels;
using System;
using System.Windows.Forms;
using AbstractDishShopView;

namespace AbstractDishShopView
{
    public partial class FormClient : Form
    {
        public int Id { set { id = value; } }
        private int? id;

        public FormClient()
        {
            InitializeComponent();
        }
        private void FormClient_Load(object sender, EventArgs e)
        {
            if (id.HasValue)
            {
                try
                {
                    SClientViewModel view = APIClient.GetRequest<SClientViewModel>("api/SClient/Get/" + id.Value);
                    textBoxFIO.Text = view.SClientFIO;
                    if (view != null)
                    {
                        textBoxFIO.Text = view.SClientFIO;
                    }
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
                MessageBox.Show("Заполните ФИО", "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
                return;
            }
            try
            {
                if (id.HasValue)
                {
                    APIClient.PostRequest<SClientBindingModel,
                   bool>("api/SClient/UpdElement", new SClientBindingModel
                   {
                       Id = id.Value,
                       SClientFIO = textBoxFIO.Text
                   });
                }
                else
                {
                    APIClient.PostRequest<SClientBindingModel,
                   bool>("api/SClient/AddElement", new SClientBindingModel
                   {
                       SClientFIO = textBoxFIO.Text
                   });
                }
                MessageBox.Show("Сохранение прошло успешно", "Сообщение",
               MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK,
               MessageBoxIcon.Error);
            }
        }
        private void buttonCancel_Click(object sender, EventArgs e)

        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}