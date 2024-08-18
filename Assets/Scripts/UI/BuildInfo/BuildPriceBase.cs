using Buildings;
using DefaultNamespace;
using Events;
using JetBrains.Annotations;
using Models;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace UI.BuildInfo
{
    public abstract class BuildPriceBase: MonoBehaviour
    {
        [SerializeField] protected BuildController _buildController;
        [SerializeField] [CanBeNull] private TextMeshProUGUI moneyPrice;
        [SerializeField] [CanBeNull] private TextMeshProUGUI foodPrice;
        [SerializeField] [CanBeNull] private TextMeshProUGUI armyPrice;

        protected virtual void Start()
        {
            _buildController = FindObjectOfType<BuildController>();
        }

        protected void SetPrice(BuildingPrice price)
        {
            if (moneyPrice != null)
            {
                moneyPrice.text = ToString(price.MoneyPrice);
            }

            if (foodPrice != null)
            {
                foodPrice.text = ToString(price.FoodPrice);
            }

            if (armyPrice != null)
            {
                armyPrice.text = ToString(price.ArmyPrice);
            }
        }

        private string ToString(int price)
        {
            return Flags.DigitalRomanReformFlag
                ? DigitalRomanReformEvent.ToRoman(price)
                : price.ToString();
        }
    }
}