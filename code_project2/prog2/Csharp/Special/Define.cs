
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
			
				return null;


			// the key of a frame is represented by the alist.getCar().getCar().getName();

			// the val of a frame is represented by the alist.getCar().getCdr


			// check if thing being defined is a function ... or just a variable
			if (  !  cons_expression.getCdr ().getCar ().isPair ())
				it_is_variable_Identifier__notFunction = true;
			else
				it_is_variable_Identifier__notFunction = false;



			if (it_is_variable_Identifier__notFunction) {

				Node place_of_Node_with_Value = cons_expression.getCdr ().getCdr ().getCar ();

				// access evalued form of value in assignment
				Node valueBeingAssigned = place_of_Node_with_Value.eval (env1);

				env1.define (cons_expression.getCdr ().getCar (), valueBeingAssigned);

			}
			// TODO:
			// work out storing function in environment

			else {

				// "function assignment";

				// create a lambda of this definition of a function
				// lambda node = (cons 'lambda (cons (cdr (car (cdr a))) (cdr (cdr a))))
				Node lambda_function =     new Cons (  new Ident("lambda") ,    new Cons ( cons_expression.getCdr().getCar().getCdr(), 
					cons_expression.getCdr().getCdr() ));

				Closure defined_closure = new Closure (lambda_function, env1);

				env1.define (cons_expression.getCdr ().getCar().getCar() , defined_closure);

			}

			return null;
		}
    }
}








/**
oldest  draft  for reference
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
    }
}  end oldest






// less old

// this is a draft and incomplete form of define .


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

          // needs work.  It is incomplete because it does not evaluate the args to (define x y  expression
		public override Node eval(Node cons_expression, Environment env1) {


			// the key of a frame is represented by the alist.getCar().getCar().getName();

			// the val of a frame is represented by the alist.getCar().getCdr

			env1.define (cons_expression.getCdr().getCar(),  cons_expression.getCdr().getCdr().getCar());

			return null;
		}
    }
}




**/
