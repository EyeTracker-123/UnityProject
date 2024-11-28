using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class EyeBlinkSoundControl : MonoBehaviour
{
    public CatchEyeDate eyeTracker; // EyeTrackerのスクリプト
    private AudioSource audioSource; // 音を制御するためのAudioSource

    // Start is called before the first frame update
    void Start()
    {
        // TETConnectスクリプトを取得
        eyeTracker = GetComponent<CatchEyeDate>();

        // AudioSourceコンポーネントを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // fix変数を取得（true: 目が開いている, false: 目が閉じている）
        bool isFixing = eyeTracker. eyeTracker.TET.values.frame.fix;

        // 目が開いている場合は音を再生、閉じている場合は音を停止
        if (isFixing)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.mute = true;
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.mute = false;
            }
        }
    }
}
