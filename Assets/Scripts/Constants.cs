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
        public const int DefaultIncome = 5;
        public static int HouseIncome { get; set; } = DefaultIncome;
        public static int FarmIncome { get; set; } = DefaultIncome;
        public static int TowerIncome { get; set; } = DefaultIncome;
        
        public static readonly BuildingPrice BaseHousePrice = new(5, 5, 0);
        public static readonly BuildingPrice BaseFarmPrice = new(0, 5, 5);
        public static readonly BuildingPrice BaseTowerPrice = new(5, 0, 5);
    }
}