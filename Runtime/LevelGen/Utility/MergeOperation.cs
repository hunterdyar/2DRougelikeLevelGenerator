using UnityEngine;

namespace HDyar.RougeLevelGen
{
	public enum MergeOperation
	{
		[InspectorName("Union Floors-Intersect Walls (Floor or Floor)")]
		UnionFloors,
		[InspectorName("Union Walls-Intersect Floors (Wall or Wall)")]
		UnionWalls,
		[InspectorName("Difference (Layer Floor where other floor)")]
		Difference,
	}
}