using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HDyar.RougeLevelGen
{
	[Serializable]
	public class Builder
	{
		//todo: auto set layer correctly if only one in generator.
		public string layer;
		public Transform parent;
		public Vector3 offset;
		private LevelGenerator _generator;
		
		public GameObject WallPrefab;
		public GameObject FloorPrefab;
		
		private List<GameObject> _createdObjects = new List<GameObject>();


		public void Initiate(LevelGenerator generator)
		{
			_generator = generator;
			ClearExistingPrefabs();
		}

		private void ClearExistingPrefabs()
		{
			foreach (var go in _createdObjects)
			{
				//if in editor mode...
				Object.DestroyImmediate(go);
			}

			_createdObjects = new List<GameObject>();
		}
		public virtual void Build()
		{
			InstantiatePrefabs();
		}
		private void InstantiatePrefabs()
		{
			if (!_generator.HasLayer(layer))
			{
				Debug.LogError("Can't build for layer: "+layer+". Layer doesn't exist.");
				return;
			}

			var tiles = _generator.GetTiles(layer);
			foreach (var t in tiles)
			{
				InstantiatePrefab(t.Key, t.Value);
			}
		}

		private void InstantiatePrefab(Vector2Int gridPos, Tile tile)
		{
			if (tile == Tile.Wall && WallPrefab != null)
			{
				_createdObjects.Add(GameObject.Instantiate(WallPrefab, _generator.Settings.GridToWorld(gridPos)+offset, Quaternion.identity, parent));
			}else if (tile == Tile.Floor && FloorPrefab != null)
			{
				_createdObjects.Add(GameObject.Instantiate(FloorPrefab, _generator.Settings.GridToWorld(gridPos)+offset, Quaternion.identity, parent));
			}
		}

		public void DestroyAllImmediate()
		{
			ClearExistingPrefabs();
		}
	}
}