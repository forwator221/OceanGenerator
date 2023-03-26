using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OceanManager : MonoBehaviour
{
    // Wave params
    public float waveHeight =1f;
    public float waveScale = 1f;
    public float waveSpeed = 1f;

    //Foam params
    public float foamAmount = 2.78f;
    public float foamCutOff = 2.35f;
    public float foamScale = 20000;
    public float foamSpeed = 0.0001f;


    public Transform ocean;

    Material oceanMaterial;

    Texture2D displacementWaves;
    // Start is called before the first frame update
    void Start()
    {
        SetVariables();
    }

    void  SetVariables()
    {
        oceanMaterial = ocean.GetComponent<Renderer>().sharedMaterial;
        displacementWaves = (Texture2D)oceanMaterial.GetTexture("_WavesDisplacement");
    }

    public float WaterHeightAtPosition(Vector3 position)
    {
        return ocean.position.y + displacementWaves.GetPixelBilinear(position.x * waveScale /100, position.z * waveScale/100 + Time.time * waveSpeed/100).g * waveHeight/100 * ocean.localScale.x;
    }

    private void OnValidate()
    {
        if(!oceanMaterial)
        {
            SetVariables();
        }
        UpdateMaterial();
    }
    void UpdateMaterial()
    {
        oceanMaterial.SetFloat("_WaveScale", waveScale/100);
        oceanMaterial.SetFloat("_WaveSpeed", waveSpeed/100);
        oceanMaterial.SetFloat("_WaveHeight", waveHeight/100);
        oceanMaterial.SetFloat("_FoamAmount", foamAmount);
        oceanMaterial.SetFloat("_FoamCutOff", foamCutOff);
        oceanMaterial.SetFloat("_FoamScale", foamScale);
        oceanMaterial.SetFloat("_FoamSpeed", foamSpeed);
    }
}
