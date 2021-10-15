using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsRock : MonoBehaviour
{

    public bool IsIns = false;//check availibility
    public float InsTime = 15;//倒计时，木头变成树的时间

    public GameObject PreRock;//pre made rock

    public GameObject stone;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (IsIns == false)
        {
            //varies with time
            InsTime -= Time.deltaTime;


            if (InsTime < 0)
            {
                if (stone != null)
                {
                    Destroy(stone);
                }

                IsIns = true;

                //当为0的话，实例化树生效
                Instantiate(PreRock, this.transform.position, this.transform.rotation);
                //
                Destroy(this.gameObject);
            }
        }


    }



}
