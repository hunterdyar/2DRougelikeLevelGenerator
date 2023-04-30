using System.Collections;
using System.Linq;

namespace HDyar.RougeLevelGen
{
	public class InvertGenerator : Generator
	{
		public InvertGenerator(string layer, LevelGenerator levelGenerator) : base(layer, levelGenerator)
		{
		}

		public override IEnumerator Generate()
		{
			LevelGenerator.ProgressStage = "Invert";
			var keys = LevelGenerator.GetTiles(_layer).Keys.ToArray();
			foreach (var pos in keys)
			{
				//While I don't know actively plan to, I anticipate possibly having more tile types, perhaps to help mark edges. so elseif instead of else. This could be a one-line ternary, but whatever
				if (GetTile(pos) == Tile.Floor)
				{
					SetTile(pos,Tile.Wall);
				}else if (GetTile(pos) == Tile.Wall)
				{
					SetTile(pos, Tile.Floor);
				}
			}
			return base.Generate();
		}
	}
}