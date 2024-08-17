using System;
using UnityEngine;

namespace Resources
{
    public class ResourceManager: MonoBehaviour
    {
        public int MoneyCount { get; private set; }
        public int FoodCount { get; private set; }
        public int ArmyCount { get; private set; }

        public void AddMoney(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Value can't be < 0");
            }
            
            MoneyCount += count;
        }

        public bool TrySpendMoney(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Value can't be < 0");
            }

            if (MoneyCount < count)
            {
                return false;
            }

            MoneyCount -= count;
            return true;
        }

        public void AddFood(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Value can't be < 0");
            }
            
            FoodCount += count;
        }
        
        public bool TrySpendFood(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Value can't be < 0");
            }

            if (FoodCount < count)
            {
                return false;
            }

            FoodCount -= count;
            return true;
        }

        public void AddArmy(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Value can't be < 0");
            }
            
            ArmyCount += count;
        }
        
        public bool TrySpendArmy(int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, "Value can't be < 0");
            }

            if (ArmyCount < count)
            {
                return false;
            }

            ArmyCount -= count;
            return true;
        }
    }
}