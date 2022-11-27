using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace RougeLevelGen
{
	public class Walker
	{
		private LevelGenSettings _settings => _generator.Settings;
		private DrunkWalkGenerator _generator;
		public Vector2Int position;
		public Vector2Int facingDir;
		private WalkMode _mode = WalkMode.OnlyWalkThroughWalls;
		private readonly Stack<Vector2Int> _history = new Stack<Vector2Int>();
		public Walker(Vector2Int position, DrunkWalkGenerator generator)
		{
			this.position = position;
			this._generator = generator;
			facingDir = Utility.RandomFacingDirection();
			_history.Push(this.position);
		}
		
		public void Step()
		{
			//maybe rotate?
			facingDir = facingDir.Rotate(_settings.RandomRotationDirection());
			
			//Maybe create walker
			if (_generator.ShouldWalkerSpawnWalker())
			{
				_generator.CreateWalker(position);
			}

			if (_generator.ShouldWalkerDestroy())
			{
				_generator.DestroyWalker(this);
			}
			
			//Check if we are moving out of bounds
			if (!_settings.IsInBounds(position + facingDir))
			{
				//if leaving bounds, turn around.
				facingDir = -facingDir;
			}
			
			//Move in facing Direction and stamp.
			if (_mode == WalkMode.SimpleDrunk)
			{
				position = position + facingDir;
			}else if (_mode == WalkMode.OnlyWalkThroughWalls)
			{
				bool doWalk = false;
				while (!doWalk)
				{
					if (_history.Count > 0)
					{
						var walkOptions = _generator.LevelLevelGenerator.GetNeighborPositions(_history.Peek(), false).Where(x => _generator.GetTile(x) == Tile.Wall).ToList();
						if (walkOptions.Count > 0)
						{
							position = walkOptions[Random.Range(0, walkOptions.Count)];
							doWalk = true;
						}
						else
						{
							_history.Pop();
						}
					}else
					{
							_generator.DestroyWalker(this);
							return; //end the while loop ANd dont push/stamp
					}
				}
			}

			_history.Push(position);
			Stamp();
		}
		
		public void Stamp()
		{
			_generator.SetTile(position, Tile.Floor);
		}
	}
}