using System.Collections;
using Core.Singletons;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class Viewfinder : Singleton<Viewfinder>
{
    //public GameObject MoveDevice;
    public GameObject selectObjPrefab;
    private GameObject selectObj;




    private bool isViewFinder;
    //[SerializeField]
    //private Transform hitBox;
    public ARPlaneManager _arPlaneManager;
    public ARRaycastManager _arRaycastManager;

    public static List<ARRaycastHit> s_Hits00 = new List<ARRaycastHit>();

    static List<ARRaycastHit> s_Hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        //MoveDevice.SetActive(true);
        isViewFinder = true;
 
        ApplicationState.Instance.CurrentState = State.PlaneDetection;
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 center = new Vector3(Screen.width / 2, Screen.height / 2);
        startViewFinder();

    }

    public void closeViewFinder()
    {
        isViewFinder = false;
        Destroy(selectObj);


    }

    public void startViewFinder()
    {
        //var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var screenPoint = new Vector3(Screen.width / 2, Screen.height / 2);
        //Ray ray = Camera.main.ScreenPointToRay(center);
        if (_arRaycastManager.Raycast(screenPoint, s_Hits, UnityEngine.XR.ARSubsystems.TrackableType.Planes) && isViewFinder == true)
        {
            //MoveDevice.SetActive(false);
            Pose hitPose = s_Hits[0].pose;

            s_Hits00 = s_Hits;

            Debug.Log(hitPose.position);


            if (selectObj == null)
            {
                selectObj = Instantiate(selectObjPrefab, hitPose.position, hitPose.rotation);

            }
            else
            {
                selectObj.transform.position = hitPose.position;
            }

        }

    }

}
