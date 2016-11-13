// Let -- Parse tree node strategy for printing the special form let

using System;

namespace Tree
{
    public class Let : Special
    {
	public Let() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printLet(t, n, p);
        }


		public override Node eval(Node node1, Environment env1) {
		
			// set up environment for scope of this let body
			Environment current_env_of_let = new Environment (env1);

			bool need_to_make_bindings = true;

			// this will hold the list of evaluated arguments cumulatively.
			// dummy value to start with
			Cons evaluated_argsList__in_progress = new Cons(new IntLit(1), Nil.getInstance());

			// check if there are items to assign to environment...
			// and
			// check if there is an body item for evaluation of let.
			if (node1.getCdr () != null && node1.getCdr ().isPair ()

			    && node1.getCdr ().getCar () != null

			    && node1.getCdr ().getCdr () != null

			    && node1.getCdr ().getCdr ().isPair ()) 
			{

				// Make a list of values to be bound to keys given...

				// __list_of_terms
				Node current_lead_binding_node = node1.getCdr().getCar();

				// this will hold the list of evaluated arguments cumulatively.
				// Cons evaluated_argsList__in_progress;

				// assign a dummy value
				Cons current_fringe_of_argList = new Cons(new IntLit(1), Nil.getInstance());

				bool has_more_bindings_in_list = true;

				// this is the case that there are no items to be associated in Frame.
				if (current_lead_binding_node.isNull() || !current_lead_binding_node.isPair() ) {

					has_more_bindings_in_list = false;
					// now it is not necessary to bind anything
					need_to_make_bindings = false;
				}

				bool first_run = true;

				while(has_more_bindings_in_list) {

					if (current_lead_binding_node == null) {
						Console.WriteLine ("Error: in Let, one of items in terms being bound has null lead node.");
						return Nil.getInstance ();
					}

					//stopping criteria
					if ( current_lead_binding_node.isNull() ) {

						has_more_bindings_in_list = false;
						break;
					}

					// get values if otherwise
					if (current_lead_binding_node.isPair ()) {

						Node current_key_value_pair_item = current_lead_binding_node.getCar ();


						// check if this term is null or empty list

						if(current_key_value_pair_item == null || current_key_value_pair_item.isNull()) {
							Console.WriteLine ("Error: in Let, one of key value pairs is either null    or an empty list , nil.");
							return Nil.getInstance ();
						}
						// check if this term is not a pair
						if(  ! current_key_value_pair_item.isPair()) {
							Console.WriteLine ("Error: in Let, one of key value pairs is not a key value pair cons node.");
							return Nil.getInstance ();
						}

					// get evaluated value here
						Node node_to_add;


						if (current_key_value_pair_item.getCdr () != null &&

							!current_key_value_pair_item.getCdr ().isNull () && 

							current_key_value_pair_item.getCdr ().isPair () ) {


							// first check if there is the right number of value, namely just one

							if (current_key_value_pair_item.getCdr ().getCdr () == null ||
								!current_key_value_pair_item.getCdr ().getCdr ().isNull() ) {

								Console.WriteLine ("Error: in let function call,... one of key value pairs has more than one value or a null for a tail.");
								return Nil.getInstance ();

							}
						

							// check if item holding value is an expression that needs to be evaluated
							if (current_key_value_pair_item.getCdr ().getCar() != null &&
								current_key_value_pair_item.getCdr ().getCar().isPair()  ) {

								// build new environment for this to be computed for.
								Environment envExtend = new Environment (current_env_of_let);

								node_to_add = current_key_value_pair_item.getCdr ().getCar() .eval (envExtend);

							} 
							// lookup value from symbol in next case...

							else if (current_key_value_pair_item.getCdr ().getCar() != null  &&

								! current_key_value_pair_item.getCdr ().getCar().isNull()  &&

								current_key_value_pair_item.getCdr ().getCar().isSymbol() ) {


								node_to_add = current_env_of_let.lookup(current_key_value_pair_item.getCdr ().getCar());

								if (node_to_add == null) {
									Console.WriteLine ("Error: one of values being bound in let  is undefined in lookup, and thus null.");
									return Nil.getInstance ();
								}
								// case where value is not an expression or a symbol, but only a literal
							} else if ( current_key_value_pair_item.getCdr ().getCar() != null ) {
								node_to_add = current_key_value_pair_item.getCdr ().getCar ();
							}
							else {
								Console.WriteLine ("Error: one of values being bound in let  is null.");
								return Nil.getInstance ();
							}


						} else {
							Console.WriteLine ("Error: in Let, one of items that should represent value, in a key value pair, is null or holds no value.");
							return Nil.getInstance ();
						}

						// add latest value of key value pairs  to cumulative list of values
						if (first_run) {
							evaluated_argsList__in_progress = new Cons (node_to_add, Nil.getInstance ());

							current_fringe_of_argList = evaluated_argsList__in_progress;
							first_run = false;
						}
						else {

							current_fringe_of_argList.setCdr (new Cons (node_to_add,  Nil.getInstance() ) );

							current_fringe_of_argList = (Cons) current_fringe_of_argList.getCdr ();

						}

						// increment to next fringe key value pair item...
						current_lead_binding_node = current_lead_binding_node.getCdr ();


					} else {
						Console.WriteLine ("Error: in Let, one of bindings does not have cons format.");
						return Nil.getInstance ();
					}


				}





			} else {
				Console.WriteLine ("Error: In let expression, ... either not enought arguments given , or null node encountered.");
				return Nil.getInstance ();
			}


			if (need_to_make_bindings) {

				// bind each variable in key value pair


				// current pair of all terms being binding
				Node current_binding_node = node1.getCdr().getCar();

				// current fring of value list
				Node current_fringe_of_list_of_values = evaluated_argsList__in_progress;

				// this will hold the list of evaluated arguments cumulatively.
				//Cons evaluated_argsList__in_progress;

				bool more_key_val_pairs_to_bind = true;


				while (more_key_val_pairs_to_bind) {

					if (current_binding_node == null) {
						Console.WriteLine ("Error: one of key value pairs lead node is null");
						return Nil.getInstance ();
					}

					if (current_binding_node.isNull ()) {
						more_key_val_pairs_to_bind = false;
						break;
					}

					if (current_binding_node.isPair ()) {

						if (current_binding_node.getCar () == null || current_binding_node.getCar ().isNull ()) {
							Console.WriteLine ("Error: in Let, one of key value pairs is either null    or an empty list , nil.");
							return Nil.getInstance ();
						} else if (!current_binding_node.getCar ().isPair ()) {
							Console.WriteLine ("Error: in Let, one of bindings does not have cons format.");
							return Nil.getInstance ();
							// go into binding association list
						} else if (current_binding_node.getCar ().isPair ()) {

							if (current_binding_node.getCar ().getCar () == null) {
								Console.WriteLine ("Error: in Let, one of key/symbol fields is null.");
								return Nil.getInstance ();
							}
							// check if this key is actually an identifier.
							else if (current_binding_node.getCar ().getCar().isSymbol ()) {

								current_env_of_let.define (current_binding_node.getCar ().getCar(), current_fringe_of_list_of_values.getCar ());

								current_fringe_of_list_of_values = current_fringe_of_list_of_values.getCdr ();

								current_binding_node = current_binding_node.getCdr ();

							} else {
								Console.WriteLine ("Error: in Let, one of keys is not an identifier.");
								return Nil.getInstance ();
							}
						}

					}
					else {
						Console.WriteLine ("Error: in Let, pair field outer group does not have cons format.");
						return Nil.getInstance ();
					}

				}

			}  


			// jump ahead to body function expression of let. 



			Node current_locale_of_body = node1.getCdr ().getCdr ();


			if (current_locale_of_body == null) {

				Console.WriteLine ("Error: Let's  body is Null");

				return Nil.getInstance ();
			}
			// then check if the nil at list end      without having anything fulfilled.
			// also check to rule out there not being a child  a cons node.
			else if (current_locale_of_body.isNull () || !current_locale_of_body.isPair ()) {

				Console.WriteLine ("Error: no arguments for evaluating in Begin statement .");

				return Nil.getInstance ();

			}
			// start loop that flips through elements until we get at the last destination expression.
			else {
				// initialize the item that may be returned.
				Node returnItem;

				// set a conditional and (on hold prospect)  return element.
				if (current_locale_of_body.getCar () == null) {
					Console.WriteLine ("Error: one of expressions in begin's arguments null .");

					return Nil.getInstance ();
				} else {
					returnItem = current_locale_of_body.getCar ().eval (current_env_of_let);
				}
				bool hasAdditionalExp = false;

				Node one_further_outExtending_tail = current_locale_of_body.getCdr ();

				// set one item extra in store of members of set of further tail items...  
				// if it is something to start a third part in a chain of further tail cycle.
				// & nil term test
				if (one_further_outExtending_tail != null && !one_further_outExtending_tail.isNull ()) {

					if (!one_further_outExtending_tail.isPair ()) {

						Console.WriteLine ("Error: one of tail expressions is not in correct pair procession .");

						return Nil.getInstance ();
					} else if (one_further_outExtending_tail.getCar () == null) {

						Console.WriteLine ("Error: one of expressions in begin's arguments null .");

						return Nil.getInstance ();
					} else {   

						// set a conditional and (on hold prospect)  return element.
						returnItem = one_further_outExtending_tail.getCar ().eval (current_env_of_let);
						hasAdditionalExp = true;
					}
				}
				// terminates with current return item.   // because cant go further in chain for begin expression chain
				else if (one_further_outExtending_tail != null && one_further_outExtending_tail.isNull ()) {

					hasAdditionalExp = false;
					// end
					return returnItem;
				} else {					// ends here with error
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
							returnItem = one_further_outExtending_tail.getCdr ().getCar ().eval (current_env_of_let);

							hasAdditionalExp = true;
							// increment to next in sequence of cdr's
							one_further_outExtending_tail = one_further_outExtending_tail.getCdr ();

						}
					} else {						// ends here with error
						Console.WriteLine ("Error: Null encountered further on in Begin's  expressions.");

						return Nil.getInstance ();
					}
				} // while loop ended

				return returnItem;



			}


		}

    }
}



