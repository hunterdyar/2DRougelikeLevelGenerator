using UnityEngine;

namespace RougeLevelGen
{
	public class Utility
	{
		public static Vector2Int RandomFacingDirection()
		{
			switch (Random.Range(0, 4))
			{
				case 0: return Vector2Int.up;
				case 1: return Vector2Int.right;
				case 2: return Vector2Int.down;
				case 3: return Vector2Int.left;
			}

			return Vector2Int.zero;
		}

		public static Vector2Int[] CardinalDirections = new[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left};
		public static Vector2Int[] EightDirections = new[] { Vector2Int.up, Vector2Int.right, Vector2Int.down, Vector2Int.left, Vector2Int.one, -Vector2Int.one,new Vector2Int(1,-1),new Vector2Int(-1,1)};
	}
}