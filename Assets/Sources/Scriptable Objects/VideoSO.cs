using RenderHeads.Media.AVProVideo;
using UnityEngine;

[CreateAssetMenu(fileName = "VideoData", menuName = "VideoData/New video item")]
public class VideoSO : ScriptableObject
{
    public string VideoTitle;
    public int VideoId;
    public string VideoURLPath;
    public string PreviewURLPath;
    public MediaReference MediaReference;
}
