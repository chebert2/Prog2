
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
	 
	 // I would do...

			Node cdr = t.getCdr();


			if (cdr == null) {

				Console.WriteLine("Error: Null");

				return Nil.getInstance();
			}
			// then check if the nil at list end      without having anything fulfilled.
			// also check to rule out there not being a child  a cons node.
			else if ( cdr.isNull() || !cdr.isPair() )
			{

				Console.WriteLine("Error: no arguments for evaluating in Begin statement .");

				return Nil.getInstance();

			}
			// start loop that flips through elements until we get at the last destination expression.
			else{
				// initialize the item that may be returned.
				Node returnItem;

				// set a conditional and (on hold prospect)  return element.
				if(cdr.getCar() == null){
					Console.WriteLine("Error: one of expressions in begin's arguments null .");

					return Nil.getInstance();
				}
				else {
					returnItem = cdr.getCar().eval(env);
				}
				bool hasAdditionalExp = false;

				Node one_further_outExtending_tail = cdr.getCdr();

				// set one item extra in store of members of set of further tail items...  
				// if it is something to start a third part in a chain of further tail cycle.
				// & nil term test
				if (one_further_outExtending_tail != null && !one_further_outExtending_tail.isNull() ) 
				{

					if(!one_further_outExtending_tail.isPair())
					{

						Console.WriteLine("Error: one of tail expressions is not in correct pair procession .");

						return Nil.getInstance();
					}
					else if(one_further_outExtending_tail.getCar() == null){

						Console.WriteLine("Error: one of expressions in begin's arguments null .");

						return Nil.getInstance();
					}
					else {   

						// set a conditional and (on hold prospect)  return element.
						returnItem = one_further_outExtending_tail.getCar().eval(env);
						hasAdditionalExp = true;
					}
				}
				// terminates with current return item.   // because cant go further in chain for begin expression chain
				else if (one_further_outExtending_tail != null && one_further_outExtending_tail.isNull() ){

					hasAdditionalExp = false;
					// end
					return returnItem;
				}
				else 
					// ends here with error
				{
					Console.WriteLine("Error: Null encountered further on in Begin's  expressions.");

					return Nil.getInstance();
				}

				// loops over each expression leading up to last.
				while(hasAdditionalExp) {


					// set end now.
					if (one_further_outExtending_tail.getCdr() != null && one_further_outExtending_tail.getCdr().isNull() ) 
					{
						//end
						hasAdditionalExp = false;
					}
					// go to next in sequence of cdr's
					else if (one_further_outExtending_tail.getCdr() != null && 
						!one_further_outExtending_tail.getCdr().isNull()) { 


						if (!one_further_outExtending_tail.getCdr ().isPair ()) {

							Console.WriteLine("Error: one of expressions is not in correct pair procession .");

							return Nil.getInstance();
						}

						else if(one_further_outExtending_tail.getCdr().getCar() == null){

							Console.WriteLine("Error: one of expressions in begin's arguments null .");

							return Nil.getInstance();
						}
						else  {   

							// set a conditional and (on hold prospect)  return element.                  
							returnItem = one_further_outExtending_tail.getCdr().getCar().eval(env);

							hasAdditionalExp = true;
							// increment to next in sequence of cdr's
							one_further_outExtending_tail = one_further_outExtending_tail.getCdr();

						}
					}
					else
						// ends here with error
					{
						Console.WriteLine("Error: Null encountered further on in Begin's  expressions.");

						return Nil.getInstance();
					}
				} // while loop ended

				return returnItem;
			}
           
	   
	   
	   
	   
	}
    }
}

