# tq.NET
Twitch commandline interface in .NET inspired by [tq](https://github.com/stnuessl/tq)
tq is a simple and easy to use command-line tool to query information about streams and channels on the twitch.tv website.

* [tq.NET](https://github.com/freemindhv/tq.NET#tqnet)
    * [Why tq?](https://github.com/freemindhv/tq.NET#why-tq)
    * [Usage](https://github.com/freemindhv/tq.NET#usage)
        * [Featured Streams](https://github.com/freemindhv/tq.NET#featured-streams)
        * [Top Games](https://github.com/freemindhv/tq.NET#top-games)
        * [Search Streams](https://github.com/freemindhv/tq.NET#search-streams)
        * [Retrieve Channel information](https://github.com/freemindhv/tq.NET#retrieve-channel-information)
        * [Retrieve Stream information](https://github.com/freemindhv/tq.NET#retrieve-stream-information)

##Why tq?
Inspired by the implementation of [stnuessl](https://github.com/stnuessl) i created an implementation of tq in .NET to fulfill my Windows needs.
At the moment it is at a very early stage and there is a lot of work to do.

##Usage
```
  -?, --help, -h             Prints this help message
  -f, -F, --featured         Shows the featured Streams
  -t, -T, --top, --top-games Shows the Top Games sorted by viewers
  -s, --search=VALUE         Searches for a stream
  -C, --channel=VALUE        Retrieve information about a channel
  -S, --stream=VALUE         Retrieve information about a stream
 ```

###Featured Streams
To query the twitch.tv featured Streams run tq with the /f switch.

```
tq /f
tq -f
tq --featured
```

###Top Games
To query the Top Games orderd by viewers on twitch.tv run tq with the /t switch.
```
tq /t
tq -t
tq --top
```

###Search Streams
You can search for streams matching your search string with /s switch
As of now you have to surround strings with spaces with " e.g. tq /s "League of Legends"
```
tq /s VALUE
tq -s VALUE
tq --search VALUE
```

###Retrieve Channel information
You can retrieve infromations about a given Channel. You will not get any information about the online status of this channel's stream though.
```
tq /C VALUE
tq -C VALUE
tq --channel VALUE
```


###Retrieve Stream information
You can retrieve infromations about a given Stream, however you will not get any Informations if the Stream is offline..
```
tq /S VALUE
tq -S VALUE
tq --stream VALUE
```
