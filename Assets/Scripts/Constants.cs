using Models;

public static class Constants
{
    public static class SceneNames
    {
        public const string MainMenu = "MainMenu";
        public const string Game = "Game";
    }

    public static class Buildings
    {
        public const int HouseIncome = 5;
        public static readonly BuildingPrice BaseHousePrice = new(5, 5, 0);
        public const int FarmIncome = 5;
        public static readonly BuildingPrice BaseFarmPrice = new(0, 5, 5);
        public const int TowerIncome = 5;
        public static readonly BuildingPrice BaseTowerPrice = new(5, 0, 5);
    }
}