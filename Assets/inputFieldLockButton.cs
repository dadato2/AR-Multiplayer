using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inputFieldLockButton : MonoBehaviour
{
    [SerializeField] CanvasGroup button;

    InputField Player_name;

    void Start()
    {
        Player_name = GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player_name.text.Length <= 2)
        {
            button.interactable = false;
            button.alpha = 0.5f;
        }
        else
        {
            button.interactable = true;
            button.alpha = 1f;
            if (Player_name.text.Length > 12)
            {
                Player_name.text = Player_name.text.Remove(Player_name.text.Length-1);
            }
        }
    }
}
