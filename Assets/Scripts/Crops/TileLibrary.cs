using UnityEngine;
using System.Collections.Generic;
using Gameplay;
using UnityEngine.Tilemaps;
using System;

public class TileLibrary : MonoBehaviour
{
    public static TileLibrary instance;
    public Dictionary<string, IGameTile> Tiles;

    private void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else if (instance != this)
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

        return clonedTile;
    }

    private void InitLibrary()
    {
        Tiles.Add("beetroot", new CropTile()
        {
            Description = "Beetroot Sprout - Stage 1",
            TileBase = Resources.Load<TileBase>("Scenes/Common/Textures/Flooring Tiles/Crops/beetroot_1"), 
            TileData = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/beetroot_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "Beetroot Small Leaf - Stage 2",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/beetroot_2"),
                },
                 new GrowthStage() {
                    Description = "Beetroot Leaf - Stage 3",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/beetroot_3"),
                },
                new GrowthStage() {
                    Description = "Beetroot Bulb - Stage 4",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/beetroot_4"),
                },
                new GrowthStage() {
                    Description = "Grown Beetroot - Stage 5",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/beetroot_5"),
                },
            },
            GrowthTime = 2,
        });
        Tiles.Add("carrot", new CropTile()
        {
            Description = "Carrot Sprout - Stage 1",
            TileBase = Resources.Load<TileBase>("Scenes/Common/Textures/Flooring Tiles/Crops/carrot_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/carrot_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "Carrot Small Leaf - Stage 2",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/carrot_2"),
                },
                 new GrowthStage() {
                    Description = "Carrot Bulb - Stage 3",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/carrot_3"),
                },
                new GrowthStage() {
                    Description = "Grown Carrot - Stage 4",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/carrot_4"),
                },
            },
            GrowthTime = 1,
        });
        Tiles.Add("tomato", new CropTile()
        {
            Description = "Tomato Sprout - Stage 1",
            TileBase = Resources.Load<TileBase>("Scenes/Common/Textures/Flooring Tiles/Crops/tomato_1"),  // location to the art sprites
            TileData = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/tomato_1"),
            GrowthStageTiles = new GrowthStage[]
            {
                 new GrowthStage() {
                    Description = "Tomato Small Stem - Stage 2",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/tomato_2"),
                },
                 new GrowthStage() {
                    Description = "Tomato Stem - Stage 3",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/tomato_3"),
                },
                new GrowthStage() {
                    Description = "Unripe Tomato - Stage 4",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/tomato_4"),
                },
                new GrowthStage() {
                    Description = "Grown Tomato - Stage 5",
                    Tile = Resources.Load<Tile>("Scenes/Common/Textures/Flooring Tiles/Crops/tomato_5"),
                },
            },
            GrowthTime = 1,
        });
        Tiles.Add("grass_001", new GameTile()
        {
            Description = "Grass",
            TileBase = Resources.Load<TileBase>("Sprites/grass_001"),
            TileData = Resources.Load<Tile>("Sprites/grass_001"),
        });
    }
}