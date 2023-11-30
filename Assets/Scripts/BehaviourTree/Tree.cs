using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTree
{
    public abstract class Tree : MonoBehaviour
    {
        public Node root = null;

        protected void Start()
        {
            //zorg dat initialization BOVEN staat. het moet voor alles geroepen worden.
            Initialization();
            root = SetupTree();
        }

        private void Update()
        {
            if (root != null)
            {
                root.Evaluate();
            }
        }
        protected abstract Node SetupTree();
        protected virtual void Initialization() { }
    }
}
