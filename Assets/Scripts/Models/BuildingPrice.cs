using System;
using UnityEngine;

namespace Models
{
    [Serializable]
    public struct BuildingPrice
    {
        public BuildingPrice(int foodPrice, int armyPrice, int moneyPrice)
        {
            _foodPrice = foodPrice;
            _armyPrice = armyPrice;
            _moneyPrice = moneyPrice;
        }

        [SerializeField] private int _moneyPrice;
        [SerializeField] private int _foodPrice;
        [SerializeField] private int _armyPrice;

        public int MoneyPrice => _moneyPrice;
        public int FoodPrice => _foodPrice;
        public int ArmyPrice => _armyPrice;
    }
}