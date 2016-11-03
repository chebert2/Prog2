// Define -- Parse tree node strategy for printing the special form define

using System;

namespace Tree
{
    public class Define : Special
    {
	public Define() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printDefine(t, n, p);
        }

		public override Node eval(Node cons_expression, Environment env1) {


			bool it_is_variable_Identifier__notFunction = false;

			// check for null  in construct of (define elem1 elem2)...  and ends current method
			// test1 checks if there might even be elements in expression
			// test 2 checks if elem1 is a Identifier or not
			// test 3 checks if elem2 exists
			if (cons_expression.getCdr ().isNull () || cons_expression.getCdr ().getCar().isNull()
				|| cons_expression.getCdr ().getCdr().isNull()  )
			
				return new StringLit("Error: lacking items to be defined in (define ...) .");


			// the key of a frame is represented by the alist.getCar().getCar().getName();

			// the val of a frame is represented by the alist.getCar().getCdr


			// check if thing being defined is a function ... or just a variable
			if (  !  cons_expression.getCdr ().getCar ().isPair ())
				it_is_variable_Identifier__notFunction = true;
			else
				it_is_variable_Identifier__notFunction = false;



			if (it_is_variable_Identifier__notFunction) {

				if (cons_expression.getCdr ().getCdr ().getCar () == null)
					return new StringLit ("Error: node with value in definition is null. cannot define.");

				Node place_of_Node_with_Value = cons_expression.getCdr ().getCdr ().getCar ();

				if (place_of_Node_with_Value.isPair ()) {
					// assign val to id variable
					// this in fact might assign our variable to point to a function term   [indirectly.]
					env1.define (cons_expression.getCdr ().getCar (), place_of_Node_with_Value.eval (env1));

					// finish here.
					return null;

				} else {
					if (place_of_Node_with_Value.isSymbol ()) {
						Node lookedUp_val_term = env1.lookup (place_of_Node_with_Value);

						// error ... value to be assigned is undefined.
						if (lookedUp_val_term == null)
							return new StringLit ("Error: value to be assigned is undefined expression.");
						else
							// assign val looked up , unto id variable
							// this in fact might assign our variable to point to a function term   [indirectly.]
							env1.define (cons_expression.getCdr ().getCar (), lookedUp_val_term);

					}
					else
						// assign val onto id variable
						// this in fact might assign our variable to point to a function term   [indirectly.]
						env1.define (cons_expression.getCdr ().getCar (), place_of_Node_with_Value);

					// finish here.
					return null;

				}



			}
			// work out storing function in environment

			else {

				// "function assignment";

				if (cons_expression.getCdr ().getCar ().getCdr ().isNull () || cons_expression.getCdr ().getCar ().getCdr ().isPair ()) {
					// normal case without dot in parameters of express

					// create a lambda of this definition of a function
					// lambda node = (cons 'lambda (cons (cdr (car (cdr a))) (cdr (cdr a))))
					Node lambda_function = new Cons (new Ident ("lambda"), new Cons (cons_expression.getCdr ().getCar ().getCdr (), 
						                       cons_expression.getCdr ().getCdr ()));

					Closure defined_closure = new Closure (lambda_function, env1);

					env1.define (cons_expression.getCdr ().getCar ().getCar (), defined_closure);
				} else {
					// particular spec case with a dot expression for parameters ... that ends the list without a nil 

					// create a lambda of this definition of a function
					// lambda node = (cons 'lambda (cons (cdr (car (cdr a))) (cdr (cdr a))))
					Node lambda_function = new Cons (new Ident ("lambda"), 
						                                     new Cons ( 
							                                    
							                                    new Cons(cons_expression.getCdr ().getCar ().getCdr (), Nil.getInstance() ) 

							                                   , 

						                                      cons_expression.getCdr ().getCdr ()));

					Closure defined_closure = new Closure (lambda_function, env1);

					env1.define (cons_expression.getCdr ().getCar ().getCar (), defined_closure);
				}

			}

			return null;
		}
    }
}


