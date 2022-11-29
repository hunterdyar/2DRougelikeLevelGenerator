﻿using System;
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

		//Todo: Take perlin noise and keep those above a certain threshold walls, the rest floors.
		public override IEnumerator Generate()
		{
			seed = new Vector2((float)LevelGenerator.Random.NextDouble()*LevelGenerator.Settings.LevelWidth*100*_scale, (float)LevelGenerator.Random.NextDouble() * LevelGenerator.Settings.LevelWidth*100*_scale);
			
			var posArray = LevelGenerator.GetTiles(_layer).Keys.ToArray();
			int p = 0;
			int c = posArray.Length;
			LevelGenerator.ProgressStage = "Creating Noise";
			foreach (var pos in posArray)
			{
				Tile t = (Mathf.PerlinNoise(seed.x+(pos.x*_scale),+(seed.y)+pos.y*_scale) < _threshold )? Tile.Floor : Tile.Wall;
				SetTile(pos, t);
				p++;
				LevelGenerator.Progress = p / (float)c;
			}

			
			LevelGenerator.ProgressStage = "Done with Perlin";
			return base.Generate();
		}
	}
}