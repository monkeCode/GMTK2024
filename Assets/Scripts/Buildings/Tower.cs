namespace Buildings
{
    public class Tower: BuildingBase
    {
        protected override int Income => Constants.Buildings.TowerIncome;

        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            ResourceManager.AddArmy(Income);
        }
    }
}