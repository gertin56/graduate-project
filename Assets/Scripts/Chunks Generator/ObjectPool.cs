using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]private Chunk[] _chunks;

    private List<Chunk> _pool = new List<Chunk>();

    protected void Initialize()
    {
        foreach (var chunk in _chunks)
        {
            Chunk spawned = Instantiate(chunk);
            spawned.gameObject.SetActive(false);
            _pool.Add(spawned);
        }
    }

    protected bool TryGetChunk(out Chunk result)
    {
        var inactiveChunks = GetInactiveChunks();

        if (inactiveChunks != null && inactiveChunks.Count > 0)
        {
            result =  inactiveChunks[Random.Range(0, inactiveChunks.Count - 1)];
            return true;
        }
        else
        {
            result = null;
            return false;
        }
    }

    protected void PoolReset()
    {
        foreach(var chunk in _pool)
        {
            chunk.gameObject.SetActive(false);
        }
    }

    private List<Chunk> GetInactiveChunks()
    {
        return _pool.Where(x => x.gameObject.activeSelf == false).ToList();
    }
}
