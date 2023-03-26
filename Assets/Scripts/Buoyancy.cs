using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public class Buoyancy : MonoBehaviour
{
    public Transform[] floaters; 

    public float underWaterDrag = 3f;

    public float underWaterAngularDrag = 1f;

    public float airDrag = 0f;

    public float airAngularDrag = 0.05f;

    public float floatingPower = 15f;

    OceanManager oceanManager;

    Rigidbody m_Rigidbody;

    private int floatersUnderWater;

    private bool underwater;


    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        oceanManager= FindObjectOfType<OceanManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        floatersUnderWater = 0;
        for (int i = 0;i < floaters.Length; i++)
        {
            float difference = floaters[i].position.y - oceanManager.WaterHeightAtPosition(floaters[i].position);

            if (difference < 0)
            {
                m_Rigidbody.AddForceAtPosition(Vector3.up * floatingPower * Mathf.Abs(difference), floaters[i].position, ForceMode.Force);
                floatersUnderWater++;
                if (!underwater)
                {
                    underwater = true;
                    SwitchState(true);
                }
            }

        
        }
        if (underwater && floatersUnderWater == 0)
        {
            underwater = false;
            SwitchState(false);
        }
    }

    void SwitchState(bool isUnderwater)
    {
        if (isUnderwater)
        {
            m_Rigidbody.drag = underWaterDrag;
            m_Rigidbody.angularDrag = underWaterAngularDrag;
        }
        else
        {
            m_Rigidbody.drag = airDrag;
            m_Rigidbody.angularDrag = airAngularDrag;
        }
    }
}
