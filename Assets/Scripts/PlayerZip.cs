
using UnityEngine;

public class PlayerZip : MonoBehaviour
{
    [SerializeField] private float checkOffset = 1f;
    [SerializeField] private float checkRadius = 2f;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position + new Vector3(0, checkOffset, 0), checkRadius, Vector3.up);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.tag == "Zipline")
                {
                    hit.collider.GetComponent<Zipline>().StartZipline(gameObject);
                }
            }
        }
    }
}
