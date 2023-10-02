using System.Collections.Generic;
using UnityEngine;

public class MusicBehaviour : MonoBehaviour
{
    public List<AudioClip> musics;

    private AudioSource _audioSource;
    private AudioClip _playedMusic;
    private int _clipIndex;
    
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _clipIndex = musics.Count - 1;
    }
    
    private void Update()
    {
        if (_clipIndex < 0)
        {
            _clipIndex = musics.Count - 1;
        }
        if (!_audioSource.isPlaying)
        {
            _audioSource.clip = musics[_clipIndex];
            _audioSource.Play();
            _clipIndex--;
        }
    }
}
