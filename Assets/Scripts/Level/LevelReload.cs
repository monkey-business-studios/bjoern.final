using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelReload : MonoBehaviour
{
    public float timer;
    public float waitTime = 2.0f;
    
    public void LevelReset()
    {
        timer += Time.deltaTime;

        if (timer >= waitTime)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
