using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Joystick : ScrollRect
{
    private bool dragStatus = false;
    private float radius;
    private JoystickAdapter adapter;

    protected override void Start()
    {
        base.Start();
        adapter = GetComponent<JoystickAdapter>();
        radius = adapter.radius;
    }

    void FixedUpdate()
    {
        if (dragStatus)
        {
            adapter.vector = content.anchoredPosition;
        }
    }

    public override void OnDrag(PointerEventData data)
    {
        base.OnDrag(data);
        var contentPosition = content.anchoredPosition;

        if (contentPosition.magnitude > radius)
        {
            contentPosition = contentPosition.normalized * radius;
            SetContentAnchoredPosition(contentPosition);
        }
    }

    public override void OnBeginDrag(PointerEventData data)
    {
        base.OnBeginDrag(data);
        dragStatus = true;
        adapter.isControlled = true;
    }

    public override void OnEndDrag(PointerEventData data)
    {
        base.OnEndDrag(data);
        dragStatus = false;
        adapter.isControlled = false;
    }
}
