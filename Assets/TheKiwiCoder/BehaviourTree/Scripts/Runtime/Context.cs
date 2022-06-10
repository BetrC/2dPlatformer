using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TheKiwiCoder {

    // The context is a shared object every node has access to.
    // Commonly used components and subsytems should be stored here
    // It will be somewhat specfic to your game exactly what to add here.
    // Feel free to extend this class 
    public class Context {
        public GameObject gameObject;
        public Transform transform;
        public Animator animator;
        public Rigidbody2D physics;
        public BoxCollider2D boxCollider;
        public BoxCollider2D triggerCollider;
        public Movement movement;
        public Enemy enemy;

        // Add other game specific systems here

        public static Context CreateFromGameObject(GameObject gameObject) {
            // Fetch all commonly used components
            Context context = new Context();
            context.gameObject = gameObject;
            context.transform = gameObject.transform;
            context.animator = gameObject.GetComponent<Animator>();
            context.physics = gameObject.GetComponent<Rigidbody2D>();
            context.boxCollider = gameObject.GetComponent<BoxCollider2D>();
            context.movement = gameObject.GetComponentInChildren<Movement>();
            context.enemy = gameObject.GetComponentInChildren<Enemy>();
            context.triggerCollider = gameObject.transform.Find("ColliderBox").GetComponent<BoxCollider2D>();
            
            // Add whatever else you need here...

            return context;
        }
    }
}