namespace CalcPi
open System

module calcPi =

    /// Calculates whether a point is within the unit circle
    let insideCircle (x:float, y:float) =
        if x * x + y * y <= 0.25 then 1 else 0

    /// Generates random X, Y values between 0 and 1
    let randomPoints max = seq {
        let rnd = Random()
        for _ in 0 .. max do
            yield 0.5 - rnd.NextDouble(), 0.5 - rnd.NextDouble() }

    /// Generate specified number of random points and
    /// calculate PI using Monte Carlo simulation
    let monteCarloPI size =
        let inside =
            let points = randomPoints size
            points |> Seq.sumBy insideCircle

        // Estimate the value of PI
        float inside / float size * 4.0

    // Test the Monte Carlo PI calculation
    // let pi = monteCarloPI 1000000

    // Run the calculation 10 times and calculate average
    // Change to 'Array.Parallel.map' to parallelize!
    let averagePi points iterations =
        [| for _ in 0 .. iterations -> points |]
        |> Array.Parallel.map monteCarloPI
        |> Array.average

open Amazon.Lambda.Core
open calcPi
open Newtonsoft.Json
open Newtonsoft.Json.Linq

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[<assembly: LambdaSerializer(typeof<Amazon.Lambda.Serialization.Json.JsonSerializer>)>]
()

type Input = { points: int; iterations: int }

type Function() =
    /// <summary>
    /// A function that calculates pi.
    /// </summary>
    /// <param name="iterations"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    member __.FunctionHandler (input: Input) (_: ILambdaContext) =
        let pi = averagePi input.points input.iterations
        let ret = {| statusCode = 200; body = pi |}
        JObject.FromObject(ret)
