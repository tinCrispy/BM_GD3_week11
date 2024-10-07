using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerPostProcessing : MonoBehaviour
{
    public Volume volume1;
    public Volume volume2;
    DepthOfField dof;
    ColorAdjustments ca;
    float toxicity;
    int toxicityInt = 0;

    // Start is called before the first frame update
    void Start()
    {
        volume1.profile.TryGet(out dof);
        volume1.profile.TryGet(out ca);
        ca.contrast.value = -20f;
        toxicity = volume2.weight;




    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
        {
            dof.focusDistance.value = Vector3.Distance(Camera.main.transform.position, hit.point);

            if (dof.focusDistance.value < 3)
            {
                ca.hueShift.value = 125;
            }
            else
            {
                ca.hueShift.value = 0;
            }


            if (hit.collider != null)
            {
                Rigidbody target = hit.collider.attachedRigidbody;
                target.AddForce(Camera.main.transform.forward * 10, ForceMode.Impulse);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ca.contrast.value = 100f;
        }

       

        if (Input.GetKeyDown(KeyCode.X))
        {
            ca.contrast.value = -20f;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            volume2.weight += 0.1f * Time.deltaTime;
            string toxicityString = toxicity.ToString("0");
            toxicityInt = int.Parse(toxicityString);

        }
           else
                {
                     ToxicityDecay();
                }
    }

    void ToxicityDecay()
    {
        volume2.weight -= 0.1f *Time.deltaTime;
    }
}
