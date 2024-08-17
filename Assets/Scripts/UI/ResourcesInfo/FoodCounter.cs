namespace UI.ResourcesInfo
{
    public class FoodCounter: ResourceCounterBase
    {
        protected override string Template { get; set; } = "Food: ";

        protected override void Start()
        {
            base.Start();
            resourceManager.onFoodCountChanged.AddListener(HandleOnFoodCountChanged);
        }

        private void HandleOnFoodCountChanged()
        {
            SetCount(resourceManager.FoodCount);
        }
    }
}