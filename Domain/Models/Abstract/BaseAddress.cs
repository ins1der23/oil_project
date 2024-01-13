namespace Models
{
    public abstract class BaseAddress : BaseElement<BaseAddress>
    {
        private int roleId;
        private int cityId;
        private City city;
        private int streetId;
        private Street street;
        private string houseNum;
        public int RoleId { get => roleId; set => roleId = value; }
        public int CityId { get => cityId; set => cityId = value; }
        public virtual City City { get => city; set => city = value; }
        public int StreetId { get => streetId; set => streetId = value; }
        public virtual Street Street { get => street; set => street = value; }
        public string HouseNum { get => houseNum; set => houseNum = value; }

        protected BaseAddress()
        {
            roleId = 1;
            city = new();
            street = new();
            houseNum = string.Empty;
        }
        public abstract string ShortString();


    }
}
