using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Quaternion targetRotation;
    public float moveSpeed = 5f;
    private int currentIndex = 0;
    private Vector3 targetPosition;

   
    void Start()
    {
        Debug.Log("RoomKeyDict count: " + GlobalData.RoomKeyDict.Count);
        if (GlobalData.tilePositions.Count > 0)
        {
            targetPosition = GlobalData.tilePositions[0];
            targetRotation = GlobalData.tileRotations[0];
       
            transform.position = targetPosition ;
            transform.rotation = targetRotation ;   
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (currentIndex + 1 < GlobalData.tilePositions.Count)
            {
                currentIndex++;
                targetPosition = GlobalData.tilePositions[currentIndex];
                targetRotation = GlobalData .tileRotations[currentIndex];
                

            }

        }
        Vector3 raisedTarget = new Vector3(
                     targetPosition.x,
                     targetPosition.y + 1,
                     targetPosition.z);

        transform.position = Vector3.MoveTowards(transform.position, raisedTarget, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100f * Time.deltaTime);
    }
}
