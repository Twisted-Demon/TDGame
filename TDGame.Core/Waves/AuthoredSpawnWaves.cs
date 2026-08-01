namespace TDGame.Core;

public static class AuthoredSpawnWaves
{
    public static readonly WavePlan FirstWave = new WavePlan
    {
        Name = "First Contact",
        IntermissionAfterWave = 1f,
        Groups = 
        [
            new SpawnGroup()
            {
                EnemyId = "space_diver",
                Count = 24,
                SpawnInterval = 0.75f,
                
                Sector = SpawnSector.EastSector
            }
        ]
    };
    
    public static readonly WavePlan SecondWave = new WavePlan
    {
        Name = "Opposite Side",
        IntermissionAfterWave = 1f,
        Groups = 
        [
            new SpawnGroup()
            {
                EnemyId = "space_diver",
                Count = 24,
                SpawnInterval = 0.5f,
                
                Sector = SpawnSector.WestSector
            }
        ]
    };

    public static readonly WavePlan ThirdWave = new WavePlan
    {
        Name = "Two Fronts",
        IntermissionAfterWave = 1f,
        Groups =
        [
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 32,
                SpawnInterval = 0.75F,

                Sector = SpawnSector.WestSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 32,
                SpawnInterval = 0.75F,

                Sector = SpawnSector.EastSector
            }
        ]
    };
    
    public static readonly WavePlan FourthWave = new WavePlan
    {
        Name = "Three Fronts",
        IntermissionAfterWave = 1f,
        Groups =
        [
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 42,
                SpawnInterval = 0.75F,

                Sector = SpawnSector.WestSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 42,
                SpawnInterval = 0.75F,

                Sector = SpawnSector.EastSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 42,
                SpawnInterval = 0.75F,
                DelayBeforeGroup = 7.5f,

                Sector = SpawnSector.NorthSector
            }
        ]
    };
}