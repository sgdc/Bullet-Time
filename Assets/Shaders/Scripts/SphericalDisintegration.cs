using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphericalDisintegration : MonoBehaviour
{
    Transform trans;
    Material[] mats;
    float expansion;
    Vector4[] localPoints;
    Vector4[] points;

    void Start()
    {
        trans = GetComponent<Transform>();
        mats = GetComponent<Renderer>().materials;
        foreach (Material mat in mats)
        {
            mat.SetInt("_OrbsLength", 5);
        }
        localPoints = new Vector4[5];
        points = new Vector4[5];
        for (int i = 0; i < 5; i++)
        {
            localPoints[i] = new Vector4(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        }
    }
    
    void Update()
    {

    }
    public IEnumerator ExpandCoroutine(float seconds)
    {
        float time = 0;
        while (time<seconds)
        {
            expansion = time / seconds;
            updateShaders();
            time += Time.deltaTime;
            yield return null;
        }
        expansion = 1;
        updateShaders();
    }
    public void Expand(float seconds)
    {
        StartCoroutine(ExpandCoroutine(seconds));
    }
    private void updateShaders()
    {
        Vector4 localPos = trans.position;
        for (int i = 0; i < 5; i++)
        {
            points[i] = localPos + localPoints[i];
        }
        foreach (Material mat in mats)
        {
            mat.SetFloat("_Expansion", expansion);
            mat.SetVectorArray("_Orbs", points);
        }
    }
}
