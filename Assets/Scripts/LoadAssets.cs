using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class LoadAssets
{
    private GameObject game;
    private Sprite sprite;
    private TextAsset text;
    private bool finished;
    public Sprite loadSprite(string path) {
        finished = false;
        Addressables.LoadAssetAsync<Sprite>(path).Completed += loadingSprite;

        while (finished == false) {

        }
        return sprite;
    }

    private void loadingSprite(AsyncOperationHandle<Sprite> obj) {
        if (obj.Status == AsyncOperationStatus.Succeeded) {
            sprite = obj.Result;
            finished = true;
        }
        else if (obj.Status == AsyncOperationStatus.Failed) {
            sprite = null;
            finished = true;
        }
    }

    public GameObject loadGameObject(string path) {
        finished = false;
        Addressables.LoadAssetAsync<GameObject>(path).Completed += loadingGameObject;
        while (finished == false) {

        }
        return game;
    }

    private void loadingGameObject(AsyncOperationHandle<GameObject> obj) {
        if (obj.Status == AsyncOperationStatus.Succeeded) {
            game = obj.Result;
            finished = true;
        }
        else if (obj.Status == AsyncOperationStatus.Failed) {
            game = null;
            finished = true;
        }
    }

    public TextAsset loadText(string path) {
        finished = false;

        Addressables.LoadAssetAsync<GameObject>(path).Completed += (obj => {

        });
        Addressables.LoadAssetAsync<GameObject>(path);


        Addressables.LoadAssetAsync<GameObject>(path).Completed += loadingGameObject;
        while (finished == false) {

        }
        return text;
    }

    private void loadingText(AsyncOperationHandle<TextAsset> obj) {
        if (obj.Status == AsyncOperationStatus.Succeeded) {
            text = obj.Result;
            finished = true;
        }
        else if (obj.Status == AsyncOperationStatus.Failed) {
            text = null;
            finished = true;
        }
    }
}
