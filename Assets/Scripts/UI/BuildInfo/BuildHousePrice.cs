namespace UI.BuildInfo
{
    public class BuildHousePrice: BuildPriceBase
    {
        protected override void Start()
        {
            base.Start();
            SetPrice(_buildController.HousePrice);
            _buildController.onHousePriceChangedEvent.AddListener(HandleOnHousePriceChanged);
        }

        private void HandleOnHousePriceChanged()
        {
            SetPrice(_buildController.HousePrice);
        }
    }
}