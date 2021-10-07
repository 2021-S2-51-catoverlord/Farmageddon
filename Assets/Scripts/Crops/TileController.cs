using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
using Utils;


/*
   Tile Controller by Macauley
	Allows us to dynamically set tiles and run coroutines on individual positions
*/
namespace Gameplay
{
	public delegate void PlantPlantedHandler(string ID);

	public class CropEvent : UnityEvent<CropTile> { }

	public class TileController : MonoBehaviour
	{
		public Dictionary<Vector3, IGameTile> tiles = new Dictionary<Vector3, IGameTile>();

		public PlayerController player;

		public Tilemap tilemap;

		public Tilemap crop_tilemap;

		public Tile farmland_tile;

		public Inventory inventory;

		private IGameTile lastTile;

		public Item[] seeds;

		public Item[] crops;

		public static DayNightCycleBehaviour timeCycle;

		public event PlantPlantedHandler OnStageGrow;

		public static TileController instance;

		public static CropEvent c_cropDeath = new CropEvent();


		public List<string> springCrops = new List<string> { "tomato", "beetroot", "corn", "strawberry"};
		public List<string> summerCrops = new List<string> { "tomato", "melon", "corn", "strawberry"};
		public List<string> autumnCrops = new List<string> { "corn", "pumpkin", "beetroot"};
		public List<string> winterCrops = new List<string> { "pumpkin", "potato", "corn"};

		public Color wiltedCrop = Color.blue;


		private void Start()
        {

			player = GameObject.Find("Player").GetComponent<PlayerController>(); // Finds the player controller and saves its reference.
			tilemap = GameObject.Find("Farmland").GetComponent<Tilemap>(); // Temporary fix: Need to find out which actual tilemap to find.
			crop_tilemap = GameObject.Find("Crops").GetComponent<Tilemap>(); // Temporary fix: Need to find out which actual tilemap to find.
			inventory = Resources.FindObjectsOfTypeAll<Inventory>()[0]; // Finds the first occurence of Inventory GameObj and saves its script.
			timeCycle = GameObject.Find("Time Light").GetComponent<DayNightCycleBehaviour>(); // Temporary fix: Just a guess.

			timeCycle.t_dayChange.AddListener(DayChanged);
		}

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
			bool inSeason = false;

			IGameTile tile;

			tiles.TryGetValue(tiles.FirstOrDefault(x => x.Value.ID == ID).Key, out tile);

			switch (timeCycle.season)
			{
				case Season.SPRIMMER:
					foreach (string item in TileController.instance.springCrops)
					{
						if (tile.Description.Contains(item))
						{
							inSeason = true;
						}
					}
					if (inSeason)
					{
						timeToGrow = (int)((double)timeToGrow * 0.5);
					}

					break;
				case Season.SUMTUMN:
					foreach (string item in TileController.instance.summerCrops)
					{
						if (tile.Description.Contains(item))
						{
							inSeason = true;
						}
					}
					if (inSeason)
					{
						timeToGrow = (int)((double)timeToGrow * 0.5);
					}
					break;
				case Season.AUNTER:
					foreach (string item in TileController.instance.autumnCrops)
					{
						if (tile.Description.Contains(item))
						{
							inSeason = true;
						}
					}
					if (inSeason)
					{
						timeToGrow = (int)((double)timeToGrow * 0.5);
					}
					break;
				case Season.WINTING:
					foreach (string item in TileController.instance.winterCrops)
					{
						if (tile.Description.Contains(item))
						{
							inSeason = true;
						}
					}
					if (inSeason)
					{
						timeToGrow = (int)((double)timeToGrow * 0.5);
					}
					else
					{
						timeToGrow = (int)((double)timeToGrow * 2);

						TileController.instance.crop_tilemap.SetTileFlags(Vector3Int.RoundToInt(tile.WorldLocation), TileFlags.None);
						TileController.instance.crop_tilemap.SetColor(Vector3Int.RoundToInt(tile.WorldLocation), Color.blue);
					}
					break;
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

								tiles.Remove(tiles.FirstOrDefault(x => x.Value == tile).Key);
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

							tiles.Remove(tiles.FirstOrDefault(x => x.Value == tile).Key);
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

		private void DayChanged()
        {
			bool isSeasonal = false;

			if (timeCycle.season == Season.WINTING)
            {
				List<KeyValuePair<Vector3, IGameTile>> existingTiles = tiles.ToList();
				foreach (KeyValuePair<Vector3, IGameTile> t in existingTiles)
				{
					foreach (string item in winterCrops)
					{
                        if (t.Value.Description.Contains(item))
						{
							isSeasonal = true;
						}
					}

					if (!isSeasonal)
					{
						IGameTile tile;
						int i = UnityEngine.Random.Range(0, 20);
						if (i == 0)
						{
							tiles.TryGetValue(t.Key, out tile);

							Debug.Log("Plant at Coords " + t.Value.LocalPlace + " should be dead");
							Debug.Log("Invoking Crop Death");
							CropTile cropTile = tile as CropTile;
							cropTile.isDead = true;

							c_cropDeath.Invoke(tile as CropTile);

							PlaceTile(t.Key);
							tiles.Remove(t.Key);

						} else
						{
							tiles.TryGetValue(t.Key, out tile);
							crop_tilemap.SetTileFlags(Vector3Int.RoundToInt(tile.WorldLocation), TileFlags.None);
							crop_tilemap.SetColor(Vector3Int.RoundToInt(tile.WorldLocation), Color.blue);
						}

					}
				}
			}
			
		}

	}
}