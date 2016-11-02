// Regular -- Parse tree node strategy for printing regular lists

using System;

namespace Tree
{
    public class Regular : Special
    {
        public Regular() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printRegular(t, n, p);
        }

		// dummy eval
		public override Node eval(Node node1, Environment env1) {

			// note: given that the first element is function...  
			// a needed check is seeing as to  whether
			// it is    a  builtin type   or a closure.


			// giberish
			return new Ident("one");
		}
    }
}


