﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace HDyar.RougeLevelGen
{
	public static class LevelGenExtension
	{
		public static float GetPercentageFloor(this Dictionary<Vector2Int, Tile> tiles)
		{
			float count = tiles.Count;
			float floorCount = tiles.Count(x => x.Value == Tile.Floor);
			return floorCount / count;
		}

		public static float GetPercentageWall(this Dictionary<Vector2Int, Tile> tiles)
		{
			float count = tiles.Count;
			float wallCount = tiles.Count(x => x.Value == Tile.Wall);
			return wallCount / count;
		}

		public static Dictionary<Vector2Int, Tile> Clone(this Dictionary<Vector2Int, Tile> tiles)
		{
			Dictionary<Vector2Int, Tile> clone = new Dictionary<Vector2Int, Tile>(tiles);
			return clone;
		}


		public static Vector2Int Rotate(this Vector2Int v, RotationDirection direction)
		{
			if (direction == RotationDirection.None)
			{
				return v;
			}
			
			if (v == Vector2Int.down)
			{
				v = direction == RotationDirection.Clockwise ? Vector2Int.left : Vector2Int.right;
			}else if (v == Vector2Int.up)
			{
				v = direction == RotationDirection.Clockwise ? Vector2Int.right : Vector2Int.left;
			}
			else if (v == Vector2Int.right)
			{
				v = direction == RotationDirection.Clockwise ? Vector2Int.down : Vector2Int.up;
			}
			else if (v == Vector2Int.left)
			{
				v = direction == RotationDirection.Clockwise ? Vector2Int.up : Vector2Int.down;
			}

			return v;
		}
		
	}
}