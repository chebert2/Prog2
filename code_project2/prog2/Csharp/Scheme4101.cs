// SPP -- The main program of the Scheme pretty printer.

using System;
using Parse;
using Tokens;
using Tree;

public class Scheme4101
{
    public static Parser parser;
    public static int Main(string[] args)
    {
        // Create scanner that reads from standard input
        Scanner scanner = new Scanner(Console.In);
        
        if (args.Length > 1 ||
            (args.Length == 1 && ! args[0].Equals("-d")))
        {
            Console.Error.WriteLine("Usage: mono SPP [-d]");
            return 1;
        }
        
        // If command line option -d is provided, debug the scanner.
        if (args.Length == 1 && args[0].Equals("-d"))
        {
            // Console.Write("Scheme 4101> ");
            Token tok = scanner.getNextToken();
            while (tok != null)
            {
                TokenType tt = tok.getType();

                Console.Write(tt);
                if (tt == TokenType.INT)
                    Console.WriteLine(", intVal = " + tok.getIntVal());
                else if (tt == TokenType.STRING)
                    Console.WriteLine(", stringVal = " + tok.getStringVal());
                else if (tt == TokenType.IDENT)
                    Console.WriteLine(", name = " + tok.getName());
                else
                    Console.WriteLine();

                // Console.Write("Scheme 4101> ");
                tok = scanner.getNextToken();
            }
            return 0;
        }

        // set this to false as default setting.
		Tree.BuiltIn.builtIn_display__do_not_print_double_quotes = false;

		// Create parser
		TreeBuilder builder = new TreeBuilder();
		parser = new Parser(scanner, builder);
		Node root;

		// Create and populate the built-in environment and

		// create the top-level environment

		// Read-eval-print loop

		root = (Node) parser.parseExp();

		Tree.Environment env_global = new Tree.Environment ();
		// define environment as an identifier   for future access in scheme
		env_global.define (new Ident ("global-environment"), env_global);

		Ident globalEnvIdent = new Ident ("global-environment");


		Ident readBuiltIn = new Ident ("read");
		env_global.define (readBuiltIn, new BuiltIn(readBuiltIn));

		Ident writeBuiltIn = new Ident ("write");
		env_global.define (writeBuiltIn, new BuiltIn(writeBuiltIn) );

		Ident displayBuiltIn = new Ident ("display");
		env_global.define (displayBuiltIn, new BuiltIn(displayBuiltIn));

		Ident newLineBuiltIn = new Ident ("newline");
		env_global.define (newLineBuiltIn, new BuiltIn (newLineBuiltIn) );

		Ident evalBuiltIn = new Ident ("eval");
		env_global.define (evalBuiltIn, new BuiltIn(evalBuiltIn));

		Ident eofBuiltIn = new Ident ("eof-object?");
		env_global.define (eofBuiltIn, new BuiltIn(eofBuiltIn));


		Ident pred_null = new Ident ("null?");
		env_global.define (pred_null, new BuiltIn(pred_null));

		Ident pred_symbol = new Ident ("symbol?");
		env_global.define (pred_symbol, new BuiltIn(pred_symbol));

		Ident pred_number = new Ident ("number?");
		env_global.define (pred_number, new BuiltIn(pred_number));

		Ident pred_boolean = new Ident ("boolean?");
		env_global.define (pred_boolean, new BuiltIn(pred_boolean));

		Ident pred_procedure = new Ident ("procedure?");
		env_global.define (pred_procedure, new BuiltIn(pred_procedure));

		Ident pred_pair = new Ident ("pair?");
		env_global.define (pred_pair, new BuiltIn(pred_pair));

		Ident pred_environment = new Ident ("environment?");
		env_global.define (pred_environment, new BuiltIn(pred_environment));

		Ident pred_string = new Ident ("string?");
		env_global.define (pred_string, new BuiltIn(pred_string));

		Ident binary_add = new Ident ("b+");
		env_global.define (binary_add, new BuiltIn (binary_add));

		Ident plus_op = new Ident ("+");
		env_global.define (plus_op, binary_add);

		Ident binary_sub = new Ident ("b-");
		env_global.define (binary_sub, new BuiltIn (binary_sub));

		Ident subtract_op = new Ident ("-");
		env_global.define (subtract_op, binary_sub);

		Ident binary_multiplic = new Ident ("b*");
		env_global.define (binary_multiplic, new BuiltIn (binary_multiplic));

		Ident multiply_op = new Ident ("*");
		env_global.define (multiply_op, binary_multiplic);

		Ident binary_division = new Ident ("b/");
		env_global.define (binary_division, new BuiltIn (binary_division));

		Ident divide_op = new Ident ("/");
		env_global.define (divide_op, binary_division);

		Ident binary_equality = new Ident ("b=");
		env_global.define (binary_equality, new BuiltIn (binary_equality));

		Ident equality_op = new Ident ("=");
		env_global.define (equality_op, binary_equality);

		Ident binary_lessthan = new Ident ("b<");
		env_global.define (binary_lessthan, new BuiltIn (binary_lessthan));

		Ident lessthan_op = new Ident ("<");
		env_global.define (lessthan_op, binary_lessthan);

		Ident binary_greaterthan = new Ident ("b>");
		env_global.define (binary_greaterthan, new BuiltIn (binary_greaterthan));

		Ident greaterthan_op = new Ident (">");
		env_global.define (greaterthan_op, binary_greaterthan);

		Ident binary_lessthanEq = new Ident ("b<=");
		env_global.define (binary_lessthanEq, new BuiltIn (binary_lessthanEq));

		Ident lessthanEq_op = new Ident ("<=");
		env_global.define (lessthanEq_op, binary_lessthanEq);

		Ident binary_greaterthanEq = new Ident ("b>=");
		env_global.define (binary_greaterthanEq, new BuiltIn (binary_greaterthanEq));

		Ident greaterthanEq_op = new Ident (">=");
		env_global.define (greaterthanEq_op, binary_greaterthanEq);


		Ident not_op = new Ident ("not");
		env_global.define (not_op, new BuiltIn (not_op));


		Ident pred_equal = new Ident ("eq?");
		env_global.define (pred_equal, new BuiltIn (pred_equal));

		Ident listBuiltIn = new Ident ("list");
		env_global.define (listBuiltIn, new BuiltIn (listBuiltIn));

		Ident carBuiltIn = new Ident ("car");
		env_global.define (carBuiltIn, new BuiltIn (carBuiltIn));

		Ident cdrBuiltIn = new Ident ("cdr");
		env_global.define (cdrBuiltIn, new BuiltIn (cdrBuiltIn));

		Ident consBuiltIn = new Ident ("cons");
		env_global.define (consBuiltIn, new BuiltIn (consBuiltIn) );

		Ident setCarBuiltIn = new Ident ("set-car!");
		env_global.define (setCarBuiltIn, new BuiltIn (setCarBuiltIn));

		Ident setCdrBuiltIn = new Ident ("set-cdr!");
		env_global.define (setCdrBuiltIn, new BuiltIn (setCdrBuiltIn) );




		// working on top level loop
		Tree.Environment top_level = new Tree.Environment (env_global);

		// define top-level Scheme scm function

		// lets define the S-expression for (define (Scheme) ... func )

		Ident defineIdent = new Ident ("define");

		Ident SchemeIdent = new Ident ("Scheme");

		Cons cons_holdingSchemeIdent = new Cons (SchemeIdent, Nil.getInstance() );



		StringLit printpromptScheme = new StringLit ("Scheme 4101> ");

		Cons cons_holding_display_promptStr = new Cons (displayBuiltIn, new Cons (printpromptScheme , Nil.getInstance() ));


		Ident LetIdent = new Ident ("let");

		Ident InputIdent = new Ident ("input");

		Ident If_Ident = new Ident ("if");

		Cons cons_holding_assocList = new Cons ( new Cons (InputIdent, new Cons( new Cons(readBuiltIn, Nil.getInstance()), Nil.getInstance() ) ) , Nil.getInstance() );

		Ident BeginIdent = new Ident ("begin");



		Cons cons_holding_write_SchemeCode = new Cons ( writeBuiltIn,     new Cons ( new Cons (evalBuiltIn, new Cons (InputIdent, new Cons(globalEnvIdent, Nil.getInstance() ) ) ),  Nil.getInstance() )      );

		Cons cons_holding_begin_SchemeCode = new Cons (BeginIdent ,  new Cons ( cons_holding_write_SchemeCode       , 

			          new Cons (new Cons(newLineBuiltIn, Nil.getInstance() ),    new Cons( new Cons(SchemeIdent, Nil.getInstance()), Nil.getInstance() )) ) );


		Cons cons_holding_newline = new Cons (  new Cons(newLineBuiltIn, Nil.getInstance() )  , Nil.getInstance() );


		Cons cons_holding_not = new Cons (not_op, new Cons( new Cons(eofBuiltIn, new Cons(InputIdent, Nil.getInstance() ) ), Nil.getInstance() ) );

		Cons cons_holding_if_SchemeCode = new Cons ( If_Ident , new Cons (cons_holding_not, new Cons (cons_holding_begin_SchemeCode, cons_holding_newline) ) );


		Cons cons_holding_let_SchemeCode = new Cons (LetIdent, new Cons(cons_holding_assocList , new Cons(cons_holding_if_SchemeCode, Nil.getInstance()) ) );

		// done defining subcomponents of  top-level Scheme scm function

		// here is a parse tree defining func (Scheme)...
		Cons define_Scheme = new Cons (defineIdent, new Cons( cons_holdingSchemeIdent, new Cons (cons_holding_display_promptStr, 
			new Cons (cons_holding_let_SchemeCode, Nil.getInstance()) ) ) );


		define_Scheme.eval (top_level);


		cons_holdingSchemeIdent.eval (top_level);






		return 0;
	}
}
