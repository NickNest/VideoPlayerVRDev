using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VideoData", menuName = "VideoData/New video collection")]
public class VideoCollectionSO : ScriptableObject
{
    public List<VideoSO> VideoCollection;
}
