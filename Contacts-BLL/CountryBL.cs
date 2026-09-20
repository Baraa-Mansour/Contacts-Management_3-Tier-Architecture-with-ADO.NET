using Contacts_Contracts.DTOs;
using Contacts_DAL;
using System.Data;

namespace Contacts_BLL
{
    public class CountryBL
    {
        private enum Mode { ADD = 0, UPDATE = 1 }

        public CountryDto countryDto { set; get; }
        private Mode _mode;
        private int _updateId; // This use to prevent change Id in Presentation Layer

        private CountryBL(CountryDto countryDto, Mode mode, int updateId)
        {
            this.countryDto = countryDto;
            this._mode = mode;
            this._updateId = updateId;
        }

        public CountryBL(CountryDto countryDto)
        {
            this.countryDto = countryDto;
            this._mode = Mode.ADD;
            this._updateId = -1;
        }

        public static CountryBL FindById(int CountryId)
        {
            CountryDto countryDto = CountryDA.FindById(CountryId);

            if (countryDto != null)
                return new CountryBL(countryDto, Mode.UPDATE, countryDto.CountryID);

            return null;
        }

        private bool _Add()
        {
            this.countryDto.CountryID = CountryDA.Add(this.countryDto);

            return (this.countryDto.CountryID != -1);
        }

        private bool _Update()
        {
            this.countryDto.CountryID = this._updateId;

            return CountryDA.Update(this.countryDto);
        }

        public static bool Delete(int countryId)
        {
            return CountryDA.Delete(countryId);
        }

        public bool Save()
        {
            switch (this._mode)
            {
                case Mode.ADD:
                    if (this._Add())
                    {
                        this._mode = Mode.UPDATE;
                        this._updateId = this.countryDto.CountryID;

                        return true;
                    }
                    return false;

                case Mode.UPDATE:
                    return this._Update();

                default:
                    return false;
            }
        }

        public static DataTable GetAllCountries()
        {
            return CountryDA.GetAllCountries();
        }

        public static bool IsExist(int countryId)
        {
            return CountryDA.IsExist(countryId);
        }
    }
}