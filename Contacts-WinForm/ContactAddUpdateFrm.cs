using Contacts_BLL;
using Contacts_Contracts.DTOs;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Contacts_WinForm
{
    public partial class ContactAddUpdateFrm : Form
    {
        private enum Mode { Add=0, Update=1}

        private ContactBL _contact;
        private Mode _mode;

        public ContactAddUpdateFrm()
        {
            InitializeComponent();
            _mode = Mode.Add;
        }
        public ContactAddUpdateFrm(int contactId)
        {
            InitializeComponent();
            _contact = ContactBL.FindById(contactId);
            _mode = Mode.Update;
        }

        private void ContactAddUpdateFrm_Load(object sender, EventArgs e)
        {
            FillCountriesInComboBox();
            switch (_mode)
            {
                case Mode.Add:
                    LoadInAddMode();
                    break;

                case Mode.Update:
                    LoadInUpdateMode();
                    break;

                default:
                    LoadInAddMode();
                    break;
            }
        }
        private void LoadInAddMode()
        {
            lblMode.Text = "Add New Contact";
            lblRemoveImage.Visible = false;
            if(cbCountries.Items.Count > 0 )
            {
                cbCountries.SelectedIndex = 0;
            }
        }
        private void LoadInUpdateMode()
        {
            lblMode.Text = "Update Contact";

            if(_contact == null)
            {
                MessageBox.Show("Contact Not Found, The Form Will Close", "NotFound", 
                    MessageBoxButtons.OK, 
                    MessageBoxIcon.Error
                );
                this.Close();
            }

            lblRemoveImage.Visible = !string.IsNullOrWhiteSpace(_contact.contactDto.ImagePath);

            FillFormInfo();
        }

        private void FillCountriesInComboBox()
        {
            DataTable countries = CountryBL.GetAllCountries();

            foreach (DataRow row in countries.Rows)
            {
                cbCountries.Items.Add(row["CountryName"]);
            }
        
        }

        private void FillFormInfo()
        {
            lblId.Text = _contact.contactDto.ContactID.ToString();
            txtFirstName.Text = _contact.contactDto.FirstName.ToString();
            txtLastName.Text = _contact.contactDto.LastName.ToString();
            txtEmail.Text = _contact.contactDto.Email.ToString();
            txtPhone.Text = _contact.contactDto.Phone.ToString();
            txtAddress.Text = _contact.contactDto.Address.ToString();

            dtDOB.Value = _contact.contactDto.DateOfBirth;

            CountryBL country = CountryBL.FindById(_contact.contactDto.CountryId);

            if (cbCountries.Items.Count > 0 && country != null)
                cbCountries.SelectedIndex = cbCountries.FindString(country.countryDto.CountryName);

            if (!string.IsNullOrWhiteSpace(_contact.contactDto.ImagePath))
            {
                if(File.Exists(_contact.contactDto.ImagePath))
                    pbImage.Load(_contact.contactDto.ImagePath);
            }
        }

        private void FillContact()
        {
            ContactDto dto = new ContactDto();

            dto.FirstName = txtFirstName.Text;
            dto.LastName = txtLastName.Text;
            dto.Email = txtEmail.Text;
            dto.Phone = txtPhone.Text;
            dto.Address = txtAddress.Text;
            dto.DateOfBirth = dtDOB.Value;

            CountryBL country = CountryBL.FindByName(cbCountries.Text);
            dto.CountryId = country != null ? country.countryDto.CountryID : -1;

            if (pbImage.ImageLocation != null)
                dto.ImagePath = pbImage.ImageLocation.ToString();
            else
                dto.ImagePath = null;

            if (_contact == null)
            {
                _contact = new ContactBL(dto);
            }
            else
            {
                _contact.contactDto = dto;
            }
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            fdImageSet.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            fdImageSet.FilterIndex = 1;
            fdImageSet.RestoreDirectory = true;

            if (fdImageSet.ShowDialog() == DialogResult.OK)
            {
                string selectedFilePath = fdImageSet.FileName;

                pbImage.Load(selectedFilePath);

                lblRemoveImage.Visible = true;
            }
        }

        private void lblRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            pbImage.ImageLocation = null;
            pbImage.Image = null;
            lblRemoveImage.Visible = false;
        }

        private void btnSaveContact_Click(object sender, EventArgs e)
        {
            FillContact();

            if(_contact.Save())
            {
                MessageBox.Show("Done Successfully", "Success");

                this._mode = Mode.Update;
                lblId.Text = _contact.contactDto.ContactID.ToString();
                lblMode.Text = "Update Contact";
            }
            else
            {
                MessageBox.Show(
                    "Something Went wrong"
                    , "Failed"
                    ,MessageBoxButtons.OK
                    ,MessageBoxIcon.Error
                );
            }
            
        }
    }
}
