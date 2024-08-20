using UnityEngine;

namespace Buildings
{
    public class Farm: BuildingBase
    {
        [SerializeField] private SpriteRenderer _krutilka;
        protected override int Income => Constants.Buildings.FarmIncome;
        
        protected override void OnMouseEnter()
        {
            base.OnMouseEnter();
            ResourceManager.AddFood(Income);
        }

    }
}