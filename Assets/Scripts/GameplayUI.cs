using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;
    public Image removeImage;
    GraphicRaycaster gr;
    private void Awake()
    {
        instance = this;
        gr = GetComponent<GraphicRaycaster>();
    }
    public void SetRemoveImageEnabled(bool value)
    {
        removeImage.enabled = value;
    }
    public bool IsOverUI(Vector3 position)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        gr.Raycast(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
