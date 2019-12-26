
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    public float slowdownFactor = 0.05f;
    public float slowdownLength = 2f;

    public Freezer Frezz;

    void Update()
    {

        if (Frezz.Frezing == false)
        { 


            Time.timeScale += (1f / slowdownLength) * Time.unscaledDeltaTime;


        }

        Time.timeScale = Mathf.Clamp(Time.timeScale, 0f, 1f);

    }









     public void SlowDown()
     {

        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;

     }
}
