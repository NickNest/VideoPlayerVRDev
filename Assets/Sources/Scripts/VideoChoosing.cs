using RenderHeads.Media.AVProVideo;
using System;
using UnityEngine;
using TMPro;

public class VideoChoosing : MonoBehaviour
{
    [SerializeField] private MediaPlayer _mediaPlayer;
    [SerializeField] private VideoSO _firstVideo;
    [SerializeField] private TMP_Text _videoTitle;
    [SerializeField] private UIButtonController _UIButtonController;

    private MediaReference _currentMediaReference;

    private void Start()
    {
        UpdateVideoTitle(_firstVideo.VideoTitle);
        UpdateMediaReference(_firstVideo.MediaReference);
    }

    private void OnEnable() =>
        Actions.ChooseMedia += OnChoosingMedia;

    private void OnDisable() =>
        Actions.ChooseMedia -= OnChoosingMedia;

    private void UpdateVideoTitle(String title) =>
        _videoTitle.text = title;

    private void UpdateMediaReference(MediaReference mediaReference) =>
        _currentMediaReference = mediaReference;

    public void OnChoosingMedia(VideoSO videoSO)
    {
        if (_currentMediaReference != videoSO.MediaReference)
        {
            _UIButtonController.ChooseMediaUpdateButtonSprite();
            UpdateMediaReference(videoSO.MediaReference);
            UpdateVideoTitle(videoSO.VideoTitle);
            _mediaPlayer.OpenMedia(videoSO.MediaReference, false);
        }
    }
}
