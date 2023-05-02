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

		//todo: add dependency on grid to package information.
		[Tooltip("Unity Grid component, used for world/cell mapping.")]
		public Grid grid;

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
		///  Converts a grid space (Vector2Int arbitrary space) to World space. Wrapper for the Unity Grid's cell-to-world function.
		/// </summary>
		/// <param name="gridPos">Input position in grid space.</param>
		/// <returns></returns>
		public Vector3 GridToWorld(Vector2Int gridPos)
		{
			return grid.CellToWorld((Vector3Int)gridPos);
			// switch (swizzle)
			// {
			// 	case Swizzle.XY:
			// 	default:
			// 		return new Vector3(gridPos.x, gridPos.y, planePos) + globalSpawnOffset;
			// 	case Swizzle.XZ:
			// 		return new Vector3(gridPos.x, planePos, gridPos.y) + globalSpawnOffset;
			// 	case Swizzle.YX:
			// 		return new Vector3(gridPos.y, gridPos.x, planePos) + globalSpawnOffset;
			// 	case Swizzle.YZ:
			// 		return new Vector3(planePos, gridPos.x,gridPos.y) + globalSpawnOffset;
			// 	case Swizzle.ZX:
			// 		return new Vector3(gridPos.y, planePos, gridPos.x) + globalSpawnOffset;
			// 	case Swizzle.ZY:
			// 		return new Vector3(planePos, gridPos.y, gridPos.x) + globalSpawnOffset;
			// }

		}
	}
}