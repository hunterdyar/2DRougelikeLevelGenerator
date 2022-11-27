using System;
using System.Collections;
using Random = UnityEngine.Random;

namespace RougeLevelGen
{
	public class PerlinNoiseThreshold : Generator
	{
		
		public PerlinNoiseThreshold(string layer, LevelGenerator levelGenerator) : base(layer, levelGenerator)
		{
		}

		//Todo: Take perlin noise and keep those above a certain threshold walls, the rest floors.
		public override IEnumerator Generate()
		{
			LevelGenerator.ProgressStage = "Done with Perlin";
			return base.Generate();
		}
	}
}