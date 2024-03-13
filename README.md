# Introduction 
TODO: Give a short introduction of your project. Let this section explain the objectives or the motivation behind this project. 

# Getting Started
TODO: Guide users through getting your code up and running on their own system. In this section you can talk about:
1.	Installation process
2.	Software dependencies
3.	Latest releases
4.	API references

# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)

<!-- Push steps
1 - Run this command line to remove all credentials : dotnet nuget locals all --clear
2 - Go to Solution .sln path where nuget.config file is available run : dotnet restore --interactive 
3 - Add these attributes to the project that you want to pack :
  --<GeneratePackageOnBuild>true</GeneratePackageOnBuild>
  --<PackageId>EasyAccess</PackageId>
  --<Version>1.0.1</Version>
  --<Authors>BiApp</Authors>
  --<Company>FunctionAir</Company>
4 - Go to the project .csproj and run : dotnet pack / dotnet pack --configuration Release
5 - then go to the ./bin/debug folder where your .nupkg available and run : dotnet nuget push --source "EasyAccess" --api-key az .\EasyAccess.1.0.1.nupkg -->