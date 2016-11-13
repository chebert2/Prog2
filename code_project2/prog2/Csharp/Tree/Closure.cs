// Closure.java -- the data structure for function closures

// Class Closure is used to represent the value of lambda expressions.
// It consists of the lambda expression itself, together with the
// environment in which the lambda expression was evaluated.

// The method apply() takes the environment out of the closure,
// adds a new frame for the function call, defines bindings for the
// parameters with the argument values in the new frame, and evaluates
// the function body.

using System;

namespace Tree
{
    public class Closure : Node
    {
        private Node fun;		// a lambda expression
        private Environment env;	// the environment in which
                                        // the function was defined

        public Closure(Node f, Environment e)	{ fun = f;  env = e; }

        public Node getFun()		{ return fun; }
        public Environment getEnv()	{ return env; }

        // TODO: The method isProcedure() should be defined in
        // class Node to return false.
        public /* override */ bool isProcedure()	{ return true; }

        public override void print(int n) {
            // there got to be a more efficient way to print n spaces
            for (int i = 0; i < n; i++)
                Console.Write(' ');
            Console.WriteLine("#{Procedure");
            if (fun != null)
                fun.print(Math.Abs(n) + 4);
            for (int i = 0; i < Math.Abs(n); i++)
                Console.Write(' ');
            Console.WriteLine('}');
        }

        
        public override Node apply (Node args)
		{

			if (args == null) {
				Console.WriteLine ("Error: arguments list is null, for applying closure.");
				return Nil.getInstance ();
			}


			Node function_here = this.getFun ();


			if (function_here == null || this.getEnv () == null) {

				Console.WriteLine ("Error : function evaluating has null lambda definition or environment.");
				return Nil.getInstance ();
			}


			// add a environment for function parameters...
			Environment added_List__for_function_params = new Environment (this.getEnv ());


			// flag for having necessary number of parameters in apply.
			bool has_right_number_arguments = true;


			// check for parameters to lambda function.
			if (function_here.getCdr ().getCar () != null && function_here.getCdr ().getCar ().isNull ()) {
				// no parameters to extract

				// check if number of args given is also empty.  or if they do not match.
				if (args.isNull ()) {
					has_right_number_arguments = true;
				} else {
					// report error    because  : no arguments should be given for the function!
					Console.WriteLine ("Error: wrong number of arguments.  0 number of parameters required.");
					return Nil.getInstance ();
				}

			} else if (function_here.getCdr ().getCar () != null && !function_here.getCdr ().getCar ().isNull ()
			           && function_here.getCdr ().getCar ().isPair ()) {
				// list of defined lambda params
				int number_of_parameters_counted = 0;
				// list of arguments for applying called function
				int count__of_number_of_arguments_given = 0;

				// current parent node for function list parameters in lambda expression.
				Node begin_values_node_for_lambda = function_here.getCdr ().getCar ();

				// current node value to be extracted  in lambda params
				Node node_of_value__currently;


				// current arguments parent node    in list of args formed to apply
				Node begin_values_node_for_args = args;
				bool more_args_to_pass = true;





				// extract parameter names,   and add the value given to them.

				// flag for if there are more values to assign.
				bool more_values = true;


				while (more_values) {

					// increment   current count of arguments passed and given to apply

					if (more_args_to_pass && begin_values_node_for_args.getCar () == null || begin_values_node_for_args.getCar ().isNull ()) {

						Console.WriteLine ("Error: one of arguments given is null or nil (empty list).");
						return Nil.getInstance ();

					} else if (more_args_to_pass) {
						// add one for the number of caller arguments supplied
						count__of_number_of_arguments_given++;
					}
						


					// designate variable identifier in function definition
					node_of_value__currently = begin_values_node_for_lambda.getCar ();


					if (node_of_value__currently == null || node_of_value__currently.isNull ()) {

						Console.WriteLine ("Error: one of parameters in closure function is null or nil (empty list).");
						return Nil.getInstance ();
					} else {
						number_of_parameters_counted++;
					}

					// examine number parameter's to eval    if it is congruent
					// 
					// compare number of input arguments to  current count of parameter's required
					if (count__of_number_of_arguments_given < number_of_parameters_counted) {
						has_right_number_arguments = false;
						Console.WriteLine ("Error : for applying function closure  :  shortage of arguments given for applying function.");
						return Nil.getInstance ();

					}

					// add a key value pair   for  one parameter definition
					else {
						added_List__for_function_params.define (node_of_value__currently, begin_values_node_for_args.getCar ());
					}


					//examine next connecting branch of Lambda elements in function parameter to extract
					if (begin_values_node_for_lambda.getCdr () != null && begin_values_node_for_lambda.getCdr ().isNull ()) {
						more_values = false;
					} else if (begin_values_node_for_lambda.getCdr () != null && !begin_values_node_for_lambda.getCdr ().isNull ()
					           && begin_values_node_for_lambda.getCdr ().isPair ()) {

						begin_values_node_for_lambda = begin_values_node_for_lambda.getCdr ();

					} else if (begin_values_node_for_lambda.getCdr () != null && !begin_values_node_for_lambda.getCdr ().isNull ()
					           && !begin_values_node_for_lambda.getCdr ().isPair ()) {

						Console.WriteLine ("Error: function's lambda expression has a lambda parameter list that is non cons in procession of args.");
						return Nil.getInstance ();
					} else {

						Console.WriteLine ("Error: function's lambda expression has a lambda parameter that is null.");
						return Nil.getInstance ();
					}


					//examine next connecting branch of arguments given
					if (begin_values_node_for_args.getCdr () != null && begin_values_node_for_args.getCdr ().isNull ()) {
						more_args_to_pass = false;
					} else if (begin_values_node_for_args.getCdr () != null && !begin_values_node_for_args.getCdr ().isNull ()
					           && begin_values_node_for_args.getCdr ().isPair ()) {

						begin_values_node_for_args = begin_values_node_for_args.getCdr ();

					} else if (begin_values_node_for_args.getCdr () != null && !begin_values_node_for_args.getCdr ().isNull ()
					           && !begin_values_node_for_args.getCdr ().isPair ()) {

						Console.WriteLine ("Error: arguments to apply has list quality that is non cons in procession of args.");
						return Nil.getInstance ();
					} else {

						Console.WriteLine ("Error: arguments to apply has a argument that is null.");
						return Nil.getInstance ();
					}



				}


				// examine number parameter's to eval    if it is congruent
				// 
				// compare number of input arguments to  current count of parameter's required
				//
				//  test if   count__of_number_of_arguments_given > number_of_parameters_counted 
				//
				if (more_args_to_pass) {
					has_right_number_arguments = false;
					Console.WriteLine ("Error : for applying function closure  :  excess number of arguments given for applying function.");
					return Nil.getInstance ();

				}




			} else if (function_here.getCdr ().getCar () != null && !function_here.getCdr ().getCar ().isNull ()
			           && !function_here.getCdr ().getCar ().isPair ()) {
				   
				Console.WriteLine ("Error: function's lambda expression has a lambda parameter list that has non-cons structured list of arguments.");
				return Nil.getInstance ();
			} else {
				Console.WriteLine ("Error: list of parameters given in function definition  is null or nil (empty list).");
				return Nil.getInstance ();
			}


			// begin evaluating function pieces with data we have.
			if (has_right_number_arguments) {

				// begin running lambda expression now.

				Node cdr = function_here.getCdr ().getCdr ();


				if (cdr == null) {

					Console.WriteLine ("Error: Null");

					return Nil.getInstance ();
				}
				// then check if the nil at list end      without having anything fulfilled.
				// also check to rule out there not being a child  a cons node.
				else if (cdr.isNull () || !cdr.isPair ()) {

					Console.WriteLine ("Error: no chain of arguments for evaluating lambda statement.");

					return Nil.getInstance ();

				}
				// start loop that flips through elements until we get at the last destination expression.
				else {
					// initialize the item that may be returned.
					Node returnItem;

					// set a conditional and (on hold prospect)  return element.
					if (cdr.getCar () == null) {
						Console.WriteLine ("Error: one of function expressions is null .");

						return Nil.getInstance ();
					} else {
						returnItem = cdr.getCar ().eval (added_List__for_function_params);
					}
					bool hasAdditionalExp = false;

					Node one_further_outExtending_tail = cdr.getCdr ();

					// set one item extra in store of members of set of further tail items...  
					// if it is something to start a third part in a chain of further tail cycle.
					// & nil term test
					if (one_further_outExtending_tail != null && !one_further_outExtending_tail.isNull ()) {

						if (!one_further_outExtending_tail.isPair ()) {

							Console.WriteLine ("Error: one of function's tail expressions is not in correct pair procession .");

							return Nil.getInstance ();
						} else if (one_further_outExtending_tail.getCar () == null) {

							Console.WriteLine ("Error: one of expressions in function null .");

							return Nil.getInstance ();
							// more function things ahead   in pair tree
						} else {   

							// set a conditional and (on hold prospect)  return element.
							returnItem = one_further_outExtending_tail.getCar ().eval (added_List__for_function_params);
							hasAdditionalExp = true;
						}
					}
					// terminates with current return item.   // because cant go further in chain for begin expression chain
					else if (one_further_outExtending_tail != null && one_further_outExtending_tail.isNull ()) {

						hasAdditionalExp = false;
						// end
						return returnItem;
					} else {
						// ends here with error
						Console.WriteLine ("Error: Null encountered further on in Begin's  expressions.");

						return Nil.getInstance ();
					}

					// loops over each expression leading up to last.
					while (hasAdditionalExp) {


						// set end now.
						if (one_further_outExtending_tail.getCdr () != null && one_further_outExtending_tail.getCdr ().isNull ()) {
							//end
							hasAdditionalExp = false;
						}
						// go to next in sequence of cdr's
						else if (one_further_outExtending_tail.getCdr () != null &&
						         !one_further_outExtending_tail.getCdr ().isNull ()) { 


							if (!one_further_outExtending_tail.getCdr ().isPair ()) {

								Console.WriteLine ("Error: one of expressions is not in correct pair procession .");

								return Nil.getInstance ();
							} else if (one_further_outExtending_tail.getCdr ().getCar () == null) {

								Console.WriteLine ("Error: one of expressions in begin's arguments null .");

								return Nil.getInstance ();
							} else {   

								// set a conditional and (on hold prospect)  return element.                  
								returnItem = one_further_outExtending_tail.getCdr ().getCar ().eval (added_List__for_function_params);

								hasAdditionalExp = true;
								// increment to next in sequence of cdr's
								one_further_outExtending_tail = one_further_outExtending_tail.getCdr ();

							}
						} else {	
							// ends here with error
							Console.WriteLine ("Error: Null encountered further on in Begin's  expressions.");

							return Nil.getInstance ();
						}
					} // while loop ended

					return returnItem;
				}



			} else {
				Console.WriteLine ("Error : for applying function closure  :  wrong number of arguments given for applying function.");
				return Nil.getInstance ();

			}


        }
        
        
    }    
}
