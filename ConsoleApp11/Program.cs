using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsoleApp11
{
    public class calculatorVisitor : calculatorBaseVisitor<Decimal>
    {

        public Dictionary<String, Decimal> variables = new Dictionary<String, Decimal>();

        public override decimal VisitAdd([NotNull] calculatorParser.AddContext context)
        {
            Console.WriteLine(context.ToStringTree());
            decimal left = Convert.ToDecimal(Visit(context.expr1()));
            decimal right = Convert.ToDecimal(Visit(context.expr2()));

            return left + right;
        }

        public override decimal VisitSubtraction([NotNull] calculatorParser.SubtractionContext context)
        {
            Console.WriteLine(context.ToStringTree());
            decimal left = Convert.ToDecimal(Visit(context.expr1()));
            decimal right = Convert.ToDecimal(Visit(context.expr2()));

            return left - right;
        }

        public override decimal VisitMul([NotNull] calculatorParser.MulContext context)
        {
            decimal left = Convert.ToDecimal(Visit(context.expr2()));
            decimal right = Convert.ToDecimal(Visit(context.expr3()));

            return left * right;
        }

        public override decimal VisitDiv([NotNull] calculatorParser.DivContext context)
        {
            decimal left = Convert.ToDecimal(Visit(context.expr2()));
            decimal right = Convert.ToDecimal(Visit(context.expr3()));

            return left / right;
        }

        public override decimal VisitNumber([NotNull] calculatorParser.NumberContext context)
        {
            Console.WriteLine(context.ToStringTree());
            return Convert.ToDecimal(context.GetText());
        }

        public override decimal VisitExpr1Parentheses([NotNull] calculatorParser.Expr1ParenthesesContext context)
        {
            return Visit(context.expr1());
        }

        public override decimal VisitAssignment([NotNull] calculatorParser.AssignmentContext context)
        {
            var name = context.IDENTIFIER().ToString();
            var value = Visit(context.expr0());

            if (!variables.ContainsKey(name))
            {
                variables.Add(name, value);
            }
            else
            {
                variables[name] = value;
            }
            return value;
        }

        public override decimal VisitGetValueIDENTIFIER([NotNull] calculatorParser.GetValueIDENTIFIERContext context)
        {
            var name = context.IDENTIFIER().ToString();

            if (!variables.ContainsKey(name))
            {
                throw new Exception();
            }
            else
            {
                return variables[name];
            }
            //return base.VisitGetValueIDENTIFIER(context);
        }

        public override decimal VisitOP_EQU([NotNull] calculatorParser.OP_EQUContext context)
        {
            decimal left = Convert.ToDecimal(Visit(context.expr1(0)));
            decimal right = Convert.ToDecimal(Visit(context.expr1(1)));
            return left == right ? 1 : 0;
        }

        public override decimal VisitOP_GT([NotNull] calculatorParser.OP_GTContext context)
        {
            decimal left = Convert.ToDecimal(Visit(context.expr1(0)));
            decimal right = Convert.ToDecimal(Visit(context.expr1(1)));
            return left > right ? 1 : 0;
        }

        public override decimal VisitOP_GTEQU([NotNull] calculatorParser.OP_GTEQUContext context)
        {
            decimal left = Convert.ToDecimal(Visit(context.expr1(0)));
            decimal right = Convert.ToDecimal(Visit(context.expr1(1)));
            return left >= right ? 1 : 0;
        }

        public override decimal VisitOP_LESS([NotNull] calculatorParser.OP_LESSContext context)
        {
            decimal left = Convert.ToDecimal(Visit(context.expr1(0)));
            decimal right = Convert.ToDecimal(Visit(context.expr1(1)));
            return left < right ? 1 : 0;
        }

        public override decimal VisitOP_LESSEQU([NotNull] calculatorParser.OP_LESSEQUContext context)
        {
            decimal left = Convert.ToDecimal(Visit(context.expr1(0)));
            decimal right = Convert.ToDecimal(Visit(context.expr1(1)));
            return left <= right ? 1 : 0;
        }

        //public override decimal VisitIfstatement([NotNull] calculatorParser.IfstatementContext context)
        //{
        //    if (1 == Convert.ToDecimal(Visit(context.expr0())))
        //    {
        //        Visit(context.subprogram(0));
        //    }
        //    else
        //    {
        //        Visit(context.subprogram(1));
        //    }
        //    return 0;
        //}
    }


    class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                StringBuilder text = new StringBuilder("AAA=(11+1)/(2+2)\\r\\nBBB=3+6");

                AntlrInputStream inputStream = new AntlrInputStream(File.ReadAllText("Code.txt"));
                calculatorLexer calculatorLexer = new calculatorLexer(inputStream);
                CommonTokenStream commonTokenStream = new CommonTokenStream(calculatorLexer);
                calculatorParser calculatorParser = new calculatorParser(commonTokenStream);

                var formulaContext = calculatorParser.program();
                var visitor = new calculatorVisitor();
                var val = visitor.Visit(formulaContext);
                //Console.WriteLine(val);


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex);
            }
        }
    }
}
