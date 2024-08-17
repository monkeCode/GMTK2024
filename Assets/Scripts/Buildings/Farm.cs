namespace Buildings
{
    public class Farm: BuildingBase
    {
        protected override int Income { get; set; } = Constants.Buildings.FarmIncome;
        
        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            ResourceManager.AddFood(Income);
        }
    }
}