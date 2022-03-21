using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoleManager : MonoBehaviour
{
    List<MoleController> moles = new List<MoleController>();
    bool generate;
    public AnimationCurve maxMoles;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Mole");

        foreach(GameObject go in gos)
        {
            moles.Add(go.GetComponent<MoleController>());
        }

        this.generate = false;
    }

    public void StartGenerate()
    {
        StartCoroutine("Generate");
    }

    public void StopGenerate()
    {
        this.generate = false;
    }

    IEnumerator Generate()
    {
        this.generate = true;

        while(this.generate)
        {
            yield return new WaitForSeconds(1.0f);

            int n = moles.Count;

            int maxNum = (int)this.maxMoles.Evaluate(GameManager.time);

            for(int i = 0; i < maxNum; i++)
            {
                print(2);
                this.moles[Random.Range(0, n)].Up();

                yield return new WaitForSeconds(0.3f);
            }
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
