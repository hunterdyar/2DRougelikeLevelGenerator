using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

namespace RougeLevelGen
{
	/// <summary>
	/// Uses a non-optimized implementation of "Fast Poisson Disk Sampling in Arbitrary Dimensions" https://www.cs.ubc.ca/~rbridson/docs/bridson-siggraph07-poissondisk.pdf
	/// </summary>
	public class PoissonDisk : Generator
	{
		private float _radius = 3;
		private readonly int _searchK = 5;
		public PoissonDisk(string layer, LevelGenerator levelGenerator, float radius) : base(layer, levelGenerator)
		{
			_radius = radius;
		}
		

		public override IEnumerator Generate()
		{
			//First, pick a random
			Vector2Int start = Settings.RandomPositionInLevel();
			List<Vector2Int> samples = new List<Vector2Int>();
			samples.Add(start);
			SetTile(start, Tile.Floor);
			while (samples.Count > 0)
			{
				int randomIndex = LevelGenerator.Random.Next(samples.Count);
				var random = samples[randomIndex];
				var ks = RandomSphericalAnnulus(_searchK, _radius, 2 * _radius, random);
				bool sampleAdded = false;
				for (int i = 0; i < ks.Count; i++)
				{
					if (IsNoFloorsNearby(ks[i]))
					{
						samples.Add(ks[i]);
						SetTile(ks[i],Tile.Floor);
						sampleAdded = true;
					}
				}

				if (!sampleAdded)
				{
					samples.RemoveAt(randomIndex);
				}
			}
			return base.Generate();
		}

		private bool IsNoFloorsNearby(Vector2Int center)
		{
			int top = Mathf.CeilToInt(center.y - _radius);
			int bottom = Mathf.FloorToInt(center.y + _radius);
			int left = Mathf.CeilToInt(center.x - _radius);
			int right = Mathf.FloorToInt(center.x + _radius);

			for (int y = top; y <= bottom; y++)
			{
				for (int x = left; x <= right; x++)
				{
					var s = new Vector2Int(x, y);
					if (InsideCircle(center, s, _radius))
					{
						if (GetTile(s) == Tile.Floor)
						{
							return false;
						}
					}
				}
			}

			return true;
		}

		private List<Vector2Int> RandomSphericalAnnulus(int number, float minRadius, float maxRadius, Vector2Int starting)
		{
			List<Vector2Int> samples = new List<Vector2Int>();
			for (int i = 0; i < number; i++)
			{
				var k = UnityEngine.Random.insideUnitCircle * maxRadius;
				if (k.magnitude > minRadius)
				{
					var ki = new Vector2Int(Mathf.RoundToInt(k.x), Mathf.RoundToInt(k.y)) + starting;
					if (LevelGenerator.Settings.IsInBounds(ki))
					{
						samples.Add(ki);
					}
				}
			}

			return samples;
		}

		bool InsideCircle(Vector2Int center, Vector2Int tile, float radius)
		{
			float dx = center.x - tile.x,
				dy = center.y - tile.y;
			float distance_squared = dx * dx + dy * dy;
			return distance_squared <= radius * radius;
		}
	}
}