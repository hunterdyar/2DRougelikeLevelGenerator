using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering.Universal;
using UnityEngine;

namespace RougeLevelGen
{
    public class LevelGenerator : MonoBehaviour
    {
        public static bool DebugWatch = true;
        public LevelGenSettings Settings => _settings;
        [SerializeField] private LevelGenSettings _settings;
        [SerializeField] private bool generateOnStart;
        
        public GeneratorConfiguration[] _generationLayersSetup;
        private Dictionary<string, Dictionary<Vector2Int, Tile>> _generationLayers = new Dictionary<string, Dictionary<Vector2Int, Tile>>();
        private List<Generator> _generators;

        [SerializeField] private List<Builder> _builders;
        
        [ContextMenu("Generate")]
        public void Generate()
        {
            StartCoroutine(DoGeneration());
        }
        
        private void Start()
        {
            if (generateOnStart)
            {
                Generate();
            }
        }
        private IEnumerator DoGeneration()
        {
            
            //Setup
            InitiateEmptyTiles();
            
            //Inititate
            InitiateGenerators();
            
           //Generate
            foreach(Generator g in _generators)
            {
                yield return StartCoroutine(g.Generate());
                //todo: generation layers. A dictionary of dictionaries.
            }
            
            //build layers
            foreach (var builder in _builders)
            {
                builder.Initiate(this);
                builder.Build();
            }
        }

        private void InitiateGenerators()
        {
            _generators = new List<Generator>();

            foreach (var gc in _generationLayersSetup)
            {
                var g = gc.GetGenerator(this);
                _generators.Add(g);
                g.Initiate();
            }
        }

        public int CountFloorNeighbors(string layer, Vector2Int pos, bool includeDiagonal)
        {
            var neighbors = GetNeighborPositions(pos, includeDiagonal);
            return neighbors.Count(x => _generationLayers[layer][x] == Tile.Floor);
        }

        public List<Vector2Int> GetNeighborPositions(Vector2Int pos, bool includeDiagonal)
        {
            List<Vector2Int> positions = new List<Vector2Int>();
            var array = includeDiagonal ? Utility.EightDirections : Utility.CardinalDirections;
            foreach (var delta in array)
            {
                if (_settings.IsInBounds(pos + delta))
                {
                    positions.Add(pos+delta);
                }
            }
            return positions;
        }
        
        [ContextMenu("Clear Existing")]
       

        public string[] GetGenerationLayers()
        {
            //HashSets force uniqueness.
            HashSet<string> layers = new HashSet<string>();
            foreach (var gc in _generationLayersSetup)
            {
                layers.Add(gc.layer);
            }

            return layers.ToArray();
        }
        void InitiateEmptyTiles()
        {
            foreach (var key in GetGenerationLayers())
            {
                _generationLayers[key] = new Dictionary<Vector2Int, Tile>();
                
                //Create a grid of walls.
                for (int i = 0; i < _settings.LevelWidth; i++)
                {
                    for (int j = 0; j < _settings.LevelHeight; j++)
                    {
                        var pos = new Vector2Int(i, j);
                        _generationLayers[key][pos] = Tile.Wall;
                    }
                }
            }
        }

        public void SetTile(string layer, Vector2Int position, Tile floor)
        {
            if (_generationLayers[layer].ContainsKey(position))
            {
                _generationLayers[layer][position] = floor;
            }
            else
            {
                Debug.LogWarning("Can't set tile here.");
            }
        }

        public Tile GetTile(string layer, Vector2Int pos)
        {
            if (_generationLayers[layer].TryGetValue(pos, out var tile))
            {
                return tile;
            }
            else
            {
                //OOPS
                return Tile.Wall;
            }
        }

        public Dictionary<Vector2Int, Tile> GetTiles(string layer)
        {
            return _generationLayers[layer];
        }
    }
}
