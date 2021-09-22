using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using UnityEngine.UI;

/**
    Abstraction of the Tilemap layers
**/
namespace Gameplay
{
    public enum Layers
    {
        GROUND = 0, OBJECTS = 1, SELECTION = 100
    }
    public struct TilemapLayer
    {
        public int layer;
        public Tilemap tilemap;
    }

    public class TilemapLayerController : MonoBehaviour
    {
        [Header("Tilemap Layers")]
        public TilemapLayer GroundLayer;
        public TilemapLayer ObjectsLayer;

        [Header("Tilemap Layers")]
        [SerializeField]
        public Tilemap groundTilemap;
        [SerializeField]
        public Tilemap objectsTilemap;



        private int currentSelectionLayer = (int)Layers.OBJECTS;

        public void Awake()
        {
            InitLayers();
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Home))
            {
                IncreaseSelectionLayer();
            }
            if (Input.GetKeyDown(KeyCode.End))
            {
                DecreaseSelectionLayer();
            }
        }

        private void InitLayers()
        {
            GroundLayer = new TilemapLayer
            {
                layer = (int)Layers.GROUND,
                tilemap = groundTilemap,
            };
            ObjectsLayer = new TilemapLayer
            {
                layer = (int)Layers.OBJECTS,
                tilemap = objectsTilemap,
            };
        }


        public TilemapLayer GetCurrentSelectedLayer()
        {
            switch (currentSelectionLayer)
            {
                case (int)Layers.GROUND: return GroundLayer;
                case (int)Layers.OBJECTS: return ObjectsLayer;
                default: return ObjectsLayer;
            }

        }

        #region Debug Tools

        public void IncreaseSelectionLayer()
        {
            currentSelectionLayer = currentSelectionLayer + 1;
        }
        public void DecreaseSelectionLayer()
        {
            currentSelectionLayer = currentSelectionLayer - 1;
        }


        #endregion
    }

}