
// Cond -- Parse tree node strategy for printing the special form cond

using System;

namespace Tree
{
    public class Cond : Special
    {
	public Cond() { }

        public override void print(Node t, int n, bool p)
        { 
            Printer.printCond(t, n, p);
        }
	public Node eval(Node t, Environment env) {
           Node ex;
           ex = t.getCdr();
           while ((!(ex.getCar()).eval(env).getBoolean()) && (!ex.isNull())) {
			exp = exp.getCdr();
		}
	   if (exp.isNull()) 
		return new Nil();
	   else
	       return (exp.getCar().getCdr().getCar().eval(env));

	}
    }
}


