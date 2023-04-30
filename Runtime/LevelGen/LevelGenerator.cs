using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HDyar.RougeLevelGen
{
    public class LevelGenerator : MonoBehaviour
    {
        //statics
        public static System.Random Random;
        public static bool DebugWatch = true;
        public static string ProgressStage;
        
        //serialized
        public LevelGenSettings Settings => _settings;
        [SerializeField] private LevelGenSettings _settings;
        [SerializeField] private bool generateOnStart;
        public GeneratorConfiguration[] _generationLayersSetup;
        [SerializeField] private List<Builder> _builders;
        
        //private
        private Dictionary<string, Dictionary<Vector2Int, Tile>> _generationLayers = new Dictionary<string, Dictionary<Vector2Int, Tile>>();
        private List<Generator> _generators;
        private Coroutine _generationCoroutine;
        
        [ContextMenu("Generate")]
        public void Generate()
        {
            Cancel();
            _generationCoroutine = StartCoroutine(DoGeneration());
        }

        [ContextMenu("Cancel")]
        public void Cancel()
        {
            if (_generationCoroutine != null)
            {
                StopCoroutine(_generationCoroutine);
                ProgressStage = "Cancelled";
            }
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
            ProgressStage = "Initializing";
            if(string.IsNullOrEmpty(Settings.seed))
            {
                //todo: write my own noise class :p
                Random = new System.Random();
            }else
            {
                Random = new System.Random(Settings.seed.GetHashCode());
                
            }
            //Setup
            InitiateEmptyTiles();
            
            //Inititate
            InitiateGenerators();
            
           //Generate
            foreach(Generator g in _generators)
            {
                ProgressStage = g.ToString();
                yield return StartCoroutine(g.Generate());
            }

            ProgressStage = "Building";

            //build layers
            foreach (var builder in _builders)
            {
                builder.Initiate(this);
                builder.Build();
            }

            ProgressStage = "Complete";

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

        public bool HasLayer(string layer)
        {
            return GetGenerationLayers().Contains(layer);
        }

        //QoL convenience
        [ContextMenu("Clear Children")]
        private void ClearChildrenObjects()
        {
            foreach (var builder in _builders)
            {
                builder.DestroyAllImmediate();
            }
            foreach (Transform child in transform)
            {
                DestroyImmediate(child.gameObject);
            }
        }
    }
}
