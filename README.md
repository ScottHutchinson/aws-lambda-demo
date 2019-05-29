# CalcPi
AWS Lambda function that calculates the value of pi

## Getting Started with AWS Lambda and Visual Studio Code (on Windows)
1. [Create an Amazon Web Services account](https://aws.amazon.com/)
2. [Install Docker Desktop (requires a Docker hub login)](https://www.docker.com/products/docker-desktop)
3. [Install AWS SAM (Serverless Application Model) CLI](https://docs.aws.amazon.com/serverless-application-model/latest/developerguide/serverless-sam-cli-install.html)
4. Setup your AWS Credentials in the [AWS Management Console](https://console.aws.amazon.com/iam):  
  a. Create an IAM (Identity and Access Management) User, and attach the `AWSLambdaRole` policy to the user's permissions. NOTE: If you ever change the IAM user's permissions and get the `not authorized to perform: lambda:InvokeFunction` error message when invoking the function from the command line, then you might need to deploy the function again to refresh the permissions.  
  b. Create an access key for the IAM user. NOTE: The secret part of the key can only be copied from the screen displayed immediately after the key is created. If you lose the key after dismissing that screen, then you will need to create another key.  
5. [Install the AWS Toolkit for Visual Studio Code](https://docs.aws.amazon.com/toolkit-for-vscode/latest/userguide/getting-started.html)  
6. In the VS Code Command Palette:  
  a. Type `AWS: Create Credentials Profile`  
  When prompted, enter the access key (labeled `Access key ID` in the `IAM Management Console`) and then the secret key. This will store the access keys in the file `"%UserProfile%\.aws\credentials"`  
  b. Type `AWS: Connect to AWS`  
7. Install the dotnet AWS Lambda project templates using this command line:  
```dotnet new -i Amazon.Lambda.Templates::*```  
8. Create a new project using this command line (where `FpDevUser` is whatever name you gave to the profile in step 6a):  
```dotnet new lambda.EmptyFunction --language F# --name CalcPi --profile FpDevUser --region us-west-2```
9. Add this element to the .fsproj file for the project containing the lambda function:
```  
  <ItemGroup>
    <DotNetCliToolReference Include="Amazon.Lambda.Tools" Version="2.2.0" />
  </ItemGroup>
```
10. Deploy the lambda function to the cloud using this command line (where `FpDevUser` is whatever name you gave to the IAM user):  
```dotnet lambda deploy-function CalcPi –function-role FpDevUser```
11. Invoke the lambda function using this command line:  
```dotnet lambda invoke-function CalcPi --payload 10```
