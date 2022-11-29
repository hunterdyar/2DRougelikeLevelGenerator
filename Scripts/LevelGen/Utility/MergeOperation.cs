using UnityEngine;

namespace RougeLevelGen
{
	public enum MergeOperation
	{
		[InspectorName("Merge (Floor or Floor)")]
		Intersect,
		
		[InspectorName("Difference (Only both floor)")]
		Difference,
		Union,//a and b
	}
}