// Quote -- Parse tree node strategy for printing the special form quote

using System;

namespace Tree
{
    public class Quote : Special
    {
	public Quote() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printQuote(t, n, p);
        }


	public override Node eval(Node node1, Environment env1) {
	        // cannot evaluate what is absent.
            	if (node1.getCdr ().isNull ())
			return null;
		// return argument to quote as the same tree it was.
		else
			return node1.getCdr ().getCar ();
		}

    }
}

