namespace Buildings
{
    public class MoneyBuilding: BuildingBase
    {
        protected override int Income { get; set; } = 5;
        
        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            ResourceManager.AddMoney(Income);
        }
    }
}