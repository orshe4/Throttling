# Throttling
Throttling is a simple library that helps to limit tasks by a period of time.

[![NuGet version (Throttling)](https://img.shields.io/nuget/v/Throttling.svg?style=flat-square)](https://www.nuget.org/packages/Throttling/)

## Give a Star! :star:
If you like or are using this project please give it a star. Thanks!

## Usage

```C#
	var taskLimit = 30;	
	var limitingPeriodInMilliseconds = 1000;
        Throttled throttled = new Throttled(taskLimit, limitingPeriodInMilliseconds);
	await throttled.Run(() => {
		//Some task that should be limited...
		return Task.CompletedTask;		
	});
```
