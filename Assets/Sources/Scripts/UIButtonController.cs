using UnityEngine;
using UnityEngine.UI;
using RenderHeads.Media.AVProVideo;

public class UIButtonController : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer;

    [SerializeField] private Button _playPauseButton;
    [SerializeField] private Sprite _playSprite;
    [SerializeField] private Sprite _pauseSprite;


    private Image _buttonImageComponent;
    private Material _playPauseMaterial;

    private void Start()
    {
        SetupPlayPauseButton();
    }

    private void OnEnable()
    {
        _mediaPlayer?.Events.AddListener(OnMediaEvent);
    }

    private void OnDisable()
    {
        _mediaPlayer?.Events.RemoveListener(OnMediaEvent);
    }

    private void SetupPlayPauseButton()
    {
        _playPauseButton?.onClick.AddListener(OnPlayPauseButtonPressed);
        _buttonImageComponent = _playPauseButton.GetComponent<Image>();
    }

    private void OnPlayPauseButtonPressed()
    {
        if (_mediaPlayer.Control.IsPlaying())
        {
            _mediaPlayer.Pause();
            SetButtonSprite(_playSprite);
        }
        else
        {
            _mediaPlayer.Play();
            SetButtonSprite(_pauseSprite);
        }
    }

    private void OnMediaEvent(MediaPlayer mp, MediaPlayerEvent.EventType eventType, ErrorCode errorCode)
    {
        if (eventType == MediaPlayerEvent.EventType.FinishedPlaying)
        {
            SetButtonSprite(_playSprite);
        }
    }

    private void SetButtonSprite(Sprite sprite) =>
        _buttonImageComponent.sprite = sprite;
    
    public void ChooseMediaUpdateButtonSprite() =>
        SetButtonSprite(_playSprite);
}
