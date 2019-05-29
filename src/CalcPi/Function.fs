namespace CalcPi
open System

module calcPi =

    /// Calculates whether a point is within the unit circle
    let insideCircle (x:float, y:float) =
        if x * x + y * y <= 0.25 then 1 else 0

    /// Generates random X, Y values between 0 and 1
    let randomPoints max = seq {
        let rnd = new Random()
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
        averagePi 1000000 iterations
