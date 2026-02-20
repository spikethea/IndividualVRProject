using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLook : MonoBehaviour
{
    [SerializeField] private float sightDistance;
    [SerializeField] private float fieldOfView;
    [SerializeField] private LayerMask layerMask;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // create a ray at the center of the camera, shooting outwards.
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * sightDistance);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, sightDistance, layerMask))
        {
            Debug.Log(hitInfo.collider.name);
            //Ray Hitting Interactables
            if (hitInfo.distance < sightDistance)
            {
                if(hitInfo.collider.CompareTag("Head"))
                {
                    Vector3 headPos = hitInfo.collider.transform.position;
                    Vector3 targetDirection = headPos - transform.position ;

                    //checking if the player is within the NPC's field of view
                    float angleToPlayer = Vector3.Angle(targetDirection, hitInfo.collider.transform.forward);
                    if (angleToPlayer <= fieldOfView * 0.5f)
                    {
                        Commuter Commuter = hitInfo.collider.GetComponentInParent<Commuter>();
                        // NPC looks away from the player
                        if (Commuter != null)
                        {
                            Commuter.LookAround();
                        }
                       
                    }
                    

                    
                }

            }
        }
    }
}
