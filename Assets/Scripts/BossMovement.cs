using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {
    [SerializeField] private float speed;

    private Transform bossTr;
    // Start is called before the first frame update
    private void Start()
    {
        bossTr = GameObject.FindWithTag("Boss").transform;
    }

    // Update is called once per frame
    private void Update()
    {
        bossTr.Translate(speed * Time.deltaTime * Vector3.left);
    }
}
