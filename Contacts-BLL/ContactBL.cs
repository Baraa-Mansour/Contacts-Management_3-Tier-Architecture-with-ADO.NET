using Contacts_Contracts.DTOs;
using Contacts_DAL;


namespace Contacts_BLL
{
    public class ContactBL
    {
        private enum Mode { ADD=0, UPDATE=1}

        public ContactDto contactDto { set; get; }
        private Mode _mode; 

        private ContactBL(ContactDto contactDto, Mode mode)
        {
            this.contactDto = contactDto;
            this._mode = mode;
        }
        public ContactBL(ContactDto contactDto)
        {
            this.contactDto = contactDto;
            this._mode = Mode.ADD;
        }

        public static ContactBL FindById(int ContactId)
        {
            ContactDto contactDto = ContactDA.FindById(ContactId);

            if (contactDto != null)
                return new ContactBL(contactDto, Mode.UPDATE);

            return null;
        }
    }
}
