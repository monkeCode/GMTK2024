﻿namespace UI.ResourcesInfo
{
    public class ArmyCounter: ResourceCounterBase
    {
        protected override void Start()
        {
            base.Start();
            resourceManager.onArmyCountChanged.AddListener(HandleOnArmyCountChanged);
        }

        private void HandleOnArmyCountChanged()
        {
            SetCount(resourceManager.ArmyCount);
        }
    }
}