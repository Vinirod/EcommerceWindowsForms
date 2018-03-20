using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LayerBusiness;

namespace LayerPresentation
{
    public partial class fmrCategory : Form
    {
        private bool isNew = false;
        private bool isEdit = false;

        public fmrCategory()
        {
            InitializeComponent();
            this.ttMessage.SetToolTip(this.txtName, "Insert name category");
            this.ttMessage.SetToolTip(this.txtDescription, "Insert Description"); this.ttMessage.SetToolTip(this.txtName, "Insert name category");
        }

        private void MessageOk(string message)
        {
            MessageBox.Show(message, "System Ecommerce", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void MessageError(string message)
        {
            MessageBox.Show(message, "System Ecommerce", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void Clear()
        {
            this.txtName.Text = string.Empty;
            this.txtIdCategory.Text = string.Empty;
            this.txtDescription.Text = string.Empty;
        }

        private void Enable(bool value)
        {
            this.txtName.ReadOnly = !value;
            this.txtDescription.ReadOnly = !value;
            this.txtIdCategory.ReadOnly = !value;
        }

        private void ButtonsEnable()
        {
            if(this.isNew || this.isEdit)
            {
                this.Enable(true);
                this.btnNew.Enabled = false;
                this.btnSave.Enabled = true;
                this.btnEdit.Enabled = false;
                this.btnCancel.Enabled = true;

            }
            else
            {
                this.Enable(false);
                this.btnNew.Enabled = true;
                this.btnSave.Enabled = false;
                this.btnEdit.Enabled = true;
                this.btnCancel.Enabled = false;
            }
        }


        private void HideColumns()
        {
            this.dataList.Columns[0].Visible = false;
        }

        private void ShowData()
        {
            this.dataList.DataSource = NCategory.Show();
            this.HideColumns();
            lblTotal.Text = "Total records: "+ Convert.ToString(dataList.Rows.Count);
        }

        private void SearchName()
        {
            this.dataList.DataSource = NCategory.SearchName(this.txtSearch.Text);
            this.HideColumns();
            lblTotal.Text = "Total records: " + Convert.ToString(dataList.Rows.Count);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SearchName();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.SearchName();
        }

        private void fmrCategory_Load(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = 0; ;
            this.ShowData();
            this.Enable(false);
            this.ButtonsEnable();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            this.isNew = true;
            this.isEdit = false;
            this.ButtonsEnable();
            this.Enable(true);
            this.txtName.Focus();
            this.txtIdCategory.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string resp = "";
                if(this.txtName.Text == string.Empty)
                {
                    MessageError("Fill in all the fields");
                    errorIcon.SetError(txtName, "Insert name");
                }
                else
                {
                    if (this.isNew)
                    {
                        resp = NCategory.Insert(txtName.Text.Trim().ToUpper(), txtDescription.Text.Trim());
                    }
                    else if(this.isEdit)
                    {
                        resp = NCategory.Edit(Convert.ToInt32(this.txtIdCategory.Text), txtName.Text.Trim().ToUpper(), txtDescription.Text.Trim());
                    }
                    if (resp.Equals("OK"))
                    {
                        if (this.isNew)
                        {
                            this.MessageOk("Registration successfully saved");
                        }
                        else if(this.isEdit)
                        {
                            this.MessageOk("Registration successfully deleted");
                        }
                    }
                    else
                    {
                        this.MessageError(resp);
                    }
                    this.isNew = false;
                    this.isEdit = false;
                    this.ButtonsEnable();
                    this.Clear();
                    this.Show();

                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void dataList_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdCategory.Text = Convert.ToString(this.dataList.CurrentRow.Cells["IdCategory"].Value);
            this.txtName.Text = Convert.ToString(this.dataList.CurrentRow.Cells["Name"].Value);
            this.txtDescription.Text = Convert.ToString(this.dataList.CurrentRow.Cells["Description"].Value);
            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.txtIdCategory.Text.Equals(""))
            {
                this.MessageError("Select a register for insert");
            }
            else
            {
                this.isEdit = true;
                this.ButtonsEnable();
                this.Enable(true);
            }
        }
    }
}
