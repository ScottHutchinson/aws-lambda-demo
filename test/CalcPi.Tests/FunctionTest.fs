namespace CalcPi.Tests
open System
open System.IO
open FsUnit.Xunit
open Xunit
open Xunit.Abstractions
open Amazon.Lambda.TestUtilities
open CalcPi

type Converter(output: ITestOutputHelper) =
    inherit TextWriter()
    override __.Encoding = stdout.Encoding
    override __.WriteLine message = output.WriteLine message
    override __.Write message = output.WriteLine message

type MyTests(output:ITestOutputHelper) =
    do new Converter(output) |> Console.SetOut

    [<Fact>]
    member __.``Lambda Function Should Return Pi`` () =
        // Invoke the lambda function and confirm the result.
        let lambdaFunction = Function()
        let context = TestLambdaContext()
        let pi = lambdaFunction.FunctionHandler 10 context
        output.WriteLine("pi = {0}", pi)
        printfn "pi = %f" pi
        pi |> should (equalWithin 0.0005) 3.14159265
