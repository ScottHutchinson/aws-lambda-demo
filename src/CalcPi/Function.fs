namespace CalcPi


open Amazon.Lambda.Core

open System


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.Json.JsonSerializer>)>]
()


type Function() =
    /// <summary>
    /// A simple function that takes a string and does a ToUpper
    /// </summary>
    /// <param name="iterations"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    member __.FunctionHandler (iterations: int) (_: ILambdaContext) =
        3.14 + 0.001 * float iterations
