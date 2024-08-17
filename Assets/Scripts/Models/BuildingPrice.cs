namespace Models
{
    public class BuildingPrice
    {
        public BuildingPrice(int foodPrice, int armyPrice, int moneyPrice)
        {
            FoodPrice = foodPrice;
            ArmyPrice = armyPrice;
            MoneyPrice = moneyPrice;
        }

        public int MoneyPrice { get; }
        public int FoodPrice { get; }
        public int ArmyPrice { get; }
    }
}