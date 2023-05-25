using System.Collections.Generic;
using UnityEngine;

public class WallsGenerator : ObjectPool
{
    [SerializeField] private Chunk _startChunk;
    [SerializeField] private int _chunkBufferSize;
    [SerializeField] private Player _player;

    private List<Chunk> _chunkBuffer = new List<Chunk>();
    private Chunk _currentChunk;

    private void Start()
    {
        Initialize();
        Restart();
    }

    private void Restart()
    {
        PoolReset();
        _chunkBuffer.Clear();
        _currentChunk = _startChunk;

        for (int i = 0; i < _chunkBufferSize - 1; i++)
        {
            _chunkBuffer.Add(GenerateNextChunk());
        }

        Camera.main.transform.position = _startChunk.ChunkBegin.position + new Vector3(0, 0, -10);
        _player.transform.position = _startChunk.ChunkBegin.position;
    }

    private Chunk GenerateNextChunk()
    {
        if(TryGetChunk(out Chunk nextChunk))
        {
            nextChunk.transform.position = _currentChunk.ChunkEnd.position - nextChunk.ChunkBegin.localPosition;
            nextChunk.gameObject.SetActive(true);
            _currentChunk = nextChunk;
            _currentChunk.Passed += OnChunkPassed;
            return nextChunk;
        }
        else
        {
            return null;
        }
    }

    private void OnChunkPassed(Chunk current)
    {
        _chunkBuffer.Add(GenerateNextChunk());
        current.Passed -= OnChunkPassed;

        if(_chunkBuffer.Count > _chunkBufferSize)
        {
            RemoveChunk();
        }
    }

    private void RemoveChunk()
    {
        _chunkBuffer[0].gameObject.SetActive(false);
        _chunkBuffer.RemoveAt(0);
    }

    private void OnEnable()
    {
        _player.Dead += OnPlayerDead;
    }

    private void OnDisable()
    {
        _player.Dead -= OnPlayerDead;
    }

    private void OnPlayerDead()
    {
        Restart();
    }
}
