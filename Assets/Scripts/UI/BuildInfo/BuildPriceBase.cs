using Buildings;
using JetBrains.Annotations;
using Models;
using TMPro;
using UnityEngine;

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
                moneyPrice.text = price.MoneyPrice.ToString();
            }

            if (foodPrice != null)
            {
                foodPrice.text = price.FoodPrice.ToString();
            }

            if (armyPrice != null)
            {
                armyPrice.text = price.ArmyPrice.ToString();
            }
        }
    }
}