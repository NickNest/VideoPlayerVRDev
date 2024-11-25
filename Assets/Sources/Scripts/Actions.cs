using RenderHeads.Media.AVProVideo;
using System;
using UnityEngine;

public class Actions
{
    public static event Action<VideoSO> ChooseMedia;

    public static void OnChoosingMedia(VideoSO videoSO)
    {
        ChooseMedia?.Invoke(videoSO);
    }
}
