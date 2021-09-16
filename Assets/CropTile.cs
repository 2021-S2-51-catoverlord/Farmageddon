using System.Collections;
using System.Collections.Generic;
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
        public Light2D timeCycle;

        private int currStageIndex = 0;

        public void StartGrowing()
        {
            TileController.instance.Grow(GrowthTime * (int)timeCycle.GetComponent<DayNightCycleBehaviour>().gameDayLength, GrowthStageTiles.Length, ID);
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
