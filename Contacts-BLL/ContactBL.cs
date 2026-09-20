using Contacts_Contracts.DTOs;
using Contacts_DAL;


namespace Contacts_BLL
{
    public class ContactBL
    {
        private enum Mode { ADD=0, UPDATE=1}

        public ContactDto contactDto { set; get; }
        private Mode _mode;
        private int _updateId;

        private ContactBL(ContactDto contactDto, Mode mode, int updateId)
        {
            this.contactDto = contactDto;
            this._mode = mode;
            this._updateId = updateId;
        }
        public ContactBL(ContactDto contactDto)
        {
            this.contactDto = contactDto;
            this._mode = Mode.ADD;
            this._updateId = -1;
        }

        public static ContactBL FindById(int ContactId)
        {
            ContactDto contactDto = ContactDA.FindById(ContactId);

            if (contactDto != null)
                return new ContactBL(contactDto, Mode.UPDATE, contactDto.ContactID);

            return null;
        }

        private bool _Add()
        {
            this.contactDto.ContactID = ContactDA.Add(this.contactDto);

            return (this.contactDto.ContactID != -1);
        }

        public bool Save()
        {
            switch(this._mode)
            {
                case Mode.ADD:
                    if(this._Add())
                    {
                        this._mode = Mode.UPDATE;
                        this._updateId = this.contactDto.ContactID;

                        return true;
                    }
                    return false;

                case Mode.UPDATE:

                default:
                    return false;
            }
        }
    }
}
