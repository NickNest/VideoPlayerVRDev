using DG.Tweening;
using UnityEngine;

public class LoadingCircle : MonoBehaviour
{
    public void StartRotation()
    {
        transform.DOKill();

        transform
            .DORotate(new Vector3(0, 0, -360), 1f, RotateMode.FastBeyond360)
            .SetLoops(-1)
            .SetEase(Ease.Linear);
    }

    public void StopRotation()
    {
        transform.DOKill();
    }
}
