using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BundleManager : MonoBehaviour // Resources.Load로 가능한 모든 파일
{
    [MenuItem("Assets/AssetBundles")]

    static void BuildAssetBundles()
    {
        string dir = "Assets/StreamingAssets";

        if(!Directory.Exists(Application.streamingAssetsPath))
        {
            Directory.CreateDirectory(dir);
        }

        BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None,
            EditorUserBuildSettings.activeBuildTarget);
    }
}
