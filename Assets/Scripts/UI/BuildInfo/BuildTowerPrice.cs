namespace UI.BuildInfo
{
    public class BuildTowerPrice: BuildPriceBase
    {
        protected override void Start()
        {
            base.Start();
            SetPrice(_buildController.TowerPrice);
            _buildController.onTowerPriceChangedEvent.AddListener(HandleOnTowerPriceChanged);
        }

        private void HandleOnTowerPriceChanged()
        {
            SetPrice(_buildController.TowerPrice);
        }
    }
}