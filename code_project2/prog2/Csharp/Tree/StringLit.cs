// StringLit -- Parse tree node class for representing string literals

using System;

namespace Tree
{
    public class StringLit : Node
    {
        private string stringVal;

        public StringLit(string s)
        {
            stringVal = s;
        }

        public override void print(int n)
        {
			// special StringLit print case     where String lit print will not print double quotes
			if (Tree.BuiltIn.builtIn_display__do_not_print_double_quotes) {
				// There got to be a more efficient way to print n spaces.
				for (int i = 0; i < n; i++)
					Console.Write (" ");
				Console.Write (this.stringVal);
			}

			Printer.printStringLit(n, stringVal);

        }

        public override bool isString()
        {
            return true;
        }

		public String getString(){
			return stringVal;
		}

		public override Node eval(Environment env1){
			return this;
		}
    }
}

