using System;
using UnityEngine;

namespace RougeLevelGen
{
	[Serializable]
	public struct LevelGenSettings
	{
		public string seed;
		[Min(0)]
		public int LevelWidth;
		[Min(0)]
		public int LevelHeight;
		
		public Vector3 globalSpawnOffset;

		
		public Vector2Int RandomPositionInLevel()
		{
			return new Vector2Int(LevelGenerator.Random.Next(0, LevelWidth), LevelGenerator.Random.Next(0, LevelHeight));
		}

		public bool IsInBounds(Vector2Int pos)
		{
			return pos.x >= 0 && pos.x < LevelWidth && pos.y >= 0 && pos.y < LevelHeight;
		}

		public RotationDirection RandomRotationDirection()
		{
			switch (LevelGenerator.Random.Next(0, 3))
			{
				case 0: return RotationDirection.None;
				case 1: return RotationDirection.Clockwise;
				case 2: return RotationDirection.Counterclockwise;
			}

			return RotationDirection.None;
		}

		public Vector3 GridToWorld(Vector2Int gridPos)
		{
			return new Vector3(gridPos.x, gridPos.y, 0)+globalSpawnOffset;
		}
	}
}