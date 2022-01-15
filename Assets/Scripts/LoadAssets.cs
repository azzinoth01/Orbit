using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadAssets
{

    public Sprite loadSprite(string path) {
        if (path == "" || path == null) {
            return null;
        }
        Sprite sprite;

        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(path);
        handle.WaitForCompletion();

        if (handle.Status == AsyncOperationStatus.Succeeded) {
            sprite = handle.Result;
        }
        else {
            sprite = null;
        }

        Addressables.Release(handle);
        return sprite;
    }



    public GameObject loadGameObject(string path) {
        if (path == "" || path == null) {
            return null;
        }
        GameObject game;
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(path);
        handle.WaitForCompletion();

        if (handle.Status == AsyncOperationStatus.Succeeded) {
            game = handle.Result;
        }
        else {
            game = null;
        }
        Addressables.Release(handle);
        return game;
    }

    public TextAsset loadText(string path) {
        if (path == "" || path == null) {
            return null;
        }
        TextAsset text;

        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(path);

        handle.WaitForCompletion();

        if (handle.Status == AsyncOperationStatus.Succeeded) {
            text = handle.Result;
        }
        else {
            text = null;
        }
        Addressables.Release(handle);

        return text;
    }

}
