using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Unity;
using AbstractDishShopServiceDAL.Interfaces;
using AbstractDishShopServiceDAL.BindingModels;
using AbstractDishShopServiceDAL.ViewModel;

namespace AbstractDishShopView_
{
    public partial class FormPutOnStock : Form
    {
        [Dependency]
        public new IUnityContainer Container { get; set; }
        private readonly IStockService serviceS;
        private readonly IMaterialsService serviceC;
        private readonly IMainService serviceM;
        public FormPutOnStock(IStockService serviceS, IMaterialsService serviceC, IMainService serviceM)
        {
            InitializeComponent();
            this.serviceS = serviceS;
            this.serviceC = serviceC;
            this.serviceM = serviceM;
        }
        private void FormPutOnStock_Load(object sender, EventArgs e)
        {
            try
            {
                List<MaterialsViewModel> listC = serviceC.GetList();
                if (listC != null)
                {
                    comboBoxMaterials.DisplayMember = "MaterialsName";
                    comboBoxMaterials.ValueMember = "Id";
                    comboBoxMaterials.DataSource = listC;
                    comboBoxMaterials.SelectedItem = null;
                }
                List<StockViewModel> listS = serviceS.GetList();
                if (listS != null)
                {
                    
                comboBoxStock.DisplayMember = "StockName";
                    comboBoxStock.ValueMember = "Id";
                    comboBoxStock.DataSource = listS;
                    comboBoxStock.SelectedItem = null;
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
                MessageBox.Show("Выберите компонент", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (comboBoxStock.SelectedValue == null)
            {
                MessageBox.Show("Выберите склад", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                serviceM.PutMaterialsOnStock(new StockMaterialsBindingModel
                {
                    MaterialsId = Convert.ToInt32(comboBoxMaterials.SelectedValue),
                    StockId = Convert.ToInt32(comboBoxStock.SelectedValue),
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