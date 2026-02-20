using System.Collections;
using UnityEngine;

public class Train : MonoBehaviour
{
    [SerializeField] Transform DepotPoint;
    [SerializeField] Transform PlatformPoint;
    [SerializeField] Transform TunnelPoint;

    public AudioSource audioSource;

    [SerializeField] AudioClip stoppingSound;
    [SerializeField] AudioClip openingSound;
    [SerializeField] AudioClip passingSound;

    private float smoothTime = 5;

    public Vector3 velocity = new Vector3(0, 0, 2);
    Vector3 targetPos;

    public bool scheduleIsRunning = false;
    Vector3 originalPosition;
    public int stoppingState = 0;
    IEnumerator TrainSchedule()
    {
        while (true)
        {
            if (scheduleIsRunning) {
                stoppingState = 0;
                yield return new WaitForSeconds(15);
                stoppingState = 1;
                yield return new WaitForSeconds(15);
                stoppingState = 2;
                yield return new WaitForSeconds(15);
            } else {
                stoppingState = 4;

                // Train non stopping schedule
                audioSource.loop = true;
                if(audioSource.clip != passingSound)
                    audioSource.clip = passingSound;
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
                transform.position = DepotPoint.position;
                yield return new WaitForSeconds(5);

            }
        }
    }

    void Start()
    {
        originalPosition = transform.position;
        StartCoroutine(TrainSchedule());
    }

    void SwitchTrainState() {
        switch (stoppingState)
        {
            case 0:
                // code block
                transform.position = DepotPoint.position;
                audioSource.clip = stoppingSound;
                if(!audioSource.isPlaying)
                    audioSource.Play();
                break;
            case 1:
                // code block
                transform.position = Vector3.SmoothDamp(transform.position, PlatformPoint.position, ref velocity, smoothTime);
                audioSource.clip = openingSound;
                if (!audioSource.isPlaying)
                    audioSource.Play();
                break;
            case 2:
                transform.position = Vector3.SmoothDamp(transform.position, TunnelPoint.position, ref velocity, smoothTime);
                audioSource.clip = passingSound;
                if (!audioSource.isPlaying)
                    audioSource.Play();
                break;

            case 4:
                transform.position = Vector3.SmoothDamp(transform.position, TunnelPoint.position, ref velocity, smoothTime);
                break;
            default:
                // code block
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {
        SwitchTrainState();
    }
}
