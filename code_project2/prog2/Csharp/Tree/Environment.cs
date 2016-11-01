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
            if (alist == null || ! alist.isPair())
                return null;	// in Scheme we'd return #f
            else
            {
                
                Node bind = alist.getCar();
                if (id.getName().Equals(bind.getCar().getName()))
                    // return a list containing the value as only element
                    return bind.getCdr();
                else
                    return find(id, alist.getCdr());
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
                    return val.getCar();
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

            // it should be noted  that when parsing the written structure
            //   print elements,... if there is a left parenthesis in front
            //  in front of variable name, ... we are dealing with a function definition.
            //  
            //  and the variable name ought to be considered an identifier.




            // function example  : where root cons node is given by: identifier 
            // a   .  

            // a equals  : (define (xoo x) (+ x x))
            
            // to get  expression for lambda out of any function defintion...

            // node funcLambda = (cons 'lambda (cons (cdr (car (cdr a))) (cdr (cdr a))))

            // xoo    can be looked up by querying:  (car (car (cdr a)))




            // we only want to define this in the frontmost frame at hand.
            Node result_lookup = find(id, frame.get_root_of_frame());
            if (result_lookup == null)
            {
                Cons node_being_added = new Cons(id, new Cons(val, Nil.getInstance()));
                // put new definition in front of frame ( association list structure )
                frame.set_root_car_node(node_being_added);
            }
            else
                // if preexisting...
                // update  node for value (,  in innermost scope). 
            {
                result_lookup = new Cons(val, Nil.getInstance() ) ;
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
                result_lookup = new Cons(val, Nil.getInstance() );


            
        }
    }
}
