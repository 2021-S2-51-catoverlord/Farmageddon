using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsRock : MonoBehaviour
{
    public bool IsIns = false; //check availibility
    public float InsTime = 15; //counting down lumber become tree

    public GameObject PreRock; //pre made rock
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

                float Pos_x = Random.Range(-40, 0);
                float Pos_y = Random.Range(-17, -1);

                Vector3 Pos = new Vector3(Pos_x, Pos_y, this.transform.position.z);

               
                Instantiate(PreRock, Pos, transform.rotation);
                //when equal to 0 , start instantiate.
                Instantiate(PreRock, transform.position, transform.rotation);
               
                Destroy(gameObject);
            }
        }
    }
}
