using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Basket : MonoBehaviour {
    [Header("Set Dynamically")]
    public Text scoreGT;

    void Start() {
        GameObject scoreGO = GameObject.Find("ScoreCounter");
        scoreGT = scoreGO.GetComponent<Text>();
        scoreGT.text = "0";
    }

    void Update() {
        Vector3 mousePos2D = Input.mousePosition;
        mousePos2D.z = -Camera.main.transform.position.z;

        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(mousePos2D);

        Vector3 pos = transform.position;
        pos.x = mousePos3D.x;
        transform.position = pos;
    }

    void OnCollisionEnter(Collision coll) {
        GameObject collidedWith = coll.gameObject;
        Apple appleScript = collidedWith.GetComponent<Apple>();

        if (collidedWith.CompareTag("Apple")) {
            if (appleScript != null) {
                appleScript.Explode();
            } else {
                Destroy(collidedWith);
            }

            int score = int.Parse(scoreGT.text);
            score += 100;
            scoreGT.text = score.ToString();

            if (score > HighScore.score) {
                HighScore.score = score;
            }
        } else if (collidedWith.CompareTag("Castle")) {
            if (appleScript != null) {
                appleScript.ExplodeCastle();
            } else {
                Destroy(collidedWith);
            }

            int score = int.Parse(scoreGT.text);
            score -= 50;
            scoreGT.text = score.ToString();
        }
    }
}