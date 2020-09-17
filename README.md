# Hacker News Project by Thiago Vivas

Hello, What I am delivering here is a project that looks very simple but has some complex parts on it. 
I use the standard 3 layered architecture with dependency injection, swagger, asynchronous and parallel programming.

## How to run?

1. Clone the project
2. Build it
3. Push F5. 

You will be prompted with the swagger API where you can test the request.
The Web Api returns 20 best stories by default but you could return any number as you wish. You only need to send the desired number as query string `count`

## Why I did like this?

I love simple and clean code, even though we are using more complex solutions it does not mean that the code must be complex.
To solve the issue to do not overload the Hacker News API I used cache for 60 seconds.
To communicate between layers I applied dependecy injection.
To have a faster result I used asynchronous and parallel programming, so it could get the details of each story faster.

### The `countCommentsInsideComments` flag

This flag was created in order to search for comments inside comments or not. I decided to make it false by default because it was taking too much time to retrieve the data, if you want to test it use a fewer number of stories to retrieve.

## Enhancements

  - Logging, the project has no logging.
  - Unit Tests in the business logic layer.
  - AutoMapper, for no need to manually map one object to another like in the StoryManager.GetStoryDetail
  - Comments, there are comments only in the interfaces and I like to do small comments through the whole project.
  - Pagination, for greater responses would be good to paginate but as we return a small data set I decide not to implement it.
