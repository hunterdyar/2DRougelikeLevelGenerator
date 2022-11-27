using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RougeLevelGen
{
	[Serializable]
	public class DrunkWalkGenerator : Generator
	{

		private LevelGenerator _generator;
		private int _maxWalkers;
		private readonly List<Walker> _walkers = new List<Walker>();

		private float _chanceToSpawnNewWalker;
		private float _chanceToDestroyWalker;
		private float _desiredPercentageFloors;
		public DrunkWalkGenerator(string layer, LevelGenerator levelGenerator, int maxWalkers, float chanceToSpawnNewWalker, float chanceToDestroyWalker, float desiredPercentageFloorFill) : base(layer, levelGenerator)
		{
			this._maxWalkers = maxWalkers;
			this._chanceToSpawnNewWalker = chanceToSpawnNewWalker;
			this._chanceToDestroyWalker = chanceToDestroyWalker;
			this._desiredPercentageFloors = desiredPercentageFloorFill;
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
			while (Tiles.GetPercentageFloor() < _desiredPercentageFloors)
			{
				for (var i = _walkers.Count - 1; i >= 0; i--)
				{
					var walker = _walkers[i];
					walker.Step();
				}
			}
			yield break;
		}

		public bool ShouldWalkerSpawnWalker()
		{
			return Random.value < _chanceToSpawnNewWalker;
		}

		public bool ShouldWalkerDestroy()
		{
			return Random.value < _chanceToDestroyWalker;
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