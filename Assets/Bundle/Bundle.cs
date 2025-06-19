using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.U2D;

public class Bundle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IELoad());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator IELoad()
    {
        AssetBundleCreateRequest async = AssetBundle.LoadFromFileAsync(Path.Combine
            (Application.streamingAssetsPath, "Chracter"));

        yield return async;

        AssetBundle local = async.assetBundle;

        if (local == null)
            yield break;

        var obj = local.LoadAsset("Item");

        SpriteAtlas sa = obj as SpriteAtlas;
    }
}
