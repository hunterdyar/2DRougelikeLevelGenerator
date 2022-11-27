using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RougeLevelGen
{
	public class SimpleCellularAutomata : Generator
	{
		public int repeat;
		
		//four-five rule
		public SimpleCellularAutomata(string layer, LevelGenerator levelGenerator, int repeat) : base(layer, levelGenerator)
		{
			if (repeat < 0)
			{
				repeat = 0;
			}
			this.repeat = repeat;
		}

		public override IEnumerator Generate()
		{
			LevelGenerator.ProgressStage = $"Starting Cellular Automata";

			for (int i = 0; i < repeat + 1; i++)
			{
				LevelGenerator.ProgressStage = $"Progess {i} of {repeat+1} of Cellular Automata";
				ProcessFourFive();
			}
			yield break;
		}

		//http://www.roguebasin.com/index.php?title=Cellular_Automata_Method_for_Generating_Random_Cave-Like_Levels
		private void ProcessFourFive()
		{
			var t = new Dictionary<Vector2Int,Tile>();//
			var posArray = LevelGenerator.GetTiles(_layer).Keys.ToArray();
			LevelGenerator.Progress = 0;
			float delta = 1f / posArray.Length;
			foreach (var pos in posArray)
			{
				int n = LevelGenerator.CountFloorNeighbors(_layer, pos, true);
				
				//w is number of wall neighbors
				int w = 8 - n;//this is true while we only have walls and floors.
				var c = GetTile(pos);
				//: a tile becomes a wall if it was a wall and 4 or more of its eight neighbors were walls,
				if (c == Tile.Wall && w >= 4)
				{
					t[pos] = Tile.Wall;
				}
				else if(c == Tile.Floor && w >= 5)
				{
					//if it was not a wall and 5 or more neighbors were.
					t[pos] = Tile.Wall;
				}
				else
				{
					t[pos] = Tile.Floor;
					//if it was not a wall and 5 or more neighbors were.
				}

				LevelGenerator.Progress += delta;
			}

			//Set tiles to our new copy array.
			foreach (var pos in posArray)
			{
				SetTile(pos,t[pos]);
			}
		}
	}
}