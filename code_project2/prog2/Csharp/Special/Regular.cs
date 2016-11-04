// Regular -- Parse tree node strategy for printing regular lists

using System;
using Environment;
using BuiltIn;

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

				return new StringLit ("Error: the expression is not initialized to values: null list and car");
			}



			// for any regular defined function enclosed in parentheses,
			// the first element is supposed to be a function to be evaluated.  

			// this variable will hold the final function head term we get.
			Node firstElem_car;

			// step one   :  if Car is a pair, evaluate it and store resulting ident, ... which ought to be
			// another function once evaluated...
			if (node1.getCar ().isPair ()) {
				// build new environment for this to be computed for.
				Environment envExtend = new Environment (env1);
				firstElem_car = node1.getCar ().eval (envExtend);
				//report error if not   a builtin/  or /closure
				if (!firstElem_car.isProcedure ())
					return new StringLit ("Error: regular expression needs a function builtin/closure for first term!, __ + returning null instead.");

			} else if (node1.getCar ().isSymbol ()) {
				firstElem_car = env1.lookup (node1.getCar ());
				//report error if not   a builtin/  or /closure
				if ( !firstElem_car.isProcedure() )
					return new StringLit ("Error: regular expression needs a function builtin/closure for first term!, __ + returning null instead.");

			}
			// this would be improper end for cons   regular expression.
			if (node1.getCdr () == null)
				return null;

			// special builtIn eval case.
			if (firstElem_car.isBuiltIn ()) {
				BuiltIn givenId = (BuiltIn)firstElem_car;



				if (givenId.getSymbol ().getName ().Equals ("eval")) {


					// check for null values now  so null pointer doesnt get encountered
					if (node1.getCdr ().getCar () == null || node1.getCdr ().getCdr () == null ||
					    node1.getCdr ().getCdr ().getCar () == null)
						return new StringLit ("Error: lacking one of the following for eval : expression or environment.");



					// side maneuver. _ work out the environment first...
					bool environment_taken = false;
					Node env_symbol_to_lookup = node1.getCdr ().getCdr ().getCar ();
					Node environment_found = null;
					// check if symbol needs to be evaluated
					if (env_symbol_to_lookup.isSymbol ()) {
						if (env1.lookup (env_symbol_to_lookup) != null)
							environment_found = env1.lookup (env_symbol_to_lookup);
						else
							return new StringLit ("Error: environment symbol was not found.");
					} // check if expression needs to be evaluated
					else if (env_symbol_to_lookup.isPair ())
						environment_found = env_symbol_to_lookup.eval (env1);
 
					if (environment_found != null && environment_found.isEnvironment ())
						environment_taken = true;
					else
						return new StringLit ("Error: evaluation op_ did not find a fitting environment.");

					// working on expression now
					Cons args_to_eval = null;

					Node localTerms_data = node1.getCdr ().getCar ();

					// check if second argument is code   
					//   and then evaluate if it needs more processing
					// for eval to implement as code
					if (localTerms_data.isPair () && environment_taken) {

						Cons cons_data = (Cons)localTerms_data;

						if (cons_data.getForm_ofCons () == Quote)
							// copy evaluated quote expression and then the environment
							args_to_eval = new Cons (cons_data.eval (env1), environment_found);
						else if (cons_data.getForm_ofCons () == Lambda)
							// copy evaluated lambda expression and then the environment
							args_to_eval = new Cons (cons_data.eval (env1), environment_found);
						else
							// this needs fine tuning for more special cases..\\

							//("may have more cases... ")
							//??
							args_to_eval = new Cons (localTerms_data, environment_found);
					} else if (localTerms_data.isSymbol () && environment_taken) {
						Node node_symbol_expression = env1.lookup (localTerms_data);
						if (node_symbol_expression != null)
							// copy evaluated symbol and then the environment
							args_to_eval = new Cons (node_symbol_expression, environment_found);
						else
							return new StringLit ("Error: first expression argument symbol not found.");
					}



					return givenId.eval (args_to_eval);
					    
				}
			}


			// other cases  builtin or closure...



			Node args_given = node1.getCdr ();
			bool hasArguments = true;

			// test if there are arguments to this form  function
			if (args_given.isNull ())
				hasArguments = false;



			if (hasArguments){

				// need to evaluate args (each car separately)... and then return what is resulting list of elements...

				// build new environment on hand _ for each car to be computed at times.
				Environment envExtend1 = new Environment (env1);

				// this will hold the list of evaluated arguments cumulatively.
				Cons evaluated_argsList__in_progress;


				// this will hold a latest fringe of for extending fringe items.

				Cons fringe_cons = null;


				bool hasMoreDescendents =   ! args_given.getCdr ().isNull();


				if (hasMoreDescendents) {
					// start this off as unassigned.
					Cons added_fringe_con = null;

					    // this will hold the list of evaluated arguments cumulatively.
				    	// Cons evaluated_argsList__in_progress;

					// check if eval is not null
					Node evalItem1 = args_given.getCar ().eval (envExtend1)
					if( evalItem1 != null)
						evaluated_argsList__in_progress = new Cons (evalItem1, added_fringe_con);
					else
						return new StringLit("Error: one of the args items in regular function was null. ");
				
					// under the alias of fringe_cons...
				    // we will continue this reference down to assess the next fringe branch
					fringe_cons = evaluated_argsList__in_progress;

				}
			    	// dealt with the sole generation here.
				  // no more fringes left.
				else  {

					Cons evaluated_argsList__in_progress;
					// check if eval is not null
					Node evalItem1 = args_given.getCar ().eval (envExtend1)
						if( evalItem1 != null)
							evaluated_argsList__in_progress = new Cons (evalItem1, Nil.getInstance() );
						else
							return new StringLit("Error: the sole additional argument item  in regular function was null. ");
				}

				// done with first step in two levels descendents


				// get rest of fringes
				while ( hasMoreDescendents )
			    {
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
						if( evalItem_more != null)
							fringe_cons.setCdr() = new Cons (evalItem_more, added_fringe_con );
						else
							return new StringLit("Error: one of the later argument items  in regular function was null. ");


						fringe_cons = fringe_cons.getCdr();

					}
					else if(args_given.getCdr().isNull()) {
						fringe_cons.setCdr() = Nil.getInstance();
						hasMoreDescendents = false;
					}
					else {

						// build new environment for this to be computed for.
						Environment envExtend2 = new Environment (env1);

						// get one remaining fringe descendent.
						args_given = args_given.getCdr ();

						// check if additional eval items are not null
						Node evalItem_more = args_given.getCar().eval(envExtend2)
						
						if( evalItem_more != null)
							fringe_cons.setCdr() = new Cons (evalItem_more, Nil.getInstance() );

						else
							return new StringLit("Error: one of the later argument items  in regular function was null. ");
								

						hasMoreDescendents = false;

						return firstElem_car.eval(evaluated_argsList__in_progress, env1);

					}

					return firstElem_car.eval(evaluated_argsList__in_progress, env1);


			    }

				return firstElem_car.eval(evaluated_argsList__in_progress, env1);

			}

			return firstElem_car.eval(new BoolLit(false), env1) ;



		}


    }
}


