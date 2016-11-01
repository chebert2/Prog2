

// Frame -- Association list data tree  class for representing the key variables  and their contents

using System;

namespace Tree
{
    public class Frame : Node
    {
        private Node root_node;

        public bool has_elements;

        // construct an association list that only holds the nil
        public Frame() {

            root_node = null;
            has_elements = false;
            
        }

        public void set_root_car_node(Node node_val_for_car)
        {
            // place latest var binding in front of the current root list of vars in frame.
            Node new_root = new Cons(node_val_for_car, root_node);
            // update to this new root
            root_node = new_root;

            has_elements = true;
        }
        // this gives us a root node to work with for finding keys' data.
        public Node get_root_of_frame()
        {
            if (!has_elements)
            {
                
                //Console.Error.WriteLine("root has no elements yet.");
                return null;
            }
            else
            {
                return root_node;
            }
                
        }

        public override void print(int n)
        {
            // cannot print this... because of circular data structure.
        }

        public override void print(int n, bool p)
        {
            // cannot print this... because of circular data structure.
        }
        public override bool isFrame() { return true; }
    }
}
