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

### Task description
##### Assignment
Create an application that performs sentiment analysis of the entered document. (Sentiment analysis is a standard task in natural language processing and represents the determination of the dominant sentiment (emotion) of a document based on its content. In practice, the most common classification into positive and negative documents is used extensively in recommendation systems and public opinion predictions).
##### Description
The Sentiments database contains the “Lexicon” table. The “Lexicon” table is a lexicon that contains words and their sentiment scores from the range [-1, 1]. If the value is greater than zero, the word is considered to have a positive connotation, if the value is less than zero, the word is considered to have a negative connotation, and if the value is zero, the word is considered neutral and does not contribute to emotional content.
The application should contain two tabs. The “Lexicon” tab allows you to view lexicons, enter new words and their sentiment ratings, delete and edit existing ones. Words with a positive connotation should be coloured green, and words with a negative connotation should be coloured red. The filter can narrow the set to only positive or only negative words. The “Calculation” tab allows you to enter a document and calculate its overall sentiment score. The document is entered in two ways: by entering text in the appropriate field or by uploading. The document is a .txt file.
##### Technical requirements
- Use ASP.NET on the back-end (any version)
- Use Angular on the front end
- Any database is possible
- The main focus in task design is on architecture. Use good practices that can be applied in a specific case.
- Write a brief explanation of setting up an application launch environment
##### Bonus
- Use Authentication and authorization on SPA and on the middleware (any Oauth provider can be used, or custom logic) 
- Setup API documentation and versioning 
- Run applications over docker
- Take care of the security aspect of the application (emphasis is on Cross Site Scripting)
##### Attachment
Initial list of words and their sentiment rating: 
1. nice: 0,4
2. excellent: 0,8
3. modest: 0 
4. horrible: -0,8
5. ugly: -0.5
