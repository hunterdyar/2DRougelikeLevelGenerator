using UnityEngine;

namespace RougeLevelGen
{
	public enum MergeOperation
	{
		[InspectorName("Union Floors-Intersect Walls (Floor or Floor)")]
		UnionFloors,
		[InspectorName("Union Walls-Intersect Floors (Wall or Wall)")]
		UnionWalls,
		[InspectorName("Difference (Layer Floor only were other also floor)")]
		Difference,
	}
}