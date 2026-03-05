using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundBoard : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private BtnSfx _btnPrefab;
    [SerializeField] private List<AudioClip> _clips;

    private BtnSfx _btnSfx;
    
    private void Start()
    {
        foreach (var audioClip in _clips)
        {
            BtnSfx bs = Instantiate(_btnPrefab, _btnPrefab.transform.parent);
            bs.Init(audioClip.name,Clicked);
        }
        _btnPrefab.gameObject.SetActive(false);
    }

    public void Stop()
    {
        if (_btnSfx==null) return;
        _audioSource.Stop();
        _btnSfx.Disable();
        _btnSfx = null;
    }

    private void Clicked(BtnSfx btnSfx)
    {
        AudioClip clip = _clips.FirstOrDefault(c => c.name == btnSfx.SoundId);
        if (clip==null) return;
        _audioSource.Stop();
        if (_audioSource.clip != clip)
            _audioSource.clip = clip;
        _audioSource.Play();
        btnSfx.Enable();
        
        if (_btnSfx!=null)
            _btnSfx.Disable();
        _btnSfx = btnSfx;
    }

    private void Update()
    {
        return;
        if (_btnSfx==null) return;
        
        if (!_audioSource.isPlaying && _audioSource.time >= _audioSource.clip.length)
        {
            _btnSfx.Disable();
            _btnSfx = null;
        }
    }
}
