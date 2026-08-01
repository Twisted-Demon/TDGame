namespace TDGame.Core;

public static class AuthoredSpawnWaves
{
    public static readonly WavePlan FirstWave = new WavePlan
    {
        Name = "First Wave",
        
        Groups = 
        [
            new SpawnGroup()
            {
                EnemyId = "space_diver",
                Count = 16,
                SpawnInterval = 0.8f,
                
                Sector = new SpawnSector(
                    minimumRadius:18f,
                    maximumRadius:20f,
                    minimumAngleDegrees: 15f,
                    maximumAngleDegrees: 25f)
            },
            new SpawnGroup()
            {
                EnemyId = "space_diver",
                Count = 16,
                SpawnInterval = 0.8f,
                
                Sector = new SpawnSector(
                    minimumRadius:18f,
                    maximumRadius:20f,
                    minimumAngleDegrees: 180f,
                    maximumAngleDegrees: 190f)
            }
        ]
    };
    
    public static readonly WavePlan SecondWave = new WavePlan
    {
        Name = "First Wave",
        
        Groups = 
        [
            new SpawnGroup()
            {
                EnemyId = "space_diver",
                Count = 24,
                SpawnInterval = 0.6f,
                
                Sector = new SpawnSector(
                    minimumRadius:18f,
                    maximumRadius:20f,
                    minimumAngleDegrees: 85f,
                    maximumAngleDegrees: 95f)
            },
            new SpawnGroup()
            {
                EnemyId = "space_diver",
                Count = 24,
                SpawnInterval = 0.6f,
                
                Sector = new SpawnSector(
                    minimumRadius:18f,
                    maximumRadius:20f,
                    minimumAngleDegrees: 265f,
                    maximumAngleDegrees: 275f)
            }
        ]
    };
}