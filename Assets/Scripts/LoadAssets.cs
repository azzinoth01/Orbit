using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;


/// <summary>
/// class to load assets while runtime
/// </summary>
public class LoadAssets
{
    private static LoadAssets _instance;

    private Dictionary<string,AsyncOperationHandle> _loadedHandles;

    public static LoadAssets Instance {
        get {
            if(_instance == null) {
                _instance = new LoadAssets();
            }
            return _instance;
        }
    }


    /// <summary>
    /// standard constructor
    /// </summary>
    private LoadAssets() {

        _loadedHandles = new Dictionary<string,AsyncOperationHandle>();
    }

    /// <summary>
    /// loads a sprite out from the addressables path
    /// </summary>
    /// <param name="path"> the addressables path</param>
    /// <returns> the loaded sprite</returns>
    public Sprite loadSprite(string path) {
        if(path == "" || path == null) {
            //   Debug.LogError(" empty path");
            return null;
        }
        // Debug.LogError(path);

        if(_loadedHandles.ContainsKey(path)) {
            return (Sprite) _loadedHandles[path].Result;
        }
        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(path);
        handle.WaitForCompletion();

        _loadedHandles[path] = handle;
        return (Sprite) _loadedHandles[path].Result;
    }


    /// <summary>
    /// loads a gameobject out from the addressables path
    /// </summary>
    /// <param name="path"> the addressables path</param>
    /// <returns> the loaded gameobject</returns>
    public GameObject loadGameObject(string path) {
        if(path == "" || path == null) {
            //  Debug.LogError(" empty path");
            return null;
        }

        if(_loadedHandles.ContainsKey(path)) {
            return (GameObject) _loadedHandles[path].Result;
        }

        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(path);
        handle.WaitForCompletion();

        _loadedHandles[path] = handle;
        return (GameObject) _loadedHandles[path].Result;
    }

    /// <summary>
    /// loads a textAsset out from the addressables path
    /// </summary>
    /// <param name="path"> the addressables path</param>
    /// <returns> the loaded textAsset</returns>
    public TextAsset loadText(string path) {
        if(path == "" || path == null) {
            //  Debug.LogError(" empty path");
            return null;
        }

        if(_loadedHandles.ContainsKey(path)) {
            return (TextAsset) _loadedHandles[path].Result;
        }
        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(path);
        handle.WaitForCompletion();

        _loadedHandles[path] = handle;
        return (TextAsset) _loadedHandles[path].Result;
    }

}
