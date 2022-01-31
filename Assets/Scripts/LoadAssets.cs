using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadAssets
{
    private List<AsyncOperationHandle> handleList;

    public LoadAssets() {
        handleList = new List<AsyncOperationHandle>();
    }
    public Sprite loadSprite(string path) {
        if (path == "" || path == null) {
            //   Debug.LogError(" empty path");
            return null;
        }
        // Debug.LogError(path);
        Sprite sprite;

        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(path);
        handleList.Add(handle);
        handle.WaitForCompletion();


        if (handle.Status == AsyncOperationStatus.Succeeded) {
            sprite = handle.Result;
        }
        else {
            sprite = null;
        }

        //Addressables.Release(handle);
        return sprite;
    }



    public GameObject loadGameObject(string path) {
        if (path == "" || path == null) {
            //  Debug.LogError(" empty path");
            return null;
        }
        //    Debug.LogError(path);
        GameObject game;
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(path);
        handleList.Add(handle);
        handle.WaitForCompletion();

        if (handle.Status == AsyncOperationStatus.Succeeded) {
            game = handle.Result;
        }
        else {
            game = null;
        }
        // Addressables.Release(handle);
        return game;
    }

    public TextAsset loadText(string path) {
        if (path == "" || path == null) {
            //  Debug.LogError(" empty path");
            return null;
        }
        //path = path.Replace("Assets/", "");

        // Debug.LogError(path);
        TextAsset text;

        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(path);
        handleList.Add(handle);
        handle.WaitForCompletion();

        // Debug.LogError("asst loaded");

        if (handle.Status == AsyncOperationStatus.Succeeded) {
            //   Debug.LogError("funktioniert");
            text = handle.Result;

            // Debug.LogError(text.text);
        }
        else {
            //Debug.LogError("fehler");
            text = null;
        }
        // Addressables.Release(handle);

        return text;
    }

    public void releaseLastHandle() {
        Addressables.Release(handleList[(handleList.Count - 1)]);
        handleList.RemoveAt((handleList.Count - 1));
    }
    public void releaseAllHandle() {
        foreach (AsyncOperationHandle handle in handleList) {
            Addressables.Release(handle);
        }
    }

}
