using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Cysharp.Threading.Tasks;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _backgroundRenderer;
    [SerializeField] private string _backgroundLabel = "Background";

    private List<Sprite> _loadedBackgrounds = new();
    private AsyncOperationHandle<IList<Sprite>> handle;


    private async void Start()
    {
        await LoadAndSetRandomBackground();
    }

    private void OnEnable()
    {
        SetRandomBackground();
        GameEventDispatcher.OnTryAgainClicked += SetRandomBackground;
    }

    private void OnDisable()
    {
        GameEventDispatcher.OnTryAgainClicked -= SetRandomBackground;
    }

    private void OnDestroy()
    {
        if (_loadedBackgrounds.Count > 0)
        {
            Addressables.Release(handle);
        }
    }

    private async UniTask LoadAndSetRandomBackground()
    {
        handle = Addressables.LoadAssetsAsync<Sprite>(_backgroundLabel, null);
        await handle.ToUniTask();

        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _loadedBackgrounds = (List<Sprite>)handle.Result;

            if (_loadedBackgrounds.Count > 0)
            {
                int randomIndex = Random.Range(0, _loadedBackgrounds.Count);
                _backgroundRenderer.sprite = _loadedBackgrounds[randomIndex];
            }
        }
        else
        {
            Debug.LogError("Failed to load backgrounds");
        }
    }

    public void SetRandomBackground()
    {
        if (_loadedBackgrounds.Count > 0)
        {
            int randomIndex = Random.Range(0, _loadedBackgrounds.Count);
            _backgroundRenderer.sprite = _loadedBackgrounds[randomIndex];
        }
    }

}
