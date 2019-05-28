namespace CalcPi.Tests

open Xunit
open Amazon.Lambda.TestUtilities
open CalcPi

module FunctionTest =
    [<Fact>]
    let ``Invoke ToUpper Lambda Function``() =
        // Invoke the lambda function and confirm the result.
        let lambdaFunction = Function()
        let context = TestLambdaContext()
        let pi = lambdaFunction.FunctionHandler 10 context

        Assert.Equal(3.15, pi)

    [<EntryPoint>]
    let main _ = 0
