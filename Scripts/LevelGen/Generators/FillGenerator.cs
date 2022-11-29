using System.Collections;
using UnityEngine;

namespace RougeLevelGen
{
	public class FillGenerator : Generator
	{
		private Tile _tile = Tile.Floor;
		public FillGenerator(string layer, LevelGenerator levelGenerator, Tile tile) : base(layer, levelGenerator)
		{
			_tile = tile;
		}

		public override IEnumerator Generate()
		{
			for (int i = 0; i < Settings.LevelWidth; i++)
			{
				for (int j = 0; j < Settings.LevelHeight; j++)
				{
					SetTile(new Vector2Int(i,j), _tile);
				}
			}
			yield break;
		}
	}
}