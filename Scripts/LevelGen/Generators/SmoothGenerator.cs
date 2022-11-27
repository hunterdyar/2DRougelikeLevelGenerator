using System.Collections;
using UnityEngine;

namespace RougeLevelGen
{
	//
	public class SmoothGenerator : Generator
	{
		private int timesToSmooth;
		public SmoothGenerator(string layer, LevelGenerator generator, int repeat) : base(layer, generator)
		{
			timesToSmooth = repeat + 1;
			if (timesToSmooth < 0)
			{
				Debug.LogWarning("Invalid Repeat property into Smooth");
				timesToSmooth = 0;
			}
		}

		public override IEnumerator Generate()
		{
			//iterating through each cell/
			//removing wall cells with 4 or more orthogonally or diagonally adjacent non-wall cells.

			for (int i = 0; i < timesToSmooth; i++)
			{
				Smooth();
			}
			
			yield break;
		}

		public void Smooth()
		{
			for (int i = 0; i < Settings.LevelWidth; i++)
			{
				for (int j = 0; j < Settings.LevelHeight; j++)
				{
					var pos = new Vector2Int(i, j);
					if (GetTile(pos) == Tile.Wall)
					{
						var c = Gen.CountFloorNeighbors(_layer, pos, true);
						if (c > 4)
						{
							SetTile(pos, Tile.Floor);
						}
					}
				}
			}
		}
	}
}