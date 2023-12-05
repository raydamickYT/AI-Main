using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        public Node root = null;
        private string aiID;
        protected GlobalBlackboard blackboard;

        protected void Start()
        {
            aiID = gameObject.name;
            GlobalBlackboard.Instance.RegisterAI(aiID, transform.position);

            //zorg dat initialization BOVEN staat. het moet voor alles geroepen worden.
            Initialization();
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
            {
                GlobalBlackboard.Instance.UpdateAIPosition(aiID, transform.position);

                root.Evaluate();
            }
        }

        void OnDestroy()
        {
            //verwijder de ai weer uit de global blackboard
            GlobalBlackboard.Instance.UnregisterAI(aiID);
        }
        protected abstract Node SetupTree();
        protected virtual void Initialization() { }
    }
}
