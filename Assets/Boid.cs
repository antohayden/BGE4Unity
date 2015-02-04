using UnityEngine;
using System.Collections;
using System;


namespace BGE
{
    public class Boid : MonoBehaviour
    {
        public Vector3 seekTarget, velocity;
        private float mass;
        private Vector3  force, acceleration, look;
        private float speed, maxSpeed, maxForce;
        public GameObject camera_target;
        

        // Use this for initialization
        void Start()
        {
            mass = 1;
            maxForce = 1f;
            maxSpeed = 1f;
            seekTarget = new Vector3(0, 0, 1);
            transform.position =  new Vector3(0, 0, 0);
           
        }

        // Update is called once per frame
        void Update()
        {
            seekTarget = camera_target.transform.position + ( camera_target.transform.forward * 20 );
            if (Math.Abs(transform.position.magnitude - seekTarget.magnitude) < 0.01f)
                return;

            speed = velocity.magnitude;

            velocity = truncate(velocity + Seek(seekTarget), maxSpeed);

            transform.position = transform.position + velocity * Time.deltaTime;

            if (speed > 0.001f)
            {
                look = velocity;
                look.Normalize();
            }

            transform.LookAt(look * Time.deltaTime);

        }

        Vector3 Seek(Vector3 target) {

            Vector3 desired_velocity = Vector3.Normalize(target - transform.position);

            if(desired_velocity.magnitude <  0.001f)
                 return look;

            desired_velocity *= maxSpeed;

            Vector3 steering = desired_velocity - velocity;
            steering = truncate(steering, maxForce);
            steering /= mass;

            return steering;
        }

        Vector3 truncate( Vector3 v, float max)
        {
            if (v.magnitude > max)
            {
                v.Normalize();
                v *= max;
            }

            return v;
        }
    }
}
