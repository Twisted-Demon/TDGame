namespace TDGame.Core;

public static class AuthoredSpawnWaves
{
    public static WavePlan FirstWave = new WavePlan
    {
        Name = "First Contact",
        
        Groups = 
        [
            new SpawnGroup()
            {
                EnemyId = "space_diver",
                Count = 32,
                SpawnInterval = 0.8f,
                
                Sector = new SpawnSector(
                    minimumRadius:18f,
                    maximumRadius:20f,
                    minimumAngleDegrees: 15f,
                    maximumAngleDegrees: 25f)
            }
        ]
    };

}