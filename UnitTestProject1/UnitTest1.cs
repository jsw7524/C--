using System;
using System.IO;
using Antlr4.Runtime;
using ConsoleApp11;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private calculatorParser Setup(string text)
        {
            AntlrInputStream inputStream = new AntlrInputStream(text);
            calculatorLexer calculatorLexer = new calculatorLexer(inputStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(calculatorLexer);
            calculatorParser calculatorParser = new calculatorParser(commonTokenStream);
            return calculatorParser;
        }

        [TestMethod]
        public void AssignNumber()
        {
            calculatorParser parser = Setup("A=1");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(1, visitor.variables["A"]);
        }

        [TestMethod]
        public void AddTwoNumber()
        {
            calculatorParser parser = Setup("A=1+2");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(3, visitor.variables["A"]);
        }
        [TestMethod]
        public void CaculateMath1()
        {
            calculatorParser parser = Setup("A=(1+2)*(24/8)-4");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(5, visitor.variables["A"]);
        }
        [TestMethod]
        public void CaculateMath2()
        {
            calculatorParser parser = Setup("A=(1*10-10+2*10+40)/(9+1)");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(6, visitor.variables["A"]);
        }

        [TestMethod]
        public void CheckBoolean1()
        {
            calculatorParser parser = Setup("A=1>2");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(0, visitor.variables["A"]);
        }

        [TestMethod]
        public void CheckBoolean2()
        {
            calculatorParser parser = Setup("A=2>=2");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(1, visitor.variables["A"]);
        }

        [TestMethod]
        public void CheckBoolean3()
        {
            calculatorParser parser = Setup("A=5*2==(6+4)");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(1, visitor.variables["A"]);
        }

        [TestMethod]
        public void CheckIfStatement1()
        {
            calculatorParser parser = Setup(
                                    @"B=1
                                    if (B==1)
                                       A=9
                                    else
                                       A=0");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(9, visitor.variables["A"]);
        }
        [TestMethod]
        public void CheckIfStatement2()
        {
            calculatorParser parser = Setup(
                                    @"B=1
                                    if (B==2)
                                       A=9
                                    else
                                       A=0");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(0, visitor.variables["A"]);
        }
    }
}
