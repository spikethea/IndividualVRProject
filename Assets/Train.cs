using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{
    private float speed = 10f;
    private bool scheduleIsRunning = true;
    Vector3 originalPosition;
    public int stoppingState = 0;
    IEnumerator TrainSchedule()
    {
        while (scheduleIsRunning)
        {
            yield return new WaitForSeconds(5);
            stoppingState = 1;
            yield return new WaitForSeconds(5);
            stoppingState = 2;
        }
    }

    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(TrainSchedule());
    }


    // Update is called once per frame
    void Update()
    {
        switch (stoppingState)
        {
            case 0:
                // code block
                break;
            case 1:
                // code block
                transform.position = originalPosition;
                break;
            case 2:
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
                break;
            default:
                // code block
                break;
        }
    }
}
