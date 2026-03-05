using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private List<AudioClip> _clips;
    [SerializeField] private Image _pbStamina;
    [SerializeField] private Text _clipName;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Button _btnPlay;
    [SerializeField] private Button _btnStop;
    [SerializeField] private Button _btnNext;

    private int _index;


    private void Start()
    {
        Shuffle();
        _btnPlay.onClick.AddListener(Play);
        _btnStop.onClick.AddListener(Stop);
        _btnNext.onClick.AddListener(PlayNext);
        Play();
    }
    
    public void Shuffle()
    {
        for (int i = _clips.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (_clips[i], _clips[j]) = (_clips[j], _clips[i]);
        }
    }
    
    private void PlayNext()
    {
        _index++;
        _audioSource.clip = _clips[_index];
        _clipName.text = _clips[_index].name;
        _btnPlay.gameObject.SetActive(false);
        _btnStop.gameObject.SetActive(true);
        _audioSource.Play();
    }

    private void Play()
    {
        _btnPlay.gameObject.SetActive(false);
        _btnStop.gameObject.SetActive(true);
        if (_audioSource == null)
        {
            PlayNext();
        }
        else
        {
            if (!_audioSource.isPlaying)
            {
                _audioSource.Play();
            }
        }
    }

    private void Stop()
    {
        if (_audioSource.isPlaying)
            _audioSource.Pause();
        _btnPlay.gameObject.SetActive(true);
        _btnStop.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_audioSource.clip==null) return;
        _pbStamina.fillAmount = _audioSource.time / _audioSource.clip.length;
        if (!_audioSource.isPlaying && _audioSource.time >= _audioSource.clip.length)
        {
            PlayNext();
        }
    }
}
