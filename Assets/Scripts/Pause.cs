
using UnityEngine;

public class Pause : MonoBehaviour

{
    private bool isPause = false;
  
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ToggleTimeScale();
        }
    }

    void ToggleTimeScale()
    {
        if (!isPause)
        {
            Time.timeScale = 0;     //Bullettime wenn auf 0.1f eingestellt, sonst pausiert das Spiel bei 0;
        }

        else
        {
            Time.timeScale = 1;     // das Spiel spiel wird gestartet
        }

        isPause = !isPause;
    }
}
