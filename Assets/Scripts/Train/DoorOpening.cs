using UnityEngine;
using UnityEngine.UI;

public class DoorOpening : MonoBehaviour
{
    [SerializeField] Transform LeftDoor;
    [SerializeField] Transform RightDoor;


    private Vector3 LClosedPoint;
    private Vector3 RClosedPoint;
    private Vector3 LOpenPoint;
    private Vector3 ROpenPoint;
    private Vector3 leftDoorVelocity = new Vector3(0, 0, 2);
    private Vector3 rightDoorVelocity = new Vector3(0, 0, 0);

    public bool isClosed = true;
    public float t = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LClosedPoint = LeftDoor.localPosition;
        RClosedPoint = RightDoor.localPosition;
        LOpenPoint = LClosedPoint - Vector3.forward;
        ROpenPoint = RClosedPoint + Vector3.forward;
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Clamp01(t);
        var speed = 0.5f;
        t += isClosed ? speed * Time.deltaTime : -speed * Time.deltaTime;

        LeftDoor.localPosition = Vector3.Lerp(LClosedPoint, LOpenPoint, t);
        RightDoor.localPosition = Vector3.Lerp(RClosedPoint, ROpenPoint, t);

        //if (isClosed) {
        //    if (LeftDoor.position.z <= LClosedPoint.z)
        //    {
        //        LeftDoor.transform.position = Vector3.SmoothDamp(
        //            LeftDoor.transform.position,
        //            LClosedPoint,
        //            ref leftDoorVelocity,
        //            3
        //        );
        //    }
        //} else {
        //    if (LeftDoor.position.z >= LOpenPoint.position.z)
        //    {
        //        LeftDoor.transform.position = Vector3.SmoothDamp(
        //            LeftDoor.transform.position,
        //            LOpenPoint.position,
        //            ref leftDoorVelocity,
        //            3
        //        );
        //    }
        //}
    }

    public void OpenDoors() {
        isClosed = true;
    }


}
