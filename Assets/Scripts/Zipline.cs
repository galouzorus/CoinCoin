using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zipline : MonoBehaviour
{
    [SerializeField] private Zipline targetZip;
    [SerializeField] private float zipSpeed = 5f;
    [SerializeField] private float zipScale = 0.2f;

    [SerializeField] public float arrivalThreshold = 0.4f;
    [SerializeField] private LineRenderer cable;

    public Transform ZipTransform;

    private bool zipping = false;
    private GameObject localZip;


    private void Awake()
    {
        cable.SetPosition(0, ZipTransform.position);
        cable.SetPosition(1, targetZip.ZipTransform.position);
    }
    // Update is called once per frame
    void Update()
    {
        if (!zipping || localZip == null) return;

        localZip.GetComponent<Rigidbody>().AddForce((targetZip.ZipTransform.position - ZipTransform.position).normalized * zipSpeed * Time.deltaTime, ForceMode.Acceleration);

        if (Vector3.Distance(localZip.transform.position, targetZip.ZipTransform.position) <= arrivalThreshold)
        {
            ResetZipline();
        }
    }

    public void StartZipline(GameObject player)
    {
        if (zipping) return;

        localZip = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        localZip.transform.position = ZipTransform.position;
        localZip.transform.localScale = new Vector3(zipScale, zipScale, zipScale);
        localZip.AddComponent<Rigidbody>().useGravity = false;
        localZip.GetComponent<Collider>().isTrigger = true;

        player.GetComponent<Rigidbody>().useGravity = false;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //trouver la correspondance pour desactiver input et controller         import invector controller namespace ?
        //player.GetComponent<vThirdPersonInput>().enable = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        //player.GetComponent<vThirdPersonController>().enable = false;
        player.transform.parent = localZip.transform;
        zipping = true;
    }

    private void ResetZipline()
    {
        if (!zipping) return;
        GameObject player = localZip.transform.GetChild(0).gameObject;
        player.GetComponent<Rigidbody>().useGravity = true;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //trouver la correspondance pour desactiver input et controller
        player.GetComponent<PlayerMovement>().enabled = true;
        //player.GetComponent<vThirdPersonController>().enable = true;
        player.transform.parent = null;
        Destroy(localZip);
        localZip = null;
        zipping = false;
        Debug.Log("Zipline Reset");
    }
}