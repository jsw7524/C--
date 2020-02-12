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
        public void CaculateMod()
        {

            calculatorParser parser = Setup("A=(5*(24/5))");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(24, visitor.variables["A"]);
        }

        [TestMethod]
        public void CaculatePrime1()
        {

            calculatorParser parser = Setup(@"
n=4
A=[101]
A[2]=1
A[3]=1
while (n<=100)
{
   divisor=2
   isPrime=1
   while (divisor*divisor <= n)
   {
        tmp=n
        while (tmp>=divisor)
        {
            tmp=tmp-divisor
        }
        if (tmp==0)
        {
            isPrime=0
        }
        else
        {
            none=0
        }
        divisor=divisor+1
   }
   if (isPrime==1)
   {
        A[n]=1
   }
   else
   {
        none=0
   }
   n=n+1
}
                ");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(1, visitor.arrays["A"][2]);
            Assert.AreEqual(1, visitor.arrays["A"][13]);
            Assert.AreEqual(1, visitor.arrays["A"][53]);
            Assert.AreEqual(1, visitor.arrays["A"][97]);
            Assert.AreEqual(0, visitor.arrays["A"][20]);
            Assert.AreEqual(0, visitor.arrays["A"][77]);
            Assert.AreEqual(0, visitor.arrays["A"][36]);
        }

        [TestMethod]
        public void CaculatePrime2()
        {

            calculatorParser parser = Setup(@"
n=4
A=[101]
A[2]=1
A[3]=1
while (n<=100)
{
   divisor=2
   isPrime=1
   while (divisor*divisor <= n)
   {
        tmp=n
        while (tmp>=divisor)
        {
            tmp=tmp-divisor
        }
        if (tmp==0)
        {
            isPrime=0
        }
        divisor=divisor+1
   }
   if (isPrime==1)
   {
        A[n]=1
   }
   n=n+1
}
                ");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(1, visitor.arrays["A"][2]);
            Assert.AreEqual(1, visitor.arrays["A"][13]);
            Assert.AreEqual(1, visitor.arrays["A"][53]);
            Assert.AreEqual(1, visitor.arrays["A"][97]);
            Assert.AreEqual(0, visitor.arrays["A"][20]);
            Assert.AreEqual(0, visitor.arrays["A"][77]);
            Assert.AreEqual(0, visitor.arrays["A"][36]);
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
                                    A=3
                                    if (B==1){
                                       A=9
                                     }");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(9, visitor.variables["A"]);
        }
        [TestMethod]
        public void CheckIfStatement2()
        {
            calculatorParser parser = Setup(
                                    @"B=1
                                    if (B==2){
                                       A=9}
                                    else{
                                       A=6}");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(6, visitor.variables["A"]);
        }

        [TestMethod]
        public void CheckIfStatement3()
        {
            calculatorParser parser = Setup(
                                    @"B=1
                                    C=3
                                    if (B==1)
                                    {
                                       if (C==7)
                                       {
                                          A =8
                                       }
                                       else
                                       {
                                          A = 30
                                       }
                                    }
                                    else
                                    {
                                       A=6
                                    }");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(30, visitor.variables["A"]);
        }

        [TestMethod]
        public void CheckWhileStatement1()
        {
            calculatorParser parser = Setup(
                                    @"A=0
                                    while (A<5)
                                    {
                                       A=A+1
                                    }");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(5, visitor.variables["A"]);
        }

        [TestMethod]
        public void CaculateFibonacci()
        {
            calculatorParser parser = Setup(

 @"A=0
B=1
N=2
while (N<=10)
{
     tmp=A+B
     A=B
     B=tmp
     N=N+1
}");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(55, visitor.variables["B"]);
        }

        [TestMethod]
        public void CaculateFibonacciArray()
        {
            calculatorParser parser = Setup(

 @"
Array=[11]
Array[0]=0
Array[1]=1
n=2
while (n<=10)
{
	Array[n]=Array[n-2]+Array[n-1]
	n=n+1
}
");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(1, visitor.arrays["Array"][2]);
            Assert.AreEqual(13, visitor.arrays["Array"][7]);
            Assert.AreEqual(55, visitor.arrays["Array"][10]);
        }


        [TestMethod]
        public void CheckArray1()
        {
            calculatorParser parser = Setup(
                                    @"A=[10]
                                    A[2]=999
                                    B=1+A[2]");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            Assert.AreEqual(999, visitor.arrays["A"][2]);
            Assert.AreEqual(1000, visitor.variables["B"]);
        }

        [TestMethod]
        public void CheckPrint()
        {
            calculatorParser parser = Setup(
                                    @"print(1+2)");
            calculatorVisitor visitor = new calculatorVisitor();
            visitor.Visit(parser.program());
            //Assert.AreEqual(999, visitor.arrays["A"][2]);
            //Assert.AreEqual(1000, visitor.variables["B"]);
        }

    }
}
