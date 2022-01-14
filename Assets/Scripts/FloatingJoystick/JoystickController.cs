using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class JoystickController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public Vector3 inputVector;

    [SerializeField] RectTransform background;
    [SerializeField] RectTransform handle;

    private void Start()
    {
        inputVector = Vector3.zero;
        background.gameObject.SetActive(false);
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(background, eventData.position, eventData.pressEventCamera, out pos))
        {
            pos.x = (pos.x / background.sizeDelta.x);
            pos.y = (pos.y / background.sizeDelta.y);

            inputVector = new Vector3(pos.x * 2, pos.y * 2, 0);
            inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;

            handle.anchoredPosition = new Vector3(inputVector.x * (background.sizeDelta.x / 3), inputVector.y * (background.sizeDelta.y / 3));
        }
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        background.gameObject.SetActive(true);
        background.transform.position = Input.mousePosition;
        handle.transform.position = Input.mousePosition;
        handle.transform.position = background.transform.position;
        OnDrag(eventData);
    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {
        background.gameObject.SetActive(false);
        inputVector = Vector3.zero;
        handle.anchoredPosition = Vector3.zero;
    }
}
