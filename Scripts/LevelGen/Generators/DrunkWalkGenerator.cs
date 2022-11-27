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
			//turns out this will crash unity lol
			if (maxWalkers < 1)
			{
				maxWalkers = 1;
			}
			
			this._maxWalkers = maxWalkers;
			this._chanceToSpawnNewWalker = Mathf.Clamp01(chanceToSpawnNewWalker);
			this._chanceToDestroyWalker = Mathf.Clamp01(chanceToDestroyWalker);
			this._desiredPercentageFloors = Mathf.Clamp01(desiredPercentageFloorFill);
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
			float p = Tiles.GetPercentageFloor();
			while (p < _desiredPercentageFloors)
			{
				for (var i = _walkers.Count - 1; i >= 0; i--)
				{
					var walker = _walkers[i];
					walker.Step();
				}

				p = Tiles.GetPercentageFloor();
				LevelGenerator.Progress = p / _desiredPercentageFloors;
			}
			yield break;
		}

		public bool ShouldWalkerSpawnWalker()
		{
			return LevelGenerator.Random.NextDouble() < _chanceToSpawnNewWalker;
		}

		public bool ShouldWalkerDestroy()
		{
			return LevelGenerator.Random.NextDouble() < _chanceToDestroyWalker;
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