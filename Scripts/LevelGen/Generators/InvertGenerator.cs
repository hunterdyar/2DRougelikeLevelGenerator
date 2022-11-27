using System.Collections;
using System.Linq;

namespace RougeLevelGen
{
	public class InvertGenerator : Generator
	{
		public InvertGenerator(string layer, LevelGenerator levelGenerator) : base(layer, levelGenerator)
		{
		}

		//Todo: 
		public override IEnumerator Generate()
		{
			var keys = LevelGenerator.GetTiles(_layer).Keys.ToArray();
			foreach (var pos in keys)
			{
				if (GetTile(pos) == Tile.Floor)
				{
					SetTile(pos,Tile.Wall);
				}else if (GetTile(pos) == Tile.Wall)
				{
					SetTile(pos, Tile.Floor);
				}//While I don't know why, I anticipate adding more tile types, so elseif instead of else.
			}
			return base.Generate();
		}
	}
}