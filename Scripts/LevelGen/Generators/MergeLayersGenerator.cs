using System.Collections;
using System.ComponentModel;
using UnityEngine;

namespace RougeLevelGen
{
	public class MergeLayersGenerator : Generator
	{
		//The original layer will be modified by this layer.
		private string _otherLayer;


		public MergeLayersGenerator(string layer, LevelGenerator levelGenerator, string other) : base(layer, levelGenerator)
		{
			_otherLayer = other;

		}

		//todo: validate

		public override IEnumerator Generate()
		{
			if (!LevelGenerator.HasLayer(_otherLayer))
			{
				Debug.LogError("Can't Merge, can't find layer " + _otherLayer);
				yield break;
			}

			for (int i = 0; i < Settings.LevelWidth; i++)
			{
				for (int j = 0; j < Settings.LevelHeight; j++)
				{
					var pos = new Vector2Int(i, j);
					var a = GetTile(pos);
					var b = LevelGenerator.GetTile(_otherLayer, pos);
					
				}
			}

		}

		Tile MergeTile(Tile a, Tile b, MergeOperation merge)
		{
			// switch (merge)
			// {
			// 	
			// }

			return a;
		}

	}
}