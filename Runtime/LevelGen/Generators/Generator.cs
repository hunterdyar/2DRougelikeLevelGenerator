using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HDyar.RougeLevelGen
{
	[Serializable]
	public class Generator
	{
		protected string _layer;
		public LevelGenSettings Settings => _levelGenerator.Settings;
		public LevelGenerator LevelGenerator => _levelGenerator;
		private LevelGenerator _levelGenerator;
		public Dictionary<Vector2Int, Tile> Tiles => _levelGenerator.GetTiles(_layer);

		public Generator(string layer, LevelGenerator levelGenerator)
		{
			_layer = layer;
			_levelGenerator = levelGenerator;
		}
		public virtual void Initiate()
		{
			
		}

		public virtual IEnumerator Generate()
		{
			yield break;
		}

		//Wrappers for layer-agnostic interaction
		public Tile GetTile(Vector2Int pos)
		{
			return _levelGenerator.GetTile(_layer, pos);
		}

		public void SetTile(Vector2Int position, Tile tile)
		{
			_levelGenerator.SetTile(_layer, position, tile);
		}

		//Not sure where this belongs. Utility?
		public static Tile RandomFloorOrWall(float percentageFloor)
		{
			if (LevelGenerator.Random.NextDouble() <= percentageFloor)
			{
				return Tile.Floor;
			}
			else
			{
				return Tile.Wall;
			}
		}
	}
}