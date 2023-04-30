using System.Collections;
using UnityEngine;

namespace HDyar.RougeLevelGen
{
	public class LevelEdgesGenerator : Generator
	{
		private int _thickness;
		private Tile _type = Tile.Wall;

		public LevelEdgesGenerator(string layer, LevelGenerator levelGenerator, int thickness) : base(layer, levelGenerator)
		{
			_thickness = thickness;
		}

		//Set the boundries to type.
		public override IEnumerator Generate()
		{
			//top and bottom
			for (int x = 0; x < Settings.LevelWidth; x++)
			{
				for (int t = 0; t < _thickness; t++)
				{
					SetTile(new Vector2Int(x, t), _type);
					SetTile(new Vector2Int(x,Settings.LevelWidth-1-t), _type);
				}
			}

			for (int y = 0; y < Settings.LevelHeight; y++)
			{
				for (int t = 0; t < _thickness; t++)
				{
					SetTile(new Vector2Int(t, y), _type);
					SetTile(new Vector2Int(Settings.LevelHeight - 1 - t,y), _type);
				}
			}

			yield break;
		}
	}
}