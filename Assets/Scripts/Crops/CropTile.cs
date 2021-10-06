using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
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

        private int currStageIndex = 0;


        public void Start()
        {
            time = GameObject.Find("Time Light").GetComponent<DayNightCycleBehaviour>();
        }

        public void StartGrowing()
        {
            TileController.instance.Grow(GrowthTime, GrowthStageTiles.Length, ID);
            TileController.instance.OnStageGrow += OnGrowEvent;
        }


        private void OnGrowEvent(string plantID)
        {
            if (plantID != ID) return;

            // Unsubscribe
            if (currStageIndex >= GrowthStageTiles.Length)
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
