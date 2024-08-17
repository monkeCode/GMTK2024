namespace UI.ResourcesInfo
{
    public class MoneyCounter: ResourceCounterBase
    {
        protected override void Start()
        {
            base.Start();
            resourceManager.onMoneyCountChanged.AddListener(HandleOnMoneyCountChanged);
        }

        private void HandleOnMoneyCountChanged()
        {
            SetCount(resourceManager.MoneyCount);
        }
    }
}