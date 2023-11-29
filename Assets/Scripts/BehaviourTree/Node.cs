using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace BehaviourTree
{
    public enum NodeState
    {
        RUNNING,
        SUCCES,
        FAILURE
    }
    public class Node
    {
        protected NodeState state;

        public Node Parent;
        protected List<Node> Children = new();
        private Dictionary<string, object> dataContext = new();

        public Node()
        {
            Parent = null;
        }

        public Node(List<Node> children)
        {
            foreach (Node Child in children)
            {
                attach(Child);
            }
        }

        private void attach(Node node)
        {
            node.Parent = this;
            Children.Add(node);
        }

        public virtual NodeState Evaluate() => NodeState.FAILURE;

        public void SetData(string key, object value)
        {
            dataContext[key] = value;
        }

        //this function returns an object from the dictionary
        public object GetData(string key)
        {
            //tries to get a value from the list based on the key
            object value = null;
            if (dataContext.TryGetValue(key, out value))
                return value;

            //if the key isn't found, this checks every node until it finds the key we were looking or it reaches a node without parent (root of the tree).
            Node node = Parent;
            while (node != null)
            {
                value = node.GetData(key);
                if (value != null)
                {
                    return value;
                }
                node = node.Parent;
            }
            return null;
        }

        //this function clears an object from the dictionary
        public bool ClearData(string key)
        {
            //checks if key is in dictionary
            if (dataContext.ContainsKey(key))
            {
                //succes: return true
                dataContext.Remove(key);
                return true;
            }

            //if we reach the root, we ignore the request.
            Node node = Parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if (cleared)
                {
                    return true;
                }
                node = node.Parent;
            }
            return false;
        }
    }

}
