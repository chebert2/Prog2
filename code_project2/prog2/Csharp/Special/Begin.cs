
// Begin -- Parse tree node strategy for printing the special form begin

using System;

namespace Tree
{
    public class Begin : Special
    {
	public Begin() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printBegin(t, n, p);
        }
	public Node eval(Node t, Environment env) {
/**Node cdr = t.getCdr();

            if (cdr == null)

            {

                Console.WriteLine("Error: Null)");

                return Nil.getInstance();

            }



            Node car = evalCdr(cdr, null, e);

            if (car == null)

            {

                Console.WriteLine("Error: Null)");

            }
	      return car;
*/
          
	  
	  
	  
		return null;
		
	 // return evals every item elem.  On the last item, the value is returned.
         // what needs to be returned  is the last non-nil tail items eval.

	}
    }
}

