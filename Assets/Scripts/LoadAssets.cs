using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadAssets
{

    public Sprite loadSprite(string path) {

        AsyncOperationHandle<Sprite> handle = Addressables.LoadAssetAsync<Sprite>(path);
        handle.WaitForCompletion();
        Sprite sprite = handle.Result;
        Addressables.Release(handle);
        return sprite;
    }



    public GameObject loadGameObject(string path) {
        AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync<GameObject>(path);
        handle.WaitForCompletion();

        GameObject game = handle.Result;
        Addressables.Release(handle);
        return game;
    }

    public TextAsset loadText(string path) {


        AsyncOperationHandle<TextAsset> handle = Addressables.LoadAssetAsync<TextAsset>(path);

        handle.WaitForCompletion();

        TextAsset text = handle.Result;

        Addressables.Release(handle);

        return text;
    }

}
