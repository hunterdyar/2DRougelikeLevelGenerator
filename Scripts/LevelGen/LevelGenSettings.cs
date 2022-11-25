using System;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RougeLevelGen
{
	[Serializable]
	public struct LevelGenSettings
	{
		[Min(0)]
		public int LevelWidth;
		[Min(0)]
		public int LevelHeight;

		[Min(1)] public int MaxWalkers;
		[Min(0)] public int SmoothCount;
		public Vector3 spawnOffset;
		public Transform CreatedParent;
		[Range(0,1)]
		public float desiredPercentageFloors;

		[Range(0, 1)] public float chanceToSpawnNewWalker;
		[Range(0, 1)] public float chanceToDestroyWalker;		
		public Vector2Int RandomPositionInLevel()
		{
			return new Vector2Int(Random.Range(0, LevelWidth), Random.Range(0, LevelHeight));
		}

		public bool IsInBounds(Vector2Int pos)
		{
			return pos.x >= 0 && pos.x < LevelWidth && pos.y >= 0 && pos.y < LevelHeight;
		}

		public RotationDirection RandomRotationDirection()
		{
			switch (Random.Range(0, 3))
			{
				case 0: return RotationDirection.None;
				case 1: return RotationDirection.Clockwise;
				case 2: return RotationDirection.Counterclockwise;
			}

			return RotationDirection.None;
		}

		public Vector3 GridToWorld(Vector2Int gridPos)
		{
			return new Vector3(gridPos.x, gridPos.y, 0)+spawnOffset;
		}

		public bool ShouldWalkerSpawnWalker()
		{
			return Random.value < chanceToSpawnNewWalker;
		}

		public bool ShouldWalkerDestroy()
		{
			return Random.value < chanceToDestroyWalker;
		}
	}
}