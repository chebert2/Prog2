// Environment.java -- a data structure for Scheme environments

// An Environment is a list of frames.  Each frame represents a scope
// in the program and contains a set of name-value pairs.  The first
// frame in the environment represents the innermost scope.

// For the code below, I am assuming that a frame is implemented
// as an association list, i.e., a list of two-element lists.  E.g.,
// the association list ((x 1) (y 2)) associates the value 1 with x
// and the value 2 with y.

// To implement environments in an object-oriented style, it would
// be better to define a Frame class and make an Environment a list
// of such Frame objects or to implement a frame as a hash table.

// You need the following methods for modifying environments:
//  - constructors:
//      - create the empty environment (an environment with an empty frame)
//      - add an empty frame to the front of an existing environment
//  - lookup the value for a name (for implementing variable lookup)
//      if the name exists in the innermost scope, return the value
//      if it doesn't exist, look it up in the enclosing scope
//      if we don't find the name, it is an error
//  - define a name (for implementing define and parameter passing)
//      if the name already exists in the innermost scope, update the value
//      otherwise add a name-value pair as first element to the innermost scope
//  - assign to a name (for implementing set!)
//      if the name exists in the innermost scope, update the value
//      if it doesn't exist, perform the assignment in the enclosing scope
//      if we don't find the name, it is an error

using System;

namespace Tree
{
    public class Environment : Node
    {
        // An Environment is implemented like a Cons node, in which
        // every list element (every frame) is an association list.
        // Instead of Nil(), we use null to terminate the list.

        private Frame frame;     	// the innermost scope, an assoc list
	    private Environment env;	// the enclosing environment
   
        public Environment()
        {
            frame = new Frame() ;
            env = null;
        }
   
        public Environment(Environment e)
	{
            frame = new Frame();
            env = e;
        }

        public override void print(int n) {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.WriteLine("#{Environment");
            if (frame != null)
                frame.print(Math.Abs(n) + 4);
            if (env != null)
                env.print(Math.Abs(n) + 4);
            for (int i = 0; i < Math.Abs(n); i++)
                Console.Write(' ');
            Console.WriteLine('}');
        }

        // This is not in an object-oriented style, it's more or less a
        // translation of the Scheme assq function.
        private static Node find(Node id, Node alist)
        {
            if (! alist.isPair())
                return null;	// in Scheme we'd return #f
            else
            {
                Node parent_bind = alist.getCar();
                Node bind;
                if (parent_bind.isPair())
                {
                    bind = parent_bind.getCar();

                    // extra case  > look to further descendent cdr's since 
                    //first car is null , which it shouldnot be.

                    // if (bind.isNull() && !alist.getCdr().isNull())
                    //    return find(id, alist.getCdr());

                    if (id.getName().Equals(bind.getCar().getName()))
                        // return a list containing the value as only element
                        return bind.getCdr();
                    else
                        return find(id, alist.getCdr());
                }
                else
                {
                    Console.Error.WriteLine("Structure_ association list _ has wrong registered node somewhere.");
                    return null;
                }
            }
        }


        public Node lookup(Node id)
        {
            //bool continueLookup ?;
            if (frame.get_root_of_frame() != null)
            {
                //continueLookup == true;

                //  the associated list start is
                // represented by the root of the frame.
                Node val = find(id, frame.get_root_of_frame());

                if (val == null && env == null)
                {
                    Console.Error.WriteLine("undefined variable " + id.getName());
                    return null;
                }
                else if (val == null)
                    // look up the identifier in the enclosing scope
                    return env.lookup(id);
                // success in finding id
                else
                    // return value--node we got from find call.s
                    return val;
            }
            else
            // continue lookup current env == false
            {
                if (env != null)
                    return env.lookup(id);
                else
                    return null;
            }
        }


        public void define(Node id, Node val)
        {
            // we only want to define this in the frontmost frame at hand.
            Node result_lookup = find(id, frame.get_root_of_frame());
            if (result_lookup == null)
            {
                Cons node_being_added = new Cons(new Cons(id, val), this);
                // put new definition in front of frame ( association list structure )
                frame.set_root_car_node(node_being_added);
            }
            else
                // if preexisting...
                // update to newest value (,  in innermost scope). 
            {
                result_lookup = val;
            }

        }


        public void assign(Node id, Node val)
        {
            // search for id in id_value_pair in frame heirarchy
            Node result_lookup = lookup(id);
            // if nothing found, report error.
            if (result_lookup == null)
            {
                Console.Error.WriteLine("Exception in assigning: variable with that name does not exist");
            }
            else
                result_lookup = val;


            
        }
    }
}
