using UnityEngine;
using SpeedrunPractice.Extensions;
public class FreeCam : MonoBehaviour
{
    float baseSpeed = 10f;
    float fastMovementSpeed = 100f;

    void Update()
    {
        if (MainManager_Ext.toggleFreeCam)
        {
            var fastMode = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
            var speed = fastMode ? fastMovementSpeed : baseSpeed;

            if (Input.GetKey(KeyCode.A))
            {
                transform.position = transform.position + (-transform.right * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.position = transform.position + (transform.right * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.W))
            {
                transform.position = transform.position + (transform.forward * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                transform.position = transform.position + (-transform.forward * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.Q))
            {
                transform.position = transform.position + (transform.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.E))
            {
                transform.position = transform.position + (-transform.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.I))
            {
                transform.eulerAngles = transform.eulerAngles + (Vector3.right * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.K))
            {
                transform.eulerAngles = transform.eulerAngles + (-Vector3.right * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.L))
            {
                transform.eulerAngles = transform.eulerAngles + (Vector3.up * speed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.J))
            {
                transform.eulerAngles = transform.eulerAngles + (-Vector3.up * speed * Time.deltaTime);
            }
        }
    }
}
