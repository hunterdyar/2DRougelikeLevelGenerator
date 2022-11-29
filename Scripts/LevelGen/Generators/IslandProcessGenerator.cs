using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace RougeLevelGen
{
	public class IslandProcessGenerator : Generator
	{
		//Keep largest island of type
		private Tile _tile;
		private List<List<Vector2Int>> islands = new List<List<Vector2Int>>();
		private List<Vector2Int> _processed = new List<Vector2Int>();
		public IslandProcessGenerator(string layer, LevelGenerator levelGenerator, Tile tileToFill) : base(layer, levelGenerator)
		{
			_tile = tileToFill;
		}

		public override IEnumerator Generate()
		{
			LevelGenerator.ProgressStage = "Culling Islands (recursive find)";
			GetIslands();

			LevelGenerator.ProgressStage = "Culling Islands (cull)";

			if (islands.Count > 1)
			{
				//Sort islands by their size.
				islands.Sort((x,y)=> y.Count - x.Count);
				for (int i = 1; i < islands.Count; i++)
				{
					//What are we flipping into?
					Tile turnInto = Tile.Wall;
					if (_tile == Tile.Wall)
					{
						turnInto = Tile.Floor;
					}
					
					//flip the islands values!
					foreach (var pos in islands[i])
					{
						SetTile(pos,turnInto);
					}
				}
			}
			return base.Generate();
		}

		private void GetIslands()
		{
			//Flood fill Saturate Islands
			for (int i = 0; i < Settings.LevelWidth; i++)
			{
				for (int j = 0; j < Settings.LevelHeight; j++)
				{
					var pos = new Vector2Int(i, j);
					if (GetTile(pos) == _tile && !_processed.Contains(pos))
					{
						var island = new List<Vector2Int>();
						island.Add(pos);

						islands.Add(island);
						Fill(islands.IndexOf(island), pos);
					}
				}
			}
		}
		private void Fill(int islandindex, Vector2Int pos)
		{
			var neighbors = LevelGenerator.GetNeighborPositions(pos, false);
			foreach (var n in neighbors)
			{
				if (GetTile(n) == _tile)
				{
					if (!islands[islandindex].Contains(n))
					{
						islands[islandindex].Add(n);
						_processed.Add(n);
						//recursive flood fill
						Fill(islandindex,n);
					}
				}
			}
		}
	}
}