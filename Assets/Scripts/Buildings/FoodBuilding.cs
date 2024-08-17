namespace Buildings
{
    public class FoodBuilding: BuildingBase
    {
        protected override int Income { get; set; } = 5;
        
        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            ResourceManager.AddFood(Income);
        }
    }
}