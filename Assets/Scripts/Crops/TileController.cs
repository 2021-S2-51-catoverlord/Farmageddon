using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Utils;


/*
   Tile Controller made by Macauley
	This controller allows us to dynamically set tiles and run coroutines on individual positions
*/
namespace Gameplay
{
	public delegate void PlantPlantedHandler(string ID);

	public class TileController : MonoBehaviour
	{
		//Dictionary of crop tiles for growth and harvesting purposes
		public Dictionary<Vector3, IGameTile> tiles = new Dictionary<Vector3, IGameTile>();

		//Player Entity that can interact with the crops
		public PlayerController player;

		//Object Tilemap which shows planted crops
		public Tilemap crop_tilemap;

		//Ground Tilemap which delimits farmable area
		public Tilemap tilemap;

		//Tile which represents farmable area for the Ground Tilemap
		public Tile farmland_tile;

		//Player Entity's Inventory for Planting Seeds / Harvesting Crops
		public Inventory inventory;

		//Array of available Seeds to plant / receive
		public Item[] seeds;

		//Array of available Crops to receive
		public Item[] crops;

		//Global controller of the Time Cycle
		public DayNightCycleBehaviour timeCycle;

		//Receives output from Tile Library checks
		private IGameTile lastTile;

		//Execute code upon Crop Tile growth
		public event PlantPlantedHandler OnStageGrow;

		//The current instance of TileController
		public static TileController instance;
		private void Awake()
		{
			//This ensures that there is only one instance of TileController at any given time
			if (!instance)
			{
				instance = this;
			}
			else if (instance != this)
			{
				Destroy(gameObject);
			}
		}

		/// <summary>
		/// Set tile in position to tile listed in the Tile Library
		/// </summary>
		/// <param name="pos"></param>
		/// <param name="assetName"></param>
		/// <returns></returns>
		/* This function accepts a Vector3 Position and a string as its parameters.
		 * 
		 * 
		 * 
		 */
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

		public void PlaceTile(Vector3 pos)
		{
			Vector3Int tilemapPos = crop_tilemap.WorldToCell(pos);
			Vector3 layeredWorldPosition = new Vector3(tilemapPos.x, tilemapPos.y);

			Vector3Int localPlace = new Vector3Int(tilemapPos.x, tilemapPos.y, 0);


			// if a tile already exists there, just replace it.
			bool tileExistsInPos = tiles.ContainsKey(layeredWorldPosition);
			if (tileExistsInPos)
			{
				tiles[layeredWorldPosition] = null;
			}
			else
			{
				tiles.Add(layeredWorldPosition, null);
			}


			crop_tilemap.SetTile(tilemapPos, null);
		}


		// Starts a coroutine and returns to the caller after it's time is passed
		public void Grow(int timeToGrow, int stages, string ID)
		{
			if(timeCycle.season == Season.WINTING)
            {
				timeToGrow *= 2;
            } else if (timeCycle.season == Season.SPRIMMER){
				timeToGrow = (int)Math.Ceiling(timeToGrow * 0.5f);
            }
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
			if (Input.GetMouseButtonDown(0) && !player.isInventoryActive)
			{	// For the time being, we use the first inventory slot as the player's equipped item
				if(inventory.itemSlots[0].Item != null)
                {
					if (inventory.itemSlots[0].Item is Seed){
						Seed seed = (Seed)inventory.itemSlots[0].Item;
						string seedname = "";

                        switch (seed.itemName)
                        {
							case "Beetroot Seeds": seedname = "beetroot"; break;
							case "Carrot Seeds": seedname = "carrot"; break;
							case "Corn Seeds": seedname = "corn"; break;
							case "Potato Seeds": seedname = "potato"; break;
							case "Pumpkin Seeds": seedname = "pumpkin"; break;
							case "Strawberry Seeds": seedname = "strawberry"; break;
							case "Tomato Seeds": seedname = "tomato"; break;
							case "Watermelon Seeds": seedname = "melon"; break;
							case "": seedname = "potato"; break;
                        }

						var wpos = player.GetComponent<CapsuleCollider2D>().transform.position;
						wpos.y += 1.0f;

						// get tile pos
						var tilePos = tilemap.WorldToCell(wpos);

						if (tilemap.GetTile(tilePos) == farmland_tile && crop_tilemap.HasTile(tilePos) == false)
						{
							// Set new tile to location
							PlaceTile(tilePos, seedname);
							inventory.RemoveItem(seed);
						}
						else if (tilemap.GetTile(tilePos) == farmland_tile && crop_tilemap.HasTile(tilePos) == true && instance.tiles.TryGetValue(tilePos, out lastTile))
						{
							IGameTile tile;
							instance.tiles.TryGetValue(tilePos, out tile);
							if (tile.Description.Contains("Grown"))
                            {
								PlaceTile(wpos);
								Debug.Log(tile.TileBase.name);
                                switch (tile.TileBase.name)
								{
									case "beet_6": GiveSeeds(0); GiveCrops(0); break;
									case "carrot_5": GiveSeeds(1); GiveCrops(1); break;
									case "corn_6": GiveSeeds(2); GiveCrops(2); break;
									case "potato_6": GiveSeeds(3); GiveCrops(3); break;
									case "pumpkin_6": GiveSeeds(4); GiveCrops(4); break;
									case "strawberry_6": GiveSeeds(5); GiveCrops(5); break;
									case "tomato_6": GiveSeeds(6); GiveCrops(6); break;
									case "melon_6": GiveSeeds(7); GiveCrops(7); break;
								}
                            }
						}
					}
				} else
				{
					var wpos = player.GetComponent<CapsuleCollider2D>().transform.position;
					wpos.y += 1.0f;

					// get tile pos
					var tilePos = tilemap.WorldToCell(wpos);
					if (tilemap.GetTile(tilePos) == farmland_tile && crop_tilemap.HasTile(tilePos) == true && instance.tiles.TryGetValue(tilePos, out lastTile))
					{
						IGameTile tile;
						instance.tiles.TryGetValue(tilePos, out tile);
						if (tile.Description.Contains("Grown"))
						{
							PlaceTile(wpos);
							Debug.Log(tile.TileBase.name);
							switch (tile.TileBase.name)
							{
								case "beet_6": GiveSeeds(0); GiveCrops(0); break;
								case "carrot_5": GiveSeeds(1); GiveCrops(1); break;
								case "corn_6": GiveSeeds(2); GiveCrops(2); break;
								case "potato_6": GiveSeeds(3); GiveCrops(3); break;
								case "pumpkin_6": GiveSeeds(4); GiveCrops(4); break;
								case "strawberry_6": GiveSeeds(5); GiveCrops(5); break;
								case "tomato_6": GiveSeeds(6); GiveCrops(6); break;
								case "melon_6": GiveSeeds(7); GiveCrops(7); break;
							}
						}
					}
				}
			}
		}

		private void GiveSeeds(int i)
        {
			int seedNo = UnityEngine.Random.Range(0, 4);
			Debug.Log("Should be this many seeds: " + seedNo);

			inventory.AddItem(seeds[i].GetItemCopy(), seedNo);
			
		}
		private void GiveCrops(int i)
        {
			inventory.AddItem(crops[i].GetItemCopy());
		}
	}
}