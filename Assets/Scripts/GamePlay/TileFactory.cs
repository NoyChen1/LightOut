using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class TileFactory
{
    private readonly int _gridSize;
    private readonly Transform _parent;
    private ObjectPool<Tile> _pool;

    public TileFactory(int gridSize, Transform parent)
    {
        _gridSize = gridSize;
        _parent = parent;
    }

    public async UniTask InitAsync(CancellationToken ct)
    {
        //GameObject prefabGO = await Addressables.LoadAssetAsync<GameObject>("Tile").WithCancellation(ct);
        var handle = Addressables.LoadAssetAsync<GameObject>("Tile");
        await handle.WithCancellation(ct);
        GameObject prefabGO = handle.Result;

        Tile tilePrefab = prefabGO.GetComponent<Tile>();


        int totalTiles = _gridSize * _gridSize;
        _pool = new ObjectPool<Tile>(tilePrefab, totalTiles, _parent);
    }

    public Tile GetTile(Vector2 position)
    {
        Tile tile = _pool.Get();
        tile.transform.position = position;
        return tile;
    }

    public void ReleaseTile(Tile tile)
    {
        tile.ResetState();
        _pool.Release(tile);
    }
}
