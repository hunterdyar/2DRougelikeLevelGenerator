using UnityEditor;
using UnityEngine;

namespace RougeLevelGen
{
	public class Walker
	{
		private LevelGenSettings settings => _generator.Settings;
		private LevelGenerator _generator;
		public Vector2Int position;
		public Vector2Int facingDir;

		public Walker(Vector2Int position, LevelGenerator generator)
		{
			this.position = position;
			this._generator = generator;
			facingDir = Utility.RandomFacingDirection();
		}
		
		public void Step()
		{
			//maybe rotate?
			facingDir = facingDir.Rotate(settings.RandomRotationDirection());
			
			//Maybe create walker
			if (settings.ShouldWalkerSpawnWalker())
			{
				_generator.CreateWalker(position);
			}

			if (settings.ShouldWalkerDestroy())
			{
				_generator.DestroyWalker(this);
			}
			
			//Check if we are moving out of bounds
			if (!settings.IsInBounds(position + facingDir))
			{
				//if leaving bounds, turn around.
				facingDir = -facingDir;
			}
			
			//Move in facing Direction and stamp.
			position = position + facingDir;
			Stamp();
		}
		
		public void Stamp()
		{
			_generator.SetTile(position, Tile.Floor);
		}
	}
}