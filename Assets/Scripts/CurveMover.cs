using UnityEngine;
using DG.Tweening;

public class CurveMover : MonoBehaviour
{
    [SerializeField] private Transform _target;

    private Vector3[] _points;

    private void OnEnable()
    {
        _points = new Vector3[transform.childCount];

        for(int i = 0; i < _points.Length; i++)
        {
            _points[i] = transform.GetChild(i).position;
        }
    }

    private void Start()
    {
        Tween tween = _target.transform.DOPath(_points, 3, PathType.CubicBezier, PathMode.TopDown2D);
        tween.SetLoops(-1, LoopType.Yoyo);
    }
}
