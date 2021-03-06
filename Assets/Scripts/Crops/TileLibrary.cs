using Gameplay;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileLibrary : MonoBehaviour
{
    public static TileLibrary instance;
    public Dictionary<string, IGameTile> Tiles;
    public DayNightCycleBehaviour timecycle;

    private void Awake()
    {
        if(!instance)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }

        Tiles = new Dictionary<string, IGameTile>();

        InitLibrary();
    }

    public IGameTile GetClonedTile(string id)
    {
        IGameTile originalTile = Tiles[id];
        IGameTile clonedTile = (IGameTile)originalTile.Clone();
        clonedTile.ID = Guid.NewGuid().ToString();

        print(clonedTile.Description);

        return clonedTile;
    }

    //public IGameTile GetClonedTile(string id)
    //{
    //    IGameTile originalTile = Tiles[id];
    //    GameTile copy = new GameTile();

    //    copy.ID = Guid.NewGuid().ToString();
    //    //copy.LocalPlace.x = originalTile.LocalPlace.x;
    //    //copy.LocalPlace.y = originalTile.LocalPlace.y;
    //    copy.LocalPlace = new Vector3Int(originalTile.LocalPlace.x, originalTile.LocalPlace.y, originalTile.LocalPlace.z);
    //    copy.WorldLocation = new Vector3(originalTile.WorldLocation.x, originalTile.WorldLocation.y, originalTile.WorldLocation.z);
    //    copy.TileBase = originalTile.TileBase;
    //    copy.TilemapMember = originalTile.TilemapMember;
    //    copy.Description = originalTile.Description;
    //    copy.Cost = originalTile.Cost;
    //    copy.TileData = originalTile.TileData;

    //    print(copy.Description);

    //    return copy;
    //}

    private void InitLibrary()
    {
        Tiles.Add("beetroot", new CropTile()
        {
            Description = "beetroot - Seeds",
            TileBase = Resources.Load<TileBase>("Crops/beet_1"),
            TileData = Resources.Load<Tile>("Crops/beet_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "beetroot - Stage 2",
                    Tile = Resources.Load<Tile>("Crops/beet_2"),
                },
                 new GrowthStage() {
                    Description = "beetroot - Stage 3",
                    Tile = Resources.Load<Tile>("Crops/beet_3"),
                },
                new GrowthStage() {
                    Description = "beetroot - Stage 4",
                    Tile = Resources.Load<Tile>("Crops/beet_4"),
                },
                new GrowthStage() {
                    Description = "beetroot - Stage 5",
                    Tile = Resources.Load<Tile>("Crops/beet_5"),
                },
                new GrowthStage() {
                    Description = "beetroot - Grown",
                    Tile = Resources.Load<Tile>("Crops/beet_6"),
                },
            },
            GrowthTime = 3 * (int)timecycle.gameDayLength,
        });
        Tiles.Add("carrot", new CropTile()
        {
            Description = "carrot - Seeds",
            TileBase = Resources.Load<TileBase>("Crops/carrot_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Crops/carrot_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "carrot - Stage 2",
                    Tile = Resources.Load<Tile>("Crops/carrot_2"),
                },
                 new GrowthStage() {
                    Description = "carrot - Stage 3",
                    Tile = Resources.Load<Tile>("Crops/carrot_3"),
                },
                new GrowthStage() {
                    Description = "carrot - Stage 4",
                    Tile = Resources.Load<Tile>("Crops/carrot_4"),
                },
                new GrowthStage() {
                    Description = "carrot - Grown",
                    Tile = Resources.Load<Tile>("Crops/carrot_5"),
                },
            },
            GrowthTime = 3 * (int)timecycle.gameDayLength,
        });
        Tiles.Add("corn", new CropTile()
        {
            Description = "Corn - Seeds",
            TileBase = Resources.Load<TileBase>("Crops/corn_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Crops/corn_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "Corn - Stage 2",
                    Tile = Resources.Load<Tile>("Crops/corn_2"),
                },
                 new GrowthStage() {
                    Description = "Corn - Stage 3",
                    Tile = Resources.Load<Tile>("Crops/corn_3"),
                },
                new GrowthStage() {
                    Description = "Corn - Stage 4",
                    Tile = Resources.Load<Tile>("Crops/corn_4"),
                },
                new GrowthStage() {
                    Description = "Corn - Stage 5",
                    Tile = Resources.Load<Tile>("Crops/corn_5"),
                },
                new GrowthStage() {
                    Description = "Corn - Grown",
                    Tile = Resources.Load<Tile>("Crops/corn_6"),
                },
            },
            GrowthTime = 3 * (int)timecycle.gameDayLength,
        });
        Tiles.Add("melon", new CropTile()
        {
            Description = "melon - Seeds",
            TileBase = Resources.Load<TileBase>("Crops/melon_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Crops/melon_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "melon - Stage 2",
                    Tile = Resources.Load<Tile>("Crops/melon_2"),
                },
                 new GrowthStage() {
                    Description = "melon - Stage 3",
                    Tile = Resources.Load<Tile>("Crops/melon_3"),
                },
                new GrowthStage() {
                    Description = "melon - Stage 4",
                    Tile = Resources.Load<Tile>("Crops/melon_4"),
                },
                new GrowthStage() {
                    Description = "melon - Stage 5",
                    Tile = Resources.Load<Tile>("Crops/melon_5"),
                },
                new GrowthStage() {
                    Description = "melon - Grown",
                    Tile = Resources.Load<Tile>("Crops/melon_6"),
                },
            },
            GrowthTime = 4 * (int)timecycle.gameDayLength,
        });
        Tiles.Add("potato", new CropTile()
        {
            Description = "potato - Seeds",
            TileBase = Resources.Load<TileBase>("Crops/potato_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Crops/potato_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "potato - Stage 2",
                    Tile = Resources.Load<Tile>("Crops/potato_2"),
                },
                 new GrowthStage() {
                    Description = "potato - Stage 3",
                    Tile = Resources.Load<Tile>("Crops/potato_3"),
                },
                new GrowthStage() {
                    Description = "potato - Stage 4",
                    Tile = Resources.Load<Tile>("Crops/potato_4"),
                },
                new GrowthStage() {
                    Description = "potato - Stage 5",
                    Tile = Resources.Load<Tile>("Crops/potato_5"),
                },
                new GrowthStage() {
                    Description = "potato - Grown",
                    Tile = Resources.Load<Tile>("Crops/potato_6"),
                },
            },
            GrowthTime = 2 * (int)timecycle.gameDayLength,
        });
        Tiles.Add("pumpkin", new CropTile()
        {
            Description = "pumpkin - Seeds",
            TileBase = Resources.Load<TileBase>("Crops/pump_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Crops/pump_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "pumpkin - Stage 2",
                    Tile = Resources.Load<Tile>("Crops/pump_2"),
                },
                 new GrowthStage() {
                    Description = "pumpkin - Stage 3",
                    Tile = Resources.Load<Tile>("Crops/pump_3"),
                },
                new GrowthStage() {
                    Description = "pumpkin - Stage 4",
                    Tile = Resources.Load<Tile>("Crops/pump_4"),
                },
                new GrowthStage() {
                    Description = "pumpkin - Stage 5",
                    Tile = Resources.Load<Tile>("Crops/pump_5"),
                },
                new GrowthStage() {
                    Description = "pumpkin - Grown",
                    Tile = Resources.Load<Tile>("Crops/pump_6"),
                },
            },
            GrowthTime = 5 * (int)timecycle.gameDayLength,
        });
        Tiles.Add("strawberry", new CropTile()
        {
            Description = "strawberry - Seeds",
            TileBase = Resources.Load<TileBase>("Crops/straw_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Crops/straw_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "strawberry - Stage 2",
                    Tile = Resources.Load<Tile>("Crops/straw_2"),
                },
                 new GrowthStage() {
                    Description = "strawberry - Stage 3",
                    Tile = Resources.Load<Tile>("Crops/straw_3"),
                },
                new GrowthStage() {
                    Description = "strawberry - Stage 4",
                    Tile = Resources.Load<Tile>("Crops/straw_4"),
                },
                new GrowthStage() {
                    Description = "strawberry - Stage 5",
                    Tile = Resources.Load<Tile>("Crops/straw_5"),
                },
                new GrowthStage() {
                    Description = "strawberry - Grown",
                    Tile = Resources.Load<Tile>("Crops/straw_6"),
                },
            },
            GrowthTime = 3 * (int)timecycle.gameDayLength,
        });
        Tiles.Add("tomato", new CropTile()
        {
            Description = "tomato - Seeds",
            TileBase = Resources.Load<TileBase>("Crops/tomato_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Crops/tomato_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "tomato - Stage 2",
                    Tile = Resources.Load<Tile>("Crops/tomato_2"),
                },
                 new GrowthStage() {
                    Description = "tomato - Stage 3",
                    Tile = Resources.Load<Tile>("Crops/tomato_3"),
                },
                new GrowthStage() {
                    Description = "tomato - Stage 4",
                    Tile = Resources.Load<Tile>("Crops/tomato_4"),
                },
                new GrowthStage() {
                    Description = "tomato - Stage 5",
                    Tile = Resources.Load<Tile>("Crops/tomato_5"),
                },
                new GrowthStage() {
                    Description = "tomato - Grown",
                    Tile = Resources.Load<Tile>("Crops/tomato_6"),
                },
            },
            GrowthTime = 3 * (int)timecycle.gameDayLength,
        });
        Tiles.Add("empty_farmland", new GameTile()
        {
            Description = "Farmland",
            TileBase = Resources.Load<TileBase>("Crops/farmland"),
            TileData = Resources.Load<Tile>("Crops/farmland"),
        });
    }
}