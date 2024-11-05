using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

using Random = UnityEngine.Random;


// не обовязково mono
public class AudioHandler : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    [SerializeField] private List<AudioClip> throwAudio;
    [SerializeField] private List<AudioClip> touchAudio;
    [SerializeField] private List<AudioClip> mergeAudio;


    private IThrowingPlatform platform;

    [Inject]
    private void Construct(ITable table)
    {
        this.platform = table.Platform;
    }

    private void Start()
    {
        platform.OnThrow += OnThrowPlatform;
        platform.OnTouch += OnTouchPlatform;
        platform.OnDetectSameItem += OnDetectSameItemPlatform;
    }

    // впадло робити новий івент
    private void OnDetectSameItemPlatform(int obj)
    {
        Play(mergeAudio);
    }

    private void OnTouchPlatform()
    {
        Play(touchAudio);
    }

    private void OnThrowPlatform()
    {
        Play(throwAudio);
    }




    private AudioClip GetRandomClip(List<AudioClip> clipList)
    {
        if (clipList != null)
        {
            int randomNumber = Random.Range(0, clipList.Count);

            return clipList[randomNumber];
        }

        throw new NullReferenceException("list Null");
    }


    private void Play(List<AudioClip> clips)
    {
        AudioClip randomThrowClip = GetRandomClip(clips);
        source.PlayOneShot(randomThrowClip);
    }
}
