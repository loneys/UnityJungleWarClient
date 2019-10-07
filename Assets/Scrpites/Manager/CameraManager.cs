using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : BaseManager
{
    private GameObject cameraGo;
    private Animator cameraAnim;
    private FollowTarget followTarget;
    private Vector2 orignalPosition;
    private Vector3 orignalRotation;

    public CameraManager(GameFacade facade) : base(facade) { }

    public override void OnInit()
    {
        Debug.Log("--wangzhi--有被调用到吗--");
        cameraGo = Camera.main.gameObject;
        cameraAnim = cameraGo.GetComponent<Animator>();
        followTarget = cameraGo.GetComponent<FollowTarget>();
        Debug.Log(followTarget);

    }

    public void FollowRole()
    {
        followTarget.target = facade.GetCurrentRoleGameObject().transform;
        cameraAnim.enabled = false;
        orignalPosition = cameraGo.transform.position;
        orignalRotation = cameraGo.transform.eulerAngles;

        Quaternion targetQuaternion = Quaternion.LookRotation(followTarget.target.position - cameraGo.transform.position);
        cameraGo.transform.DORotateQuaternion(targetQuaternion, 1f).OnComplete(delegate()
        {
            followTarget.enabled = true;
        });
        
    }


    //public override void Update()
    //{
    //    if(Input.GetMouseButtonDown(0))
    //    {
    //        FollowTarget(null);
    //    }
    //    if(Input.GetMouseButtonDown(1))
    //    {
    //        WalkThroughScene();
    //    }
    //}

    public void WalkThroughScene()
    {
        followTarget.enabled = false;
        cameraGo.transform.DOMove(orignalPosition, 1f);
        cameraGo.transform.DORotate(orignalRotation, 1f).OnComplete(delegate ()
        {
            cameraAnim.enabled = true;
        });
    }

}
