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
				if (args == null) {
					Console.WriteLine ("Error: no arguments given for binary addition operation.");
					return Nil.getInstance ();

				}

				// return zero if there are no arguments
				if (args.isNull ())
					return new IntLit (0);
				// extend for all argument vars
				// and
				//check if first args have null
				if (args.getCar () != null
				    && args.getCdr () != null) {


					bool twoArguments_or_more;
					// check if second argument is is nil.
					if (args.getCdr ().isNull ())
						twoArguments_or_more = false;
					// see if there is a second item ,    ... and check that last tail of args is nil.
					else if (args.getCdr ().getCar () != null && args.getCdr ().getCdr ().isNull ())
						twoArguments_or_more = true;
					else if (args.getCdr ().getCar () != null && !args.getCdr ().getCdr ().isNull ()) {
						Console.WriteLine ("Error: cannot process more than two args for binary addition.");
						return Nil.getInstance ();
					} else {
						Console.WriteLine ("Error: in b+ , one of arguments for expression has null node.");
						return Nil.getInstance ();
					}

					if (twoArguments_or_more == false) {
						// check if argument is an intLit
						if (args.getCar ().isNumber ()) 
							// return the sole int lit node.
							return args.getCar ();
						else { 
							Console.WriteLine ("Error: in b+ , one of arguments for expression has null node.");
							return Nil.getInstance ();
						}

					} else {

						if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

							Console.WriteLine ("Error: arguments must be IntLit for binary addition.");
							return Nil.getInstance ();
						} else {
							IntLit int1 = (IntLit)args.getCar ();
							IntLit int2 = (IntLit)args.getCdr ().getCar ();
							return new IntLit (int1.getInt () + int2.getInt ());
						}
					}

				} else {
					Console.WriteLine ("Error: in b+ , there is a null  external node");
					return Nil.getInstance ();
				}

			} else if (this.symbol.getName ().Equals ("b-")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for binary subtraction operation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

						Console.WriteLine ("Error: arguments must be IntLit for binary subtraction.");
						return Nil.getInstance ();
					} else {
						IntLit int1 = (IntLit)args.getCar ();
						IntLit int2 = (IntLit)args.getCdr ().getCar ();
						return new IntLit (int1.getInt () - int2.getInt ());
					}
				} else {
					Console.WriteLine ("Error: more than two arguments for binary subtraction is not permissable.");
					return Nil.getInstance ();
				}

			} else if (this.symbol.getName ().Equals ("b*")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for binary multiplication operation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

						Console.WriteLine ("Error: arguments must be IntLit for binary multiplication.");
						return Nil.getInstance ();
					} else {
						IntLit int1 = (IntLit)args.getCar ();
						IntLit int2 = (IntLit)args.getCdr ().getCar ();
						return new IntLit (int1.getInt () * int2.getInt ());
					}

				} else {

					Console.WriteLine ("Error: more than two arguments for binary multiplication is not permissable.");
					return Nil.getInstance ();
				}

			} else if (this.symbol.getName ().Equals ("b/")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for binary division operation.");
					return Nil.getInstance ();
				}
				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

						Console.WriteLine ("Error: arguments must be IntLit for binary division.");
						return Nil.getInstance ();
					} else {
						IntLit int1 = (IntLit)args.getCar ();
						IntLit int2 = (IntLit)args.getCdr ().getCar ();
						if (int2.getInt () == 0) {

							Console.WriteLine ("Error: input not acceptable. cannot divide by zero.");
							return Nil.getInstance ();
						} else
							return new IntLit (int1.getInt () / int2.getInt ());
					}

				} else {

					Console.WriteLine ("Error: more than two arguments for binary division is not permissable.");
					return Nil.getInstance ();
				}

			} else if (this.symbol.getName ().Equals ("b=")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for binary equality test operation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

						Console.WriteLine ("Error: arguments must be IntLit for binary equality test.");
						return Nil.getInstance ();
					} else {
						IntLit int1 = (IntLit)args.getCar ();
						IntLit int2 = (IntLit)args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt () == int2.getInt ());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				} else {

					Console.WriteLine ("Error: more than two arguments for binary equality test is not permissable.");
					return Nil.getInstance ();
				}

			} else if (this.symbol.getName ().Equals ("b<")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for binary less than test operation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

						Console.WriteLine ("Error: arguments must be IntLit for binary less than test.");
						return Nil.getInstance ();
					} else {
						IntLit int1 = (IntLit)args.getCar ();
						IntLit int2 = (IntLit)args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt () < int2.getInt ());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				} else {
					Console.WriteLine ("Error: more than two arguments for binary less than test is not permissable.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("b>")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for binary greater than test operation.");
					return Nil.getInstance ();
				}
				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

						Console.WriteLine ("Error: arguments must be IntLit for binary greater than test.");
						return Nil.getInstance ();
					} else {
						IntLit int1 = (IntLit)args.getCar ();
						IntLit int2 = (IntLit)args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt () > int2.getInt ());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				} else {

					Console.WriteLine ("Error: more than two arguments for binary greater than test is not permissable.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("b<=")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for binary less than equal than test operation.");
					return Nil.getInstance ();
				}
				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

						Console.WriteLine ("Error: arguments must be IntLit for binary less than equal than test.");
						return Nil.getInstance ();
					} else {
						IntLit int1 = (IntLit)args.getCar ();
						IntLit int2 = (IntLit)args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt () <= int2.getInt ());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				} else {

					Console.WriteLine ("Error: more than two arguments for binary less than equal than test is not permissable.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("b>=")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for binary greater than equal than test operation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					if (!args.getCar ().isNumber () || !args.getCdr ().getCar ().isNumber ()) {

						Console.WriteLine ("Error: arguments must be IntLit for binary greater than equal than test.");
						return Nil.getInstance ();
					} else {
						IntLit int1 = (IntLit)args.getCar ();
						IntLit int2 = (IntLit)args.getCdr ().getCar ();

						bool resultOfTest = (int1.getInt () >= int2.getInt ());

						if (resultOfTest)
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

				} else {

					Console.WriteLine ("Error: more than two arguments for binary greater than equal than test is not permissable.");
					return Nil.getInstance ();
				}

			} else if (this.symbol.getName ().Equals ("null?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for testing null evaluation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().isNull ())
					argument_number_good = true;

				if (argument_number_good) {
				
					if (args.getCar ().isNull ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				
				} else {

					Console.WriteLine ("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("symbol?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for testing symbol evaluation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().isNull ())
					argument_number_good = true;

				if (argument_number_good) {

					if (args.getCar ().isSymbol ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);

				} else {

					Console.WriteLine ("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("number?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for testing number evaluation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().isNull ())
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isNumber ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				} else {

					Console.WriteLine ("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("boolean?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for testing boolean evaluation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().isNull ())
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isBool ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				} else {

					Console.WriteLine ("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
					return Nil.getInstance ();
				}

			} else if (this.symbol.getName ().Equals ("procedure?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for testing procedure evaluation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().isNull ())
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isProcedure ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				} else {

					Console.WriteLine ("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("pair?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for testing pair evaluation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().isNull ())
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isPair ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				} else {

					Console.WriteLine ("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("environment?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for testing environment evaluation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().isNull ())
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isEnvironment ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				} else {

					Console.WriteLine ("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
					return Nil.getInstance ();
				}
			} else if (this.symbol.getName ().Equals ("string?")) {

				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for testing string evaluation.");
					return Nil.getInstance ();
				}

				// extend for all argument vars

				// check if number of args is correct
				bool argument_number_good = false;
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().isNull ())
					argument_number_good = true;

				if (argument_number_good) {
					if (args.getCar ().isString ())
						return BoolLit.getInstance (true);
					else
						return BoolLit.getInstance (false);
				} else {

					Console.WriteLine ("Error: wrong number of arguments for test. only need 1 cons node with nil tail for argument.");
					return Nil.getInstance ();
				}
			}

			// working on predicate built in :    eq?
			else if (this.symbol.getName ().Equals ("eq?")) {


				// if there are no arguments, report error
				if (args == null || args.isNull ()) {

					Console.WriteLine ("Error: no arguments given for eq?   test operation.");
					return Nil.getInstance ();
				}
				// extend for all argument vars
				// and
				//check if any args have null or nil.
				if (args != null && args.getCar () != null
				    && args.getCdr () != null && args.getCdr ().getCar () != null
				    && args.getCdr ().getCdr ().isNull ()) {

					//if (!args.getCar () || !args.getCdr ().getCar () )


					// check nil/null? first

					// first car is nil but  second item is not nil... 
					// or the other way around   (vice versa)
					if ((args.getCar ().isNull () && !args.getCdr ().getCar ().isNull ()) ||
					    (!args.getCar ().isNull () && args.getCdr ().getCar ().isNull ())) {
						return BoolLit.getInstance (false);
					}
					// success: both nil
					else if (args.getCar ().isNull () && args.getCdr ().getCar ().isNull ()) {
						return BoolLit.getInstance (true);
					}

					// first car is type symbol but  second item is not symbol... 
					// or the other way around   (vice versa)
					else if ((args.getCar ().isSymbol () && !args.getCdr ().getCar ().isSymbol ()) ||
					         (!args.getCar ().isSymbol () && args.getCdr ().getCar ().isSymbol ())) {
						return BoolLit.getInstance (false);
					}
					// check if both symbol   now...
					else if (args.getCar ().isSymbol () && args.getCdr ().getCar ().isSymbol ()) {
						// check if the symbol's are identical

						if (args.getCar ().getName ().Equals (args.getCdr ().getCar ().getName ()))
							// success:    both same symbol!
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

					// first car is type number but  second item is not number... 
					// or the other way around   (vice versa)
					else if ((args.getCar ().isNumber () && !args.getCdr ().getCar ().isNumber ()) ||
					         (!args.getCar ().isNumber () && args.getCdr ().getCar ().isNumber ())) {
						return BoolLit.getInstance (false);
					}
					// check if both number   now...
					else if (args.getCar ().isNumber () && args.getCdr ().getCar ().isNumber ()) {

						// cast to new IntLits
						IntLit intLit_1 = (IntLit)args.getCar ();
						IntLit intLit_2 = (IntLit)args.getCdr ().getCar ();


						// check if the int values's are equal

						if (intLit_1.getInt () == intLit_2.getInt ())
							// success:    both same integer!
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}


					// first car is type boolean but  second item is not boolean... 
					// or the other way around   (vice versa)
					else if ((args.getCar ().isBool () && !args.getCdr ().getCar ().isBool ()) ||
					         (!args.getCar ().isBool () && args.getCdr ().getCar ().isBool ())) {
						return BoolLit.getInstance (false);
					}
					// check if both boolean   now...
					else if (args.getCar ().isBool () && args.getCdr ().getCar ().isBool ()) {
						// cast nodes as new booleanNodes

						BoolLit boolVal1 = (BoolLit)args.getCar ();
						BoolLit boolVal2 = (BoolLit)args.getCdr ().getCar ();

						// check if the bool's equal

						if (boolVal1.Equals (boolVal2))
							// success:    both same bool!
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}



					// first car is type builtIn but  second item is not builtIn... 
					// or the other way around   (vice versa)
					else if ((args.getCar ().isBuiltIn () && !args.getCdr ().getCar ().isBuiltIn ()) ||
					         (!args.getCar ().isBuiltIn () && args.getCdr ().getCar ().isBuiltIn ())) {
						return BoolLit.getInstance (false);
					}
					// check if both builtIn   now...
					else if (args.getCar ().isBuiltIn () && args.getCdr ().getCar ().isBuiltIn ()) {
						// cast nodes as new builtIn
						BuiltIn builtIn_val_1 = (BuiltIn)args.getCar ();
						BuiltIn builtIn_val_2 = (BuiltIn)args.getCdr ().getCar ();

						// check if the builtIn's are identical

						if (builtIn_val_1.getSymbol ().Equals (builtIn_val_2.getSymbol ()))
							// success:    both same builtIn!
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

					// by process of elimination
					//now
					// a object that ends up tested here would be surely a closure... 

					// first car is type closure but  second item is not closure... 
					// or the other way around   (vice versa)
					else if ((args.getCar ().isProcedure () && !args.getCdr ().getCar ().isProcedure ()) ||
					         (!args.getCar ().isProcedure () && args.getCdr ().getCar ().isProcedure ())) {
						return BoolLit.getInstance (false);
					}
					// check if both closure   now...
					else if (args.getCar ().isProcedure () && args.getCdr ().getCar ().isProcedure ()) {
						// cast nodes as new builtIn
						Closure closure_val_1 = (Closure)args.getCar ();
						Closure closure_val_2 = (Closure)args.getCdr ().getCar ();

						// check if the closure's are identical

						if (closure_val_1.Equals (closure_val_2))
							// success:    both same closure!
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}



					// first car is type environment but  second item is not environment... 
					// or the other way around   (vice versa)
					else if ((args.getCar ().isEnvironment () && !args.getCdr ().getCar ().isEnvironment ()) ||
					         (!args.getCar ().isEnvironment () && args.getCdr ().getCar ().isEnvironment ())) {
						return BoolLit.getInstance (false);
					}
					// check if both environment   now...
					else if (args.getCar ().isEnvironment () && args.getCdr ().getCar ().isEnvironment ()) {
						// cast nodes as new Environment
						Environment environ_1 = (Environment)args.getCar ();
						Environment environ_2 = (Environment)args.getCdr ().getCar ();

						// check if the environment's are identical

						if (environ_1.Equals (environ_2))
							// success:    both same environment!
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}

					// first car is type String but  second item is not String... 
					// or the other way around   (vice versa)
					else if ((args.getCar ().isString () && !args.getCdr ().getCar ().isString ()) ||
					         (!args.getCar ().isString () && args.getCdr ().getCar ().isString ())) {
						return BoolLit.getInstance (false);
					}
					// check if both String   now...
					else if (args.getCar ().isString () && args.getCdr ().getCar ().isString ()) {
						// cast nodes as new StringLits
						StringLit String_1 = (StringLit)args.getCar ();
						StringLit String_2 = (StringLit)args.getCdr ().getCar ();

						// check if the StringLit's are identical

						if (String_1.getString ().Equals (String_2.getString ()))
							// success:    both same StringLit!
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}


					// eq? test note:  there is one caveat.  If the items are both quote terms... this will be handled directly in regular eval 

					// first car is type Pair but  second item is not Pair... 
					// or the other way around   (vice versa)
					else if ((args.getCar ().isPair () && !args.getCdr ().getCar ().isPair ()) ||
					         (!args.getCar ().isPair () && args.getCdr ().getCar ().isPair ())) {
						return BoolLit.getInstance (false);
					}
					// check if both are type Pair   now...
					else if (args.getCar ().isPair () && args.getCdr ().getCar ().isPair ()) {
						// cast nodes as new cons Pair
						Cons cons_1 = (Cons)args.getCar ();
						Cons cons_2 = (Cons)args.getCdr ().getCar ();

						// check if the Cons's are identical

						if (cons_1.Equals (cons_2))
							// success:    both same pair reference!
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);

						// illegal function for argument
					} else {

						Console.WriteLine ("Error: Argument types for eq? cannot be recognized in context.");
						return Nil.getInstance ();
					}

				} else {

					Console.WriteLine ("Error: null parameter _ or _ more than two arguments for eq?  test is not permissable.");
					return Nil.getInstance ();
				}
			} else {

				Console.WriteLine ("Error: builtin is not identifiable.");
				return Nil.getInstance ();

			}





    	}




    }    
}

