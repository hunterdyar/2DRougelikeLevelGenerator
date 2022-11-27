using System;
using UnityEngine;

namespace RougeLevelGen
{
	//Lazy custom editor setup
	[Serializable]
	public struct GeneratorConfiguration
	{
		public string layer;
		public GeneratorTypes type;

		//Update the Editor UI depending on which type is selected...
		public int maxWalkers;
		[Range(0, 1)] public float chanceToSpawnNewWalker;
		[Range(0, 1)] public float chanceToDestroyWalker;
		[Range(0, 1)] public float desiredPercentageFloorFill;
		public int repeat;

		public Generator GetGenerator(LevelGenerator levelGenerator)
		{
			//There are so many clever-er ways do write this. That's a future problem.
			if (type == GeneratorTypes.DrunkWalk)
			{
				return new DrunkWalkGenerator(layer, levelGenerator, maxWalkers, chanceToSpawnNewWalker, chanceToDestroyWalker, desiredPercentageFloorFill);
			}else if (type == GeneratorTypes.Smooth)
			{
				return new SmoothGenerator(layer,levelGenerator, repeat);
			}

			return new DrunkWalkGenerator(layer, levelGenerator, maxWalkers, chanceToSpawnNewWalker, chanceToDestroyWalker, desiredPercentageFloorFill);
		}
	}
}