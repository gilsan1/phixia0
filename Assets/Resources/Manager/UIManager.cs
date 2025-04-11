using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using Unity.VisualScripting;

public class UIManager : MonoBehaviour
{
    public VideoPlayer VIDEOPLAYER;
    public RawImage RAWIMG;

    VideoClip Clip = null;
    // Start is called before the first frame update
    void Start()
    {
        string file = "Video/" + "EF_Normal";

        Clip = Resources.Load(file) as VideoClip;

        if (Clip == null) return;

        //VIDEOPLAYER.gameObject.SetActive(true);

        RAWIMG.texture = VIDEOPLAYER.texture;

        VIDEOPLAYER.clip = Clip;

        VIDEOPLAYER.Prepare();



        StartCoroutine(VideoPlay());
    }

    // Update is called once per frame
    void Update()
    {
    }

    IEnumerator VideoPlay()
    {
        yield return null;

        VIDEOPLAYER.Play();

        while (true)
        {
            yield return new WaitForSeconds(0.1f);

            if (VIDEOPLAYER.isPlaying)
            {
                RAWIMG.texture = VIDEOPLAYER.texture;

                continue;
            }
            break;
        }

        VIDEOPLAYER.gameObject.SetActive(false);
    }
}
