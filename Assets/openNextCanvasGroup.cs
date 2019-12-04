using UnityEngine;
using UnityEngine.UI;

public class openNextCanvasGroup : MonoBehaviour
{
    CanvasGroup thisGroup;
    private void Start()
    {
        thisGroup = GetComponent<CanvasGroup>();
    }

    public void OpenNextGroup(CanvasGroup nextGroup)
    {
        thisGroup.alpha = 0;
        thisGroup.interactable = false;
        thisGroup.blocksRaycasts = false;

        nextGroup.alpha = 1;
        nextGroup.interactable = true;
        nextGroup.blocksRaycasts = true;
    }
    public void HideThisGroup()
    {
        thisGroup.alpha = 0;
        thisGroup.interactable = false;
        thisGroup.blocksRaycasts = false;
    }
}
