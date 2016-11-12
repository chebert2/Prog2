// Regular -- Parse tree node strategy for printing regular lists

using System;
using Tree;
using System.IO;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }

		// also now need to   ensure that all arguments in regular expression 
		// get their values looked up if they are symbols


		// implementation          
		// Note : sometimes env1  will not conform, or be pertinent to what is written relation to c# loops and scheme code
		public override Node eval(Node node1, Environment env1) {

			if (node1 == null || node1.getCar () == null) {

				Console.WriteLine ("Error: the expression is not initialized to values: null list and car");
				return Nil.getInstance ();
			}



			// for any regular defined function enclosed in parentheses,
			// the first element is supposed to be a function to be evaluated.  

			// this variable will hold the final function head term we get.
			Node firstElem_car = node1.getCar();

			// step one   :  if Car is a pair, evaluate it and store resulting ident, ... which ought to be
			// another function once evaluated...
			if (node1.getCar ().isPair ()) {
				// build new environment for this to be computed for.
				Environment envExtend = new Environment (env1);
				firstElem_car = node1.getCar ().eval (envExtend);
				//report error if not   a builtin/  or /closure
				if (firstElem_car == null || !firstElem_car.isProcedure ()) {

					Console.WriteLine ("Error: regular expression needs a function builtin/closure for first term!, __ + returning null instead.");
					return Nil.getInstance ();
				}
			} else if (node1.getCar ().isSymbol ()) {

				// start loop to get closure from obstructing intermediate symbolic expression.
				bool first_element_is_not_closure = true;
				firstElem_car = node1.getCar ();
				while(first_element_is_not_closure) {
					firstElem_car = env1.lookup (firstElem_car);
					//report error if not   a builtin/  or /closure
					if (firstElem_car == null) {

						Console.WriteLine ("Error: regular expression needs a function builtin/closure for first term!, __ + returning null instead.");
						return Nil.getInstance ();
					}
					else if (firstElem_car.isProcedure ())
						first_element_is_not_closure = false;

				}



			}
			// this would be improper end for cons   regular expression.
			if (node1.getCdr () == null) {
				Console.WriteLine ("error: one of regular expressions elements is null.");
				return Nil.getInstance ();
			}

			// special builtIn   eval  case.
			if (firstElem_car.isBuiltIn ()) {
				BuiltIn givenId = (BuiltIn)firstElem_car;


				// check if built in is eval
				// note carefully: if symbol == null, null pointer will happen.
				if (givenId.getSymbol ().getName ().Equals ("eval")) {

					// build new environment for this to be computed for.
					Environment envExtend = new Environment (env1);

					// check for null values now  so null pointer doesnt get encountered
					if (node1.getCdr ().getCar () == null || node1.getCdr ().getCdr () == null ||
					    node1.getCdr ().getCdr ().getCar () == null) {

						Console.WriteLine ("Error: lacking one of the following for eval : expression or environment.");
						return Nil.getInstance ();
					}


					// side maneuver. _ work out the environment first...
					bool environment_taken = false;
					Node env_symbol_to_lookup = node1.getCdr ().getCdr ().getCar ();
					Node environment_found = null;
					// check if symbol needs to be evaluated
					if (env_symbol_to_lookup.isSymbol ()) {
						if (env1.lookup (env_symbol_to_lookup) != null)
							environment_found = env1.lookup (env_symbol_to_lookup);
						else {
							Console.WriteLine ("Error: environment symbol was not found.");
							return Nil.getInstance ();
						}
					} // check if expression needs to be evaluated
					else if (env_symbol_to_lookup.isPair ())
						environment_found = env_symbol_to_lookup.eval (envExtend);
 
					if (environment_found != null && environment_found.isEnvironment ())
						environment_taken = true;
					else {

						Console.WriteLine ("Error: evaluation op_ did not find a fitting environment.");
						return Nil.getInstance ();
					}
					// working on expression now
					Node args_to_eval = null;

					Node localTerms_data = node1.getCdr ().getCar ();

					// check if second argument is code   
					//   and then evaluate if it needs more processing
					// for eval to implement as code
					if (localTerms_data.isPair () && environment_taken) {


						if (localTerms_data.getCar() != null && localTerms_data.getCar().isSymbol()
							&& localTerms_data.getCar().getName().Equals("quote")) {

							Node args_of_Quote = localTerms_data.eval (envExtend);

							if (args_of_Quote != null)
							// copy evaluated quote expression 
								args_to_eval = args_of_Quote;

						} else if (localTerms_data.getCar() != null && localTerms_data.getCar().isSymbol()
							&& localTerms_data.getCar().getName().Equals("lambda")) {

							Node args_of_Lambda = localTerms_data.eval (envExtend);

							if (args_of_Lambda != null)
							// copy evaluated lambda expression
								args_to_eval = args_of_Lambda;
						} else {
							// this needs fine tuning for more special cases..\\

							//("may have more cases... ")
							//??
							args_to_eval = localTerms_data;
						}
					} else if (localTerms_data.isSymbol () && environment_taken) {
						Node node_symbol_expression = env1.lookup (localTerms_data);
						if (node_symbol_expression != null)
							// copy evaluated symbol and then the environment
							args_to_eval = node_symbol_expression;
						else {
							Console.WriteLine ("Error: first expression argument symbol not found.");
							return Nil.getInstance ();
						}
					}
					// return the built in eval of args and environment given
					if (args_to_eval != null) {
						Environment environment_copy_found = (Environment)environment_found;
						if (args_to_eval.isPair ())
							return args_to_eval.eval (environment_copy_found);
						else
							return args_to_eval.eval (environment_copy_found);
					} else {

						Console.WriteLine ("Error: expression arg was null.");
						return Nil.getInstance ();
					}
				}





				// note: looking at special eq? case
				// check if built in is eq?    only look at case of (eq? 'exp 'exp)
				// note carefully: if symbol == null, null pointer will happen.
				if (givenId.getSymbol ().getName ().Equals ("eq?")      && 
					node1.getCdr ().getCar () != null                   &&
					node1.getCdr ().getCdr () != null                   &&
					node1.getCdr ().getCdr ().getCar () != null         &&
					node1.getCdr ().getCdr ().getCdr () != null         &&
					node1.getCdr ().getCdr ().getCdr ().isNull()        &&

					node1.getCdr().getCar().isPair()                 &&
					node1.getCdr().getCdr().getCar().isPair()
				   
				)


				{
					// first we must check if the arguments to (eq? ... )are in fact quote arguments.
					Cons argument1 = (Cons) node1.getCdr ().getCar ();

					Cons argument2 = (Cons) node1.getCdr ().getCdr ().getCar ();

					if (argument1.getForm_ofCons () is Tree.Quote && argument2.getForm_ofCons () is Tree.Quote) {

						String first_argument_printed = ""; 
						String second_argument_printed = "";



						// redirect output to evaulate if print of Pair nodes is same

						var originalConsoleOut = Console.Out; // preserve the original stream
						using(var writer = new StringWriter())
						{
							Console.SetOut(writer);

							argument1.print (0); 

							writer.Flush();

							var myString = writer.ToString();

							first_argument_printed = myString.TrimEnd('\n');


						}
						using(var writer1 = new StringWriter())
						{
							Console.SetOut(writer1);

							argument2.print (0); 

							writer1.Flush();

							var myString_2 = writer1.ToString();

							second_argument_printed = myString_2.TrimEnd('\n');


						}


						Console.SetOut(originalConsoleOut); // restore Console.Out


						if (first_argument_printed.Equals (second_argument_printed))
							return BoolLit.getInstance (true);
						else
							return BoolLit.getInstance (false);
					}



				}



			}



			// other cases  builtin or closure...



			Node args_given = node1.getCdr ();
			bool hasArguments = true;

			// test if there are arguments to this form  function
			if (args_given.isNull ())
				hasArguments = false;



			if (hasArguments){


				if (args_given.getCdr () == null || args_given.getCar () == null) {

					Console.WriteLine ("Error: one of passed arguments in function expression is null.");
					return Nil.getInstance ();
				}


				// need to evaluate args (each car separately)... and then return what is resulting list of elements...

				// build new environment on hand _ for each car to be computed at times.
				Environment envExtend1 = new Environment (env1);

				// this will hold the list of evaluated arguments cumulatively.
				Cons evaluated_argsList__in_progress;


				// this will hold a latest fringe of for extending fringe items.

				Cons fringe_cons = null;


				bool hasMoreDescendents =  ! args_given.getCdr ().isNull()   ;


				if (hasMoreDescendents) {
					// start this off as unassigned.
					Cons added_fringe_con = null;

					    // this will hold the list of evaluated arguments cumulatively.
				    	// Cons evaluated_argsList__in_progress;

					// check if eval is not null
					Node evalItem1 = args_given.getCar ().eval (envExtend1);
					if (evalItem1 != null)
						evaluated_argsList__in_progress = new Cons (evalItem1, added_fringe_con);
					else {

						Console.WriteLine ("Error: one of the args items in regular function was null. ");
						return Nil.getInstance ();
					}
				
					// under the alias of fringe_cons...
				    // we will continue this reference down to assess the next fringe branch
					fringe_cons = evaluated_argsList__in_progress;

				}
			    	// dealt with the sole generation here.
				  // no more fringes left.
				else  {


					// check if eval is not null
					Node evalItem1 = args_given.getCar ().eval (envExtend1);
					if (evalItem1 != null)
						evaluated_argsList__in_progress = new Cons (evalItem1, Nil.getInstance ());
					else {

						Console.WriteLine ("Error: the sole additional argument item  in regular function was null. ");
						return Nil.getInstance ();
					}
				}

				// done with first step in two levels descendents


				// get rest of fringes
   				while ( hasMoreDescendents )
			    {
					if (args_given.getCdr () == null) {

						Console.WriteLine ("Error: one of the later argument items  in regular function was null. ");
						return Nil.getInstance ();
					}
					// precaution for null pointer   so it is legal to use the cdr after this test
					if (args_given.getCdr().isPair ()) {

						// build new environment for this to be computed for.
						Environment envExtend2 = new Environment (env1);

						// get next fringe descendent.
						args_given = args_given.getCdr ();

						// start this off as unassigned.
						Cons added_fringe_con = null;


						// check if additional eval items are not null
						Node evalItem_more = args_given.getCar().eval(envExtend2);
						if (evalItem_more != null)
							fringe_cons.setCdr (new Cons (evalItem_more, added_fringe_con));
						else {

							Console.WriteLine ("Error: one of the later argument items  in regular function was null. ");
							return Nil.getInstance ();
						}

						fringe_cons = (Cons) fringe_cons.getCdr();

					}
					else if(args_given.getCdr().isNull()) {
						fringe_cons.setCdr(Nil.getInstance());
						hasMoreDescendents = false;
					}
					else {

						// build new environment for this to be computed for.
						Environment envExtend2 = new Environment (env1);

						// get one remaining fringe descendent.
						args_given = args_given.getCdr ();

						// check if additional eval items are not null
						Node evalItem_more = args_given.getCar().eval(envExtend2);
						
						if (evalItem_more != null)
							fringe_cons.setCdr (new Cons (evalItem_more, Nil.getInstance ()));
						else {

							Console.WriteLine ("Error: one of the later argument items  in regular function was null. ");
							return Nil.getInstance ();
						}	

						hasMoreDescendents = false;



					}
						

			    }

				return firstElem_car.apply(evaluated_argsList__in_progress);

			}

			return firstElem_car.apply(Nil.getInstance()) ;



		}


    }
}


