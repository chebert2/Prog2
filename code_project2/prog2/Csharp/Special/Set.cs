// Set -- Parse tree node strategy for printing the special form set!

using System;

namespace Tree
{
    public class Set : Special
    {
	public Set() { }
	
        public override void print(Node t, int n, bool p)
        {
            Printer.printSet(t, n, p);
        }
	public Node eval(Node t, Environment env) {
            Node cadr = t.getCdr().getCar(); 
            Node caddr = t.getCdr().getCdr().getCar();
            env.define(cadr, caddr.eval(env));
            return Console.WriteLine("");
	}
    }
}

