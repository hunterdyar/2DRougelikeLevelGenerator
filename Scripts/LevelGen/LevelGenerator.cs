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
        public LevelGenSettings Settings => _settings;
        [SerializeField] private LevelGenSettings _settings;
        [SerializeField] private bool generateOnStart;
        #if UNITY_EDITOR
        [SerializeField] private bool debugWatch;
        #endif
        public GameObject WallPrefab;
        private List<GameObject> _createdObjects = new List<GameObject>();
        private List<Walker> _walkers = new List<Walker>();
        private readonly Dictionary<Vector2Int, Tile> _tiles = new Dictionary<Vector2Int, Tile>();
        
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
            ClearExistingPrefabs();
            InitiateEmptyTiles();
            
            //create first walker
            _walkers.Clear();
            CreateWalker(_settings.RandomPositionInLevel());
            
            while (_tiles.GetPercentageFloor() < _settings.desiredPercentageFloors)
            {
                for (var i = _walkers.Count - 1; i >= 0; i--)
                {
                    var walker = _walkers[i];
                    walker.Step();
                }

#if UNITY_EDITOR
                if (debugWatch)
                {
                    yield return null;
                    ClearExistingPrefabs();
                    InstantiatePrefabs();
                }
#endif
            }

            //Smooth
            for (int i = 0; i < _settings.SmoothCount; i++)
            {
                Smooth();
#if UNITY_EDITOR
                if (debugWatch)
                {
                    yield return null;
                    ClearExistingPrefabs();
                    InstantiatePrefabs();
                }
#endif
            }
            
            // yield return null;
            InstantiatePrefabs();
        }

        private void Smooth()
        {
            //iterating through each cell/
            //removing wall cells with 4 or more orthogonally or diagonally adjacent non-wall cells.
            for (int i = 0; i < _settings.LevelWidth; i++)
            {
                for (int j = 0; j < _settings.LevelHeight; j++)
                {
                    var pos = new Vector2Int(i, j);
                    if (_tiles[pos] == Tile.Wall)
                    {
                        var c = CountFloorNeighbors(pos,true);
                        if (c > 4)
                        {
                            _tiles[pos] = Tile.Floor;
                        }
                    }
                }
            }
        }

        private int CountFloorNeighbors(Vector2Int pos, bool includeDiagonal)
        {
            var neighbors = GetNeighbors(pos, includeDiagonal);
            return neighbors.Count(x => _tiles[x] == Tile.Floor);
        }

        private List<Vector2Int> GetNeighbors(Vector2Int pos, bool includeDiagonal)
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

        public void CreateWalker(Vector2Int randomPositionInLevel)
        {
            if (_walkers.Count < _settings.MaxWalkers)
            {
                Walker w = new Walker(randomPositionInLevel, this);
                _walkers.Add(w);
            }
        }

        public void DestroyWalker(Walker walker)
        {
            if (_walkers.Count > 1)
            {
                _walkers.Remove(walker);
            }
        }

        private void InstantiatePrefabs()
        {
            foreach (var t in _tiles)
            {
                if (t.Value == Tile.Wall)
                {
                    InstantiatePrefab(t.Key, t.Value);
                }
            }
        }

        private void InstantiatePrefab(Vector2Int gridPos, Tile tile)
        {
            if (tile == Tile.Wall)
            {
                _createdObjects.Add(Instantiate(WallPrefab, _settings.GridToWorld(gridPos), Quaternion.identity,_settings.CreatedParent));
            }
        }

        [ContextMenu("Clear Existing")]
        private void ClearExistingPrefabs()
        {
            foreach (var go in _createdObjects)
            {
                //if in editor mode...
                DestroyImmediate(go);
            }

            _createdObjects = new List<GameObject>();
        }

        void InitiateEmptyTiles()
        {
            _tiles.Clear();
            //Create a grid of walls.
            for (int i = 0; i < _settings.LevelWidth; i++)
            {
                for (int j = 0; j < _settings.LevelHeight; j++)
                {
                    var pos = new Vector2Int(i, j);
                    _tiles[pos] = Tile.Wall;
                }
            }
        }

        public void SetTile(Vector2Int position, Tile floor)
        {
            if (_tiles.ContainsKey(position))
            {
                _tiles[position] = floor;
            }
            else
            {
                Debug.LogWarning("Can't set tile here.");
            }
        }

        
    }
}
