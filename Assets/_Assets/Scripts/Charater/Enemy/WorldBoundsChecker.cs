using UnityEngine;

public class WorldBoundsChecker : MonoBehaviour
{
    [SerializeField] Transform _top;
    [SerializeField] Transform _bottom;
    [SerializeField] Transform _forward;

    [SerializeField] LayerMask _layerMaskCheck;
    private float _radiusCheck = 0.2f;

    private bool _isTop;
    private bool _isBottom;
    private bool _isForward;

    private void Update()
    {
        CheckTop();
        CheckBottom();
        CheckForward();
    }

    private void CheckTop()
    {
        if(_top == null) return;
        _isTop = Physics2D.OverlapCircle(_top.position, _radiusCheck, _layerMaskCheck);
    }    

    private void CheckBottom()
    {
        if(_bottom == null) return;
        _isBottom = Physics2D.OverlapCircle(_bottom.position, _radiusCheck, _layerMaskCheck);
    }    

    private void CheckForward()
    {
        if(_forward == null) return;
        _isForward = Physics2D.OverlapCircle(_forward.position, _radiusCheck, _layerMaskCheck);
    }    
    
    // ============== Service ==============
    public bool IsTop() => _isTop;
    public bool IsBottom() => _isBottom;
    public bool IsForward() => _isForward;


}
