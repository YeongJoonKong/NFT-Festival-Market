using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkManager : MonoBehaviour
{
    List<SharkController> Sharks = new List<SharkController>();
    bool generate;
    public AnimationCurve maxSharkNum;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] sos = GameObject.FindGameObjectsWithTag("Shark");

        foreach(GameObject so in sos)
        {
            Sharks.Add(so.GetComponent<SharkController>());
        }
        this.generate = false;
    }

    public void StartGenerate()
    {
        StartCoroutine(Generate());
    }

    public void StopGenerate()
    {
        StopCoroutine(Generate());
        this.generate = false;
    }

    IEnumerator Generate()
    {
        this.generate = true;

        while(this.generate)
        {
            yield return new WaitForSeconds(1.0f);
            
            int n = Sharks.Count;

            int maxNum = (int)this.maxSharkNum.Evaluate(SharkGamaManager.SharkTime);

            for (int i = 0; i < maxNum; i++)
            {
                this.Sharks[Random.Range(0, n)].Up();

                yield return new WaitForSeconds(0.3f);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }




}
