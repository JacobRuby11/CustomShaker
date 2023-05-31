using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts {
    public class CameraShaker : MonoBehaviour
    {
        [SerializeField] AnimationCurve xCurve;
        [SerializeField] AnimationCurve yCurve;
        [SerializeField] float xMagnitude;
        [SerializeField] float yMagnitude;
        [Range(1,1000)]
        [SerializeField] int resolution;
        float stepLen;
        float time;
        Vector3 startPos;

        // Start is called before the first frame update
        void Start()
        {
            startPos = gameObject.transform.localPosition;

            // Get time of longest keyframe
            time = xCurve.keys[xCurve.keys.Length - 1].time;
            if(xCurve.keys[xCurve.keys.Length - 1].time > time) {
                time = xCurve.keys[xCurve.keys.Length - 1].time;
            }      
            // Get step length from time
            stepLen = time/resolution;

            StartCoroutine(Shake());
        }


        public IEnumerator Shake(){
            float current = 0;
            while(current < time){
                float nextX = xCurve.Evaluate(current) * xMagnitude;
                float nextY = yCurve.Evaluate(current) * yMagnitude;

                gameObject.transform.localPosition = startPos + new Vector3(nextX, nextY, startPos.z); 
                yield return new WaitForSeconds(stepLen);
                current += stepLen;
            }


        }
    }
}
