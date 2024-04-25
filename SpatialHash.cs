using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A spatial hash implementation using a HashSet for storing objects within grid cells.
/// This class divides the space into uniform grid cells of a specified size and allows
/// efficient querying of objects based on their positions.
/// </summary>
[System.Serializable]
public class SpatialHash
{
    private Dictionary<string, HashSet<object>> idx;
    private int cellSize;

    public SpatialHash(float cellSize)
    {
        this.cellSize = (int)cellSize;
        this.idx = new Dictionary<string, HashSet<object>>();
    }

    public SpatialHash(double cellSize)
    {
        this.cellSize = (int)cellSize;
        this.idx = new Dictionary<string, HashSet<object>>();
    }

    public SpatialHash(int cellSize)
    {
        this.cellSize = cellSize;
        this.idx = new Dictionary<string, HashSet<object>>();
    }

    public int Count
    {
        get { return idx.Count; }
    }

    public ICollection Cells
    {
        get { return idx.Keys; }
    }

    public void Insert(Vector3 v, object obj)
    {
        var key = Key(v);

        if (idx.ContainsKey(key))
        {
            idx[key].Add(obj);
        } else
        {
            HashSet<object> cell = new HashSet<object>();
            cell.Add(obj);
            idx.Add(key, cell);
        }
    }

    public void Insert(FloatingPoint v, object obj)
    {
        var key = Key(v);

        if (idx.ContainsKey(key))
        {
            idx[key].Add(obj);
        }
        else
        {
            HashSet<object> cell = new HashSet<object>();
            cell.Add(obj);
            idx.Add(key, cell);
        }
    }

    public void Remove(Vector3 v, object obj)
    {
        var key = Key(v);

        if (idx.ContainsKey(key))
        {
            idx[key].Remove(obj);

            if (idx[key].Count == 0) // If the cell is empty after removal, remove it from the index
                idx.Remove(key);
        }
    }

    public void Remove(FloatingPoint v, object obj)
    {
        var key = Key(v);

        if (idx.ContainsKey(key))
        {
            idx[key].Remove(obj);

            if (idx[key].Count == 0) // If the cell is empty after removal, remove it from the index
                idx.Remove(key);
        }
    }

    public HashSet<object> Query(Vector3 v)
    {
        string key = Key(v);
        return idx.ContainsKey(key) ? idx[key] : new HashSet<object>();
    }

    public HashSet<object> Query(FloatingPoint v)
    {
        string key = Key(v);
        return idx.ContainsKey(key) ? idx[key] : new HashSet<object>();
    }

    public HashSet<object> QueryNearbyGrids(Vector3 position)
    {
        HashSet<object> nearbyObjects = new HashSet<object>();

        // Query the grid corresponding to the given position
        HashSet<object> objectsInCurrentGrid = Query(position);
        nearbyObjects.UnionWith(objectsInCurrentGrid);

        // Get the key of the grid corresponding to the given position
        string gridKey = Key(position);

        // Split the grid key into x, y, and z components
        string[] components = gridKey.Split(':');
        int x = int.Parse(components[0]);
        int y = int.Parse(components[1]);
        int z = int.Parse(components[2]);

        // Define offsets for neighboring grids
        int[] offsets = { -cellSize, 0, cellSize };

        // Iterate over neighboring offsets
        foreach (int xOffset in offsets)
        {
            foreach (int yOffset in offsets)
            {
                foreach (int zOffset in offsets)
                {
                    // Calculate the key of the neighboring grid
                    string neighboringKey = (x + xOffset) + ":" + (y + yOffset) + ":" + (z + zOffset);

                    // Skip the grid corresponding to the given position
                    if (neighboringKey == gridKey)
                        continue;

                    // Query the spatial hash to retrieve objects in the neighboring grid
                    HashSet<object> nearbyObjectsInGrid = Query(new Vector3(x + xOffset, y + yOffset, z + zOffset));

                    // Add the objects from the neighboring grid to the result
                    nearbyObjects.UnionWith(nearbyObjectsInGrid);
                }
            }
        }

        return nearbyObjects;
    }

    public HashSet<object> QueryNearbyGrids(FloatingPoint position)
    {
        HashSet<object> nearbyObjects = new HashSet<object>();

        // Query the grid corresponding to the given position
        HashSet<object> objectsInCurrentGrid = Query(position);
        nearbyObjects.UnionWith(objectsInCurrentGrid);

        // Get the key of the grid corresponding to the given position
        string gridKey = Key(position);

        // Split the grid key into x, y, and z components
        string[] components = gridKey.Split(':');
        int x = int.Parse(components[0]);
        int y = int.Parse(components[1]);
        int z = int.Parse(components[2]);

        // Define offsets for neighboring grids
        int[] offsets = { -cellSize, 0, cellSize };

        // Iterate over neighboring offsets
        foreach (int xOffset in offsets)
        {
            foreach (int yOffset in offsets)
            {
                foreach (int zOffset in offsets)
                {
                    // Calculate the key of the neighboring grid
                    string neighboringKey = (x + xOffset) + ":" + (y + yOffset) + ":" + (z + zOffset);

                    // Skip the grid corresponding to the given position
                    if (neighboringKey == gridKey)
                        continue;

                    // Query the spatial hash to retrieve objects in the neighboring grid
                    HashSet<object> nearbyObjectsInGrid = Query(new FloatingPoint(x + xOffset, y + yOffset, z + zOffset));

                    // Add the objects from the neighboring grid to the result
                    nearbyObjects.UnionWith(nearbyObjectsInGrid);
                }
            }
        }

        return nearbyObjects;
    }

    public HashSet<string> Keys(Vector3 v)
    {
        int o = cellSize / 2;
        HashSet<string> keys = new HashSet<string>
        {
            Key(new Vector3(v.x - o, v.y - 0, v.z - o)),
            Key(new Vector3(v.x - o, v.y - 0, v.z - 0)),
            Key(new Vector3(v.x - o, v.y - 0, v.z + o)),
            Key(new Vector3(v.x - 0, v.y - 0, v.z - o)),
            Key(new Vector3(v.x - 0, v.y - 0, v.z - 0)),
            Key(new Vector3(v.x - 0, v.y - 0, v.z + o)),
            Key(new Vector3(v.x + o, v.y - 0, v.z - o)),
            Key(new Vector3(v.x + o, v.y - 0, v.z - o)),
            Key(new Vector3(v.x + o, v.y - 0, v.z - 0)),
            Key(new Vector3(v.x - o, v.y - o, v.z - o)),
            Key(new Vector3(v.x - o, v.y - o, v.z - 0)),
            Key(new Vector3(v.x - o, v.y - o, v.z + o)),
            Key(new Vector3(v.x - 0, v.y - o, v.z - o)),
            Key(new Vector3(v.x - 0, v.y - o, v.z - 0)),
            Key(new Vector3(v.x - 0, v.y - o, v.z + o)),
            Key(new Vector3(v.x + o, v.y - o, v.z - o)),
            Key(new Vector3(v.x + o, v.y - o, v.z - o)),
            Key(new Vector3(v.x + o, v.y - o, v.z - 0)),
            Key(new Vector3(v.x - o, v.y + o, v.z - o)),
            Key(new Vector3(v.x - o, v.y + o, v.z - 0)),
            Key(new Vector3(v.x - o, v.y + o, v.z + o)),
            Key(new Vector3(v.x - 0, v.y + o, v.z - o)),
            Key(new Vector3(v.x - 0, v.y + o, v.z - 0)),
            Key(new Vector3(v.x - 0, v.y + o, v.z + o)),
            Key(new Vector3(v.x + o, v.y + o, v.z - o)),
            Key(new Vector3(v.x + o, v.y + o, v.z - o)),
            Key(new Vector3(v.x + o, v.y + o, v.z - 0))
        };
        return keys;
    }

    public HashSet<string> Keys(FloatingPoint v)
    {
        double o = cellSize / 2.0;
        HashSet<string> keys = new HashSet<string>
        {
            Key(new FloatingPoint(v.x - o, v.y - 0, v.z - o)),
            Key(new FloatingPoint(v.x - o, v.y - 0, v.z - 0)),
            Key(new FloatingPoint(v.x - o, v.y - 0, v.z + o)),
            Key(new FloatingPoint(v.x - 0, v.y - 0, v.z - o)),
            Key(new FloatingPoint(v.x - 0, v.y - 0, v.z - 0)),
            Key(new FloatingPoint(v.x - 0, v.y - 0, v.z + o)),
            Key(new FloatingPoint(v.x + o, v.y - 0, v.z - o)),
            Key(new FloatingPoint(v.x + o, v.y - 0, v.z - o)),
            Key(new FloatingPoint(v.x + o, v.y - 0, v.z - 0)),
            Key(new FloatingPoint(v.x - o, v.y - o, v.z - o)),
            Key(new FloatingPoint(v.x - o, v.y - o, v.z - 0)),
            Key(new FloatingPoint(v.x - o, v.y - o, v.z + o)),
            Key(new FloatingPoint(v.x - 0, v.y - o, v.z - o)),
            Key(new FloatingPoint(v.x - 0, v.y - o, v.z - 0)),
            Key(new FloatingPoint(v.x - 0, v.y - o, v.z + o)),
            Key(new FloatingPoint(v.x + o, v.y - o, v.z - o)),
            Key(new FloatingPoint(v.x + o, v.y - o, v.z - o)),
            Key(new FloatingPoint(v.x + o, v.y - o, v.z - 0)),
            Key(new FloatingPoint(v.x - o, v.y + o, v.z - o)),
            Key(new FloatingPoint(v.x - o, v.y + o, v.z - 0)),
            Key(new FloatingPoint(v.x - o, v.y + o, v.z + o)),
            Key(new FloatingPoint(v.x - 0, v.y + o, v.z - o)),
            Key(new FloatingPoint(v.x - 0, v.y + o, v.z - 0)),
            Key(new FloatingPoint(v.x - 0, v.y + o, v.z + o)),
            Key(new FloatingPoint(v.x + o, v.y + o, v.z - o)),
            Key(new FloatingPoint(v.x + o, v.y + o, v.z - o)),
            Key(new FloatingPoint(v.x + o, v.y + o, v.z - 0))
        };
        return keys;
    }

    public string Key(Vector3 v)
    {
        int x = (int)Mathf.Floor(v.x / cellSize) * cellSize;
        int y = (int)Mathf.Floor(v.y / cellSize) * cellSize;
        int z = (int)Mathf.Floor(v.z / cellSize) * cellSize;
        return x.ToString() + ":" + y.ToString() + ":" + z.ToString();
    }

    public string Key(FloatingPoint v)
    {
        double x = Math.Floor(v.x / cellSize) * cellSize;
        double y = Math.Floor(v.y / cellSize) * cellSize;
        double z = Math.Floor(v.z / cellSize) * cellSize;
        return x.ToString() + ":" + y.ToString() + ":" + z.ToString();
    }

    public Vector3 StringToVector3(string key)
    {
        string[] parts = key.Split(':');
        if (parts.Length != 3)
        {
            throw new ArgumentException("Invalid key format. Must be in the format 'x:y:z'");
        }

        float x = float.Parse(parts[0]);
        float y = float.Parse(parts[1]);
        float z = float.Parse(parts[2]);

        // Assuming cellSize is available in the scope of this method
        // If not, you need to provide it as a parameter or make it accessible in some other way
        x += cellSize / 2f;
        y += cellSize / 2f;
        z += cellSize / 2f;

        return new Vector3(x, y, z);
    }

    public FloatingPoint StringToFloatingPoint(string key)
    {
        string[] parts = key.Split(':');
        if (parts.Length != 3)
        {
            throw new ArgumentException("Invalid key format. Must be in the format 'x:y:z'");
        }

        double x = double.Parse(parts[0]);
        double y = double.Parse(parts[1]);
        double z = double.Parse(parts[2]);

        x += cellSize / 2.0;
        y += cellSize / 2.0;
        z += cellSize / 2.0;

        return new FloatingPoint(x, y, z);
    }
}
