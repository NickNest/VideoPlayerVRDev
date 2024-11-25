using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class VideoChooserButton : MonoBehaviour
{
    [SerializeField] private VideoSO _videoSO;
    [SerializeField] private TMP_Text _title;

    private void Start()
    {
        SetupTitle();
    }

    private void SetupTitle()
    {
        _title.text = _videoSO.VideoTitle;
    }

    public void OnButtonClick()
    {
        Actions.OnChoosingMedia(_videoSO);
    }
}
