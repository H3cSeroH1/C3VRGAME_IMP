using UnityEngine;
using CleverCrow.Fluid.BTs.Tasks;
using CleverCrow.Fluid.BTs.Tasks.Actions;
using CleverCrow.Fluid.BTs.Trees;
using System.Collections.Generic;
using UnityEngine.AI;

public class AIMovementController : MonoBehaviour
{
    [SerializeField]
    private BehaviorTree _tree;

    public List<GameObject> DetectionObjList = new List<GameObject>();

    public bool DetectSomethingIcon = false;
    public bool CautionSomething = false;

    public bool DetectHostile = false;

    public float agentNomalSpeed = 5f;
    public float agentAlartSpeed = 7.5f;
    public float agentPersonalSpace = 3f;
    public float agentAttackingMargin = 3f;

    public Transform[] points;
    private int destPoint = 0;

    public NavMeshAgent agent;

    public string PathState = null;


    Coroutine coroutine;


    private void Awake()
    {
        _tree = new BehaviorTreeBuilder(gameObject)//根の部分


            .Selector()//以下の★の部分から成功が返ってきたところを実行（上から順番に検証）

                .Sequence("omg")

                    .Condition("もしデテクションコーンになにかがひっかかってたら", () =>
                    DetectSomethingIcon)

                        .Selector()

                            .Sequence("aaaa")

                                .Condition("みつけたのが敵対", () =>//分岐に使うやつ？？？？
                                DetectHostile)

                                .Selector()

                                    .Sequence("Nested Sequence")
                                        .Condition("Custom Condition", () =>
                                        {
                                            if(Vector3.Distance(this.gameObject.transform.position, DetectionObjList[0].transform.position) >= 4)
                                            {
                                                return true;
                                            }

                                            return false;
                                        })
                                        .Do("遠いから近づけ―", () =>
                                        {

                                            Debug.Log("敵を見つけた！");
                                            agent.speed = agentAlartSpeed;

                                            agent.SetDestination(DetectionObjList[0].gameObject.transform.position);//NavMeshをつかってプレイヤーの位置に行く


                                            return TaskStatus.Success;
                                        })

                                    .End()

                                    .Sequence("Nested Sequence")
                                        .Condition("Custom Condition", () =>
                                        {
                                            if ( Vector3.Distance(this.gameObject.transform.position, DetectionObjList[0].transform.position) < 4  &&  Vector3.Distance(this.gameObject.transform.position, DetectionObjList[0].transform.position) >= 3)
                                            {
                                                return true;
                                            }

                                            return false;
                                        })

                                        .Selector()

                                            .Do("ちけえからとまれ～＝", () =>
                                            {

                                                Debug.Log("近づいた！");
                                                agent.speed = 0;

                                                transform.LookAt(DetectionObjList[0].transform);

                                                agent.SetDestination(DetectionObjList[0].gameObject.transform.position);


                                                int index = Random.Range(0, 250);
                                                //Debug.Log(index);

                                                if (index == 0)
                                                {



                                                    return TaskStatus.Continue;
                                                }




                                                return TaskStatus.Success;
                                            })

                                        .End()

                                    .End()

                                    .Sequence("Nested Sequence")
                                        .Condition("Custom Condition", () =>
                                        {
                                            if (Vector3.Distance(this.gameObject.transform.position, DetectionObjList[0].transform.position) < 3)
                                            {
                                                return true;
                                            }

                                            return false;
                                        })

                                        .Selector()

                                            .Do("近すぎるンゴ！＝", () =>
                                            {
                                                Debug.Log("ちかすぎ！");

                                                Vector3 testver = this.gameObject.transform.position + Vector3.Scale(-this.gameObject.transform.forward, new Vector3(0, 0, agentPersonalSpace));

                                                agent.SetDestination(testver);

                                                agent.speed = agentNomalSpeed;

                                                transform.LookAt(DetectionObjList[0].transform);

                                                int index = Random.Range(0, 250);

                                                if (index == 0)
                                                {

                                                    return TaskStatus.Continue;
                                                }


                                                if (ReachDistenation())
                                                {
                                                    return TaskStatus.Success;
                                                }

                                                return TaskStatus.Failure;
                                            })

                                        .End()
                                    .End()
                                .End()
                            .End()

                            .Sequence("aaaa")

                                .Condition("みつけたのが敵以外", () =>//分岐に使うやつ？？？？
                                !DetectHostile)

                                .Selector()

                                    .Sequence("Nested Sequence")
                                        .Condition("Custom Condition", () =>
                                        {
                                            if (Vector3.Distance(this.gameObject.transform.position, DetectionObjList[0].transform.position) >= 4)
                                            {
                                                return true;
                                            }

                                            return false;
                                        })
                                        .Do("遠いから近づけ―", () =>
                                        {

                                            Debug.Log("敵を見つけた！");
                                            agent.speed = agentAlartSpeed;

                                            agent.SetDestination(DetectionObjList[0].gameObject.transform.position);//NavMeshをつかってプレイヤーの位置に行く


                                            return TaskStatus.Success;
                                        })

                                    .End()

                                    .Sequence("Nested Sequence")
                                        .Condition("Custom Condition", () =>
                                        {
                                            if (Vector3.Distance(this.gameObject.transform.position, DetectionObjList[0].transform.position) < 4 && Vector3.Distance(this.gameObject.transform.position, DetectionObjList[0].transform.position) >= 3)
                                            {
                                                return true;
                                            }

                                            return false;
                                        })

                                        .Selector()

                                            .Do("ちけえからとまれ～＝", () =>
                                            {

                                                Debug.Log("近づいた！");
                                                agent.speed = 0;

                                                transform.LookAt(DetectionObjList[0].transform);

                                                agent.SetDestination(DetectionObjList[0].gameObject.transform.position);


                                                int index = Random.Range(0, 250);
                                                //Debug.Log(index);

                                                if (index == 0)
                                                {



                                                    return TaskStatus.Continue;
                                                }




                                                return TaskStatus.Success;
                                            })

                                        .End()

                                    .End()

                                    .Sequence("Nested Sequence")
                                        .Condition("Custom Condition", () =>
                                        {
                                            if (Vector3.Distance(this.gameObject.transform.position, DetectionObjList[0].transform.position) < 3)
                                            {
                                                return true;
                                            }

                                            return false;
                                        })

                                        .Selector()

                                            .Do("近すぎるンゴ！＝", () =>
                                            {
                                                Debug.Log("ちかすぎ！");

                                                Vector3 testver = this.gameObject.transform.position + Vector3.Scale(-this.gameObject.transform.forward, new Vector3(0, 0, agentPersonalSpace));

                                                agent.SetDestination(testver);

                                                agent.speed = agentNomalSpeed;

                                                transform.LookAt(DetectionObjList[0].transform);

                                                int index = Random.Range(0, 250);

                                                if (index == 0)
                                                {

                                                    return TaskStatus.Continue;
                                                }


                                                if (ReachDistenation())
                                                {
                                                    return TaskStatus.Success;
                                                }

                                                return TaskStatus.Failure;
                                            })

                                        .End()
                                    .End()
                                .End()
                            .End()
                            //omg
                        .End()
                    .End()//Sequenceここまで


                .Sequence()//★　以下を順番に実行　実行失敗が返ってきたらこれも実行失敗になる


                     .Do("スピードリセット", () =>
                     {
                         

                         agent.speed = agentNomalSpeed;


                         return TaskStatus.Success;
                     })

                    .Condition("探索完了した？", () =>//分岐に使うやつ？？？？？
                    

                        ReachDistenation()


                    )

                    //.WaitTime(5f)

                    .Do("次の場所に移動しろ", () =>
                    {

                        // Done
                        Debug.Log("次の場所に行こう");

                        GotoNextPoint();//パトロールのチェックポイントを次の場所に変える
                        agent.SetDestination(points[destPoint].position);//NavMeshをつかって次のチェックポイントの位置に行く


                        agent.speed = 2;

                        Debug.Log("うろうろしてる");//基本的にチェックポイントに到達するまで走る


                        return TaskStatus.Success;
                    })
                .End()



                

            .End()
            .Build();
    }


    private void Update()
    {
        // Update our tree every frame
        _tree.Tick();


    }


    private void OnTriggerEnter(Collider other)
    {
        DetectionIdentify(other);

    }

    private void OnTriggerStay(Collider other)
    {
        DetectionIdentify(other);


    }

    private void OnTriggerExit(Collider other)
    {


        if (other.gameObject.tag == "Player")
        {
            DetectionObjList.Remove(other.gameObject);

            DetectSomethingIcon = false;
            CautionSomething = true;
            DetectHostile = false;
        }

    }

    public bool ReachDistenation()
    {
        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)//ここまでチェックポイントに到着したかの判断
                {

                    return true;
                }
            }
        }
        return false;
    }

    void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

    public void DetectionIdentify(Collider other)
    {
        
        
        var heading = other.transform.position - this.transform.position;
        var distance = heading.magnitude;
        var direction = heading / distance;

        Ray ray = new Ray(this.transform.position, direction);
        int layerMask = LayerMask.GetMask(new string[] { "Detector", "Default" });


        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distance, layerMask) && !DetectSomethingIcon)
        {
            
            //Debug.Log("レイ" + other.gameObject.tag);
            //Debug.Log("コライダー" + hit.collider.gameObject.tag);

            //プレイヤーを見つけた時！
            if (other.gameObject.tag == "Player" && hit.collider.gameObject.tag == "Player")
            {
                DetectionObjList.Add(other.gameObject);

                DetectSomethingIcon = true;

                DetectHostile = true;
            }
        }

        Debug.DrawRay(ray.origin, ray.direction, Color.red, distance);
    }



}