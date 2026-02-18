using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] Transform DepotPoint;
    [SerializeField] Transform PlatformPoint;
    [SerializeField] Transform TunnelPoint;

    private float smoothTime = 5;

    public Vector3 velocity = new Vector3(0, 0, 2);
    Vector3 targetPos;

    private bool scheduleIsRunning = true;
    Vector3 originalPosition;
    public int stoppingState = 0;
    IEnumerator TrainSchedule()
    {
        while (scheduleIsRunning)
        {
            yield return new WaitForSeconds(5);
            stoppingState = 0;
            yield return new WaitForSeconds(10);
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
                transform.position = Vector3.SmoothDamp(transform.position, DepotPoint.position, ref velocity, smoothTime);
                break;
            case 1:
                // code block
                transform.position = Vector3.SmoothDamp(transform.position, PlatformPoint.position, ref velocity, smoothTime);
                break;
            case 2:
                transform.position = Vector3.SmoothDamp(transform.position, TunnelPoint.position, ref velocity, smoothTime);
                break;
            default:
                // code block
                break;
        }
    }
}
