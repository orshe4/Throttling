# Throttling
Throttling is a simple library that helps to limit tasks by a period of time.

## Usage

```C#
	var taskLimit = 30;	
	var limitingPeriodInMilliseconds = 1000;
    Throttled throttled = new Throttled(taskLimit, limitingPeriodInMilliseconds);
	await throttled.Run(() => {
		//Some task that should be limited like http request
		return Task.CompletedTask;		
	});
```