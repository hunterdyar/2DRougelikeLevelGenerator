using System.Collections;
using System.Linq;
using UnityEngine;

namespace RougeLevelGen
{
	public class NoiseGenerator : Generator
	{
		public float _desiredPercentageFloors;

		public NoiseGenerator(string layer, LevelGenerator levelGenerator, float desiredPercentageFloors) : base(layer, levelGenerator)
		{
			_desiredPercentageFloors = Mathf.Clamp01(desiredPercentageFloors);
		}
		
		public override IEnumerator Generate()
		{
			var posArray = LevelGenerator.GetTiles(_layer).Keys.ToArray();
			int p = 0;
			int c = posArray.Length;
			LevelGenerator.ProgressStage = "Creating Noise";
			foreach (var pos in posArray)
			{
				SetTile(pos,RandomFloorOrWall(_desiredPercentageFloors));
				p++;
				LevelGenerator.Progress = p / (float)c;
			}

			LevelGenerator.ProgressStage = "Done with Noise";

			yield break;
		}
	}
}