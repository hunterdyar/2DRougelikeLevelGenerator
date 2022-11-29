using UnityEngine;

namespace RougeLevelGen
{
	public enum GeneratorTypes
	{
		[InspectorName("Generator/Drunk Walk (Additive)")]
		DrunkWalk,
		[InspectorName("Modifiers/Noise")]
		Smooth,
		[InspectorName("Generator/Noise")]
		Noise,
		[InspectorName("Generator/Cellular Automata")]
		SimpleCellularAutomata,
		[InspectorName("Modifiers/Invert")]
		Invert,
		[InspectorName("Generator/Perlin Noise Threshold")]
		Perlin,
		[InspectorName("Generator/Level Edge Fill (Additive)")]
		LevelEdges,
		[InspectorName("Merge Layers")]
		Merge,
		[InspectorName("Generator/Solid Fill")]
		Fill
	}
}