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

    public static readonly WavePlan FifthWave = new WavePlan
    {
        Name = "Southern Approach",
        IntermissionAfterWave = 1.5f,
        Groups =
        [
            // Future enemy introduction: a fast scout would fit here once the
            // player understands the standard diver and all four directions.
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 56,
                SpawnInterval = 0.55f,

                Sector = SpawnSector.SouthSector
            }
        ]
    };

    public static readonly WavePlan SixthWave = new WavePlan
    {
        Name = "Four Corners",
        IntermissionAfterWave = 1.5f,
        Groups =
        [
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 30,
                SpawnInterval = 0.55f,

                Sector = SpawnSector.EastSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 30,
                SpawnInterval = 0.55f,

                Sector = SpawnSector.WestSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 30,
                SpawnInterval = 0.55f,
                DelayBeforeGroup = 5f,

                Sector = SpawnSector.NorthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 30,
                SpawnInterval = 0.55f,
                DelayBeforeGroup = 5f,

                Sector = SpawnSector.SouthSector
            }
        ]
    };

    public static readonly WavePlan SeventhWave = new WavePlan
    {
        Name = "Clockwise Pressure",
        IntermissionAfterWave = 1.5f,
        Groups =
        [
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 36,
                SpawnInterval = 0.4f,

                Sector = SpawnSector.NorthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 36,
                SpawnInterval = 0.4f,
                DelayBeforeGroup = 4f,

                Sector = SpawnSector.EastSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 36,
                SpawnInterval = 0.4f,
                DelayBeforeGroup = 8f,

                Sector = SpawnSector.SouthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 36,
                SpawnInterval = 0.4f,
                DelayBeforeGroup = 12f,

                Sector = SpawnSector.WestSector
            }
        ]
    };

    public static readonly WavePlan EighthWave = new WavePlan
    {
        Name = "False Opening",
        IntermissionAfterWave = 2f,
        Groups =
        [
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 18,
                SpawnInterval = 0.25f,

                Sector = SpawnSector.EastSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 18,
                SpawnInterval = 0.25f,

                Sector = SpawnSector.WestSector
            },
            // Future enemy introduction: an armored enemy belongs in this
            // delayed main force, after the opening groups draw tower focus.
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 56,
                SpawnInterval = 0.45f,
                DelayBeforeGroup = 5f,

                Sector = SpawnSector.NorthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 56,
                SpawnInterval = 0.45f,
                DelayBeforeGroup = 5f,

                Sector = SpawnSector.SouthSector
            }
        ]
    };

    public static readonly WavePlan NinthWave = new WavePlan
    {
        Name = "No Safe Side",
        IntermissionAfterWave = 2f,
        Groups =
        [
            // Future enemy introduction: mix a support or disruption enemy
            // into each sector here to teach target prioritization.
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 48,
                SpawnInterval = 0.35f,

                Sector = SpawnSector.NorthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 48,
                SpawnInterval = 0.35f,

                Sector = SpawnSector.EastSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 48,
                SpawnInterval = 0.35f,

                Sector = SpawnSector.SouthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 48,
                SpawnInterval = 0.35f,

                Sector = SpawnSector.WestSector
            }
        ]
    };

    public static readonly WavePlan TenthWave = new WavePlan
    {
        Name = "Final Orbit",
        IntermissionAfterWave = 2f,
        Groups =
        [
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 28,
                SpawnInterval = 0.25f,

                Sector = SpawnSector.NorthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 28,
                SpawnInterval = 0.25f,
                DelayBeforeGroup = 2f,

                Sector = SpawnSector.EastSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 28,
                SpawnInterval = 0.25f,
                DelayBeforeGroup = 4f,

                Sector = SpawnSector.SouthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 28,
                SpawnInterval = 0.25f,
                DelayBeforeGroup = 6f,

                Sector = SpawnSector.WestSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 44,
                SpawnInterval = 0.3f,
                DelayBeforeGroup = 10f,

                Sector = SpawnSector.NorthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 44,
                SpawnInterval = 0.3f,
                DelayBeforeGroup = 10f,

                Sector = SpawnSector.EastSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 44,
                SpawnInterval = 0.3f,
                DelayBeforeGroup = 10f,

                Sector = SpawnSector.SouthSector
            },
            new SpawnGroup
            {
                EnemyId = "space_diver",
                Count = 44,
                SpawnInterval = 0.3f,
                DelayBeforeGroup = 10f,

                Sector = SpawnSector.WestSector
            }
        ]
    };
}
