using Contacts_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Contacts_WinForm
{
    public partial class ContactsFrm : Form
    {
        public ContactsFrm()
        {
            InitializeComponent();
        }

        private int GetCurrentRowId()
        {
            return Convert.ToInt32(gvContacts.CurrentRow.Cells[0].Value);
        }

        private void ContactsFrm_Load(object sender, EventArgs e)
        {
            gvContacts.DataSource = ContactBL.GetAllContacts();
        }

        private void tsmiUpdate_Click(object sender, EventArgs e)
        {
            ContactAddUpdateFrm frm = new ContactAddUpdateFrm(GetCurrentRowId());
            frm.ShowDialog();
            gvContacts.DataSource = ContactBL.GetAllContacts();
        }

        private void gvContacts_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                gvContacts.ClearSelection();
                gvContacts.Rows[e.RowIndex].Selected = true;
                gvContacts.CurrentCell = gvContacts.Rows[e.RowIndex].Cells[e.ColumnIndex];
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            bool IsConfirmed = MessageBox.Show(
                "Delete Contact ?",
                "Confirm Delete",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            ) == DialogResult.OK;

            if ( IsConfirmed )
            { 
                int contactId = GetCurrentRowId();
                
                if(ContactBL.Delete(contactId))
                {
                    MessageBox.Show("Deleted Successfully", "Success");
                    gvContacts.DataSource = ContactBL.GetAllContacts();
                }
            }
        }

        private void btnAddContact_Click(object sender, EventArgs e)
        {
            ContactAddUpdateFrm frm = new ContactAddUpdateFrm();
            frm.ShowDialog();
            gvContacts.DataSource = ContactBL.GetAllContacts();
        }
    }
}
