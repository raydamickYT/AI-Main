using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        public Node root = null;
        private string aiID;
        protected GlobalBlackboard blackboard = new();
        public Text StateText;
        [Tooltip("Zorg dat de camera of camera holder op dit object zit.")]
        public GameObject CameraHolder;

        protected void Start()
        {
            aiID = gameObject.name;
            GlobalBlackboard.Instance.RegisterAI(aiID, transform.position);

            //zorg dat initialization BOVEN staat. het moet voor alles geroepen worden.
            Initialization();
            root = SetupTree();
            root.SetupBlackboard(blackboard);
        }

        private void Update()
        {
            if (root != null)
            {
                GlobalBlackboard.Instance.UpdateAIPosition(aiID, transform.position);

                root.Evaluate();
            }
            if (CameraHolder != null && StateText != null)
            {
                StateText.gameObject.transform.LookAt(CameraHolder.transform.position);
                StateText.transform.Rotate(0, 180, 0);
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
