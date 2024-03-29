﻿using UnityEngine;

namespace HDyar.RougeLevelGen
{
	public enum GeneratorTypes
	{
		[InspectorName("Generators/Drunk Walk (Additive)")]
		DrunkWalk,
		[InspectorName("Modifiers/Noise")]
		Smooth,
		[InspectorName("Generators/Noise")]
		Noise,
		[InspectorName("Modifiers/Cellular Automata")]
		SimpleCellularAutomata,
		[InspectorName("Modifiers/Invert")]
		Invert,
		[InspectorName("Generators/Perlin Noise Threshold")]
		Perlin,
		[InspectorName("Generators/Level Edge Fill (Additive)")]
		LevelEdges,
		[InspectorName("Merge Layers")]
		Merge,
		[InspectorName("Generators/Solid Fill")]
		Fill,
		[InspectorName("Modifiers/Fill Islands")]
		FillIslands,
		[InspectorName("Generators/Poisson Disk Distribution")]
		PoissonDisk,
	}
}