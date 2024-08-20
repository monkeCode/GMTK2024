namespace Buildings
{
    public class House: BuildingBase
    {
        protected override int Income => Constants.Buildings.HouseIncome;
        
        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            ResourceManager.AddMoney(Income);
        }
    }
}