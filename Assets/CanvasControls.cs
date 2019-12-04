using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControls : MonoBehaviour
{
    public static void SetCanvas(CanvasGroup cancan, bool visible)
    {
        cancan.alpha = visible ? 1f : 0f;
        cancan.interactable = visible ? true : false;
        cancan.blocksRaycasts = visible ? true : false;
    }
}
