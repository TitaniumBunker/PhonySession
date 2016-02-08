![Alt text](https://raw.githubusercontent.com/TitaniumBunker/PhonySession/master/disguise64x64.png "PhonySession") 
#PhonySession
You know what it's like : you really want to test your controller, and while most of the functionality works there's a niggle.  Your controller checks to see if a file has been posted to the controller - because sometimes you want to allow someone to optionally upload a file.

Purists may argue that what we have here is a controller with a dependancy on the request object - and that's true.  What the PhonySession project (and I use that terms VERY loosely) does is create a fancy wrapper allowing you to create a request - which allows you to push a file and plumb it into your controller.

It's not finished yet - the reason it's on GitHub is because I am writing about my experiences of testing MVC controllers on my blog, and I also wanted to try and get Nuget build working.

If you're using a mocking framework then maybe you'll stick with that - but this does have the advantage that realistic file streams can be issues to the controller - so that adds a realism to the tests.
