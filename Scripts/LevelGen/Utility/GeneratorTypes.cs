using UnityEngine;

namespace RougeLevelGen
{
	public enum GeneratorTypes
	{
		DrunkWalk,
		Smooth,
		Noise,
		[InspectorName("Cellular Automata")]
		SimpleCellularAutomata,
		Invert,
		[InspectorName("Perlin Noise Threshold")]
		Perlin
	}
}