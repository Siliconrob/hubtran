## Introduction

This is the initial coding challenge for the HubTran interview process. You may complete this challenge in any language you like. The code you submit doesn't need to reflect the code you would write in production. We're more interested in you getting the job done. Once you have completed the challenge, please reply to the email that included the challenge url (not the email with the code attached) with an archive of your code. You have one hour to complete this challenge.

## Your Task

In the data directory you will find 5 JSON files. Each file contains OCR data for one document sent to HubTran. The format is:

```
[{"left":0.8997641509433962,"top":0.03090909090909091,"width":0.03616352201257855,"height":0.009393939393939395,"chars":"1 of 1"}...]
```

Each entry in the array represents an area of text on the page. The left, top, width, and height values are the percentage of the page from the upper left. We use this data to generate a div that will allow customers to drag and drop text from documents into text field.

Unfortunately, in these examples, the invoice number field wasn't cleanly recognized. The field label and value were included in one box of text. When a customer tries to drag and drop the invoice number, they will get "INVOICE # 1234" instead of just "1234". Your task is to fix this by writing code to read the data and output modified JSON but with the invoice numbers split into two entries in the array.

Feel free to make whatever assumptions are necessary to complete this challenge. Please share and explain your assumptions when submitting your results.

Good luck!



### My approach

In reality this should be a bash script that uses `jq/gron/sed` but my bash knowledge is not tuned enough to finish this in the time limit so I went with a language I use every day C#.  I mean I would do it in python, but I would be spending more time looking up stuff and thought to avoid cutting it close.  This is built to run on .NET 5.0 as a console application

General idea is

- Read each file (I assume they are small enough to fit into memory and didn't need to be streamed, but maybe you have that need I know what to do then as well)
- Have a flow of `input file -> extracter -> output` file
- The extracter is passed a default mapper of the `DivInput -> DivOutput`
- Could have used a regular expression, but felt more comfortable finding the keyword, getting last element in the `chars` field and then using a stack to check each character element.  I am assuming each invoice is a numeric value, but I could extend the acceptable characters to include `-` and others etc no issue.  Also as I setup the parsers as func it can be swapped out with a regular expression that would match better
- If you can extract an invoice break the chars apart into a set `invoice` and the number otherwise put the original text back there
- I use extension methods and statics because that is more of a functional approach in my opinion and translates to nice languages like python, c# and java in my opinion have an obsession with interfaces over direct testable components and then get into the mocking hell and why mock when you can do direct calls in this format.

To compile on Windows use this command

`dotnet publish -c Release -o publish -p:PublishReadyToRun=true -p:PublishSingleFile=true -p:PublishTrimmed=true --self-contained true -p:IncludeNativeLibrariesForSelfExtract=true`

That will get you a nice single executable with static libraries included like golang.  I would need to look up the syntax identifier for macOS and Linux to get you appropriate versions.