using System.Collections;
using System.ComponentModel;
using UnityEngine;

namespace HDyar.RougeLevelGen
{
	public class MergeLayersGenerator : Generator
	{
		//The original layer will be modified by this layer.
		private string _otherLayer;
		private MergeOperation _mergeOperation;

		public MergeLayersGenerator(string layer, LevelGenerator levelGenerator, string other, MergeOperation mergeOperation) : base(layer, levelGenerator)
		{
			_otherLayer = other;
			_mergeOperation = mergeOperation;
		}
		
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
					SetTile(pos,MergeTile(a,b,_mergeOperation));
				}
			}

		}

		static Tile MergeTile(Tile a, Tile b, MergeOperation merge)
		{
			switch (merge)
			{
				case MergeOperation.UnionFloors:
					return (a == Tile.Floor || b == Tile.Floor) ? Tile.Floor : Tile.Wall;
				case MergeOperation.UnionWalls:
					return (a == Tile.Wall || b == Tile.Wall) ? Tile.Wall : Tile.Floor;
				case MergeOperation.Difference:
					return (b == Tile.Floor) ? a : Tile.Wall;
			}

			return a;
		}

	}
}