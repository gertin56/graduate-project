using UnityEngine;
using UnityEngine.Events;

public class Chunk : MonoBehaviour
{
    [SerializeField] private LastWall _chunkEnd;
    [SerializeField] private Transform _chunkBegin;

    public Transform ChunkEnd => _chunkEnd.transform;
    public Transform ChunkBegin => _chunkBegin.transform;

    public event UnityAction<Chunk> Passed;

    private void OnEnable()
    {
        _chunkEnd.Reached += OnChunkendReached;
    }

    private void OnChunkendReached()
    {
        Passed?.Invoke(this);
    }

    private void OnDisable()
    {
        _chunkEnd.Reached -= OnChunkendReached;
    }
}
