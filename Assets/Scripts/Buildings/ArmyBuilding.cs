namespace Buildings
{
    public class ArmyBuilding: BuildingBase
    {
        protected override int Income { get; set; } = 5;

        protected override void OnMouseDown()
        {
            base.OnMouseDown();
            ResourceManager.AddArmy(Income);
        }
    }
}