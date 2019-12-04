using UnityEngine;
using UnityEngine.UI;

public class messageBox : MonoBehaviour
{
    float timeToShow = 4f;
    float timer = 0;
    string message = "";
    public Text textObj;
    public CanvasGroup cg;

    private void Start() {
        textObj.text = message;
        timer = 0;
        cg.alpha =0;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0) {
            timer -= Time.deltaTime;
            cg.alpha = 1-Mathf.InverseLerp(timeToShow, 0, timer);
        }
        else if(timer<0) {
            timer = 0;
            cg.alpha = 0;
        }

    }
    public string getMessage(){
        return message;
    }
    public float getTime(){
        return timer;
    }

    public void setNewMessage(string message, float timeLeft = -1f)
    {
        if(timeLeft<0) timeLeft = timeToShow;
        this.message = message;
        textObj.text = message;
        timer = timeLeft;
    }
}
