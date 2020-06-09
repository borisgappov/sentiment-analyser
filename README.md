## Sentiment Analyzer

[![Countries test app](https://img.youtube.com/vi/mu3pTU7HIpY/0.jpg)](https://www.youtube.com/watch?v=mu3pTU7HIpY)

### How to run
- Open solution in Visual Studio 2019
- Set SQL Server connection string in SentimentAnalyser/appsettings.Development.json
- Build and run, SPA error - it is ok
- Open command prompt, go to SentimentAnalyser/ClientApp dir, run `npm install`
- Run `npm start` ( because of a .net core 3 + Angular 9 bug please start the client manually https://github.com/dotnet/aspnetcore/issues/17277)
- Open https://localhost:44300/
- Open swagger document https://localhost:44300/swagger

### Prepopulated user

login: user@host.com
password: 1qaz@WSX