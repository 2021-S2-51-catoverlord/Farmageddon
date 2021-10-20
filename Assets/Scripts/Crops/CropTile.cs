using UnityEngine;
using UnityEngine.Tilemaps;

namespace Gameplay
{
    public struct GrowthStage
    {
        public TileBase Tile;
        public string Description;
    }

    public class CropTile : GameTile, IGameTile
    {
        public int GrowthTime;
        public GrowthStage[] GrowthStageTiles;
        public bool isGrown;
        public DayNightCycleBehaviour time;
        public bool isDead = false;

        public int currStageIndex = 0;

        public void Start()
        {
            time = GameObject.Find("Time Light").GetComponent<DayNightCycleBehaviour>();

            Debug.Log("Setting Listener for Crop Death");
            TileController.c_cropDeath.AddListener(OnCropDeath);
        }

        public void StartGrowing()
        {
            TileController.instance.Grow(GrowthTime, GrowthStageTiles.Length, ID, this);
            TileController.instance.OnStageGrow += OnGrowEvent;
        }

        public void OnCropDeath(CropTile tile)
        {
            TilemapMember.SetTile(tile.LocalPlace, null);
            TileBase = null;
            Description = null;
            currStageIndex = GrowthStageTiles.Length;
        }

        private void OnGrowEvent(string plantID)
        {
            DayNightCycleBehaviour time = TileController.timeCycle;
            bool inSeason = false;
            switch(time.season)
            {
                case Season.SPRIMMER:
                    foreach(string item in TileController.instance.springCrops)
                    {
                        if(Description.Contains(item))
                        {
                            inSeason = true;
                        }
                    }
                    if(inSeason)
                    {
                        GrowthTime = (int)((double)GrowthTime * 0.5);
                    }

                    break;
                case Season.SUMTUMN:
                    foreach(string item in TileController.instance.summerCrops)
                    {
                        if(Description.Contains(item))
                        {
                            inSeason = true;
                        }
                    }
                    if(inSeason)
                    {
                        GrowthTime = (int)((double)GrowthTime * 0.5);
                    }
                    break;
                case Season.AUNTER:
                    foreach(string item in TileController.instance.autumnCrops)
                    {
                        if(Description.Contains(item))
                        {
                            inSeason = true;
                        }
                    }
                    if(inSeason)
                    {
                        GrowthTime = (int)((double)GrowthTime * 0.5);
                    }
                    break;
                case Season.WINTING:
                    foreach(string item in TileController.instance.winterCrops)
                    {
                        if(Description.Contains(item))
                        {
                            inSeason = true;
                        }
                    }
                    if(inSeason)
                    {
                        GrowthTime = (int)((double)GrowthTime * 0.5);
                    }
                    else
                    {
                        GrowthTime = (int)((double)GrowthTime * 2);

                        if(!isDead)
                        {
                            TileController.instance.tiles.TryGetValue(LocalPlace, out IGameTile tile);
                            TileController.instance.crop_tilemap.SetTileFlags(Vector3Int.RoundToInt(tile.WorldLocation), TileFlags.None);
                            TileController.instance.crop_tilemap.SetColor(Vector3Int.RoundToInt(tile.WorldLocation), Color.blue);
                        }
                    }
                    break;

            }

            if(plantID != ID || isDead) return;

            // Unsubscribe
            if(currStageIndex >= GrowthStageTiles.Length)
            {
                TileController.instance.OnStageGrow -= OnGrowEvent;
                isGrown = true;
            }

            GrowthStage nextStage = GrowthStageTiles[currStageIndex];

            TilemapMember.SetTile(LocalPlace, nextStage.Tile);
            TileBase = nextStage.Tile;
            Description = nextStage.Description;

            currStageIndex++;
        }
    }
}
