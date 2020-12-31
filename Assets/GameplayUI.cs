using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class GameplayUI : MonoBehaviour
{
    public static GameplayUI instance;
    GraphicRaycaster gr;
    private void Awake()
    {
        instance = this;
        gr = GetComponent<GraphicRaycaster>();
    }
    public bool IsOverUI(Vector3 position)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        gr.Raycast(eventDataCurrentPosition, results);
        foreach (RaycastResult result in results)
        {
            Debug.Log("Hit " + result.gameObject.name);
        }
        return results.Count > 0;
    }
}
