using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Triggers;
public class AssetBundleManager : MonoBehaviour
{
    public static AssetBundleManager Instance { get; set; }
    
    //ロードしたハンドルを記憶するリスト
    List<AsyncOperationHandle> _asyncOperationHandle = new();

    public void ReleaseAssllHandle()
    {
        foreach(var handle in _asyncOperationHandle)
        {
            Addressables.Release(handle);
        }
        _asyncOperationHandle.Clear();
    }

    //同期読み込み
    public T LoadAsset<T>(object key)
    {
        var handle = Addressables.LoadAssetAsync<T>(key);
        handle.WaitForCompletion();

        _asyncOperationHandle.Add(handle);

        return handle.Result;
    }

    //非同期読み込み
    public async UniTask<T> LoadAssetAsync<T>(object key)
    {
        var handle = Addressables.LoadAssetAsync<T>(key);

        await handle.Task;

        _asyncOperationHandle.Add(handle);

        return handle.Result;
    }

    public async UniTask<UnityEngine.ResourceManagement.ResourceProviders.SceneInstance>
        ChangeSceneAysnc(object key)
    {
        var handle = Addressables.LoadSceneAsync(key);
        await handle.Task;

        return handle.Result;
    }

    private void Awake()
    {
        if(Instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
}
