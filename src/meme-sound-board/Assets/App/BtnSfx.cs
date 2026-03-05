using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BtnSfx : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _isPlaying;

    private bool _isEnabled;
    private string _soundId;
    private Action<BtnSfx> _onClick;

    public string SoundId => _soundId;

    public void Init(string id, Action<BtnSfx> onClick)
    {
        _onClick = onClick;
        _soundId = id;
        _text.text = _soundId;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _onClick?.Invoke(this);
    }

    public void Enable()
    {
        if (_isEnabled) return;
        _isEnabled = true;
        _isPlaying.SetActive(true);
    }

    public void Disable()
    {
        if (!_isEnabled) return;
        _isEnabled = false;
        _isPlaying.SetActive(false);
    }
}
