OctoCentral (Windows Phone Application 8.1 for GitHub)
========

A Windows Phone 8.1 App to navigate your GitHub account. The source code is released under the MIT License. 
It uses [Octokit](https://github.com/octokit/octokit.net/) to talk to GitHub.

[Release History](https://github.com/christophwille/wpaghapp/wiki/Release-History)

[in Windows Phone Store](http://www.windowsphone.com/en-us/store/app/octocentral/5450b4ca-875b-4d47-8d2a-75004a873f87)

## Reporting Bugs, Requesting Features

Send an email to [me](mailto:christoph.wille@gmail.com). Attach a screenshot ([Start + Power button](http://www.windowsphone.com/en-us/how-to/wp8/photos/take-a-screenshot))
if you think that will help me track down a bug quicker, or if that includes information you do not want to type out. 

## Features

* `Authorization` - uses OAuth (the main driver for me to implement my own WPA for GitHub)
* `User` - show own repositories, followers and following (allow drill-down into all three)
* `Repository` - show statistics, commits and issues

Planned, but currently not implementable:

* Repository/explore source code: Octokit.net doesn't yet support the get-contents APIs
* User/private news: not yet possible with OAuth according to service documentation

## Screenshots

Please see the StoreSubmissionAssets folder, it contains screenshots in various sizes.
