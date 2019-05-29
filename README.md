# CalcPi
AWS Lambda function that calculates the value of pi

## Getting Started with AWS Lambda and Visual Studio Code (on Windows)
1. Install Docker Desktop
2. Install AWS SAM CLI
3. dotnet lambda deploy-function CalcPi –function-role FpDevUser
4. dotnet lambda invoke-function CalcPi --payload 10
