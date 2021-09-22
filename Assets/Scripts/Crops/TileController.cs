using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;


/*
   Tile Controller by Macauley
	Allows us to dynamically set tiles and run coroutines on individual positions
*/
namespace Gameplay
{
	public delegate void PlantPlantedHandler(string ID);

	public class TileController : MonoBehaviour
	{
		public Dictionary<Vector3, IGameTile> tiles = new Dictionary<Vector3, IGameTile>();

		public GameObject player;

		public Tilemap tilemap;

		public Tilemap crop_tilemap;

		public Tile farmland_tile;


		public event PlantPlantedHandler OnStageGrow;

		public static TileController instance;
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
		}

		private void ReadTilemapToTileData(Tilemap tilemap, int layer)
		{
			foreach (Vector3Int pos in tilemap.cellBounds.allPositionsWithin)
			{
				Vector3Int localPlace = new Vector3Int(pos.x, pos.y, pos.z);

				if (!tilemap.HasTile(localPlace)) continue;

				var worldLocation = tilemap.CellToWorld(localPlace);
				var layeredWorldPosition = new Vector3(worldLocation.x, worldLocation.y, layer);

				TileBase tileBase = tilemap.GetTile(localPlace);
				IGameTile tileFromLibrary = GetTileByAssetName(tileBase.name);

				IGameTile tile = new GameTile
				{
					LocalPlace = localPlace,
					WorldLocation = layeredWorldPosition,
					TileBase = tileBase,
					TilemapMember = tilemap,
					Description = tileFromLibrary.Description,
					TileData = tileFromLibrary.TileData,
					Cost = 1
				};

				tiles.Add(layeredWorldPosition, tile);
			}
		}


		public void PlaceTile(Vector3 pos, string assetName)
		{
			Vector3Int tilemapPos = crop_tilemap.WorldToCell(pos);
			Vector3 layeredWorldPosition = new Vector3(tilemapPos.x, tilemapPos.y);

			Vector3Int localPlace = new Vector3Int(tilemapPos.x, tilemapPos.y, 0);

			IGameTile newTile = TileLibrary.instance.GetClonedTile(assetName);
			newTile.LocalPlace = localPlace;
			newTile.WorldLocation = layeredWorldPosition;
			newTile.TilemapMember = crop_tilemap;

			// if a tile already exists there, just replace it.
			bool tileExistsInPos = tiles.ContainsKey(layeredWorldPosition);
			if (tileExistsInPos)
			{
				tiles[layeredWorldPosition] = newTile;
			}
			else
			{
				tiles.Add(layeredWorldPosition, newTile);
			}

			bool isACrop = newTile.GetType() == typeof(CropTile);
			if (isACrop)
			{
				(newTile as CropTile).StartGrowing();
			}

			SetGameTile(newTile);
		}


		// Starts a coroutine and returns to the caller after it's time is passed
		public void Grow(int timeToGrow, int stages, string ID)
		{
			StartCoroutine(StartGrowing(timeToGrow, stages, ID));
		}

		private IEnumerator StartGrowing(int timeToGrow, int stages, string ID)
		{
			for (int stage = 0; stage < stages; stage++)
			{
				yield return new WaitForSeconds(timeToGrow);
				OnStageGrow?.Invoke(ID);
			}
		}

		private void SetGameTile(IGameTile gameTile)
		{
			crop_tilemap.SetTile(gameTile.LocalPlace, gameTile.TileBase);
		}

		public static IGameTile GetTileByAssetName(string assetName)
		{
			return TileLibrary.instance.GetClonedTile(assetName);
		}

		private void Update()
		{
			GetInput();
		}


        private void GetInput()
		{
			if (Input.GetMouseButtonDown(0))
			{
				// works with ortho camera
				var wpos = player.transform.position;

				// get tile pos
				var tilePos = tilemap.WorldToCell(wpos);

				if (tilemap.GetTile(tilePos) == farmland_tile && crop_tilemap.HasTile(tilePos) == false)
				{
					// Set new tile to location
					PlaceTile(tilePos, "pumpkin");
				} else
                {
					Debug.Log("Not Farmland");
                }
			}
		}
	}
}