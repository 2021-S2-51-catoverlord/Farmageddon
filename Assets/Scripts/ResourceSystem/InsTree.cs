using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsTree : MonoBehaviour
{
    public bool IsIns = false; //是否有生成
    public float InsTime = 15; //倒计时，木头变成树的时间

    public GameObject PreTree; //树预制体
    public GameObject Lumber;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(IsIns == false)
        {
            // 随时间变化
            InsTime -= Time.deltaTime;

            if(InsTime < 0)
            {
                if(Lumber != null)
                {
                    Destroy(Lumber);
                }

                IsIns = true;

                float Pos_x = Random.Range(-40, 0);
                float Pos_y = Random.Range(-17, -1);

                Vector3 Pos = new Vector3(Pos_x, Pos_y, this.transform.position.z);

              
                Instantiate(PreTree, Pos, transform.rotation);

                // 当为0的话，实例化树生效
                Instantiate(PreTree, transform.position, transform.rotation);
                // 自我消耗木头
                Destroy(gameObject);
            }
        }
    }
}
