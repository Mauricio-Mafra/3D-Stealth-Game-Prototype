using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFoV : MonoBehaviour
{
    public Transform pontoVisao;
    public AudioSource aud;

    private string tagPlayer ="Player";
    [Range(2,180)]
    public int qtdRaios = 20;
    [Range(5,170)]
    public float angVisao = 120f;
    [Range(0.1f,170f)]
    public float distanciaVisao = 120f;
    [Range(1,10)]
    public int qtdCamadas = 3;
    [Range(0.02f, 0.15f)]
    public float distanciaCamada = 0.1f;

    [Space(10)]
    public List<Transform> inimigosvisiveis = new List<Transform>();
    List<Transform> tempCollision = new List<Transform>();
    public LayerMask player;
    public GameObject luzVisao;
    LayerMask obstaculos;
    float timer;
    [Range(0.02f, 0.25f)]
    public float timerLimite;

    bool detectado;
    bool audReprod;
    float timerFuga;
    Light luz;

    private NavMeshAgent agente;
    public GameObject[] waypoints = new GameObject[4];
    private int cont;

    private void Start()
    {
        agente = GetComponent<NavMeshAgent>();
        aud = GetComponent<AudioSource>();
        timer = 0;
        timerLimite = 0;
        detectado = false;
        audReprod = false;
        timerFuga = 0;
        obstaculos = ~player;
        luz = luzVisao.GetComponent<Light>();

    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= timerLimite)
        {
            timer = 0;
            checarFoV();
        }
        if(detectado == false)
        {
            agente.SetDestination(waypoints[cont].transform.position);
            if (Vector3.Distance(transform.position, waypoints[cont].transform.position) < 5f)
            {
                if (cont == 7)
                    cont = 0;
                else
                    cont++;
                agente.SetDestination(waypoints[cont].transform.position);
            }
        }
        if(detectado == true)
        {
            agente.SetDestination(GameObject.Find("PC").transform.position);
        }
        if(detectado == true && audReprod == false)
        {
            aud.Play();
            audReprod = true;
        }

        if (detectado == true && Vector3.Distance((transform.position),(GameObject.Find("PC").transform.position)) > 10f)
        {
            timerFuga -= Time.deltaTime;
            agente.SetDestination(GameObject.Find("PC").transform.position);
            if (timerFuga < 0)
            {
                timerFuga = 5f;
                luz.color = Color.yellow;
                detectado = false;
                aud.Stop();
                audReprod = false;
            }
        }
    }

    public void checarFoV()
    {
        float limiteCamadas = qtdCamadas * 0.5f;
        for(int x = 0; x <= qtdRaios; x++){
            for (float y = -limiteCamadas +0.5f; y<= limiteCamadas; y++){
                float angleToRay = x * (angVisao / qtdRaios) + ((100.0f - angVisao) * 0.5f);
                Vector3 directionMult = (-transform.right) + (transform.up * y * distanciaCamada);
                Vector3 rayDirection = Quaternion.AngleAxis(angleToRay, pontoVisao.up)* directionMult;

                RaycastHit hit;
                if (Physics.Raycast(pontoVisao.position, rayDirection, out hit, distanciaVisao))
                {
                    if(!hit.transform.IsChildOf(transform.root) && !hit.collider.isTrigger)
                    {
                        if(hit.collider.gameObject.CompareTag(tagPlayer))
                        {
                            Debug.DrawLine(pontoVisao.position, hit.point, Color.red);
                            luz.color = Color.red;
                            detectado = true;
                            timerFuga = 5f;

                            tempCollision.Add(hit.transform);
                            if (!inimigosvisiveis.Contains(hit.transform)){
                                inimigosvisiveis.Add(hit.transform);
                            }
                        }
                    }
                }
                else
                {
                    Debug.DrawRay(pontoVisao.position, rayDirection * distanciaVisao, Color.green);
                }

            }
        }
    }
}
