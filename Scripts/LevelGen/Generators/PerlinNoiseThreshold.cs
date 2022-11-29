using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RougeLevelGen
{
	public class PerlinNoiseThreshold : Generator
	{
		private float _threshold;
		private float _scale;

		private Vector2 seed = Vector2.zero;
		public PerlinNoiseThreshold(string layer, LevelGenerator levelGenerator, float threshold, float scale) : base(layer, levelGenerator)
		{
			_threshold = threshold;
			_scale = scale;
		}

		public override IEnumerator Generate()
		{
			seed = new Vector2((float)LevelGenerator.Random.NextDouble()*LevelGenerator.Settings.LevelWidth*100*_scale, (float)LevelGenerator.Random.NextDouble() * LevelGenerator.Settings.LevelWidth*100*_scale);
			
			var posArray = LevelGenerator.GetTiles(_layer).Keys.ToArray();
			LevelGenerator.ProgressStage = "Creating Noise";
			foreach (var pos in posArray)
			{
				Tile t = (Mathf.PerlinNoise(seed.x+(pos.x*_scale),+(seed.y)+pos.y*_scale) < _threshold )? Tile.Floor : Tile.Wall;
				SetTile(pos, t);
			}

			
			LevelGenerator.ProgressStage = "Done with Perlin";
			return base.Generate();
		}
	}
}