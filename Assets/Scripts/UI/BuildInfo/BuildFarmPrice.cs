using UnityEngine;

namespace UI.BuildInfo
{
    public class BuildFarmPrice: BuildPriceBase
    {
        protected override void Start()
        {
            base.Start();
            SetPrice(_buildController.FarmPrice);
            _buildController.onFarmPriceChangedEvent.AddListener(HandleOnFarmPriceChanged);
        }

        private void HandleOnFarmPriceChanged()
        {
            Debug.Log("here");
            SetPrice(_buildController.FarmPrice);
        }
    }
}