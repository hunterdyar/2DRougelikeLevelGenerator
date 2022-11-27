using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RougeLevelGen
{
	[Serializable]
	public class DrunkWalkGenerator : Generator
	{

		private LevelGenerator _generator;
		private int _maxWalkers;
		private readonly List<Walker> _walkers = new List<Walker>();
		public DrunkWalkGenerator(string layer, LevelGenerator levelGenerator, int maxWalkers) : base(layer, levelGenerator)
		{
			this._maxWalkers = maxWalkers;
		}
		

		public override void Initiate()
		{
			//create first walker
			_walkers.Clear();
			CreateWalker(Settings.RandomPositionInLevel());
			base.Initiate();
		}

		public override IEnumerator Generate()
		{
			while (Tiles.GetPercentageFloor() < Settings.desiredPercentageFloors)
			{
				for (var i = _walkers.Count - 1; i >= 0; i--)
				{
					var walker = _walkers[i];
					walker.Step();
				}
			}
			yield break;
		}

		public void CreateWalker(Vector2Int randomPositionInLevel)
		{
			if (_walkers.Count < _maxWalkers)
			{
				Walker w = new Walker(randomPositionInLevel, this);
				_walkers.Add(w);
			}
		}

		public void DestroyWalker(Walker walker, bool replace = true)
		{
			_walkers.Remove(walker);

			if (replace && _walkers.Count == 0)
			{
				CreateWalker(Settings.RandomPositionInLevel());
			}
		}
	}
}