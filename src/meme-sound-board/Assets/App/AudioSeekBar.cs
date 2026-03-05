using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioSeekBar : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Image progressImage;

    private RectTransform rect;

    private void Awake()
    {
        rect = progressImage.rectTransform;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            rect,
            eventData.position,
            eventData.pressEventCamera,
            out localPoint))
        {
            float width = rect.rect.width;

            // переводим координату клика в диапазон 0..1
            float percent = Mathf.Clamp01((localPoint.x + width * 0.5f) / width);

            Seek(percent);
        }
    }

    private void Seek(float percent)
    {
        if (audioSource.clip == null) return;

        float time = audioSource.clip.length * percent;
        audioSource.time = time;

        progressImage.fillAmount = percent;
    }
}