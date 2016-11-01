
/**
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
}
**/






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




