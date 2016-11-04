// BuiltIn -- the data structure for built-in functions

// Class BuiltIn is used for representing the value of built-in functions
// such as +.  Populate the initial environment with
// (name, new BuiltIn(name)) pairs.

// The object-oriented style for implementing built-in functions would be
// to include the C# methods for implementing a Scheme built-in in the
// BuiltIn object.  This could be done by writing one subclass of class
// BuiltIn for each built-in function and implementing the method apply
// appropriately.  This requires a large number of classes, though.
// Another alternative is to program BuiltIn.apply() in a functional
// style by writing a large if-then-else chain that tests the name of
// the function symbol.

using System;

namespace Tree
{
    public class BuiltIn : Node
    {
        private Node symbol;            // the Ident for the built-in function

        public BuiltIn(Node s)		{ symbol = s; }

		public Node getSymbol()		{ return symbol; }

        // Done.. The method isProcedure() should be defined in
        // class Node to return false.
        public  override  bool isProcedure()	{ return true; }

		public override bool isBuiltIn(){ return true;
		}

        public override void print(int n)
        {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.Write("#{Built-in Procedure ");
            if (symbol != null)
                symbol.print(-Math.Abs(n));
            Console.Write('}');
            if (n >= 0)
                Console.WriteLine();
        }


		// TODO : build eval.
		public override Node eval (Node arguments_for_delivery, Environment env_given) {


			if (this.symbol.getName ().Equals ("eval"))
				return arguments_for_delivery.eval (env_given);
			else if (this.symbol.getName ().Equals ("read"))
				return this.apply (null);
			else if (this.symbol.getName ().Equals ("b+")) {

				// if there are no arguments, report error
				if (arguments_for_delivery != null || arguments_for_delivery.isNull ())
					return new StringLit ("Error: no arguments given for binary addition operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (arguments_for_delivery != null && arguments_for_delivery.getCar () != null
				   && arguments_for_delivery.getCdr () != null && arguments_for_delivery.getCdr ().getCar () != null
					&& arguments_for_delivery.getCdr ().getCdr ().isNull ()) {

					if (!arguments_for_delivery.getCar ().isNumber () || !arguments_for_delivery.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary addition.");
					else
					    return this.apply (arguments_for_delivery);
						}
				else
					return new StringLit ("Error: more than two arguments for binary addition is not permissable.");

			} else if (this.symbol.getName ().Equals ("null?")) {

				// if there are no arguments, report error
				if (arguments_for_delivery != null || arguments_for_delivery.isNull ())
					return new StringLit ("Error: no arguments given for binary addition operation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (arguments_for_delivery != null && arguments_for_delivery.getCar () != null
					&& arguments_for_delivery.getCdr() != null  && arguments_for_delivery.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good)
					return this.apply (arguments_for_delivery);
				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 pair node with nil tail for argument.");
			}
			else
				return new StringLit("Error: builtin is not identifiable.");
		}


        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.
		public override Node apply (Node args)
        {
			if (this.symbol.getName ().Equals ("read"))
				return (Node) Scheme4101.parser.parseExp ();


			else if (this.symbol.getName ().Equals ("b+")) {
				IntLit int1 = (IntLit) args.getCar ();
				IntLit int2 = (IntLit) args.getCdr ().getCar ();
				return new IntLit (int1.getInt () + int2.getInt () );
			}
			else if(this.symbol.getName().Equals ("null?")) {
				if(args.getCar().isNull())
					return new BoolLit(true);
				else
					return new BoolLit(false);
			}
			else
				return null;


    	}




    }    
}

