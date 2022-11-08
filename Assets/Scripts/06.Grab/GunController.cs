using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GunController : MonoBehaviour
{
    public GameObject bullet;
    public Transform spawnPoint;
    public float fireSpeed = 40;

    private Transform leftHandAttachPoint;
    private Transform rightHandAttachPoint;
    private XRGrabInteractable grabbable;

    void Start()
    {
        leftHandAttachPoint = transform.Find("LeftHand Attach Point");
        rightHandAttachPoint = transform.Find("RightHand Attach Point");
        grabbable = GetComponent<XRGrabInteractable>();
        grabbable.selectEntered.AddListener(ChangeAttachTransform);
        grabbable.activated.AddListener(FireBullet);
    }

    private void ChangeAttachTransform(SelectEnterEventArgs arg)
    {
        Transform interactor = arg.interactorObject.transform; 
        if (interactor.name == "LeftHand Controller")
        {
            grabbable.attachTransform = leftHandAttachPoint;
        }
        else if (interactor.name == "RightHand Controller")
        {
            grabbable.attachTransform = rightHandAttachPoint;
        }
    }

    private void FireBullet(ActivateEventArgs arg)
    {
        GameObject spawnBullet = Instantiate(bullet,spawnPoint.position,spawnPoint.rotation); 
        spawnBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
        Destroy(spawnBullet,5);
    }
}
