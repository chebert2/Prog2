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

			// throw error:
			return new StringLit ("Error: built eval : in is not meant to be used for evaluate in convention.");
		}


        // TODO: The method apply() should be defined in class Node
        // to report an error.  It should be overridden only in classes
        // BuiltIn and Closure.
		public override Node apply (Node args)
        {

			if (this.symbol.getName ().Equals ("read"))
				return (Node)Scheme4101.parser.parseExp ();



			else if (this.symbol.getName ().Equals ("b+")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary addition operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary addition.");

					else {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();
						return new IntLit (int1.getInt () + int2.getInt () );
					}

				}
				else
					return new StringLit ("Error: more than two arguments for binary addition is not permissable.");

			}

			else if (this.symbol.getName ().Equals ("b-")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary subtraction operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary subtraction.");

					else  {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();
						return new IntLit (int1.getInt () - int2.getInt () );
					}
				}
				else
					return new StringLit ("Error: more than two arguments for binary subtraction is not permissable.");

			}

			else if (this.symbol.getName ().Equals ("b*")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary multiplication operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary multiplication.");
						
					else {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();
						return new IntLit (int1.getInt () * int2.getInt () );
					}

				}
				else
					return new StringLit ("Error: more than two arguments for binary multiplication is not permissable.");

			}

			else if (this.symbol.getName ().Equals ("b/")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary division operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary division.");


					else  {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();
						if (int2.getInt () == 0)
							return new StringLit ("Error: input not acceptable. cannot divide by zero.");
						else
							return new IntLit (int1.getInt () / int2.getInt () );
					}

				}
				else
					return new StringLit ("Error: more than two arguments for binary division is not permissable.");

			}

			else if (this.symbol.getName ().Equals ("b=")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary equality test operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary equality test.");


					else  {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt() == int2.getInt());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				}
				else
					return new StringLit ("Error: more than two arguments for binary equality test is not permissable.");

			}


			else if (this.symbol.getName ().Equals ("b<")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary less than test operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary less than test.");

					else  {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt() < int2.getInt());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				}
				else
					return new StringLit ("Error: more than two arguments for binary less than test is not permissable.");

			}

			else if (this.symbol.getName ().Equals ("b>")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary greater than test operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary greater than test.");
						
					else {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt() > int2.getInt());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				}
				else
					return new StringLit ("Error: more than two arguments for binary greater than test is not permissable.");

			}


			else if (this.symbol.getName ().Equals ("b<=")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary less than equal than test operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary less than equal than test.");

					else   {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt() <= int2.getInt());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				}
				else
					return new StringLit ("Error: more than two arguments for binary less than equal than test is not permissable.");

			}

			else if (this.symbol.getName ().Equals ("b>=")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for binary greater than equal than test operation.");

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
					&& args.getCdr () != null && args.getCdr ().getCar () != null
					&& args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ())
						return new StringLit ("Error: arguments must be IntLit for binary greater than equal than test.");

					else  {
						IntLit int1 = (IntLit) args.getCar ();
						IntLit int2 = (IntLit) args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt() >= int2.getInt());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				}
				else
					return new StringLit ("Error: more than two arguments for binary greater than equal than test is not permissable.");

			}

			else if (this.symbol.getName ().Equals ("null?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for testing null evaluation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
					&& args.getCdr() != null  && args.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good) {
				
					if (args.getCar ().isNull ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				
				}


				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
			}
			else if (this.symbol.getName().Equals("symbol?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for testing symbol evaluation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
					&& args.getCdr() != null  && args.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good) {

					if (args.getCar ().isSymbol ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);

				}
				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");

			}
			else if (this.symbol.getName().Equals("number?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for testing number evaluation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
					&& args.getCdr() != null  && args.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isNumber ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				}
				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");

			}
			else if (this.symbol.getName().Equals("boolean?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for testing boolean evaluation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
					&& args.getCdr() != null  && args.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isBool ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				}

				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");

			}
			else if (this.symbol.getName().Equals("procedure?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for testing procedure evaluation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
					&& args.getCdr() != null  && args.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isProcedure ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				}

				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");

			}

			else if (this.symbol.getName().Equals("pair?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for testing pair evaluation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
					&& args.getCdr() != null  && args.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isPair ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				}

				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");

			}
			else if (this.symbol.getName().Equals("environment?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for testing environment evaluation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
					&& args.getCdr() != null  && args.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isEnvironment ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				}
				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");

			}
			else if (this.symbol.getName().Equals("string?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ())
					return new StringLit ("Error: no arguments given for testing string evaluation.");


				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
					&& args.getCdr() != null  && args.getCdr ().isNull () )
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isString ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				}
				else
					return new StringLit("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");

			}

			else
				return new StringLit("Error: builtin is not identifiable.");





    	}




    }    
}

