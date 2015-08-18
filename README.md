# tq.NET
Twitch commandline interface in .NET inspired by [tq](https://github.com/stnuessl/tq)
tq is a simple and easy to use command-line tool to query information about streams and channels on the twitch.tv website.

##Why tq?
Inspired by the implementation of [stnuessl](https://github.com/stnuessl) i created an implementation of tq in .NET to fulfill my Windows needs.
At the moment it is at a very early stage and there is a lot of work to do.

##Usage
At the moment there are just 2 commandline switches that are working

###Featured Streams
To query the twitch.tv featured Streams run tq with the /F switch.

```
tq /F
```

###Top Games
To query the Top Games orderd by viewers on twitch.tv run tq with the /T switch. 
```
tq /T
```
