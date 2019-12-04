using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideCanvasAtStart : MonoBehaviour
{
    [SerializeField] bool hide = true;

    CanvasGroup cg;
    void Start()
    {
        if (hide)
        {
            cg = GetComponent<CanvasGroup>();

            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }



}
