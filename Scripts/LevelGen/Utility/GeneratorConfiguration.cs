using System;

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
		public int repeat;

		public Generator GetGenerator(LevelGenerator levelGenerator)
		{
			//There are so many clever-er ways do write this. That's a future problem.
			if (type == GeneratorTypes.DrunkWalk)
			{
				return new DrunkWalkGenerator(layer, levelGenerator, maxWalkers);
			}else if (type == GeneratorTypes.Smooth)
			{
				return new SmoothGenerator(layer,levelGenerator, repeat);
			}

			return new DrunkWalkGenerator(layer, levelGenerator, maxWalkers);
		}
	}
}