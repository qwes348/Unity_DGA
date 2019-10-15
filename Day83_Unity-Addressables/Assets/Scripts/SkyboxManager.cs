using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;

// https://thegamedev.guru/unity-addressables/tutorial-learn-the-basics/
public class SkyboxManager : MonoBehaviour
{
    // AssetReference  모든 리소스의 레퍼런스를 포함할수있는 클래스
    [SerializeField] private List<AssetReference> _skyboxMaterials;
    AsyncOperationHandle _currentHandle;

    public void SetSkybox(int skyboxIndex)
    {
        //RenderSettings.skybox = _skyboxMaterials[skyboxIndex];
        StartCoroutine(SetSkyboxInternal(skyboxIndex));
    }

    IEnumerator SetSkyboxInternal(int skyboxIndex)
    {
        if(_currentHandle.IsValid())    // 이미 실행중일 때 
        {
            Addressables.Release(_currentHandle);
        }

        AssetReference skyboxMaterialReference = _skyboxMaterials[skyboxIndex];
        _currentHandle = skyboxMaterialReference.LoadAssetAsync<Material>();

        yield return _currentHandle;    // Load가 끝날때까지 제어권 넘겨주며 대기

        RenderSettings.skybox = (Material)_currentHandle.Result;
    }
}
