using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RougeLevelGen
{
	[Serializable]
	public class Generator
	{
		protected string _layer;
		public LevelGenSettings Settings => _generator.Settings;
		public LevelGenerator Gen => _generator;
		private LevelGenerator _generator;
		public Dictionary<Vector2Int, Tile> Tiles => _generator.GetTiles(_layer);

		public Generator(string layer, LevelGenerator generator)
		{
			_layer = layer;
			_generator = generator;
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
			return _generator.GetTile(_layer, pos);
		}

		public void SetTile(Vector2Int position, Tile tile)
		{
			_generator.SetTile(_layer, position, tile);
		}

	}
}