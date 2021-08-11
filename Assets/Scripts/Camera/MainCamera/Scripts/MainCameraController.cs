using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.NJUCS.Camera
{
    public class MainCameraController : MonoBehaviour
    {
        private Quaternion camRotation;             //camera 的rotation
        public float camSmoothFactor;               //camera 的旋转速度

        public float lookUpMax;                     //camera 的旋转限制
        public float lookDownMax;
        public float lookLeftMax;
        public float lookRightMax;

        public Transform t_camera;
        private RaycastHit hit;
        private Vector3 cameraOffset;
        // Start is called before the first frame update
        void Start()
        {
            camRotation = transform.localRotation;
            camSmoothFactor = 1;
            lookUpMax = 60;
            lookDownMax = -60;
            cameraOffset = t_camera.localPosition;
            //lookLeftMax = -90;
            //lookRightMax = 90;
        }

        // Update is called once per frame
        void Update()
        {
            //Debug.Log(Input.GetAxis("Mouse X") + "    :    "+Input.GetAxis("Mouse Y"));
            camRotation.y += Input.GetAxis("Mouse X") * camSmoothFactor;                    //摄像头的移动
            camRotation.x += Input.GetAxis("Mouse Y") * camSmoothFactor * (-1);
            camRotation.x = Mathf.Clamp(camRotation.x, lookDownMax, lookUpMax);             //摄像头移动的限制
                                                                                            //camRotation.y = Mathf.Clamp(camRotation.y, lookLeftMax, lookRightMax);
                                                                                            //Debug.Log(camRotation.x);
            transform.localRotation = Quaternion.Euler(camRotation.x, camRotation.y, camRotation.z);
            if (Physics.Linecast(transform.position, transform.position + (transform.rotation * cameraOffset), out hit))
            {
                t_camera.localPosition = new Vector3(0, 0, -Vector3.Distance(transform.position, hit.point));
            }
            else
            {
                t_camera.localPosition = Vector3.Lerp(t_camera.localPosition, cameraOffset, Time.deltaTime);
            }
        }
    }
}
