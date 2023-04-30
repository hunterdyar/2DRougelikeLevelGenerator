using System.Collections;
using System.Linq;
using UnityEngine;

namespace HDyar.RougeLevelGen
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
			LevelGenerator.ProgressStage = "Creating Noise";
			foreach (var pos in posArray)
			{
				SetTile(pos,RandomFloorOrWall(_desiredPercentageFloors));
			}

			LevelGenerator.ProgressStage = "Done with Noise";

			yield break;
		}
	}
}