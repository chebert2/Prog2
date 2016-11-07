// If -- Parse tree node strategy for printing the special form if

using System;

namespace Tree
{
    public class If : Special
    {
	public If() { }

        public override void print(Node t, int n, bool p)
        {
            Printer.printIf(t, n, p);
        }
	public Node eval(Node t, Environment env) {
		Node cadr = t.getCdr().getCar();
		Node csddr = t.getCdr().getCdr().getCar();
		if (cond.eval(env).getBoolean()) {
		     return exp.eval(env);
		}
		else if (!(t.getCdr().getCdr().getCdr()).isNull()) {

			return exp.eval(env);

		} 
		else {

			Console.Writeline("Error: Null");

			return new Nil();

		}
    }
}

