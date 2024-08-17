using GameResources;
using Models;
using UnityEngine;

namespace Buildings
{
    public class BuildController: MonoBehaviour
    {
        [SerializeField]private BuildingPrice _housePrice = new(5, 5, 0);
        [SerializeField]private BuildingPrice _farmPrice = new(0, 5, 5);
        [SerializeField]private BuildingPrice _towerPrice = new(5, 0, 5);
        private ResourceManager _resourceManager;

        private void Start()
        {
            _resourceManager = FindObjectOfType<ResourceManager>();
        }


        public void BuildHouse()
        {
            if(_resourceManager.TrySpendResources(_housePrice))
            {
                GameManager.Instance.BuildHouse();
                //TODO: Build
                //TODO: Update price
            }
        }

        public void BuildFarm()
        {
            if(_resourceManager.TrySpendResources(_farmPrice))
            {
                //TODO: Build
                //TODO: Update price
            }
        }

        public void BuildTower()
        {
            if(_resourceManager.TrySpendResources(_towerPrice))
            {
                //TODO: Build
                //TODO: Update price
            }
        }
    }
}