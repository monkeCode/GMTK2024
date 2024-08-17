namespace Buildings
{
    public class House: BuildingBase
    {
        protected override int Income { get; set; } = Constants.Buildings.HouseIncome;
        
        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            ResourceManager.AddMoney(Income);
        }
    }
}