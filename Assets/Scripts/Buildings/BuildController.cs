using GameResources;
using Models;
using UnityEngine;
using UnityEngine.Events;

namespace Buildings
{
    public class BuildController: MonoBehaviour
    {
        private BuildingPrice _housePrice = Constants.Buildings.BaseHousePrice;
        public BuildingPrice HousePrice
        {
            get => _housePrice;
            private set
            {
                _housePrice = value;
                onHousePriceChangedEvent.Invoke();
            }
        }
        
        private BuildingPrice _farmPrice = Constants.Buildings.BaseFarmPrice;
        public BuildingPrice FarmPrice
        {
            get => _farmPrice;
            private set
            {
                _farmPrice = value;
                onFarmPriceChangedEvent.Invoke();
            }
        }
        
        private BuildingPrice _towerPrice = Constants.Buildings.BaseTowerPrice;
        public BuildingPrice TowerPrice
        {
            get => _towerPrice;
            private set
            {
                _towerPrice = value;
                onTowerPriceChangedEvent.Invoke();
            }
        }

        public UnityEvent onHousePriceChangedEvent = new();
        public UnityEvent onFarmPriceChangedEvent = new();
        public UnityEvent onTowerPriceChangedEvent = new();
        
        private ResourceManager _resourceManager;

        private void Start()
        {
            _resourceManager = FindObjectOfType<ResourceManager>();
        }


        public void BuildHouse()
        {
            if(_resourceManager.TrySpendResources(HousePrice))
            {
                GameManager.Instance.BuildHouse();
                //TODO: Build
                HousePrice = UpdatePrice(HousePrice);
            }
        }

        public void BuildFarm()
        {
            if(_resourceManager.TrySpendResources(FarmPrice))
            {
                //TODO: Build
                FarmPrice = UpdatePrice(FarmPrice);
            }
        }

        public void BuildTower()
        {
            if(_resourceManager.TrySpendResources(TowerPrice))
            {
                //TODO: Build
                TowerPrice = UpdatePrice(TowerPrice);
            }
        }

        private BuildingPrice UpdatePrice(BuildingPrice buildingPrice)
        {
            Debug.Log("build");
            return new BuildingPrice(
                buildingPrice.FoodPrice != 0 ? buildingPrice.FoodPrice + 5 : 0, 
                buildingPrice.ArmyPrice != 0 ? buildingPrice.ArmyPrice + 5 : 0,
                buildingPrice.MoneyPrice != 0 ? buildingPrice.MoneyPrice + 5 : 0
            );
        }
    }
}