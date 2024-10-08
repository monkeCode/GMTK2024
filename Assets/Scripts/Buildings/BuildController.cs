﻿using DefaultNamespace;
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
                HousePrice = UpdatePrice(HousePrice);
            }
        }

        public void BuildFarm()
        {
            if(_resourceManager.TrySpendResources(FarmPrice))
            {
                GameManager.Instance.BuildMill();
                FarmPrice = UpdatePrice(FarmPrice);
            }
        }

        public void BuildTower()
        {
            if(_resourceManager.TrySpendResources(TowerPrice))
            {
                GameManager.Instance.BuildTower();
                TowerPrice = UpdatePrice(TowerPrice);
            }
        }

        private BuildingPrice UpdatePrice(BuildingPrice buildingPrice)
        {
            Debug.Log("build");
            return new BuildingPrice(
                buildingPrice.FoodPrice != 0 ? buildingPrice.FoodPrice + 2 : 0,
                buildingPrice.ArmyPrice != 0 ? buildingPrice.ArmyPrice + 2 : 0,
                buildingPrice.MoneyPrice != 0 ? buildingPrice.MoneyPrice + 2 : 0
            );
        }

        public void SwitchBubbles()
        {
            Flags.BubbleOffFlag = !Flags.BubbleOffFlag;
            onBubbleSwitch.Invoke();
        }

        public UnityEvent onBubbleSwitch;
    }
}