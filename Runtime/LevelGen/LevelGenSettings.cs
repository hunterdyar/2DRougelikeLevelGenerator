using System;
using UnityEngine;

namespace HDyar.RougeLevelGen
{
	[Serializable]
	public struct LevelGenSettings
	{
		public string seed;
		[Min(0)]
		public int LevelWidth;
		[Min(0)]
		public int LevelHeight;

		public Swizzle swizzle;

		[Tooltip("Applied After Swizzle")]
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

		/// <summary>
		///  Converts a grid space (Vector2Int arbitrary space) to World space (scaled, swizzled, and offset).
		/// </summary>
		/// <param name="gridPos">Input position in grid space.</param>
		/// <param name="swizzle">Puzzle generation XY coordinates converted to these coordinates. Default to XY.</param>
		/// <param name="planePos">Position on non-generated axis. If generation is XY, this is the z position in world space, before global offset.</param>
		/// <returns></returns>
		public Vector3 GridToWorld(Vector2Int gridPos, float planePos = 0)
		{
			switch (swizzle)
			{
				case Swizzle.XY:
				default:
					return new Vector3(gridPos.x, gridPos.y, planePos) + globalSpawnOffset;
				case Swizzle.XZ:
					return new Vector3(gridPos.x, planePos, gridPos.y) + globalSpawnOffset;
				case Swizzle.YX:
					return new Vector3(gridPos.y, gridPos.x, planePos) + globalSpawnOffset;
				case Swizzle.YZ:
					return new Vector3(planePos, gridPos.x,gridPos.y) + globalSpawnOffset;
				case Swizzle.ZX:
					return new Vector3(gridPos.y, planePos, gridPos.x) + globalSpawnOffset;
				case Swizzle.ZY:
					return new Vector3(planePos, gridPos.y, gridPos.x) + globalSpawnOffset;
			}
			
		}
	}
}