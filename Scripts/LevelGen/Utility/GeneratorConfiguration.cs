﻿using System;
using UnityEngine;

namespace RougeLevelGen
{
	//Lazy custom editor setup
	[Serializable]
	public class GeneratorConfiguration
	{
		public string layer = "base";
		public GeneratorTypes type;

		//Update the Editor UI depending on which type is selected...
		[Min(1)]
		public int maxWalkers = 5;
		[Range(0, 1)] public float chanceToSpawnNewWalker = 0.01f;
		[Range(0, 1)] public float chanceToDestroyWalker = 0.01f;
		[Range(0, 1)] public float desiredPercentageFloorFill = 0.51f;
		public int repeat = 0;

		public Generator GetGenerator(LevelGenerator levelGenerator)
		{
			//There are so many clever-er ways do write this. That's a future problem.
			if (type == GeneratorTypes.DrunkWalk)
			{
				return new DrunkWalkGenerator(layer, levelGenerator, maxWalkers, chanceToSpawnNewWalker, chanceToDestroyWalker, desiredPercentageFloorFill);
			}else if (type == GeneratorTypes.Smooth)
			{
				return new SmoothGenerator(layer,levelGenerator, repeat);
			}else if (type == GeneratorTypes.Noise)
			{
				return new NoiseGenerator(layer, levelGenerator, desiredPercentageFloorFill);
			}else if (type == GeneratorTypes.SimpleCellularAutomata)
			{
				return new SimpleCellularAutomata(layer, levelGenerator, repeat);
			}

			return new DrunkWalkGenerator(layer, levelGenerator, maxWalkers, chanceToSpawnNewWalker, chanceToDestroyWalker, desiredPercentageFloorFill);
		}
	}
}